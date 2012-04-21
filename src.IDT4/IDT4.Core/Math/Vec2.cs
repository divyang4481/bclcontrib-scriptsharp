using System;
namespace IDT4.Math2
{
    public struct idVec2
    {
        public readonly static idVec2 origin = new idVec2(0f, 0f);
        public float x;
        public float y;

        public idVec2() { x = y = 0f; }
        public idVec2(float x, float y) { this.x = x; this.y = y; }
        public void Set(float x, float y) { this.x = x; this.y = y; }
        public void Zero() { x = y = 0.0f; }
        public int GetDimension() { return 2; }
        public float[] ToArray() { return new float[] { x, y }; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }
        public static void Lerp(out idVec2 t, idVec2 v1, idVec2 v2, float l)
        {
            if (l <= 0.0f) t = v1;
            else if (l >= 1.0f) t = v2;
            else t = v1 + (v2 - v1) * l;
        }

        #region Operator
        //public float this[int index] { get { throw new NotSupportedException(); } }
        public static idVec2 operator -(idVec2 a) { return new idVec2(-a.x, -a.y); }
        public static idVec2 operator -(idVec2 a, idVec2 b) { return new idVec2(a.x - b.x, a.y - b.y); }
        public static float operator *(idVec2 a, idVec2 b) { return a.x * b.x + a.y * b.y; }
        public static idVec2 operator *(idVec2 a, float b) { return new idVec2(a.x * b, a.y * b); }
        public static idVec2 operator /(idVec2 a, float b) { float invb = 1.0f / b; return new idVec2(a.x * invb, a.y * invb); }
        public static idVec2 operator +(idVec2 a, idVec2 b) { return new idVec2(a.x + b.x, a.y + b.y); }
        public idVec2 opAdd(idVec2 a) { x += a.x; y += a.y; return this; }
        public idVec2 opSub(idVec2 a) { x -= a.x; y -= a.y; return this; }
        public idVec2 opDiv(idVec2 a) { x /= a.x; y /= a.y; return this; }
        public idVec2 opMul(float a) { x *= a; y *= a; return this; }
        public idVec2 opDiv(float a) { float inva = 1.0f / a; x *= inva; y *= inva; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idVec2 a) { return (x == a.x && y == a.y); }
        public bool Compare(ref idVec2 a, float epsilon) { return (idMath.Fabs(x - a.x) <= epsilon && idMath.Fabs(y - a.y) <= epsilon); }
        public static bool operator ==(idVec2 a, idVec2 b) { return a.Compare(ref b); }
        public static bool operator !=(idVec2 a, idVec2 b) { return !a.Compare(ref b); }
        #endregion

        public float Length() { return (float)Math.Sqrt(x * x + y * y); }
        public float LengthFast() { float sqrLength = x * x + y * y; return sqrLength * idMath.RSqrt(sqrLength); }
        public float LengthSqr() { return (x * x + y * y); }
        public float Normalize()
        {
            float sqrLength = x * x + y * y;
            float invLength = idMath.InvSqrt(sqrLength);
            x *= invLength;
            y *= invLength;
            return invLength * sqrLength;
        }
        public float NormalizeFast()
        {
            float lengthSqr = x * x + y * y;
            float invLength = idMath.RSqrt(lengthSqr);
            x *= invLength;
            y *= invLength;
            return invLength * lengthSqr;
        }
        public idVec2 Truncate(float length)
        {
            if (length == 0)
                Zero();
            else
            {
                float length2 = LengthSqr();
                if (length2 > length * length)
                {
                    float ilength = length * idMath.InvSqrt(length2);
                    x *= ilength;
                    y *= ilength;
                }
            }
            return this;
        }
        public void Clamp(idVec2 min, idVec2 max)
        {
            if (x < min.x) x = min.x;
            else if (x > max.x) x = max.x;
            if (y < min.y) y = min.y;
            else if (y > max.y) y = max.y;
        }
        public void Snap()
        {
            x = (float)Math.Floor(x + 0.5f);
            y = (float)Math.Floor(y + 0.5f);
        }
        public void SnapInt()
        {
            //x = float( int( x ) );
            //y = float( int( y ) );
        }
    }
}



