using System;
namespace IDT4.Math2
{
    public struct idVec4
    {
        public readonly static idVec4 origin = new idVec4(0f, 0f, 0f, 0f);
        public float x;
        public float y;
        public float z;
        public float w;

        public idVec4() { x = y = z = w = 0f; }
        public idVec4(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public void Set(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public void Zero() { x = y = z = w = 0f; }
        public int GetDimension() { return 4; }
        public idVec2 ToVec2() { return new idVec2(x, y); }
        public idVec3 ToVec3() { return new idVec3(x, y, z); }
        public float[] ToArray() { return new float[] { x, y, z, w }; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }
        public static void Lerp(out idVec4 t, idVec4 v1, idVec4 v2, float l)
        {
            if (l <= 0.0f) t = v1;
            else if (l >= 1.0f) t = v2;
            else t = v1 + (v2 - v1) * l;
        }

        #region Operator
        //public float this[int index] { get { throw new NotSupportedException(); } }
        public static idVec4 operator -(idVec4 a) { return new idVec4(-a.x, -a.y, -a.z, -a.w); }
        public static idVec4 operator -(idVec4 a, idVec4 b) { return new idVec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }
        public static float operator *(idVec4 a, idVec4 b) { return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w; }
        public static idVec4 operator *(idVec4 a, float b) { return new idVec4(a.x * b, a.y * b, a.z * b, a.w * b); }
        public static idVec4 operator /(idVec4 a, float b) { float invb = 1.0f / b; return new idVec4(a.x * invb, a.y * invb, a.z * invb, a.w * invb); }
        public static idVec4 operator +(idVec4 a, idVec4 b) { return new idVec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }
        public idVec4 opAdd(idVec4 a) { x += a.x; y += a.y; z += a.z; w += a.w; return this; }
        public idVec4 opSub(idVec4 a) { x -= a.x; y -= a.y; z -= a.z; w -= a.w; return this; }
        public idVec4 opDiv(idVec4 a) { x /= a.x; y /= a.y; z /= a.z; w /= a.w; return this; }
        public idVec4 opMul(float a) { x *= a; y *= a; z *= a; w *= a; return this; }
        public idVec4 opDiv(float a) { float inva = 1.0f / a; x *= inva; y *= inva; z *= inva; w *= inva; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idVec4 a) { return (x == a.x && y == a.y && z == a.z && w == a.w); }
        public bool Compare(ref idVec4 a, float epsilon) { return (idMath.Fabs(x - a.x) <= epsilon && idMath.Fabs(y - a.y) <= epsilon && idMath.Fabs(z - a.z) <= epsilon && idMath.Fabs(w - a.w) <= epsilon); }
        public static bool operator ==(idVec4 a, idVec4 b) { return a.Compare(ref b); }
        public static bool operator !=(idVec4 a, idVec4 b) { return !a.Compare(ref b); }
        #endregion
    }
}
