using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idMat2
    {
        public readonly static idMat2 zero = new idMat2(
            0f, 0f,
            0f, 0f);
        public readonly static idMat2 identity = new idMat2(
            1f, 0f,
            0f, 1f);
        internal idVec2[] mat = new idVec2[2];

        public idMat2() { }
        public idMat2(ref idVec2 x, ref idVec2 y)
        {
            mat[0].x = x.x; mat[0].y = x.y;
            mat[1].x = y.x; mat[1].y = y.y;
        }
        public idMat2(float xx, float xy, float yx, float yy)
        {
            mat[0].x = xx; mat[0].y = xy;
            mat[1].x = yx; mat[1].y = yy;
        }
        public idMat2(float[][] src) { Array.Copy(mat, src, 2 * 2); }
        public void Zero()
        {
            mat[0].Zero();
            mat[1].Zero();
        }
        public int GetDimension() { return 4; }
        public float[] ToArray() { return mat[0].ToArray(); }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public idVec2 this[int index] { get { return mat[index]; } }
        public static idMat2 operator -(idMat2 a)
        {
            idVec2[] a_mat = a.mat;
            return new idMat2(
                -a_mat[0].x, -a_mat[0].y,
                -a_mat[1].x, -a_mat[1].y);
        }
        public static idVec2 operator *(idMat2 a, idVec2 vec)
        {
            idVec2[] a_mat = a.mat;
            return new idVec2(
                a_mat[0].x * vec.x + a_mat[0].y * vec.y,
                a_mat[1].x * vec.x + a_mat[1].y * vec.y);
        }
        public static idMat2 operator *(idMat2 a, idMat2 b)
        {
            idVec2[] a_mat = a.mat;
            idVec2[] b_mat = b.mat;
            return new idMat2(
                a_mat[0].x * b_mat[0].x + a_mat[0].y * b_mat[1].x,
                a_mat[0].x * b_mat[0].y + a_mat[0].y * b_mat[1].y,
                a_mat[1].x * b_mat[0].x + a_mat[1].y * b_mat[1].x,
                a_mat[1].x * b_mat[0].y + a_mat[1].y * b_mat[1].y);
        }
        public static idMat2 operator *(idMat2 a, float b)
        {
            idVec2[] a_mat = a.mat;
            return new idMat2(
                a_mat[0].x * b, a_mat[0].y * b,
                a_mat[1].x * b, a_mat[1].y * b);
        }
        public static idMat2 operator +(idMat2 a, idMat2 b)
        {
            idVec2[] a_mat = a.mat;
            idVec2[] b_mat = b.mat;
            return new idMat2(
                a_mat[0].x + b_mat[0].x, a_mat[0].y + b_mat[0].y,
                a_mat[1].x + b_mat[1].x, a_mat[1].y + b_mat[1].y);
        }
        public static idMat2 operator -(idMat2 a, idMat2 b)
        {
            idVec2[] a_mat = a.mat;
            idVec2[] b_mat = b.mat;
            return new idMat2(
                a_mat[0].x - b_mat[0].x, a_mat[0].y - b_mat[0].y,
                a_mat[1].x - b_mat[1].x, a_mat[1].y - b_mat[1].y);
        }
        public idMat2 opMul(float a)
        {
            mat[0].x *= a; mat[0].y *= a;
            mat[1].x *= a; mat[1].y *= a;
            return this;
        }
        public idMat2 opMul(idMat2 a)
        {
            idVec2[] a_mat = a.mat;
            float x, y;
            x = mat[0].x; y = mat[0].y;
            mat[0].x = x * a_mat[0].x + y * a_mat[1].x;
            mat[0].y = x * a_mat[0].y + y * a_mat[1].y;
            x = mat[1].x; y = mat[1].y;
            mat[1].x = x * a_mat[0].x + y * a_mat[1].x;
            mat[1].y = x * a_mat[0].y + y * a_mat[1].y;
            return this;
        }
        public idMat2 opAdd(idMat2 a)
        {
            idVec2[] a_mat = a.mat;
            mat[0].x += a_mat[0].x; mat[0].y += a_mat[0].y;
            mat[1].x += a_mat[1].x; mat[1].y += a_mat[1].y;
            return this;
        }
        public idMat2 opSub(idMat2 a)
        {
            idVec2[] a_mat = a.mat;
            mat[0].x -= a_mat[0].x; mat[0].y -= a_mat[0].y;
            mat[1].x -= a_mat[1].x; mat[1].y -= a_mat[1].y;
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idMat2 a)
        {
            idVec2[] a_mat = a.mat;
            return (
                mat[0].Compare(ref a_mat[0]) &&
                mat[1].Compare(ref a_mat[1]));
        }
        public bool Compare(ref idMat2 a, float epsilon)
        {
            idVec2[] a_mat = a.mat;
            return (
                mat[0].Compare(ref a_mat[0], epsilon) &&
                mat[1].Compare(ref a_mat[1], epsilon));
        }
        public static bool operator ==(idMat2 a, idMat2 b) { return a.Compare(ref b); }
        public static bool operator !=(idMat2 a, idMat2 b) { return !a.Compare(ref b); }
        #endregion

        #region Identity
        public void Identity() { this = identity; }
        public bool IsIdentity(float epsilon) { return Compare(ref identity, epsilon); }
        public bool IsSymmetric(float epsilon) { return (idMath.Fabs(mat[0].y - mat[1].x) < epsilon); }
        public bool IsDiagonal(float epsilon)
        {
            return !(
                idMath.Fabs(mat[0].y) > epsilon ||
                idMath.Fabs(mat[1].x) > epsilon);
        }
        #endregion

        #region Transform
        public float Trace() { return (mat[0].x + mat[1].y); }
        public float Determinant() { return mat[0].x * mat[1].y - mat[0].y * mat[1].x; }
        public idMat2 Transpose()
        {
            return new idMat2(
                mat[0].x, mat[1].x,
                mat[0].y, mat[1].y);
        }
        public idMat2 TransposeSelf()
        {
            float tmp = mat[0].y;
            mat[0].y = mat[1].x;
            mat[1].x = tmp;
            return this;
        }
        public idMat2 Inverse()
        {
            idMat2 invMat = this;
            bool r = invMat.InverseSelf();
            Debug.Assert(r);
            return invMat;
        }
        public idMat2 InverseFast()
        {
            idMat2 invMat = this;
            bool r = invMat.InverseFastSelf();
            Debug.Assert(r);
            return invMat;
        }

        public bool InverseSelf()
        {
            // 2+4 = 6 multiplications
            //		 1 division
            double det = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            double a = mat[0].x;
            mat[0].x = (float)(mat[1].y * invDet);
            mat[0].y = (float)(-mat[0].y * invDet);
            mat[1].x = (float)(-mat[1].x * invDet);
            mat[1].y = (float)(a * invDet);
            return true;
        }
        public bool InverseFastSelf()
        {
#if true
            // 2+4 = 6 multiplications
            //		 1 division
            double det = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            double a = mat[0].x;
            mat[0].x = (float)(mat[1].y * invDet);
            mat[0].y = (float)(-mat[0].y * invDet);
            mat[1].x = (float)(-mat[1].x * invDet);
            mat[1].y = (float)(a * invDet);
            return true;
#else
            // 2*4 = 8 multiplications
            //		 2 division
            double di = mat[0];
            float s = di;
            double d;
            mat[0 * 2 + 0] = (float)(d = 1.0f / di);
            mat[0 * 2 + 1] *= d;
            d = -d;
            mat[1 * 2 + 0] *= d;
            d = mat[1 * 2 + 0] * di;
            mat[1 * 2 + 1] += mat[0 * 2 + 1] * d;
            di = mat[1 * 2 + 1];
            s *= di;
            mat[1 * 2 + 1] = d = 1.0f / di;
            mat[1 * 2 + 0] *= d;
            d = -d;
            mat[0 * 2 + 1] *= d;
            d = mat[0 * 2 + 1] * di;
            mat[0 * 2 + 0] += mat[1 * 2 + 0] * d;
            return (s != 0.0f && !FLOAT_IS_NAN(s));
#endif
        }
        #endregion
    }
}



