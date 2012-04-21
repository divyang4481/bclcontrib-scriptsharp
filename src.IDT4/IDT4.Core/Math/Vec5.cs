using System;
namespace IDT4.Math2
{
    public struct idVec5
    {
        public readonly static idVec5 origin = new idVec5(0f, 0f, 0f, 0f, 0f);
        public float x;
        public float y;
        public float z;
        public float s;
        public float t;

        public idVec5() { x = y = z = s = t = 0f; }
        public idVec5(idVec3 xyz, idVec2 st) { x = xyz.x; y = xyz.y; z = xyz.z; s = st.x; t = st.y; }
        public static implicit operator idVec5(idVec3 a) { return new idVec5(a.x, a.y, a.z, 0f, 0f); }
        public idVec5(float x, float y, float z, float s, float t) { this.x = x; this.y = y; this.z = z; this.s = s; this.t = t; }
        public void Set(float x, float y, float z, float s, float t) { this.x = x; this.y = y; this.z = z; this.s = s; this.t = t; }
        public void Zero() { x = y = z = s = t = 0f; }
        public int GetDimension() { return 5; }
        public idVec3 ToVec3() { return new idVec3(x, y, z); }
        public float[] ToArray() { return new float[] { x, y, z, s, t }; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }
        public static void Lerp(out idVec5 t, idVec5 v1, idVec5 v2, float l)
        {
            if (l <= 0.0f) t = v1;
            else if (l >= 1.0f) t = v2;
            else
            {
                t.x = v1.x + l * (v2.x - v1.x);
                t.y = v1.y + l * (v2.y - v1.y);
                t.z = v1.z + l * (v2.z - v1.z);
                t.s = v1.s + l * (v2.s - v1.s);
                t.t = v1.t + l * (v2.t - v1.t);
            }
        }

        #region Operator
        //public float this[int index] { get { throw new NotSupportedException(); } }
        public static idVec5 operator -(idVec5 a) { return new idVec5(-a.x, -a.y, -a.z, -a.s, -a.t); }
        public static idVec5 operator -(idVec5 a, idVec5 b) { return new idVec5(a.x - b.x, a.y - b.y, a.z - b.z, a.s - b.s, a.t - b.t); }
        public static float operator *(idVec5 a, idVec5 b) { return a.x * b.x + a.y * b.y + a.z * b.z + a.s * b.s + a.t * b.t; }
        public static idVec5 operator *(idVec5 a, float b) { return new idVec5(a.x * b, a.y * b, a.z * b, a.s * b, a.t * b); }
        public static idVec5 operator /(idVec5 a, float b) { float invb = 1.0f / b; return new idVec5(a.x * invb, a.y * invb, a.z * invb, a.s * invb, a.t * invb); }
        public static idVec5 operator +(idVec5 a, idVec5 b) { return new idVec5(a.x + b.x, a.y + b.y, a.z + b.z, a.s + b.s, a.t + b.t); }
        public idVec5 opAdd(idVec5 a) { x += a.x; y += a.y; z += a.z; s += a.s; t += a.t; return this; }
        public idVec5 opSub(idVec5 a) { x -= a.x; y -= a.y; z -= a.z; s -= a.s; t -= a.t; return this; }
        public idVec5 opDiv(idVec5 a) { x /= a.x; y /= a.y; z /= a.z; s /= a.s; t /= a.t; return this; }
        public idVec5 opMul(float a) { x *= a; y *= a; z *= a; s *= a; t *= a; return this; }
        public idVec5 opDiv(float a) { float inva = 1.0f / a; x *= inva; y *= inva; z *= inva; s *= inva; t *= inva; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idVec5 a) { return (x == a.x && y == a.y && z == a.z && s == a.s && t == a.t); }
        public bool Compare(ref idVec5 a, float epsilon) { return (idMath.Fabs(x - a.x) <= epsilon && idMath.Fabs(y - a.y) <= epsilon && idMath.Fabs(z - a.z) <= epsilon && idMath.Fabs(s - a.s) <= epsilon && idMath.Fabs(t - a.t) <= epsilon); }
        public static bool operator ==(idVec5 a, idVec5 b) { return a.Compare(ref b); }
        public static bool operator !=(idVec5 a, idVec5 b) { return !a.Compare(ref b); }
        #endregion
    }
}
