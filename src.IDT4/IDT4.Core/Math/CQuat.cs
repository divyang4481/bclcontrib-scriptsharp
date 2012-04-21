using System;
namespace IDT4.Math2
{
    public struct idCQuat
    {
        public float x;
        public float y;
        public float z;

        public idCQuat() { x = y = z = 0f; }
        public idCQuat(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public void Set(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public int GetDimension() { return 3; }
        public float[] ToArray() { return new float[] { x, y, z }; }
        public string ToString(int precision = 2) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public float this[int index] { get { return 0; } }
        #endregion

        #region Compare
        public bool Compare(ref idCQuat a) { return (x == a.x && y == a.y && z == a.z); }
        public bool Compare(ref idCQuat a, float epsilon)
        {
            return (
                idMath.Fabs(x - a.x) <= epsilon &&
                idMath.Fabs(y - a.y) <= epsilon &&
                idMath.Fabs(z - a.z) <= epsilon);
        }
        public static bool operator ==(idCQuat a, idCQuat b) { return a.Compare(ref b); }
        public static bool operator !=(idCQuat a, idCQuat b) { return !a.Compare(ref b); }
        #endregion

        #region Convert
        public idQuat ToQuat()
        {
            // take the absolute value because floating point rounding may cause the dot of x,y,z to be larger than 1
            return new idQuat(x, y, z, (float)Math.Sqrt(Math.Abs(1.0f - (x * x + y * y + z * z))));
        }
        public idAngles ToAngles() { return ToQuat().ToAngles(); }
        public idRotation ToRotation() { return ToQuat().ToRotation(); }
        public idMat3 ToMat3() { return ToQuat().ToMat3(); }
        public idMat4 ToMat4() { return ToQuat().ToMat4(); }
        #endregion
    }
}
