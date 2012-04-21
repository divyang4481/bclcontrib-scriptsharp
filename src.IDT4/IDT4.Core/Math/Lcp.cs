using System;
namespace IDT4.Math2
{
    /// <summary>
    ///  Box Constrained Mixed Linear Complementarity Problem solver
    ///  A is a matrix of dimension n*n and x, b, lo, hi are vectors of dimension n
    /// </summary>
    /// <remarks>
    /// Solve: Ax = b + t, where t is a vector of dimension n, with complementarity condition: (x[i] - lo[i]) * (x[i] - hi[i]) * t[i] = 0
    /// such that for each 0 &lt;= i &lt; n one of the following holds:
    /// 
    /// 1. lo[i] &lt; x[i] &lt; hi[i], t[i] == 0
    /// 2. x[i] == lo[i], t[i] &gt;= 0
    /// 3. x[i] == hi[i], t[i] &lt;= 0
    ///
    /// Partly bounded or unbounded variables can have lo[i] and/or hi[i] set to negative/positive idMath::INFITITY respectively.
    ///
    /// If boxIndex != NULL and boxIndex[i] != -1 then
    ///     lo[i] = - fabs( lo[i] * x[boxIndex[i]] )
    ///     hi[i] = fabs( hi[i] * x[boxIndex[i]] )
    ///     boxIndex[boxIndex[i]] must be -1
    ///
    /// Before calculating any of the bounded x[i] with boxIndex[i] != -1 the solver calculates all unbounded x[i] and all x[i] with boxIndex[i] == -1.
    /// </remarks>
    public abstract partial class idLCP : IDisposable
    {
        public void Dispose() { }
        public static idLCP AllocSquare()
        {
            idLCP_Square lcp = new idLCP_Square();
            lcp.SetMaxIterations(32);
            return lcp;
        }
        public static idLCP AllocSymmetric()
        {
            idLCP_Symmetric lcp = new idLCP_Symmetric();
            lcp.SetMaxIterations(32);
            return lcp;
        }
        public abstract bool Solve(ref idMatX A, ref idVecX x, ref idVecX b, ref idVecX lo, ref idVecX hi, int[] boxIndex = null);
        public void SetMaxIterations(int max) { maxIterations = max; }
        public int GetMaxIterations() { return maxIterations; }
        protected int maxIterations;
        //
        //internal static idCVar lcp_showFailures( "lcp_showFailures", "0", CVAR_SYSTEM | CVAR_BOOL, "show LCP solver failures" );

        internal const float LCP_BOUND_EPSILON = 1e-5f;
        internal const float LCP_ACCEL_EPSILON = 1e-5f;
        internal const float LCP_DELTA_ACCEL_EPSILON = 1e-9f;
        internal const float LCP_DELTA_FORCE_EPSILON = 1e-9f;
    }
}
