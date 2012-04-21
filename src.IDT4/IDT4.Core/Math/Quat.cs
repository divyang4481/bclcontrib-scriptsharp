using System;
namespace IDT4.Math2
{
    public struct idQuat
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public idQuat() { x = y = z = w = 0f; }
        public idQuat(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public void Set(float x, float y, float z, float w) { this.x = x; this.y = y; this.z = z; this.w = w; }
        public void Zero() { x = y = z = w = 0f; }
        public int GetDimension() { return 4; }
        public float[] ToArray() { return new float[] { x, y, z, w }; }
        public string ToString(int precision = 2) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public float this[int index] { get { return 0; } }
        public static idQuat operator -(idQuat a) { return new idQuat(-a.x, -a.y, -a.z, -a.w); }
        //public static idQuat operator =(idQuat a) {  }
        public static idQuat operator +(idQuat a, idQuat b) { return new idQuat(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w); }
        public static idQuat operator -(idQuat a, idQuat b) { return new idQuat(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w); }
        public static idQuat operator *(idQuat a, idQuat b)
        {
            return new idQuat(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y + a.y * b.w + a.z * b.x - a.x * b.z,
                a.w * b.z + a.z * b.w + a.x * b.y - a.y * b.x,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z);
        }
        public static idVec3 operator *(idQuat a, idVec3 b)
        {
#if false
            // it's faster to do the conversion to a 3x3 matrix and multiply the vector by this 3x3 matrix
            return (ToMat3() * a);
#else
            // result = this->Inverse() * idQuat( a.x, a.y, a.z, 0.0f ) * (*this)
            float xxzz = a.x * a.x - a.z * a.z;
            float wwyy = a.w * a.w - a.y * a.y;

            float xw2 = a.x * a.w * 2.0f;
            float xy2 = a.x * a.y * 2.0f;
            float xz2 = a.x * a.z * 2.0f;
            float yw2 = a.y * a.w * 2.0f;
            float yz2 = a.y * a.z * 2.0f;
            float zw2 = a.z * a.w * 2.0f;
            return new idVec3(
                (xxzz + wwyy) * b.x + (xy2 + zw2) * b.y + (xz2 - yw2) * b.z,
                (xy2 - zw2) * b.x + (a.y * a.y + a.w * a.w - a.x * a.x - a.z * a.z) * b.y + (yz2 + xw2) * b.z,
                (xz2 + yw2) * b.x + (yz2 - xw2) * b.y + (wwyy - xxzz) * b.z);
#endif
        }
        public static idQuat operator *(idQuat a, float b) { return new idQuat(a.x * b, a.y * b, a.z * b, a.w * b); }
        public idQuat opAdd(idQuat a) { x += a.x; y += a.y; z += a.z; w += a.w; return this; }
        public idQuat opSub(idQuat a) { x -= a.x; y -= a.y; z -= a.z; w -= a.w; return this; }
        public idQuat opMul(idQuat a) { this = this * a; return this; }
        public idQuat opMul(float a) { x *= a; y *= a; z *= a; w *= a; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idQuat a) { return (x == a.x && y == a.y && z == a.z && w == a.w); }
        public bool Compare(ref idQuat a, float epsilon)
        {
            return (
                idMath.Fabs(x - a.x) <= epsilon &&
                idMath.Fabs(y - a.y) <= epsilon &&
                idMath.Fabs(z - a.z) <= epsilon &&
                idMath.Fabs(w - a.w) <= epsilon);
        }
        public static bool operator ==(idQuat a, idQuat b) { return a.Compare(ref b); }
        public static bool operator !=(idQuat a, idQuat b) { return !a.Compare(ref b); }
        #endregion

        #region Transform
        public idQuat Inverse() { return new idQuat(-x, -y, -z, w); }
        public float Length() { float len = x * x + y * y + z * z + w * w; return idMath.Sqrt(len); }
        public idQuat Normalize()
        {
            float len = Length();
            if (len != 0f)
            {
                float ilength = 1 / len;
                x *= ilength;
                y *= ilength;
                z *= ilength;
                w *= ilength;
            }
            return this;
        }
        #endregion

        public float CalcW()
        {
            // take the absolute value because floating point rounding may cause the dot of x,y,z to be larger than 1
            return (float)Math.Sqrt(Math.Abs(1.0f - (x * x + y * y + z * z)));
        }

        #region Convert
        public idAngles ToAngles() { return ToMat3().ToAngles(); }
        public idRotation ToRotation()
        {
            idVec3 vec = new idVec3();
            vec.x = x;
            vec.y = y;
            vec.z = z;
            float angle = idMath.ACos(w);
            if (angle == 0.0f)
            {
                vec.Set(0.0f, 0.0f, 1.0f);
            }
            else
            {
                //vec *= (1.0f / sin( angle ));
                vec.Normalize();
                vec.FixDegenerateNormal();
                angle *= 2.0f * idMath.M_RAD2DEG;
            }
            return new idRotation(idVec3.origin, vec, angle);
        }
        public idMat3 ToMat3()
        {
            float x2 = x + x;
            float y2 = y + y;
            float z2 = z + z;
            float xx = x * x2;
            float xy = x * y2;
            float xz = x * z2;
            float yy = y * y2;
            float yz = y * z2;
            float zz = z * z2;
            float wx = w * x2;
            float wy = w * y2;
            float wz = w * z2;
            idMat3 mat = new idMat3(); idVec3[] mat_mat = mat.mat;
            mat_mat[0].x = 1.0f - (yy + zz);
            mat_mat[0].y = xy - wz;
            mat_mat[0].z = xz + wy;
            mat_mat[1].x = xy + wz;
            mat_mat[1].y = 1.0f - (xx + zz);
            mat_mat[1].z = yz - wx;
            mat_mat[2].x = xz - wy;
            mat_mat[2].y = yz + wx;
            mat_mat[2].z = 1.0f - (xx + yy);
            return mat;
        }
        public idMat4 ToMat4() { return ToMat3().ToMat4(); }
        public idCQuat ToCQuat() { return (w < 0.0f ? new idCQuat(-x, -y, -z) : new idCQuat(x, y, z)); }
        public idVec3 ToAngularVelocity()
        {
            idVec3 vec = new idVec3();
            vec.x = x;
            vec.y = y;
            vec.z = z;
            vec.Normalize();
            return vec * idMath.ACos(w);
        }
        #endregion

        // Spherical linear interpolation between two quaternions.
        public idQuat Slerp(ref idQuat from, ref idQuat to, float t)
        {
            //float omega, cosom, sinom, scale0, scale1;
            if (t <= 0.0f) { this = from; return this; }
            if (t >= 1.0f) { this = to; return this; }
            if (from == to) { this = to; return this; }
            float cosom = from.x * to.x + from.y * to.y + from.z * to.z + from.w * to.w;
            idQuat temp;
            if (cosom < 0.0f)
            {
                temp = -to;
                cosom = -cosom;
            }
            else
                temp = to;
            float scale0, scale1;
            if (1.0f - cosom > 1e-6f)
            {
#if false
                float omega = Math.Acos(cosom);
                float sinom = 1.0f / Math.Sin(omega);
                scale0 = idMath.Sin((1.0f - t) * omega) * sinom;
                scale1 = idMath.Sin(t * omega) * sinom;
#else
                scale0 = 1.0f - cosom * cosom;
                float sinom = idMath.InvSqrt(scale0);
                float omega = idMath.ATan16(scale0 * sinom, cosom);
                scale0 = idMath.Sin16((1.0f - t) * omega) * sinom;
                scale1 = idMath.Sin16(t * omega) * sinom;
#endif
            }
            else
            {
                scale0 = 1.0f - t;
                scale1 = t;
            }
            this = (from * scale0) + (temp * scale1);
            return this;
        }
    }
}
