using System;
namespace IDT4.Math2
{
    public struct idVec6
    {
        public readonly static idVec6 origin = new idVec6(0f, 0f, 0f, 0f, 0f, 0f);
        public readonly static idVec6 infinity = new idVec6(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        public float[] p = new float[6];

        public idVec6() { p[0] = p[1] = p[2] = p[3] = p[4] = p[5] = 0f; }
        public idVec6(float[] a) { Array.Copy(a, p, 6); }
        public idVec6(float a1, float a2, float a3, float a4, float a5, float a6) { p[0] = a1; p[1] = a2; p[2] = a3; p[3] = a4; p[4] = a5; p[5] = a6; }
        public void Set(float a1, float a2, float a3, float a4, float a5, float a6) { p[0] = a1; p[1] = a2; p[2] = a3; p[3] = a4; p[4] = a5; p[5] = a6; }
        public void Zero() { p[0] = p[1] = p[2] = p[3] = p[4] = p[5] = 0f; }
        public int GetDimension() { return 6; }
        public float[] ToArray() { return p; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public float this[int index] { get { return p[index]; } }
        public static idVec6 operator -(idVec6 a) { return new idVec6(-a.p[0], -a.p[1], -a.p[2], -a.p[3], -a.p[4], -a.p[5]); }
        public static idVec6 operator -(idVec6 a, idVec6 b) { return new idVec6(a.p[0] - b.p[0], a.p[1] - b.p[1], a.p[2] - b.p[2], a.p[3] - b.p[3], a.p[4] - b.p[4], a.p[5] - b.p[5]); }
        public static float operator *(idVec6 a, idVec6 b) { return a.p[0] * b.p[0] + a.p[1] * b.p[1] + a.p[2] * b.p[2] + a.p[3] * b.p[3] + a.p[4] * b.p[4] + a.p[5] * b.p[5]; }
        public static idVec6 operator *(idVec6 a, float b) { return new idVec6(a.p[0] * b, a.p[1] * b, a.p[2] * b, a.p[3] * b, a.p[4] * b, a.p[5] * b); }
        public static idVec6 operator /(idVec6 a, float b) { float invb = 1.0f / b; return new idVec6(a.p[0] * invb, a.p[1] * invb, a.p[2] * invb, a.p[3] * invb, a.p[4] * invb, a.p[5] * invb); }
        public static idVec6 operator +(idVec6 a, idVec6 b) { return new idVec6(a.p[0] + b.p[0], a.p[1] + b.p[1], a.p[2] + b.p[2], a.p[3] + b.p[3], a.p[4] + b.p[4], a.p[5] + b.p[5]); }
        public idVec6 opAdd(idVec6 a) { p[0] += a.p[0]; p[1] += a.p[1]; p[2] += a.p[2]; p[3] += a.p[3]; p[4] += a.p[4]; p[5] += a.p[4]; return this; }
        public idVec6 opSub(idVec6 a) { p[0] -= a.p[0]; p[1] -= a.p[1]; p[2] -= a.p[2]; p[3] -= a.p[3]; p[4] -= a.p[4]; p[5] -= a.p[5]; return this; }
        public idVec6 opDiv(idVec6 a) { p[0] /= a.p[0]; p[1] /= a.p[1]; p[2] /= a.p[2]; p[3] /= a.p[3]; p[4] /= a.p[4]; p[5] /= a.p[5]; return this; }
        public idVec6 opMul(float a) { p[0] *= a; p[1] *= a; p[2] *= a; p[3] *= a; p[4] *= a; p[5] *= a; return this; }
        public idVec6 opDiv(float a) { float inva = 1.0f / a; p[0] *= inva; p[1] *= inva; p[2] *= inva; p[3] *= inva; p[4] *= inva; p[5] *= inva; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idVec6 a) { return (p[0] == a.p[0] && p[1] == a.p[1] && p[2] == a.p[2] && p[3] == a.p[3] && p[4] == a.p[4] && p[5] == a.p[5]); }
        public bool Compare(ref idVec6 a, float epsilon) { return (idMath.Fabs(p[0] - a.p[0]) <= epsilon && idMath.Fabs(p[1] - a.p[1]) <= epsilon && idMath.Fabs(p[2] - a.p[2]) <= epsilon && idMath.Fabs(p[3] - a.p[3]) <= epsilon && idMath.Fabs(p[4] - a.p[4]) <= epsilon && idMath.Fabs(p[5] - a.p[5]) <= epsilon); }
        public static bool operator ==(idVec6 a, idVec6 b) { return a.Compare(ref b); }
        public static bool operator !=(idVec6 a, idVec6 b) { return !a.Compare(ref b); }
        #endregion

        public float Length() { return (float)Math.Sqrt(p[0] * p[0] + p[1] * p[1] + p[2] * p[2] + p[3] * p[3] + p[4] * p[4] + p[5] * p[5]); }
        public float LengthFast() { float sqrLength = p[0] * p[0] + p[1] * p[1] + p[2] * p[2] + p[3] * p[3] + p[4] * p[4] + p[5] * p[5]; return sqrLength * idMath.RSqrt(sqrLength); }
        public float LengthSqr() { return (p[0] * p[0] + p[1] * p[1] + p[2] * p[2] + p[3] * p[3] + p[4] * p[4] + p[5] * p[5]); }
        public float Normalize()
        {
            float sqrLength = p[0] * p[0] + p[1] * p[1] + p[2] * p[2] + p[3] * p[3] + p[4] * p[4] + p[5] * p[5];
            float invLength = idMath.InvSqrt(sqrLength);
            p[0] *= invLength;
            p[1] *= invLength;
            p[2] *= invLength;
            p[3] *= invLength;
            p[4] *= invLength;
            p[5] *= invLength;
            return invLength * sqrLength;
        }
        public float NormalizeFast()
        {
            float sqrLength = p[0] * p[0] + p[1] * p[1] + p[2] * p[2] + p[3] * p[3] + p[4] * p[4] + p[5] * p[5];
            float invLength = idMath.RSqrt(sqrLength);
            p[0] = invLength;
            p[1] *= invLength;
            p[2] *= invLength;
            p[3] *= invLength;
            p[4] *= invLength;
            p[5] *= invLength;
            return invLength * sqrLength;
        }
    }
}
