using System;
namespace IDT4.Math2
{
    public static class SIMDProcessor
    {
        internal static void MatX_LowerTriangularSolve(idMatX clamped, float[] p, float[] b, int numClamped, int clampedChangeStart)
        {
            throw new NotImplementedException();
        }

        internal static void Mul(float[] p, float[] p_2, float[] p_3, int numClamped)
        {
            throw new NotImplementedException();
        }

        internal static void MatX_LowerTriangularSolveTranspose(idMatX clamped, float[] p, float[] p_2, int numClamped)
        {
            throw new NotImplementedException();
        }

        internal static bool MatX_LDLTFactor(idMatX clamped, idVecX diagonal, int numClamped)
        {
            throw new NotImplementedException();
        }

        internal static void Dot(out float dot, float[] p, float[] p_2, int numClamped)
        {
            throw new NotImplementedException();
        }

        internal static void MulAdd(float[] p, float step, float[] p_2, int numClamped)
        {
            throw new NotImplementedException();
        }
    }
}
