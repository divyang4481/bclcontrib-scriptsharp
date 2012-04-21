#define IGNORE_UNSATISFIABLE_VARIABLES
using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    internal class idLCP_Square : idLCP
    {
        idMatX m;					// original matrix
        idVecX b;					// right hand side
        idVecX lo, hi;				// low and high bounds
        idVecX f, a;				// force and acceleration
        idVecX delta_f, delta_a;	// delta force and delta acceleration
        idMatX clamped;			// LU factored sub matrix for clamped variables
        idVecX diagonal;			// reciprocal of diagonal of U of the LU factored sub matrix for clamped variables
        int numUnbounded;		// number of unbounded variables
        int numClamped;			// number of clamped variables
        float[][] rowPtrs;			// pointers to the rows of m
        int[] boxIndex;			// box index
        int[] side;				// tells if a variable is at the low boundary = -1, high boundary = 1 or inbetween = 0
        int[] permuted;			// index to keep track of the permutation
        bool padded;				// set to true if the rows of the initial matrix are 16 byte padded

        bool FactorClamped()
        {
            for (int i = 0; i < numClamped; i++)
                Array.Copy(clamped[i], rowPtrs[i], numClamped);
            float d;
            for (int i = 0; i < numClamped; i++)
            {
                float s = idMath.Fabs(clamped[i][i]);
                if (s == 0.0f)
                    return false;
                diagonal[i] = d = 1.0f / clamped[i][i];
                for (int j = i + 1; j < numClamped; j++)
                    clamped[j][i] *= d;
                for (int j = i + 1; j < numClamped; j++)
                {
                    d = clamped[j][i];
                    for (int k = i + 1; k < numClamped; k++)
                        clamped[j][k] -= d * clamped[i][k];
                }
            }
            return true;
        }

        void SolveClamped(idVecX x, float[] b)
        {
            // solve L
            for (int i = 0; i < numClamped; i++)
            {
                float sum = b[i];
                for (int j = 0; j < i; j++)
                    sum -= clamped[i][j] * x[j];
                x[i] = sum;
            }
            // solve U
            for (int i = numClamped - 1; i >= 0; i--)
            {
                float sum = x[i];
                for (int j = i + 1; j < numClamped; j++)
                    sum -= clamped[i][j] * x[j];
                x[i] = sum * diagonal[i];
            }
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

        void AddClamped(int r)
        {
            Debug.Assert(r >= numClamped);
            // add a row at the bottom and a column at the right of the factored matrix for the clamped variables
            Swap(numClamped, r);
            // add row to L
            for (int i = 0; i < numClamped; i++)
            {
                float sum = rowPtrs[numClamped][i];
                for (int j = 0; j < i; j++)
                    sum -= clamped[numClamped][j] * clamped[j][i];
                clamped[numClamped][i] = sum * diagonal[i];
            }
            // add column to U
            for (int i = 0; i <= numClamped; i++)
            {
                float sum = rowPtrs[i][numClamped];
                for (int j = 0; j < i; j++)
                    sum -= clamped[i][j] * clamped[j][numClamped];
                clamped[i][numClamped] = sum;
            }
            diagonal[numClamped] = 1.0f / clamped[numClamped][numClamped];
            numClamped++;
        }

        void RemoveClamped(int r)
        {
            Debug.Assert(r < numClamped);
            numClamped--;
            // no need to swap and update the factored matrix when the last row and column are removed
            if (r == numClamped)
                return;
            float[] y0 = new float[numClamped];
            float[] z0 = new float[numClamped];
            float[] y1 = new float[numClamped];
            float[] z1 = new float[numClamped];
            // the row/column need to be subtracted from the factorization
            for (int i = 0; i < numClamped; i++)
                y0[i] = -rowPtrs[i][r];
            Array.Clear(y1, 0, numClamped);
            y1[r] = 1.0f;
            Array.Clear(z0, 0, numClamped);
            z0[r] = 1.0f;
            for (int i = 0; i < numClamped; i++)
                z1[i] = -rowPtrs[r][i];
            // swap the to be removed row/column with the last row/column
            Swap(r, numClamped);
            // the swapped last row/column need to be added to the factorization
            for (int i = 0; i < numClamped; i++)
                y0[i] += rowPtrs[i][r];
            for (int i = 0; i < numClamped; i++)
                z1[i] += rowPtrs[r][i];
            z1[r] = 0.0f;
            // update the beginning of the to be updated row and column
            for (int i = 0; i < r; i++)
            {
                double p0 = y0[i];
                double beta1 = z1[i] * diagonal[i];
                clamped[i][r] += p0;
                for (int j = i + 1; j < numClamped; j++)
                    z1[j] -= beta1 * clamped[i][j];
                for (int j = i + 1; j < numClamped; j++)
                    y0[j] -= p0 * clamped[j][i];
                clamped[r][i] += beta1;
            }
            // update the lower right corner starting at r,r
            for (int i = r; i < numClamped; i++)
            {
                double diag = clamped[i][i];
                double p0 = y0[i];
                double p1 = z0[i];
                diag += p0 * p1;
                if (diag == 0.0f)
                {
                    idLib.common.Printf("idLCP_Square::RemoveClamped: updating factorization failed\n");
                    return;
                }
                double beta0 = p1 / diag;
                double q0 = y1[i];
                double q1 = z1[i];
                diag += q0 * q1;
                if (diag == 0.0f)
                {
                    idLib.common.Printf("idLCP_Square::RemoveClamped: updating factorization failed\n");
                    return;
                }
                double d = 1.0f / diag;
                double beta1 = q1 * d;
                clamped[i][i] = diag;
                diagonal[i] = (float)d;
                for (int j = i + 1; j < numClamped; j++)
                {
                    d = clamped[i][j];
                    d += p0 * z0[j];
                    z0[j] -= (float)(beta0 * d);
                    d += q0 * z1[j];
                    z1[j] -= (float)(beta1 * d);
                    clamped[i][j] = d;
                }
                for (int j = i + 1; j < numClamped; j++)
                {
                    d = clamped[j][i];
                    y0[j] -= (float)(p0 * d);
                    d += beta0 * y0[j];
                    y1[j] -= (float)(q0 * d);
                    d += beta1 * y1[j];
                    clamped[j][i] = d;
                }
            }
        }

        // modifies this->delta_f
        void CalcForceDelta(int d, float dir)
        {
            delta_f[d] = dir;
            if (numClamped == 0)
                return;
            // get column d of matrix
            float[] ptr = new float[numClamped];
            for (int i = 0; i < numClamped; i++)
                ptr[i] = rowPtrs[i][d];
            // solve force delta
            SolveClamped(delta_f, ptr);
            // flip force delta based on direction
            if (dir > 0.0f)
            {
                ptr = delta_f.ToArray();
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

        // modifies this->a and uses this->delta_a
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
                if (side[i] == -1)
                {
                    if (delta_a[i] >= -LCP_DELTA_ACCEL_EPSILON)
                        continue;
                }
                else if (side[i] == 1)
                {
                    if (delta_a[i] <= LCP_DELTA_ACCEL_EPSILON)
                        continue;
                }
                else
                    continue;
                // ignore variables for which the force is not allowed to take any substantial value
                if (lo[i] >= -LCP_BOUND_EPSILON && hi[i] <= LCP_BOUND_EPSILON)
                    continue;
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
            // true when the matrix rows are 16 byte padded
            padded = (((o_m.GetNumRows() + 3) & ~3) == o_m.GetNumColumns());
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
            for (int i = 0; i < m.GetNumRows(); i++)
                rowPtrs[i] = m[i];
            // tells if a variable is at the low boundary, high boundary or inbetween
            side = new int[m.GetNumRows()];
            // index to keep track of the permutation
            permuted = new int[m.GetNumRows()];
            for (int i = 0; i < m.GetNumRows(); i++)
                permuted[i] = i;
            // permute input so all unbounded variables come first
            numUnbounded = 0;
            for (int i = 0; i < m.GetNumRows(); i++)
                if (lo[i] == -float.NegativeInfinity && hi[i] == float.PositiveInfinity)
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
            // all unbounded variables are clamped
            numClamped = numUnbounded;
            // if there are unbounded variables
            if (numUnbounded != 0)
            {
                // factor and solve for unbounded variables
                if (!FactorClamped())
                {
                    idLib.common.Printf("idLCP_Square::Solve: unbounded factorization failed\n");
                    return false;
                }
                SolveClamped(f, b.ToArray());
                // if there are no bounded variables we are done
                if (numUnbounded == m.GetNumRows())
                {
                    o_x = f; // the vector is not permuted
                    return true;
                }
            }
#if IGNORE_UNSATISFIABLE_VARIABLES
            int numIgnored = 0;
#endif
            // allocate for delta force and delta acceleration
            delta_f.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            delta_a.SetData(m.GetNumRows(), VECX_ALLOCA(m.GetNumRows()));
            //    // solve for bounded variables
            string failed = null;
            float dot;
            for (int i = numUnbounded; i < m.GetNumRows(); i++)
            {
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
                SIMDProcessor.Dot(out dot, rowPtrs[i], f.ToArray(), i);
                a[i] = dot - b[i];
                // if already at the low boundary
                if (lo[i] >= -LCP_BOUND_EPSILON && a[i] >= -LCP_ACCEL_EPSILON) { side[i] = -1; continue; }
                // if already at the high boundary
                if (hi[i] <= LCP_BOUND_EPSILON && a[i] <= LCP_ACCEL_EPSILON) { side[i] = 1; continue; }
                // if inside the clamped region
                if (idMath.Fabs(a[i]) <= LCP_ACCEL_EPSILON) { side[i] = 0; AddClamped(i); continue; }
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
                            AddClamped(limit);
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
            if (numIgnored > 0)
                if (lcp_showFailures.GetBool())
                    idLib.common.Printf("idLCP_Symmetric::Solve: %d of %d bounded variables ignored\n", numIgnored, m.GetNumRows() - numUnbounded);
#endif
            // if failed clear remaining forces
            if (failed != null)
            {
                if (lcp_showFailures.GetBool())
                    idLib.common.Printf("idLCP_Square::Solve: %s (%d of %d bounded variables ignored)\n", failed, m.GetNumRows() - i, m.GetNumRows() - numUnbounded);
                for (int j = i; j < m.GetNumRows(); j++)
                    f[j] = 0.0f;
            }
#if _DEBUG && false
            if (failed == null)
                // test whether or not the solution satisfies the complementarity conditions
                for (int i = 0; i < m.GetNumRows(); i++)
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
            for (int i = 0; i < f.GetSize(); i++)
                o_x[permuted[i]] = f[i];
            // unpermute original matrix
            for (int i = 0; i < m.GetNumRows(); i++)
            {
                for (int j = 0; j < m.GetNumRows(); j++)
                    if (permuted[j] == i)
                        break;
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



