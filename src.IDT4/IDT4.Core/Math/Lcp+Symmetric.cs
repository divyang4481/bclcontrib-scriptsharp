#define IGNORE_UNSATISFIABLE_VARIABLES
using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    internal class idLCP_Symmetric : idLCP
    {
        idMatX m;					// original matrix
        idVecX b;					// right hand side
        idVecX lo, hi;				// low and high bounds
        idVecX f, a;				// force and acceleration
        idVecX delta_f, delta_a;	// delta force and delta acceleration
        idMatX clamped;			// LDLt factored sub matrix for clamped variables
        idVecX diagonal;			// reciprocal of diagonal of LDLt factored sub matrix for clamped variables
        idVecX solveCache1;		// intermediate result cached in SolveClamped
        idVecX solveCache2;		// "
        int numUnbounded;		// number of unbounded variables
        int numClamped;			// number of clamped variables
        int clampedChangeStart;	// lowest row/column changed in the clamped matrix during an iteration
        float[][] rowPtrs;			// pointers to the rows of m
        int[] boxIndex;			// box index
        int[] side;				// tells if a variable is at the low boundary = -1, high boundary = 1 or inbetween = 0
        int[] permuted;			// index to keep track of the permutation
        bool padded;				// set to true if the rows of the initial matrix are 16 byte padded

        bool FactorClamped()
        {
            clampedChangeStart = 0;
            for (int i = 0; i < numClamped; i++)
                Array.Copy(clamped[i], rowPtrs[i], numClamped);
            return SIMDProcessor.MatX_LDLTFactor(clamped, diagonal, numClamped);
        }

        void SolveClamped(ref idVecX x, float[] b)
        {
            // solve L
            SIMDProcessor.MatX_LowerTriangularSolve(clamped, solveCache1.ToArray(), b, numClamped, clampedChangeStart);
            // solve D
            SIMDProcessor.Mul(solveCache2.ToArray(), solveCache1.ToArray(), diagonal.ToArray(), numClamped);
            // solve Lt
            SIMDProcessor.MatX_LowerTriangularSolveTranspose(clamped, x.ToArray(), solveCache2.ToArray(), numClamped);
            clampedChangeStart = numClamped;
        }

        void Swap(int i, int j)
        {
            if (i == j)
                return;
            idSwap(rowPtrs[i], rowPtrs[j]);
            m.SwapColumns(i, j);
            b.SwapElements(i, j);
            lo.SwapElements(i, j);
            hi.SwapElements(i, j);
            a.SwapElements(i, j);
            f.SwapElements(i, j);
            if (boxIndex != null)
                idSwap(boxIndex[i], boxIndex[j]);
            idSwap(side[i], side[j]);
            idSwap(permuted[i], permuted[j]);
        }

        void AddClamped(int r, bool useSolveCache)
        {
            Debug.Assert(r >= numClamped);
            if (numClamped < clampedChangeStart)
                clampedChangeStart = numClamped;
            // add a row at the bottom and a column at the right of the factored matrix for the clamped variables
            Swap(numClamped, r);
            // solve for v in L * v = rowPtr[numClamped]
            float dot = 0;
            if (useSolveCache)
            {
                // the lower triangular solve was cached in SolveClamped called by CalcForceDelta
                Array.Copy(clamped[numClamped], solveCache2.ToArray(), numClamped);
                // calculate row dot product
                SIMDProcessor.Dot(out dot, solveCache2.ToArray(), solveCache1.ToArray(), numClamped);
            }
            else
            {
                float[] v = new float[numClamped];
                SIMDProcessor.MatX_LowerTriangularSolve(clamped, v, rowPtrs[numClamped], numClamped);
                // add bottom row to L
                SIMDProcessor.Mul(clamped[numClamped], v, diagonal.ToArray(), numClamped);
                // calculate row dot product
                SIMDProcessor.Dot(out dot, clamped[numClamped], v, numClamped);
            }
            // update diagonal[numClamped]
            float d = rowPtrs[numClamped][numClamped] - dot;
            if (d == 0.0f) { idLib.common.Printf("AddClamped: updating factorization failed\n"); numClamped++; return; }
            clamped[numClamped][numClamped] = d;
            diagonal[numClamped] = 1.0f / d;
            numClamped++;
        }

        void RemoveClamped(int r)
        {
            double diag;
            Debug.Assert(r < numClamped);
            if (r < clampedChangeStart)
                clampedChangeStart = r;
            numClamped--;
            // no need to swap and update the factored matrix when the last row and column are removed
            if (r == numClamped) return;
            // swap the to be removed row/column with the last row/column
            Swap(r, numClamped);
            // update the factored matrix
            float[] addSub = new float[numClamped];
            if (r == 0)
            {
                if (numClamped == 1)
                {
                    diag = rowPtrs[0][0];
                    if (diag == 0.0f) { idLib.common.Printf("RemoveClamped: updating factorization failed\n"); return; }
                    clamped[0][0] = diag;
                    diagonal[0] = (float)(1.0f / diag);
                    return;
                }
                // calculate the row/column to be added to the lower right sub matrix starting at (r, r)
                float[] original = rowPtrs[numClamped];
                float[] ptr = rowPtrs[r];
                addSub[0] = ptr[0] - original[numClamped];
                for (int i = 1; i < numClamped; i++)
                    addSub[i] = ptr[i] - original[i];
            }
            else
            {
                float[] v = new float[numClamped];
                // solve for v in L * v = rowPtr[r]
                SIMDProcessor.MatX_LowerTriangularSolve(clamped, v, rowPtrs[r], r);
                // update removed row
                SIMDProcessor.Mul(clamped[r], v, diagonal.ToArray(), r);
                // if the last row/column of the matrix is updated
                if (r == numClamped - 1)
                {
                    // only calculate new diagonal
                    float dot;
                    SIMDProcessor.Dot(out dot, clamped[r], v, r);
                    diag = rowPtrs[r][r] - dot;
                    if (diag == 0.0f) { idLib.common.Printf("RemoveClamped: updating factorization failed\n"); return; }
                    clamped[r][r] = diag;
                    diagonal[r] = (float)(1.0f / diag);
                    return;
                }
                // calculate the row/column to be added to the lower right sub matrix starting at (r, r)
                for (int i = 0; i < r; i++)
                    v[i] = clamped[r][i] * clamped[i][i];
                for (int i = r; i < numClamped; i++)
                {
                    double sum = (i == r ? clamped[r][r] : clamped[r][r] * clamped[i][r]);
                    float[] ptr = clamped[i];
                    for (int j = 0; j < r; j++)
                        sum += ptr[j] * v[j];
                    addSub[i] = (float)(rowPtrs[r][i] - sum);
                }
            }
            // add row/column to the lower right sub matrix starting at (r, r)
            float[] v1 = new float[numClamped];
            float[] v2 = new float[numClamped];
            diag = idMath.SQRT_1OVER2;
            v1[r] = (float)((0.5f * addSub[r] + 1.0f) * diag);
            v2[r] = (float)((0.5f * addSub[r] - 1.0f) * diag);
            for (int i = r + 1; i < numClamped; i++)
                v1[i] = v2[i] = (float)(addSub[i] * diag);
            double alpha1 = 1.0f;
            double alpha2 = -1.0f;
            // simultaneous update/downdate of the sub matrix starting at (r, r)
            int n = clamped.GetNumColumns();
            for (int i = r; i < numClamped; i++)
            {
                diag = clamped[i][i];
                double p1 = v1[i];
                double newDiag = diag + alpha1 * p1 * p1;
                if (newDiag == 0.0f) { idLib.Common.Printf("RemoveClamped: updating factorization failed\n"); return; }
                alpha1 /= newDiag;
                double beta1 = p1 * alpha1;
                alpha1 *= diag;
                diag = newDiag;
                double p2 = v2[i];
                newDiag = diag + alpha2 * p2 * p2;
                if (newDiag == 0.0f) { idLib.common.Printf("RemoveClamped: updating factorization failed\n"); return; }
                clamped[i][i] = newDiag;
                double invNewDiag;
                diagonal[i] = (float)(invNewDiag = 1.0f / newDiag);
                alpha2 *= invNewDiag;
                double beta2 = p2 * alpha2;
                alpha2 *= diag;
                // update column below diagonal (i,i)
                float[] ptr = clamped.ToArray() + i;
                for (int j = i + 1; j < numClamped - 1; j += 2)
                {
                    float sum0 = ptr[(j + 0) * n];
                    float sum1 = ptr[(j + 1) * n];
                    v1[j + 0] -= (float)(p1 * sum0);
                    v1[j + 1] -= (float)(p1 * sum1);
                    sum0 += (float)(beta1 * v1[j + 0]);
                    sum1 += (float)(beta1 * v1[j + 1]);
                    v2[j + 0] -= (float)(p2 * sum0);
                    v2[j + 1] -= (float)(p2 * sum1);
                    sum0 += (float)(beta2 * v2[j + 0]);
                    sum1 += (float)(beta2 * v2[j + 1]);
                    ptr[(j + 0) * n] = sum0;
                    ptr[(j + 1) * n] = sum1;
                }
                for (int j; j < numClamped; j++)
                {
                    double sum = ptr[j * n];
                    v1[j] -= (float)(p1 * sum);
                    sum += beta1 * v1[j];
                    v2[j] -= (float)(p2 * sum);
                    sum += beta2 * v2[j];
                    ptr[j * n] = (float)sum;
                }
            }
        }

        // modifies this->delta_f
        void CalcForceDelta(int d, float dir)
        {
            delta_f[d] = dir;
            if (numClamped == 0)
                return;
            // solve force delta
            SolveClamped(ref delta_f, rowPtrs[d]);
            // flip force delta based on direction
            if (dir > 0.0f)
            {
                float[] ptr = delta_f.ToArray();
                for (int i = 0; i < numClamped; i++)
                    ptr[i] = -ptr[i];
            }
        }

        // modifies this->delta_a and uses this->delta_f
        void CalcAccelDelta(int d)
        {
            // only the not clamped variables, including the current variable, can have a change in acceleration
            for (int j = numClamped; j <= d; j++)
            {
                // only the clamped variables and the current variable have a force delta unequal zero
                float dot;
                SIMDProcessor.Dot(out dot, rowPtrs[j], delta_f.ToArray(), numClamped);
                delta_a[j] = dot + rowPtrs[j][d] * delta_f[d];
            }
        }

        // modifies this->f and uses this->delta_f
        void ChangeForce(int d, float step)
        {
            // only the clamped variables and current variable have a force delta unequal zero
            SIMDProcessor.MulAdd(f.ToArray(), step, delta_f.ToArray(), numClamped);
            f[d] += step * delta_f[d];
        }

        //  modifies this->a and uses this->delta_a
        void ChangeAccel(int d, float step)
        {
            // only the not clamped variables, including the current variable, can have an acceleration unequal zero
            SIMDProcessor.MulAdd(a.ToArray() + numClamped, step, delta_a.ToArray() + numClamped, d - numClamped + 1);
        }

        void GetMaxStep(int d, float dir, out float maxStep, out int limit, out int limitSide)
        {
            // default to a full step for the current variable
            maxStep = (idMath.Fabs(delta_a[d]) > LCP_DELTA_ACCEL_EPSILON ? -a[d] / delta_a[d] : 0.0f);
            limit = d;
            limitSide = 0;
            // test the current variable
            if (dir < 0.0f)
            {
                if (lo[d] != float.NegativeInfinity)
                {
                    float s = (lo[d] - f[d]) / dir;
                    if (s < maxStep)
                    {
                        maxStep = s;
                        limitSide = -1;
                    }
                }
            }
            else
            {
                if (hi[d] != float.PositiveInfinity)
                {
                    float s = (hi[d] - f[d]) / dir;
                    if (s < maxStep)
                    {
                        maxStep = s;
                        limitSide = 1;
                    }
                }
            }

            // test the clamped bounded variables
            for (int i = numUnbounded; i < numClamped; i++)
                if (delta_f[i] < -LCP_DELTA_FORCE_EPSILON)
                {
                    // if there is a low boundary
                    if (lo[i] != float.NegativeInfinity)
                    {
                        float s = (lo[i] - f[i]) / delta_f[i];
                        if (s < maxStep)
                        {
                            maxStep = s;
                            limit = i;
                            limitSide = -1;
                        }
                    }
                }
                else if (delta_f[i] > LCP_DELTA_FORCE_EPSILON)
                {
                    // if there is a high boundary
                    if (hi[i] != float.PositiveInfinity)
                    {
                        float s = (hi[i] - f[i]) / delta_f[i];
                        if (s < maxStep)
                        {
                            maxStep = s;
                            limit = i;
                            limitSide = 1;
                        }
                    }
                }
            // test the not clamped bounded variables
            for (int i = numClamped; i < d; i++)
            {
                if (side[i] == -1) { if (delta_a[i] >= -LCP_DELTA_ACCEL_EPSILON) continue; }
                else if (side[i] == 1) { if (delta_a[i] <= LCP_DELTA_ACCEL_EPSILON) continue; }
                else continue;
                // ignore variables for which the force is not allowed to take any substantial value
                if (lo[i] >= -LCP_BOUND_EPSILON && hi[i] <= LCP_BOUND_EPSILON) continue;
                float s = -a[i] / delta_a[i];
                if (s < maxStep)
                {
                    maxStep = s;
                    limit = i;
                    limitSide = 0;
                }
            }
        }

        public override bool Solve(ref idMatX o_m, ref idVecX o_x, ref idVecX o_b, ref idVecX o_lo, ref idVecX o_hi, int[] o_boxIndex)
        {
            int i;
            // true when the matrix rows are 16 byte padded
            padded = ((o_m.GetNumRows() + 3) & ~3) == o_m.GetNumColumns();
            Debug.Assert(padded || o_m.GetNumRows() == o_m.GetNumColumns());
            Debug.Assert(o_x.GetSize() == o_m.GetNumRows());
            Debug.Assert(o_b.GetSize() == o_m.GetNumRows());
            Debug.Assert(o_lo.GetSize() == o_m.GetNumRows());
            Debug.Assert(o_hi.GetSize() == o_m.GetNumRows());
            // allocate memory for permuted input
            f.SetData(o_m.GetNumRows(), VECX_ALLOCA(o_m.GetNumRows()));
            a.SetData(o_b.GetSize(), VECX_ALLOCA(o_b.GetSize()));
            b.SetData(o_b.GetSize(), VECX_ALLOCA(o_b.GetSize()));
            lo.SetData(o_lo.GetSize(), VECX_ALLOCA(o_lo.GetSize()));
            hi.SetData(o_hi.GetSize(), VECX_ALLOCA(o_hi.GetSize()));
            if (o_boxIndex != null)
            {
                boxIndex = new int[o_x.GetSize()];
                Array.Copy(boxIndex, o_boxIndex, o_x.GetSize());
            }
            else
                boxIndex = null;
            // we override the const on o_m here but on exit the matrix is unchanged
            m.SetData(o_m.GetNumRows(), o_m.GetNumColumns(), (float[])o_m[0]);
            f.Zero();
            a.Zero();
            b = o_b;
            lo = o_lo;
            hi = o_hi;
            // pointers to the rows of m
            rowPtrs = new float[m.GetNumRows()][];
            for (i = 0; i < m.GetNumRows(); i++)
                rowPtrs[i] = m[i];
            // tells if a variable is at the low boundary, high boundary or inbetween
            side = new int[m.GetNumRows()];
            // index to keep track of the permutation
            permuted = new int[m.GetNumRows()];
            for (i = 0; i < m.GetNumRows(); i++)
                permuted[i] = i;
            // permute input so all unbounded variables come first
            numUnbounded = 0;
            for (i = 0; i < m.GetNumRows(); i++)
                if (lo[i] == float.NegativeInfinity && hi[i] == float.PositiveInfinity)
                {
                    if (numUnbounded != i)
                        Swap(numUnbounded, i);
                    numUnbounded++;
                }
            // permute input so all variables using the boxIndex come last
            int boxStartIndex = m.GetNumRows();
            if (boxIndex != null)
                for (int i = m.GetNumRows() - 1; i >= numUnbounded; i--)
                    if (boxIndex[i] >= 0 && (lo[i] != float.NegativeInfinity || hi[i] != float.PositiveInfinity))
                    {
                        boxStartIndex--;
                        if (boxStartIndex != i)
                            Swap(boxStartIndex, i);
                    }
            // sub matrix for factorization 
            clamped.SetData(m.GetNumRows(), m.GetNumColumns(), MATX_ALLOCA(m.GetNumRows() * m.GetNumColumns()));
            diagonal.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            solveCache1.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            solveCache2.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            // all unbounded variables are clamped
            numClamped = numUnbounded;
            // if there are unbounded variables
            if (numUnbounded != 0)
            {
                // factor and solve for unbounded variables
                if (!FactorClamped()) { idLib.common.Printf("Solve: unbounded factorization failed\n"); return false; }
                SolveClamped(f, b.ToArray());
                // if there are no bounded variables we are done
                if (numUnbounded == m.GetNumRows()) { o_x = f; return true; } // the vector is not permuted
            }
#if IGNORE_UNSATISFIABLE_VARIABLES
            int numIgnored = 0;
#endif
            // allocate for delta force and delta acceleration
            delta_f.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            delta_a.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            // solve for bounded variables
            string failed = null;
            for (i = numUnbounded; i < m.GetNumRows(); i++)
            {
                clampedChangeStart = 0;
                // once we hit the box start index we can initialize the low and high boundaries of the variables using the box index
                if (i == boxStartIndex)
                {
                    for (int j = 0; j < boxStartIndex; j++)
                        o_x[permuted[j]] = f[j];
                    for (int j = boxStartIndex; j < m.GetNumRows(); j++)
                    {
                        float s = o_x[boxIndex[j]];
                        if (lo[j] != float.NegativeInfinity)
                            lo[j] = -idMath.Fabs(lo[j] * s);
                        if (hi[j] != float.PositiveInfinity)
                            hi[j] = idMath.Fabs(hi[j] * s);
                    }
                }
                // calculate acceleration for current variable
                float dot;
                SIMDProcessor.Dot(out dot, rowPtrs[i], f.ToArray(), i);
                a[i] = dot - b[i];
                // if already at the low boundary
                if (lo[i] >= -LCP_BOUND_EPSILON && a[i] >= -LCP_ACCEL_EPSILON) { side[i] = -1; continue; }
                // if already at the high boundary
                if (hi[i] <= LCP_BOUND_EPSILON && a[i] <= LCP_ACCEL_EPSILON) { side[i] = 1; continue; }
                // if inside the clamped region
                if (idMath.Fabs(a[i]) <= LCP_ACCEL_EPSILON) { side[i] = 0; AddClamped(i, false); continue; }
                // drive the current variable into a valid region
                int n;
                for (n = 0; n < maxIterations; n++)
                {
                    // direction to move
                    float dir = (a[i] <= 0.0f ? 1.0f : -1.0f);
                    // calculate force delta
                    CalcForceDelta(i, dir);
                    // calculate acceleration delta: delta_a = m * delta_f;
                    CalcAccelDelta(i);
                    // maximum step we can take
                    float maxStep;
                    int limit;
                    int limitSide;
                    GetMaxStep(i, dir, out maxStep, out limit, out limitSide);
                    if (maxStep <= 0.0f)
                    {
#if IGNORE_UNSATISFIABLE_VARIABLES
                        // ignore the current variable completely
                        lo[i] = hi[i] = 0.0f;
                        f[i] = 0.0f;
                        side[i] = -1;
                        numIgnored++;
#else
                        failed = string.Format("invalid step size %.4f", maxStep);
#endif
                        break;
                    }
                    // change force
                    ChangeForce(i, maxStep);
                    // change acceleration
                    ChangeAccel(i, maxStep);
                    // clamp/unclamp the variable that limited this step
                    side[limit] = limitSide;
                    switch (limitSide)
                    {
                        case 0:
                            a[limit] = 0.0f;
                            AddClamped(limit, (limit == i));
                            break;
                        case -1:
                            f[limit] = lo[limit];
                            if (limit != i)
                                RemoveClamped(limit);
                            break;
                        case 1:
                            f[limit] = hi[limit];
                            if (limit != i)
                                RemoveClamped(limit);
                            break;
                    }
                    // if the current variable limited the step we can continue with the next variable
                    if (limit == i)
                        break;
                }
                if (n >= maxIterations)
                {
                    failed = string.Format("max iterations %d", maxIterations);
                    break;
                }
                if (failed != null)
                    break;
            }
#if IGNORE_UNSATISFIABLE_VARIABLES
            if (numIgnored != null)
                if (lcp_showFailures.GetBool())
                    idLib.common.Printf("Solve: %d of %d bounded variables ignored\n", numIgnored, m.GetNumRows() - numUnbounded);
#endif
            // if failed clear remaining forces
            if (failed != null)
            {
                if (lcp_showFailures.GetBool())
                    idLib.common.Printf("Solve: %s (%d of %d bounded variables ignored)\n", failed, m.GetNumRows() - i, m.GetNumRows() - numUnbounded);
                for (int j = i; j < m.GetNumRows(); j++)
                    f[j] = 0.0f;
            }
#if _DEBUG && false
            if (failed == null)
                // test whether or not the solution satisfies the complementarity conditions
                for (i = 0; i < m.GetNumRows(); i++)
                {
                    a[i] = -b[i];
                    for (int j = 0; j < m.GetNumRows(); j++)
                        a[i] += rowPtrs[i][j] * f[j];
                    if (f[i] == lo[i])
                    {
                        if (lo[i] != hi[i] && a[i] < -LCP_ACCEL_EPSILON)
                            int bah1 = 1;
                    }
                    else if (f[i] == hi[i])
                    {
                        if (lo[i] != hi[i] && a[i] > LCP_ACCEL_EPSILON)
                            int bah2 = 1;
                    }
                    else if (f[i] < lo[i] || f[i] > hi[i] || idMath.Fabs(a[i]) > 1.0f)
                    {
                        int bah3 = 1;
                    }
                }
#endif
            // unpermute result
            for (i = 0; i < f.GetSize(); i++)
                o_x[permuted[i]] = f[i];
            // unpermute original matrix
            for (i = 0; i < m.GetNumRows(); i++)
            {
                for (int j = 0; j < m.GetNumRows(); j++)
                {
                    if (permuted[j] == i)
                        break;
                }
                if (i != j)
                {
                    m.SwapColumns(i, j);
                    idSwap(permuted[i], permuted[j]);
                }
            }
            return true;
        }
    }
}



