using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idMat5
    {
        public static readonly idMat5 zero = new idMat5(
            new idVec5(0f, 0f, 0f, 0f, 0f),
            new idVec5(0f, 0f, 0f, 0f, 0f),
            new idVec5(0f, 0f, 0f, 0f, 0f),
            new idVec5(0f, 0f, 0f, 0f, 0f),
            new idVec5(0f, 0f, 0f, 0f, 0f));
        public static readonly idMat5 identity = new idMat5(
            new idVec5(1f, 0f, 0f, 0f, 0f),
            new idVec5(0f, 1f, 0f, 0f, 0f),
            new idVec5(0f, 0f, 1f, 0f, 0f),
            new idVec5(0f, 0f, 0f, 1f, 0f),
            new idVec5(0f, 0f, 0f, 0f, 1f));
        idVec5[] mat = new idVec5[5];
        public idMat5() { }
        public idMat5(float[] src)
        {
            Array.Copy(mat, src, 5 * 5);
        }
        public idMat5(idVec5 v0, idVec5 v1, idVec5 v2, idVec5 v3, idVec5 v4) { mat[0] = v0; mat[1] = v1; mat[2] = v2; mat[3] = v3; mat[4] = v4; }
        public idMat5(ref idVec5 v0, ref idVec5 v1, ref idVec5 v2, ref idVec5 v3, ref idVec5 v4) { mat[0] = v0; mat[1] = v1; mat[2] = v2; mat[3] = v3; mat[4] = v4; }
        public void Zero()
        {
            Array.Clear(mat, 0, 5 * 5);
        }
        public int GetDimension() { return 25; }
        public float[] ToArray() { return mat[0].ToArray(); }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public idVec5 this[int index] { get { return mat[index]; } }
        public static idMat5 operator *(idMat5 a, idMat5 b)
        {
            idMat5 dst = new idMat5();
            //float[] m1Ptr = this;
            //float[] m2Ptr = a;
            //float[] dstPtr = null;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //*dstPtr = m1Ptr[0] * m2Ptr[ 0 * 5 + j ]
                    //        + m1Ptr[1] * m2Ptr[ 1 * 5 + j ]
                    //        + m1Ptr[2] * m2Ptr[ 2 * 5 + j ]
                    //        + m1Ptr[3] * m2Ptr[ 3 * 5 + j ]
                    //        + m1Ptr[4] * m2Ptr[ 4 * 5 + j ];
                    //dstPtr++;
                }
                //m1Ptr += 5;
            }
            return dst;
        }
        public static idMat5 operator *(idMat5 a, float b)
        {
            idVec5[] a_mat = a.mat;
            return new idMat5(
                new idVec5(a_mat[0].x * b, a_mat[0].y * b, a_mat[0].z * b, a_mat[0].s * b, a_mat[0].t * b),
                new idVec5(a_mat[1].x * b, a_mat[1].y * b, a_mat[1].z * b, a_mat[1].s * b, a_mat[1].t * b),
                new idVec5(a_mat[2].x * b, a_mat[2].y * b, a_mat[2].z * b, a_mat[2].s * b, a_mat[2].t * b),
                new idVec5(a_mat[3].x * b, a_mat[3].y * b, a_mat[3].z * b, a_mat[3].s * b, a_mat[3].t * b),
                new idVec5(a_mat[4].x * b, a_mat[4].y * b, a_mat[4].z * b, a_mat[4].s * b, a_mat[4].t * b));
        }
        public static idVec5 operator *(idMat5 a, idVec5 vec)
        {
            idVec5[] a_mat = a.mat;
            return new idVec5(
                a_mat[0].x * vec.x + a_mat[0].y * vec.y + a_mat[0].z * vec.z + a_mat[0].s * vec.s + a_mat[0].t * vec.t,
                a_mat[1].x * vec.x + a_mat[1].y * vec.y + a_mat[1].z * vec.z + a_mat[1].s * vec.s + a_mat[1].t * vec.t,
                a_mat[2].x * vec.x + a_mat[2].y * vec.y + a_mat[2].z * vec.z + a_mat[2].s * vec.s + a_mat[2].t * vec.t,
                a_mat[3].x * vec.x + a_mat[3].y * vec.y + a_mat[3].z * vec.z + a_mat[3].s * vec.s + a_mat[3].t * vec.t,
                a_mat[4].x * vec.x + a_mat[4].y * vec.y + a_mat[4].z * vec.z + a_mat[4].s * vec.s + a_mat[4].t * vec.t);
        }
        public static idMat5 operator +(idMat5 a, idMat5 b)
        {
            idVec5[] a_mat = a.mat;
            idVec5[] b_mat = b.mat;
            return new idMat5(
                new idVec5(a_mat[0].x + b_mat[0].x, a_mat[0].y + b_mat[0].y, a_mat[0].z + b_mat[0].z, a_mat[0].s + b_mat[0].s, a_mat[0].t + b_mat[0].t),
                new idVec5(a_mat[1].x + b_mat[1].x, a_mat[1].y + b_mat[1].y, a_mat[1].z + b_mat[1].z, a_mat[1].s + b_mat[1].s, a_mat[1].t + b_mat[1].t),
                new idVec5(a_mat[2].x + b_mat[2].x, a_mat[2].y + b_mat[2].y, a_mat[2].z + b_mat[2].z, a_mat[2].s + b_mat[2].s, a_mat[2].t + b_mat[2].t),
                new idVec5(a_mat[3].x + b_mat[3].x, a_mat[3].y + b_mat[3].y, a_mat[3].z + b_mat[3].z, a_mat[3].s + b_mat[3].s, a_mat[3].t + b_mat[3].t),
                new idVec5(a_mat[4].x + b_mat[4].x, a_mat[4].y + b_mat[4].y, a_mat[4].z + b_mat[4].z, a_mat[4].s + b_mat[4].s, a_mat[4].t + b_mat[4].t));
        }
        public static idMat5 operator -(idMat5 a, idMat5 b)
        {
            idVec5[] a_mat = a.mat;
            idVec5[] b_mat = b.mat;
            return new idMat5(
                new idVec5(a_mat[0].x - b_mat[0].x, a_mat[0].y - b_mat[0].y, a_mat[0].z - b_mat[0].z, a_mat[0].s - b_mat[0].s, a_mat[0].t - b_mat[0].t),
                new idVec5(a_mat[1].x - b_mat[1].x, a_mat[1].y - b_mat[1].y, a_mat[1].z - b_mat[1].z, a_mat[1].s - b_mat[1].s, a_mat[1].t - b_mat[1].t),
                new idVec5(a_mat[2].x - b_mat[2].x, a_mat[2].y - b_mat[2].y, a_mat[2].z - b_mat[2].z, a_mat[2].s - b_mat[2].s, a_mat[2].t - b_mat[2].t),
                new idVec5(a_mat[3].x - b_mat[3].x, a_mat[3].y - b_mat[3].y, a_mat[3].z - b_mat[3].z, a_mat[3].s - b_mat[3].s, a_mat[3].t - b_mat[3].t),
                new idVec5(a_mat[4].x - b_mat[4].x, a_mat[4].y - b_mat[4].y, a_mat[4].z - b_mat[4].z, a_mat[4].s - b_mat[4].s, a_mat[4].t - b_mat[4].t));
        }
        public idMat5 opMul(float a)
        {
            mat[0].x *= a; mat[0].y *= a; mat[0].z *= a; mat[0].s *= a; mat[0].t *= a;
            mat[1].x *= a; mat[1].y *= a; mat[1].z *= a; mat[1].s *= a; mat[1].t *= a;
            mat[2].x *= a; mat[2].y *= a; mat[2].z *= a; mat[2].s *= a; mat[2].t *= a;
            mat[3].x *= a; mat[3].y *= a; mat[3].z *= a; mat[3].s *= a; mat[3].t *= a;
            mat[4].x *= a; mat[4].y *= a; mat[4].z *= a; mat[4].s *= a; mat[4].t *= a;
            return this;
        }
        public idMat5 opMul(ref idMat5 a)
        {
            this = this * a;
            return this;
        }
        public idMat5 opAdd(ref idMat5 a)
        {
            idVec5[] a_mat = a.mat;
            mat[0].x += a_mat[0].x; mat[0].y += a_mat[0].y; mat[0].z += a_mat[0].z; mat[0].s += a_mat[0].s; mat[0].t += a_mat[0].t;
            mat[1].x += a_mat[1].x; mat[1].y += a_mat[1].y; mat[1].z += a_mat[1].z; mat[1].s += a_mat[1].s; mat[1].t += a_mat[1].t;
            mat[2].x += a_mat[2].x; mat[2].y += a_mat[2].y; mat[2].z += a_mat[2].z; mat[2].s += a_mat[2].s; mat[2].t += a_mat[2].t;
            mat[3].x += a_mat[3].x; mat[3].y += a_mat[3].y; mat[3].z += a_mat[3].z; mat[3].s += a_mat[3].s; mat[3].t += a_mat[3].t;
            mat[4].x += a_mat[4].x; mat[4].y += a_mat[4].y; mat[4].z += a_mat[4].z; mat[4].s += a_mat[4].s; mat[4].t += a_mat[4].t;
            return this;
        }
        public idMat5 opSub(ref idMat5 a)
        {
            idVec5[] a_mat = a.mat;
            mat[0].x -= a_mat[0].x; mat[0].y -= a_mat[0].y; mat[0].z -= a_mat[0].z; mat[0].s -= a_mat[0].s; mat[0].t -= a_mat[0].t;
            mat[1].x -= a_mat[1].x; mat[1].y -= a_mat[1].y; mat[1].z -= a_mat[1].z; mat[1].s -= a_mat[1].s; mat[1].t -= a_mat[1].t;
            mat[2].x -= a_mat[2].x; mat[2].y -= a_mat[2].y; mat[2].z -= a_mat[2].z; mat[2].s -= a_mat[2].s; mat[2].t -= a_mat[2].t;
            mat[3].x -= a_mat[3].x; mat[3].y -= a_mat[3].y; mat[3].z -= a_mat[3].z; mat[3].s -= a_mat[3].s; mat[3].t -= a_mat[3].t;
            mat[4].x -= a_mat[4].x; mat[4].y -= a_mat[4].y; mat[4].z -= a_mat[4].z; mat[4].s -= a_mat[4].s; mat[4].t -= a_mat[4].t;
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idMat5 a)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 5 * 5; i++)
                if (ptr1[i] != ptr2[i])
                    return false;
            return true;
        }
        public bool Compare(ref idMat5 a, float epsilon)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 5 * 5; i++)
                if (idMath.Fabs(ptr1[i] - ptr2[i]) > epsilon)
                    return false;
            return true;
        }
        public static bool operator ==(idMat5 a, idMat5 b) { return a.Compare(ref b); }
        public static bool operator !=(idMat5 a, idMat5 b) { return !a.Compare(ref b); }
        #endregion

        #region Identity
        public void Identity() { this = identity; }
        public bool IsIdentity(float epsilon) { return Compare(ref identity, epsilon); }
        public bool IsSymmetric(float epsilon)
        {
            for (int i = 1; i < 5; i++)
                for (int j = 0; j < i; j++)
                    if (idMath.Fabs(mat[i][j] - mat[j][i]) > epsilon)
                        return false;
            return true;
        }
        public bool IsDiagonal(float epsilon)
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (i != j && idMath.Fabs(mat[i][j]) > epsilon)
                        return false;
            return true;
        }
        #endregion

        #region Transform
        public float Trace() { return (mat[0].x + mat[1].y + mat[2].z + mat[3].s + mat[4].t); }
        public float Determinant()
        {
            // 2x2 sub-determinants required to calculate 5x5 determinant
            float det2_34_01 = mat[3].x * mat[4].y - mat[3].y * mat[4].x;
            float det2_34_02 = mat[3].x * mat[4].z - mat[3].z * mat[4].x;
            float det2_34_03 = mat[3].x * mat[4].s - mat[3].s * mat[4].x;
            float det2_34_04 = mat[3].x * mat[4].t - mat[3].t * mat[4].x;
            float det2_34_12 = mat[3].y * mat[4].z - mat[3].z * mat[4].y;
            float det2_34_13 = mat[3].y * mat[4].s - mat[3].s * mat[4].y;
            float det2_34_14 = mat[3].y * mat[4].t - mat[3].t * mat[4].y;
            float det2_34_23 = mat[3].z * mat[4].s - mat[3].s * mat[4].z;
            float det2_34_24 = mat[3].z * mat[4].t - mat[3].t * mat[4].z;
            float det2_34_34 = mat[3].s * mat[4].t - mat[3].t * mat[4].s;
            // 3x3 sub-determinants required to calculate 5x5 determinant
            float det3_234_012 = mat[2].x * det2_34_12 - mat[2].y * det2_34_02 + mat[2].z * det2_34_01;
            float det3_234_013 = mat[2].x * det2_34_13 - mat[2].y * det2_34_03 + mat[2].s * det2_34_01;
            float det3_234_014 = mat[2].x * det2_34_14 - mat[2].y * det2_34_04 + mat[2].t * det2_34_01;
            float det3_234_023 = mat[2].x * det2_34_23 - mat[2].z * det2_34_03 + mat[2].s * det2_34_02;
            float det3_234_024 = mat[2].x * det2_34_24 - mat[2].z * det2_34_04 + mat[2].t * det2_34_02;
            float det3_234_034 = mat[2].x * det2_34_34 - mat[2].s * det2_34_04 + mat[2].t * det2_34_03;
            float det3_234_123 = mat[2].y * det2_34_23 - mat[2].z * det2_34_13 + mat[2].s * det2_34_12;
            float det3_234_124 = mat[2].y * det2_34_24 - mat[2].z * det2_34_14 + mat[2].t * det2_34_12;
            float det3_234_134 = mat[2].y * det2_34_34 - mat[2].s * det2_34_14 + mat[2].t * det2_34_13;
            float det3_234_234 = mat[2].z * det2_34_34 - mat[2].s * det2_34_24 + mat[2].t * det2_34_23;
            // 4x4 sub-determinants required to calculate 5x5 determinant
            float det4_1234_0123 = mat[1].x * det3_234_123 - mat[1].y * det3_234_023 + mat[1].z * det3_234_013 - mat[1].s * det3_234_012;
            float det4_1234_0124 = mat[1].x * det3_234_124 - mat[1].y * det3_234_024 + mat[1].z * det3_234_014 - mat[1].t * det3_234_012;
            float det4_1234_0134 = mat[1].x * det3_234_134 - mat[1].y * det3_234_034 + mat[1].s * det3_234_014 - mat[1].t * det3_234_013;
            float det4_1234_0234 = mat[1].x * det3_234_234 - mat[1].z * det3_234_034 + mat[1].s * det3_234_024 - mat[1].t * det3_234_023;
            float det4_1234_1234 = mat[1].y * det3_234_234 - mat[1].z * det3_234_134 + mat[1].s * det3_234_124 - mat[1].t * det3_234_123;
            // determinant of 5x5 matrix
            return mat[0].x * det4_1234_1234 - mat[0].y * det4_1234_0234 + mat[0].z * det4_1234_0134 - mat[0].s * det4_1234_0124 + mat[0].t * det4_1234_0123;
        }
        public idMat5 Transpose()
        {
            idMat5 transpose = new idMat5();
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    transpose.mat[i][j] = mat[j][i];
            return transpose;
        }
        public idMat5 TransposeSelf()
        {
            for (int i = 0; i < 5; i++)
                for (int j = i + 1; j < 5; j++)
                {
                    float temp = mat[i][j];
                    mat[i][j] = mat[j][i];
                    mat[j][i] = temp;
                }
            return this;
        }
        public idMat5 Inverse()
        {
            idMat5 invMat = this;
            bool r = invMat.InverseSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseSelf()
        {
            // 280+5+25 = 310 multiplications
            //				1 division
            // 2x2 sub-determinants required to calculate 5x5 determinant
            float det2_34_01 = mat[3].x * mat[4].y - mat[3].y * mat[4].x;
            float det2_34_02 = mat[3].x * mat[4].z - mat[3].z * mat[4].x;
            float det2_34_03 = mat[3].x * mat[4].s - mat[3].s * mat[4].x;
            float det2_34_04 = mat[3].x * mat[4].t - mat[3].t * mat[4].x;
            float det2_34_12 = mat[3].y * mat[4].z - mat[3].z * mat[4].y;
            float det2_34_13 = mat[3].y * mat[4].s - mat[3].s * mat[4].y;
            float det2_34_14 = mat[3].y * mat[4].t - mat[3].t * mat[4].y;
            float det2_34_23 = mat[3].z * mat[4].s - mat[3].s * mat[4].z;
            float det2_34_24 = mat[3].z * mat[4].t - mat[3].t * mat[4].z;
            float det2_34_34 = mat[3].s * mat[4].t - mat[3].t * mat[4].s;
            // 3x3 sub-determinants required to calculate 5x5 determinant
            float det3_234_012 = mat[2].x * det2_34_12 - mat[2].y * det2_34_02 + mat[2].z * det2_34_01;
            float det3_234_013 = mat[2].x * det2_34_13 - mat[2].y * det2_34_03 + mat[2].s * det2_34_01;
            float det3_234_014 = mat[2].x * det2_34_14 - mat[2].y * det2_34_04 + mat[2].t * det2_34_01;
            float det3_234_023 = mat[2].x * det2_34_23 - mat[2].z * det2_34_03 + mat[2].s * det2_34_02;
            float det3_234_024 = mat[2].x * det2_34_24 - mat[2].z * det2_34_04 + mat[2].t * det2_34_02;
            float det3_234_034 = mat[2].x * det2_34_34 - mat[2].s * det2_34_04 + mat[2].t * det2_34_03;
            float det3_234_123 = mat[2].y * det2_34_23 - mat[2].z * det2_34_13 + mat[2].s * det2_34_12;
            float det3_234_124 = mat[2].y * det2_34_24 - mat[2].z * det2_34_14 + mat[2].t * det2_34_12;
            float det3_234_134 = mat[2].y * det2_34_34 - mat[2].s * det2_34_14 + mat[2].t * det2_34_13;
            float det3_234_234 = mat[2].z * det2_34_34 - mat[2].s * det2_34_24 + mat[2].t * det2_34_23;
            // 4x4 sub-determinants required to calculate 5x5 determinant
            float det4_1234_0123 = mat[1].x * det3_234_123 - mat[1].y * det3_234_023 + mat[1].z * det3_234_013 - mat[1].s * det3_234_012;
            float det4_1234_0124 = mat[1].x * det3_234_124 - mat[1].y * det3_234_024 + mat[1].z * det3_234_014 - mat[1].t * det3_234_012;
            float det4_1234_0134 = mat[1].x * det3_234_134 - mat[1].y * det3_234_034 + mat[1].s * det3_234_014 - mat[1].t * det3_234_013;
            float det4_1234_0234 = mat[1].x * det3_234_234 - mat[1].z * det3_234_034 + mat[1].s * det3_234_024 - mat[1].t * det3_234_023;
            float det4_1234_1234 = mat[1].y * det3_234_234 - mat[1].z * det3_234_134 + mat[1].s * det3_234_124 - mat[1].t * det3_234_123;
            // determinant of 5x5 matrix
            double det = mat[0].x * det4_1234_1234 - mat[0].y * det4_1234_0234 + mat[0].z * det4_1234_0134 - mat[0].s * det4_1234_0124 + mat[0].t * det4_1234_0123;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_23_01 = mat[2].x * mat[3].y - mat[2].y * mat[3].x;
            float det2_23_02 = mat[2].x * mat[3].z - mat[2].z * mat[3].x;
            float det2_23_03 = mat[2].x * mat[3].s - mat[2].s * mat[3].x;
            float det2_23_04 = mat[2].x * mat[3].t - mat[2].t * mat[3].x;
            float det2_23_12 = mat[2].y * mat[3].z - mat[2].z * mat[3].y;
            float det2_23_13 = mat[2].y * mat[3].s - mat[2].s * mat[3].y;
            float det2_23_14 = mat[2].y * mat[3].t - mat[2].t * mat[3].y;
            float det2_23_23 = mat[2].z * mat[3].s - mat[2].s * mat[3].z;
            float det2_23_24 = mat[2].z * mat[3].t - mat[2].t * mat[3].z;
            float det2_23_34 = mat[2].s * mat[3].t - mat[2].t * mat[3].s;
            float det2_24_01 = mat[2].x * mat[4].y - mat[2].y * mat[4].x;
            float det2_24_02 = mat[2].x * mat[4].z - mat[2].z * mat[4].x;
            float det2_24_03 = mat[2].x * mat[4].s - mat[2].s * mat[4].x;
            float det2_24_04 = mat[2].x * mat[4].t - mat[2].t * mat[4].x;
            float det2_24_12 = mat[2].y * mat[4].z - mat[2].z * mat[4].y;
            float det2_24_13 = mat[2].y * mat[4].s - mat[2].s * mat[4].y;
            float det2_24_14 = mat[2].y * mat[4].t - mat[2].t * mat[4].y;
            float det2_24_23 = mat[2].s * mat[4].s - mat[2].s * mat[4].z;
            float det2_24_24 = mat[2].s * mat[4].t - mat[2].t * mat[4].z;
            float det2_24_34 = mat[2].t * mat[4].t - mat[2].t * mat[4].s;
            // remaining 3x3 sub-determinants
            float det3_123_012 = mat[1].x * det2_23_12 - mat[1].y * det2_23_02 + mat[1].z * det2_23_01;
            float det3_123_013 = mat[1].x * det2_23_13 - mat[1].y * det2_23_03 + mat[1].s * det2_23_01;
            float det3_123_014 = mat[1].x * det2_23_14 - mat[1].y * det2_23_04 + mat[1].t * det2_23_01;
            float det3_123_023 = mat[1].x * det2_23_23 - mat[1].z * det2_23_03 + mat[1].s * det2_23_02;
            float det3_123_024 = mat[1].x * det2_23_24 - mat[1].z * det2_23_04 + mat[1].t * det2_23_02;
            float det3_123_034 = mat[1].x * det2_23_34 - mat[1].s * det2_23_04 + mat[1].t * det2_23_03;
            float det3_123_123 = mat[1].y * det2_23_23 - mat[1].z * det2_23_13 + mat[1].s * det2_23_12;
            float det3_123_124 = mat[1].y * det2_23_24 - mat[1].z * det2_23_14 + mat[1].t * det2_23_12;
            float det3_123_134 = mat[1].y * det2_23_34 - mat[1].s * det2_23_14 + mat[1].t * det2_23_13;
            float det3_123_234 = mat[1].z * det2_23_34 - mat[1].s * det2_23_24 + mat[1].t * det2_23_23;
            float det3_124_012 = mat[1].x * det2_24_12 - mat[1].y * det2_24_02 + mat[1].z * det2_24_01;
            float det3_124_013 = mat[1].x * det2_24_13 - mat[1].y * det2_24_03 + mat[1].s * det2_24_01;
            float det3_124_014 = mat[1].x * det2_24_14 - mat[1].y * det2_24_04 + mat[1].t * det2_24_01;
            float det3_124_023 = mat[1].x * det2_24_23 - mat[1].z * det2_24_03 + mat[1].s * det2_24_02;
            float det3_124_024 = mat[1].x * det2_24_24 - mat[1].z * det2_24_04 + mat[1].t * det2_24_02;
            float det3_124_034 = mat[1].x * det2_24_34 - mat[1].s * det2_24_04 + mat[1].t * det2_24_03;
            float det3_124_123 = mat[1].y * det2_24_23 - mat[1].z * det2_24_13 + mat[1].s * det2_24_12;
            float det3_124_124 = mat[1].y * det2_24_24 - mat[1].z * det2_24_14 + mat[1].t * det2_24_12;
            float det3_124_134 = mat[1].y * det2_24_34 - mat[1].s * det2_24_14 + mat[1].t * det2_24_13;
            float det3_124_234 = mat[1].z * det2_24_34 - mat[1].s * det2_24_24 + mat[1].t * det2_24_23;
            float det3_134_012 = mat[1].x * det2_34_12 - mat[1].y * det2_34_02 + mat[1].z * det2_34_01;
            float det3_134_013 = mat[1].x * det2_34_13 - mat[1].y * det2_34_03 + mat[1].s * det2_34_01;
            float det3_134_014 = mat[1].x * det2_34_14 - mat[1].y * det2_34_04 + mat[1].t * det2_34_01;
            float det3_134_023 = mat[1].x * det2_34_23 - mat[1].z * det2_34_03 + mat[1].s * det2_34_02;
            float det3_134_024 = mat[1].x * det2_34_24 - mat[1].z * det2_34_04 + mat[1].t * det2_34_02;
            float det3_134_034 = mat[1].x * det2_34_34 - mat[1].s * det2_34_04 + mat[1].t * det2_34_03;
            float det3_134_123 = mat[1].y * det2_34_23 - mat[1].z * det2_34_13 + mat[1].s * det2_34_12;
            float det3_134_124 = mat[1].y * det2_34_24 - mat[1].z * det2_34_14 + mat[1].t * det2_34_12;
            float det3_134_134 = mat[1].y * det2_34_34 - mat[1].s * det2_34_14 + mat[1].t * det2_34_13;
            float det3_134_234 = mat[1].z * det2_34_34 - mat[1].s * det2_34_24 + mat[1].t * det2_34_23;
            // remaining 4x4 sub-determinants
            float det4_0123_0123 = mat[0].x * det3_123_123 - mat[0].y * det3_123_023 + mat[0].z * det3_123_013 - mat[0].s * det3_123_012;
            float det4_0123_0124 = mat[0].x * det3_123_124 - mat[0].y * det3_123_024 + mat[0].z * det3_123_014 - mat[0].t * det3_123_012;
            float det4_0123_0134 = mat[0].x * det3_123_134 - mat[0].y * det3_123_034 + mat[0].s * det3_123_014 - mat[0].t * det3_123_013;
            float det4_0123_0234 = mat[0].x * det3_123_234 - mat[0].z * det3_123_034 + mat[0].s * det3_123_024 - mat[0].t * det3_123_023;
            float det4_0123_1234 = mat[0].y * det3_123_234 - mat[0].z * det3_123_134 + mat[0].s * det3_123_124 - mat[0].t * det3_123_123;
            float det4_0124_0123 = mat[0].x * det3_124_123 - mat[0].y * det3_124_023 + mat[0].z * det3_124_013 - mat[0].s * det3_124_012;
            float det4_0124_0124 = mat[0].x * det3_124_124 - mat[0].y * det3_124_024 + mat[0].z * det3_124_014 - mat[0].t * det3_124_012;
            float det4_0124_0134 = mat[0].x * det3_124_134 - mat[0].y * det3_124_034 + mat[0].s * det3_124_014 - mat[0].t * det3_124_013;
            float det4_0124_0234 = mat[0].x * det3_124_234 - mat[0].z * det3_124_034 + mat[0].s * det3_124_024 - mat[0].t * det3_124_023;
            float det4_0124_1234 = mat[0].y * det3_124_234 - mat[0].z * det3_124_134 + mat[0].s * det3_124_124 - mat[0].t * det3_124_123;
            float det4_0134_0123 = mat[0].x * det3_134_123 - mat[0].y * det3_134_023 + mat[0].z * det3_134_013 - mat[0].s * det3_134_012;
            float det4_0134_0124 = mat[0].x * det3_134_124 - mat[0].y * det3_134_024 + mat[0].z * det3_134_014 - mat[0].t * det3_134_012;
            float det4_0134_0134 = mat[0].x * det3_134_134 - mat[0].y * det3_134_034 + mat[0].s * det3_134_014 - mat[0].t * det3_134_013;
            float det4_0134_0234 = mat[0].x * det3_134_234 - mat[0].z * det3_134_034 + mat[0].s * det3_134_024 - mat[0].t * det3_134_023;
            float det4_0134_1234 = mat[0].y * det3_134_234 - mat[0].z * det3_134_134 + mat[0].s * det3_134_124 - mat[0].t * det3_134_123;
            float det4_0234_0123 = mat[0].x * det3_234_123 - mat[0].y * det3_234_023 + mat[0].z * det3_234_013 - mat[0].s * det3_234_012;
            float det4_0234_0124 = mat[0].x * det3_234_124 - mat[0].y * det3_234_024 + mat[0].z * det3_234_014 - mat[0].t * det3_234_012;
            float det4_0234_0134 = mat[0].x * det3_234_134 - mat[0].y * det3_234_034 + mat[0].s * det3_234_014 - mat[0].t * det3_234_013;
            float det4_0234_0234 = mat[0].x * det3_234_234 - mat[0].z * det3_234_034 + mat[0].s * det3_234_024 - mat[0].t * det3_234_023;
            float det4_0234_1234 = mat[0].y * det3_234_234 - mat[0].z * det3_234_134 + mat[0].s * det3_234_124 - mat[0].t * det3_234_123;
            //
            mat[0].x = (float)(det4_1234_1234 * invDet);
            mat[0].y = (float)(-det4_0234_1234 * invDet);
            mat[0].z = (float)(det4_0134_1234 * invDet);
            mat[0].s = (float)(-det4_0124_1234 * invDet);
            mat[0].t = (float)(det4_0123_1234 * invDet);
            //
            mat[1].x = (float)(-det4_1234_0234 * invDet);
            mat[1].y = (float)(det4_0234_0234 * invDet);
            mat[1].z = (float)(-det4_0134_0234 * invDet);
            mat[1].s = (float)(det4_0124_0234 * invDet);
            mat[1].t = (float)(-det4_0123_0234 * invDet);
            //
            mat[2].x = (float)(det4_1234_0134 * invDet);
            mat[2].y = (float)(-det4_0234_0134 * invDet);
            mat[2].z = (float)(det4_0134_0134 * invDet);
            mat[2].s = (float)(-det4_0124_0134 * invDet);
            mat[2].t = (float)(det4_0123_0134 * invDet);
            //
            mat[3].x = (float)(-det4_1234_0124 * invDet);
            mat[3].y = (float)(det4_0234_0124 * invDet);
            mat[3].z = (float)(-det4_0134_0124 * invDet);
            mat[3].s = (float)(det4_0124_0124 * invDet);
            mat[3].t = (float)(-det4_0123_0124 * invDet);
            //
            mat[4].x = (float)(det4_1234_0123 * invDet);
            mat[4].y = (float)(-det4_0234_0123 * invDet);
            mat[4].z = (float)(det4_0134_0123 * invDet);
            mat[4].s = (float)(-det4_0124_0123 * invDet);
            mat[4].t = (float)(det4_0123_0123 * invDet);
            return true;
        }
        public idMat5 InverseFast()
        {
            idMat5 invMat = this;
            bool r = invMat.InverseFastSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseFastSelf()
        {
#if false
            // 280+5+25 = 310 multiplications
            //				1 division
            // 2x2 sub-determinants required to calculate 5x5 determinant
            float det2_34_01 = mat[3].x * mat[4].y - mat[3].y * mat[4].x;
            float det2_34_02 = mat[3].x * mat[4].z - mat[3].z * mat[4].x;
            float det2_34_03 = mat[3].x * mat[4].s - mat[3].s * mat[4].x;
            float det2_34_04 = mat[3].x * mat[4].t - mat[3].t * mat[4].x;
            float det2_34_12 = mat[3].y * mat[4].z - mat[3].z * mat[4].y;
            float det2_34_13 = mat[3].y * mat[4].s - mat[3].s * mat[4].y;
            float det2_34_14 = mat[3].y * mat[4].t - mat[3].t * mat[4].y;
            float det2_34_23 = mat[3].z * mat[4].s - mat[3].s * mat[4].z;
            float det2_34_24 = mat[3].z * mat[4].t - mat[3].t * mat[4].z;
            float det2_34_34 = mat[3].s * mat[4].t - mat[3].t * mat[4].s;
            // 3x3 sub-determinants required to calculate 5x5 determinant
            float det3_234_012 = mat[2].x * det2_34_12 - mat[2].y * det2_34_02 + mat[2].z * det2_34_01;
            float det3_234_013 = mat[2].x * det2_34_13 - mat[2].y * det2_34_03 + mat[2].s * det2_34_01;
            float det3_234_014 = mat[2].x * det2_34_14 - mat[2].y * det2_34_04 + mat[2].t * det2_34_01;
            float det3_234_023 = mat[2].x * det2_34_23 - mat[2].z * det2_34_03 + mat[2].s * det2_34_02;
            float det3_234_024 = mat[2].x * det2_34_24 - mat[2].z * det2_34_04 + mat[2].t * det2_34_02;
            float det3_234_034 = mat[2].x * det2_34_34 - mat[2].s * det2_34_04 + mat[2].t * det2_34_03;
            float det3_234_123 = mat[2].y * det2_34_23 - mat[2].z * det2_34_13 + mat[2].s * det2_34_12;
            float det3_234_124 = mat[2].y * det2_34_24 - mat[2].z * det2_34_14 + mat[2].t * det2_34_12;
            float det3_234_134 = mat[2].y * det2_34_34 - mat[2].s * det2_34_14 + mat[2].t * det2_34_13;
            float det3_234_234 = mat[2].z * det2_34_34 - mat[2].s * det2_34_24 + mat[2].t * det2_34_23;
            // 4x4 sub-determinants required to calculate 5x5 determinant
            float det4_1234_0123 = mat[1].x * det3_234_123 - mat[1].y * det3_234_023 + mat[1].z * det3_234_013 - mat[1].s * det3_234_012;
            float det4_1234_0124 = mat[1].x * det3_234_124 - mat[1].y * det3_234_024 + mat[1].z * det3_234_014 - mat[1].t * det3_234_012;
            float det4_1234_0134 = mat[1].x * det3_234_134 - mat[1].y * det3_234_034 + mat[1].s * det3_234_014 - mat[1].t * det3_234_013;
            float det4_1234_0234 = mat[1].x * det3_234_234 - mat[1].z * det3_234_034 + mat[1].s * det3_234_024 - mat[1].t * det3_234_023;
            float det4_1234_1234 = mat[1].y * det3_234_234 - mat[1].z * det3_234_134 + mat[1].s * det3_234_124 - mat[1].t * det3_234_123;
            // determinant of 5x5 matrix
            double det = mat[0].x * det4_1234_1234 - mat[0].y * det4_1234_0234 + mat[0].z * det4_1234_0134 - mat[0].s * det4_1234_0124 + mat[0].t * det4_1234_0123;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_23_01 = mat[2].x * mat[3].y - mat[2].y * mat[3].x;
            float det2_23_02 = mat[2].x * mat[3].z - mat[2].z * mat[3].x;
            float det2_23_03 = mat[2].x * mat[3].s - mat[2].s * mat[3].x;
            float det2_23_04 = mat[2].x * mat[3].t - mat[2].t * mat[3].x;
            float det2_23_12 = mat[2].y * mat[3].z - mat[2].z * mat[3].y;
            float det2_23_13 = mat[2].y * mat[3].s - mat[2].s * mat[3].y;
            float det2_23_14 = mat[2].y * mat[3].t - mat[2].t * mat[3].y;
            float det2_23_23 = mat[2].z * mat[3].s - mat[2].s * mat[3].z;
            float det2_23_24 = mat[2].z * mat[3].t - mat[2].t * mat[3].z;
            float det2_23_34 = mat[2].s * mat[3].t - mat[2].t * mat[3].s;
            float det2_24_01 = mat[2].x * mat[4].y - mat[2].y * mat[4].x;
            float det2_24_02 = mat[2].x * mat[4].z - mat[2].z * mat[4].x;
            float det2_24_03 = mat[2].x * mat[4].s - mat[2].s * mat[4].x;
            float det2_24_04 = mat[2].x * mat[4].t - mat[2].t * mat[4].x;
            float det2_24_12 = mat[2].y * mat[4].z - mat[2].z * mat[4].y;
            float det2_24_13 = mat[2].y * mat[4].s - mat[2].s * mat[4].y;
            float det2_24_14 = mat[2].y * mat[4].t - mat[2].t * mat[4].y;
            float det2_24_23 = mat[2].s * mat[4].s - mat[2].s * mat[4].z;
            float det2_24_24 = mat[2].s * mat[4].t - mat[2].t * mat[4].z;
            float det2_24_34 = mat[2].t * mat[4].t - mat[2].t * mat[4].s;
            // remaining 3x3 sub-determinants
            float det3_123_012 = mat[1].x * det2_23_12 - mat[1].y * det2_23_02 + mat[1].z * det2_23_01;
            float det3_123_013 = mat[1].x * det2_23_13 - mat[1].y * det2_23_03 + mat[1].s * det2_23_01;
            float det3_123_014 = mat[1].x * det2_23_14 - mat[1].y * det2_23_04 + mat[1].t * det2_23_01;
            float det3_123_023 = mat[1].x * det2_23_23 - mat[1].z * det2_23_03 + mat[1].s * det2_23_02;
            float det3_123_024 = mat[1].x * det2_23_24 - mat[1].z * det2_23_04 + mat[1].t * det2_23_02;
            float det3_123_034 = mat[1].x * det2_23_34 - mat[1].s * det2_23_04 + mat[1].t * det2_23_03;
            float det3_123_123 = mat[1].y * det2_23_23 - mat[1].z * det2_23_13 + mat[1].s * det2_23_12;
            float det3_123_124 = mat[1].y * det2_23_24 - mat[1].z * det2_23_14 + mat[1].t * det2_23_12;
            float det3_123_134 = mat[1].y * det2_23_34 - mat[1].s * det2_23_14 + mat[1].t * det2_23_13;
            float det3_123_234 = mat[1].z * det2_23_34 - mat[1].s * det2_23_24 + mat[1].t * det2_23_23;
            float det3_124_012 = mat[1].x * det2_24_12 - mat[1].y * det2_24_02 + mat[1].z * det2_24_01;
            float det3_124_013 = mat[1].x * det2_24_13 - mat[1].y * det2_24_03 + mat[1].s * det2_24_01;
            float det3_124_014 = mat[1].x * det2_24_14 - mat[1].y * det2_24_04 + mat[1].t * det2_24_01;
            float det3_124_023 = mat[1].x * det2_24_23 - mat[1].z * det2_24_03 + mat[1].s * det2_24_02;
            float det3_124_024 = mat[1].x * det2_24_24 - mat[1].z * det2_24_04 + mat[1].t * det2_24_02;
            float det3_124_034 = mat[1].x * det2_24_34 - mat[1].s * det2_24_04 + mat[1].t * det2_24_03;
            float det3_124_123 = mat[1].y * det2_24_23 - mat[1].z * det2_24_13 + mat[1].s * det2_24_12;
            float det3_124_124 = mat[1].y * det2_24_24 - mat[1].z * det2_24_14 + mat[1].t * det2_24_12;
            float det3_124_134 = mat[1].y * det2_24_34 - mat[1].s * det2_24_14 + mat[1].t * det2_24_13;
            float det3_124_234 = mat[1].z * det2_24_34 - mat[1].s * det2_24_24 + mat[1].t * det2_24_23;
            float det3_134_012 = mat[1].x * det2_34_12 - mat[1].y * det2_34_02 + mat[1].z * det2_34_01;
            float det3_134_013 = mat[1].x * det2_34_13 - mat[1].y * det2_34_03 + mat[1].s * det2_34_01;
            float det3_134_014 = mat[1].x * det2_34_14 - mat[1].y * det2_34_04 + mat[1].t * det2_34_01;
            float det3_134_023 = mat[1].x * det2_34_23 - mat[1].z * det2_34_03 + mat[1].s * det2_34_02;
            float det3_134_024 = mat[1].x * det2_34_24 - mat[1].z * det2_34_04 + mat[1].t * det2_34_02;
            float det3_134_034 = mat[1].x * det2_34_34 - mat[1].s * det2_34_04 + mat[1].t * det2_34_03;
            float det3_134_123 = mat[1].y * det2_34_23 - mat[1].z * det2_34_13 + mat[1].s * det2_34_12;
            float det3_134_124 = mat[1].y * det2_34_24 - mat[1].z * det2_34_14 + mat[1].t * det2_34_12;
            float det3_134_134 = mat[1].y * det2_34_34 - mat[1].s * det2_34_14 + mat[1].t * det2_34_13;
            float det3_134_234 = mat[1].z * det2_34_34 - mat[1].s * det2_34_24 + mat[1].t * det2_34_23;
            // remaining 4x4 sub-determinants
            float det4_0123_0123 = mat[0].x * det3_123_123 - mat[0].y * det3_123_023 + mat[0].z * det3_123_013 - mat[0].s * det3_123_012;
            float det4_0123_0124 = mat[0].x * det3_123_124 - mat[0].y * det3_123_024 + mat[0].z * det3_123_014 - mat[0].t * det3_123_012;
            float det4_0123_0134 = mat[0].x * det3_123_134 - mat[0].y * det3_123_034 + mat[0].s * det3_123_014 - mat[0].t * det3_123_013;
            float det4_0123_0234 = mat[0].x * det3_123_234 - mat[0].z * det3_123_034 + mat[0].s * det3_123_024 - mat[0].t * det3_123_023;
            float det4_0123_1234 = mat[0].y * det3_123_234 - mat[0].z * det3_123_134 + mat[0].s * det3_123_124 - mat[0].t * det3_123_123;
            float det4_0124_0123 = mat[0].x * det3_124_123 - mat[0].y * det3_124_023 + mat[0].z * det3_124_013 - mat[0].s * det3_124_012;
            float det4_0124_0124 = mat[0].x * det3_124_124 - mat[0].y * det3_124_024 + mat[0].z * det3_124_014 - mat[0].t * det3_124_012;
            float det4_0124_0134 = mat[0].x * det3_124_134 - mat[0].y * det3_124_034 + mat[0].s * det3_124_014 - mat[0].t * det3_124_013;
            float det4_0124_0234 = mat[0].x * det3_124_234 - mat[0].z * det3_124_034 + mat[0].s * det3_124_024 - mat[0].t * det3_124_023;
            float det4_0124_1234 = mat[0].y * det3_124_234 - mat[0].z * det3_124_134 + mat[0].s * det3_124_124 - mat[0].t * det3_124_123;
            float det4_0134_0123 = mat[0].x * det3_134_123 - mat[0].y * det3_134_023 + mat[0].z * det3_134_013 - mat[0].s * det3_134_012;
            float det4_0134_0124 = mat[0].x * det3_134_124 - mat[0].y * det3_134_024 + mat[0].z * det3_134_014 - mat[0].t * det3_134_012;
            float det4_0134_0134 = mat[0].x * det3_134_134 - mat[0].y * det3_134_034 + mat[0].s * det3_134_014 - mat[0].t * det3_134_013;
            float det4_0134_0234 = mat[0].x * det3_134_234 - mat[0].z * det3_134_034 + mat[0].s * det3_134_024 - mat[0].t * det3_134_023;
            float det4_0134_1234 = mat[0].y * det3_134_234 - mat[0].z * det3_134_134 + mat[0].s * det3_134_124 - mat[0].t * det3_134_123;
            float det4_0234_0123 = mat[0].x * det3_234_123 - mat[0].y * det3_234_023 + mat[0].z * det3_234_013 - mat[0].s * det3_234_012;
            float det4_0234_0124 = mat[0].x * det3_234_124 - mat[0].y * det3_234_024 + mat[0].z * det3_234_014 - mat[0].t * det3_234_012;
            float det4_0234_0134 = mat[0].x * det3_234_134 - mat[0].y * det3_234_034 + mat[0].s * det3_234_014 - mat[0].t * det3_234_013;
            float det4_0234_0234 = mat[0].x * det3_234_234 - mat[0].z * det3_234_034 + mat[0].s * det3_234_024 - mat[0].t * det3_234_023;
            float det4_0234_1234 = mat[0].y * det3_234_234 - mat[0].z * det3_234_134 + mat[0].s * det3_234_124 - mat[0].t * det3_234_123;
            //
            mat[0].x = (float)(det4_1234_1234 * invDet);
            mat[0].y = (float)(-det4_0234_1234 * invDet);
            mat[0].z = (float)(det4_0134_1234 * invDet);
            mat[0].s = (float)(-det4_0124_1234 * invDet);
            mat[0].t = (float)(det4_0123_1234 * invDet);
            //
            mat[1].x = (float)(-det4_1234_0234 * invDet);
            mat[1].y = (float)(det4_0234_0234 * invDet);
            mat[1].z = (float)(-det4_0134_0234 * invDet);
            mat[1].s = (float)(det4_0124_0234 * invDet);
            mat[1].t = (float)(-det4_0123_0234 * invDet);
            //
            mat[2].x = (float)(det4_1234_0134 * invDet);
            mat[2].y = (float)(-det4_0234_0134 * invDet);
            mat[2].z = (float)(det4_0134_0134 * invDet);
            mat[2].s = (float)(-det4_0124_0134 * invDet);
            mat[2].t = (float)(det4_0123_0134 * invDet);
            //
            mat[3].x = (float)(-det4_1234_0124 * invDet);
            mat[3].y = (float)(det4_0234_0124 * invDet);
            mat[3].z = (float)(-det4_0134_0124 * invDet);
            mat[3].s = (float)(det4_0124_0124 * invDet);
            mat[3].t = (float)(-det4_0123_0124 * invDet);
            //
            mat[4].x = (float)(det4_1234_0123 * invDet);
            mat[4].y = (float)(-det4_0234_0123 * invDet);
            mat[4].z = (float)(det4_0134_0123 * invDet);
            mat[4].s = (float)(-det4_0124_0123 * invDet);
            mat[4].t = (float)(det4_0123_0123 * invDet);
            return true;
#elif false
	// 5*28 = 140 multiplications
	//			5 divisions
	float *mat = reinterpret_cast<float *>(this);
	float s;
	double d, di;

	di = mat[0];
	s = di;
	mat[0] = d = 1.0f / di;
	mat[1] *= d;
	mat[2] *= d;
	mat[3] *= d;
	mat[4] *= d;
	d = -d;
	mat[5] *= d;
	mat[10] *= d;
	mat[15] *= d;
	mat[20] *= d;
	d = mat[5] * di;
	mat[6] += mat[1] * d;
	mat[7] += mat[2] * d;
	mat[8] += mat[3] * d;
	mat[9] += mat[4] * d;
	d = mat[10] * di;
	mat[11] += mat[1] * d;
	mat[12] += mat[2] * d;
	mat[13] += mat[3] * d;
	mat[14] += mat[4] * d;
	d = mat[15] * di;
	mat[16] += mat[1] * d;
	mat[17] += mat[2] * d;
	mat[18] += mat[3] * d;
	mat[19] += mat[4] * d;
	d = mat[20] * di;
	mat[21] += mat[1] * d;
	mat[22] += mat[2] * d;
	mat[23] += mat[3] * d;
	mat[24] += mat[4] * d;
	di = mat[6];
	s *= di;
	mat[6] = d = 1.0f / di;
	mat[5] *= d;
	mat[7] *= d;
	mat[8] *= d;
	mat[9] *= d;
	d = -d;
	mat[1] *= d;
	mat[11] *= d;
	mat[16] *= d;
	mat[21] *= d;
	d = mat[1] * di;
	mat[0] += mat[5] * d;
	mat[2] += mat[7] * d;
	mat[3] += mat[8] * d;
	mat[4] += mat[9] * d;
	d = mat[11] * di;
	mat[10] += mat[5] * d;
	mat[12] += mat[7] * d;
	mat[13] += mat[8] * d;
	mat[14] += mat[9] * d;
	d = mat[16] * di;
	mat[15] += mat[5] * d;
	mat[17] += mat[7] * d;
	mat[18] += mat[8] * d;
	mat[19] += mat[9] * d;
	d = mat[21] * di;
	mat[20] += mat[5] * d;
	mat[22] += mat[7] * d;
	mat[23] += mat[8] * d;
	mat[24] += mat[9] * d;
	di = mat[12];
	s *= di;
	mat[12] = d = 1.0f / di;
	mat[10] *= d;
	mat[11] *= d;
	mat[13] *= d;
	mat[14] *= d;
	d = -d;
	mat[2] *= d;
	mat[7] *= d;
	mat[17] *= d;
	mat[22] *= d;
	d = mat[2] * di;
	mat[0] += mat[10] * d;
	mat[1] += mat[11] * d;
	mat[3] += mat[13] * d;
	mat[4] += mat[14] * d;
	d = mat[7] * di;
	mat[5] += mat[10] * d;
	mat[6] += mat[11] * d;
	mat[8] += mat[13] * d;
	mat[9] += mat[14] * d;
	d = mat[17] * di;
	mat[15] += mat[10] * d;
	mat[16] += mat[11] * d;
	mat[18] += mat[13] * d;
	mat[19] += mat[14] * d;
	d = mat[22] * di;
	mat[20] += mat[10] * d;
	mat[21] += mat[11] * d;
	mat[23] += mat[13] * d;
	mat[24] += mat[14] * d;
	di = mat[18];
	s *= di;
	mat[18] = d = 1.0f / di;
	mat[15] *= d;
	mat[16] *= d;
	mat[17] *= d;
	mat[19] *= d;
	d = -d;
	mat[3] *= d;
	mat[8] *= d;
	mat[13] *= d;
	mat[23] *= d;
	d = mat[3] * di;
	mat[0] += mat[15] * d;
	mat[1] += mat[16] * d;
	mat[2] += mat[17] * d;
	mat[4] += mat[19] * d;
	d = mat[8] * di;
	mat[5] += mat[15] * d;
	mat[6] += mat[16] * d;
	mat[7] += mat[17] * d;
	mat[9] += mat[19] * d;
	d = mat[13] * di;
	mat[10] += mat[15] * d;
	mat[11] += mat[16] * d;
	mat[12] += mat[17] * d;
	mat[14] += mat[19] * d;
	d = mat[23] * di;
	mat[20] += mat[15] * d;
	mat[21] += mat[16] * d;
	mat[22] += mat[17] * d;
	mat[24] += mat[19] * d;
	di = mat[24];
	s *= di;
	mat[24] = d = 1.0f / di;
	mat[20] *= d;
	mat[21] *= d;
	mat[22] *= d;
	mat[23] *= d;
	d = -d;
	mat[4] *= d;
	mat[9] *= d;
	mat[14] *= d;
	mat[19] *= d;
	d = mat[4] * di;
	mat[0] += mat[20] * d;
	mat[1] += mat[21] * d;
	mat[2] += mat[22] * d;
	mat[3] += mat[23] * d;
	d = mat[9] * di;
	mat[5] += mat[20] * d;
	mat[6] += mat[21] * d;
	mat[7] += mat[22] * d;
	mat[8] += mat[23] * d;
	d = mat[14] * di;
	mat[10] += mat[20] * d;
	mat[11] += mat[21] * d;
	mat[12] += mat[22] * d;
	mat[13] += mat[23] * d;
	d = mat[19] * di;
	mat[15] += mat[20] * d;
	mat[16] += mat[21] * d;
	mat[17] += mat[22] * d;
	mat[18] += mat[23] * d;

	return ( s != 0.0f && !FLOAT_IS_NAN( s ) );
#else
            // 86+30+6 = 122 multiplications
            //	  2*1  =   2 divisions
            float[] t_mat = this;
            // r0 = m0.Inverse();	// 3x3
            float c0 = t_mat[1 * 5 + 1] * t_mat[2 * 5 + 2] - t_mat[1 * 5 + 2] * t_mat[2 * 5 + 1];
            float c1 = t_mat[1 * 5 + 2] * t_mat[2 * 5 + 0] - t_mat[1 * 5 + 0] * t_mat[2 * 5 + 2];
            float c2 = t_mat[1 * 5 + 0] * t_mat[2 * 5 + 1] - t_mat[1 * 5 + 1] * t_mat[2 * 5 + 0];
            float det = t_mat[0 * 5 + 0] * c0 + t_mat[0 * 5 + 1] * c1 + t_mat[0 * 5 + 2] * c2;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            float invDet = 1.0f / det;
            idMat3 r0 = new idMat3(); idVec3[] r0_mat = r0.mat;
            r0_mat[0].x = c0 * invDet;
            r0_mat[0].y = (t_mat[0 * 5 + 2] * t_mat[2 * 5 + 1] - t_mat[0 * 5 + 1] * t_mat[2 * 5 + 2]) * invDet;
            r0_mat[0].z = (t_mat[0 * 5 + 1] * t_mat[1 * 5 + 2] - t_mat[0 * 5 + 2] * t_mat[1 * 5 + 1]) * invDet;
            r0_mat[1].x = c1 * invDet;
            r0_mat[1].y = (t_mat[0 * 5 + 0] * t_mat[2 * 5 + 2] - t_mat[0 * 5 + 2] * t_mat[2 * 5 + 0]) * invDet;
            r0_mat[1].z = (t_mat[0 * 5 + 2] * t_mat[1 * 5 + 0] - t_mat[0 * 5 + 0] * t_mat[1 * 5 + 2]) * invDet;
            r0_mat[2].x = c2 * invDet;
            r0_mat[2].y = (t_mat[0 * 5 + 1] * t_mat[2 * 5 + 0] - t_mat[0 * 5 + 0] * t_mat[2 * 5 + 1]) * invDet;
            r0_mat[2].z = (t_mat[0 * 5 + 0] * t_mat[1 * 5 + 1] - t_mat[0 * 5 + 1] * t_mat[1 * 5 + 0]) * invDet;
            // r1 = r0 * m1;		// 3x2 = 3x3 * 3x2
            idMat3 r1 = new idMat3(); idVec3[] r1_mat = r1.mat;
            r1_mat[0].x = r0_mat[0].x * t_mat[0 * 5 + 3] + r0_mat[0].y * t_mat[1 * 5 + 3] + r0_mat[0].z * t_mat[2 * 5 + 3];
            r1_mat[0].y = r0_mat[0].x * t_mat[0 * 5 + 4] + r0_mat[0].y * t_mat[1 * 5 + 4] + r0_mat[0].z * t_mat[2 * 5 + 4];
            r1_mat[1].x = r0_mat[1].x * t_mat[0 * 5 + 3] + r0_mat[1].y * t_mat[1 * 5 + 3] + r0_mat[1].z * t_mat[2 * 5 + 3];
            r1_mat[1].y = r0_mat[1].x * t_mat[0 * 5 + 4] + r0_mat[1].y * t_mat[1 * 5 + 4] + r0_mat[1].z * t_mat[2 * 5 + 4];
            r1_mat[2].x = r0_mat[2].x * t_mat[0 * 5 + 3] + r0_mat[2].y * t_mat[1 * 5 + 3] + r0_mat[2].z * t_mat[2 * 5 + 3];
            r1_mat[2].y = r0_mat[2].x * t_mat[0 * 5 + 4] + r0_mat[2].y * t_mat[1 * 5 + 4] + r0_mat[2].z * t_mat[2 * 5 + 4];
            // r2 = m2 * r1;		// 2x2 = 2x3 * 3x2
            idMat3 r2 = new idMat3(); idVec3[] r2_mat = r2.mat;
            r2_mat[0].x = t_mat[3 * 5 + 0] * r1_mat[0].x + t_mat[3 * 5 + 1] * r1_mat[1].x + t_mat[3 * 5 + 2] * r1_mat[2].x;
            r2_mat[0].y = t_mat[3 * 5 + 0] * r1_mat[0].y + t_mat[3 * 5 + 1] * r1_mat[1].y + t_mat[3 * 5 + 2] * r1_mat[2].y;
            r2_mat[1].x = t_mat[4 * 5 + 0] * r1_mat[0].x + t_mat[4 * 5 + 1] * r1_mat[1].x + t_mat[4 * 5 + 2] * r1_mat[2].x;
            r2_mat[1].y = t_mat[4 * 5 + 0] * r1_mat[0].y + t_mat[4 * 5 + 1] * r1_mat[1].y + t_mat[4 * 5 + 2] * r1_mat[2].y;
            // r3 = r2 - m3;		// 2x2 = 2x2 - 2x2
            idMat3 r3 = new idMat3(); idVec3[] r3_mat = r3.mat;
            r3_mat[0].x = r2_mat[0].x - t_mat[3 * 5 + 3];
            r3_mat[0].y = r2_mat[0].y - t_mat[3 * 5 + 4];
            r3_mat[1].x = r2_mat[1].x - t_mat[4 * 5 + 3];
            r3_mat[1].y = r2_mat[1].y - t_mat[4 * 5 + 4];
            // r3.InverseSelf();	// 2x2
            det = r3_mat[0].x * r3_mat[1].y - r3_mat[0].y * r3_mat[1].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            invDet = 1.0f / det;
            c0 = r3_mat[0].x;
            r3_mat[0].x = r3_mat[1].y * invDet;
            r3_mat[0].y = -r3_mat[0].y * invDet;
            r3_mat[1].x = -r3_mat[1].x * invDet;
            r3_mat[1].y = c0 * invDet;
            // r2 = m2 * r0;		// 2x3 = 2x3 * 3x3
            r2_mat[0].x = t_mat[3 * 5 + 0] * r0_mat[0].x + t_mat[3 * 5 + 1] * r0_mat[1].x + t_mat[3 * 5 + 2] * r0_mat[2].x;
            r2_mat[0].y = t_mat[3 * 5 + 0] * r0_mat[0].y + t_mat[3 * 5 + 1] * r0_mat[1].y + t_mat[3 * 5 + 2] * r0_mat[2].y;
            r2_mat[0].z = t_mat[3 * 5 + 0] * r0_mat[0].z + t_mat[3 * 5 + 1] * r0_mat[1].z + t_mat[3 * 5 + 2] * r0_mat[2].z;
            r2_mat[1].x = t_mat[4 * 5 + 0] * r0_mat[0].x + t_mat[4 * 5 + 1] * r0_mat[1].x + t_mat[4 * 5 + 2] * r0_mat[2].x;
            r2_mat[1].y = t_mat[4 * 5 + 0] * r0_mat[0].y + t_mat[4 * 5 + 1] * r0_mat[1].y + t_mat[4 * 5 + 2] * r0_mat[2].y;
            r2_mat[1].z = t_mat[4 * 5 + 0] * r0_mat[0].z + t_mat[4 * 5 + 1] * r0_mat[1].z + t_mat[4 * 5 + 2] * r0_mat[2].z;
            // m2 = r3 * r2;		// 2x3 = 2x2 * 2x3
            t_mat[3 * 5 + 0] = r3_mat[0].x * r2_mat[0].x + r3_mat[0].y * r2_mat[1].x;
            t_mat[3 * 5 + 1] = r3_mat[0].x * r2_mat[0].y + r3_mat[0].y * r2_mat[1].y;
            t_mat[3 * 5 + 2] = r3_mat[0].x * r2_mat[0].z + r3_mat[0].y * r2_mat[1].z;
            t_mat[4 * 5 + 0] = r3_mat[1].x * r2_mat[0].x + r3_mat[1].y * r2_mat[1].x;
            t_mat[4 * 5 + 1] = r3_mat[1].x * r2_mat[0].y + r3_mat[1].y * r2_mat[1].y;
            t_mat[4 * 5 + 2] = r3_mat[1].x * r2_mat[0].z + r3_mat[1].y * r2_mat[1].z;
            // m0 = r0 - r1 * m2;	// 3x3 = 3x3 - 3x2 * 2x3
            t_mat[0 * 5 + 0] = r0_mat[0].x - r1_mat[0].x * t_mat[3 * 5 + 0] - r1_mat[0].y * t_mat[4 * 5 + 0];
            t_mat[0 * 5 + 1] = r0_mat[0].y - r1_mat[0].x * t_mat[3 * 5 + 1] - r1_mat[0].y * t_mat[4 * 5 + 1];
            t_mat[0 * 5 + 2] = r0_mat[0].z - r1_mat[0].x * t_mat[3 * 5 + 2] - r1_mat[0].y * t_mat[4 * 5 + 2];
            t_mat[1 * 5 + 0] = r0_mat[1].x - r1_mat[1].x * t_mat[3 * 5 + 0] - r1_mat[1].y * t_mat[4 * 5 + 0];
            t_mat[1 * 5 + 1] = r0_mat[1].y - r1_mat[1].x * t_mat[3 * 5 + 1] - r1_mat[1].y * t_mat[4 * 5 + 1];
            t_mat[1 * 5 + 2] = r0_mat[1].z - r1_mat[1].x * t_mat[3 * 5 + 2] - r1_mat[1].y * t_mat[4 * 5 + 2];
            t_mat[2 * 5 + 0] = r0_mat[2].x - r1_mat[2].x * t_mat[3 * 5 + 0] - r1_mat[2].y * t_mat[4 * 5 + 0];
            t_mat[2 * 5 + 1] = r0_mat[2].y - r1_mat[2].x * t_mat[3 * 5 + 1] - r1_mat[2].y * t_mat[4 * 5 + 1];
            t_mat[2 * 5 + 2] = r0_mat[2].z - r1_mat[2].x * t_mat[3 * 5 + 2] - r1_mat[2].y * t_mat[4 * 5 + 2];
            // m1 = r1 * r3;		// 3x2 = 3x2 * 2x2
            t_mat[0 * 5 + 3] = r1_mat[0].x * r3_mat[0].x + r1_mat[0].y * r3_mat[1].x;
            t_mat[0 * 5 + 4] = r1_mat[0].x * r3_mat[0].y + r1_mat[0].y * r3_mat[1].y;
            t_mat[1 * 5 + 3] = r1_mat[1].x * r3_mat[0].x + r1_mat[1].y * r3_mat[1].x;
            t_mat[1 * 5 + 4] = r1_mat[1].x * r3_mat[0].y + r1_mat[1].y * r3_mat[1].y;
            t_mat[2 * 5 + 3] = r1_mat[2].x * r3_mat[0].x + r1_mat[2].y * r3_mat[1].x;
            t_mat[2 * 5 + 4] = r1_mat[2].x * r3_mat[0].y + r1_mat[2].y * r3_mat[1].y;
            // m3 = -r3;			// 2x2 = - 2x2
            t_mat[3 * 5 + 3] = -r3_mat[0].x;
            t_mat[3 * 5 + 4] = -r3_mat[0].y;
            t_mat[4 * 5 + 3] = -r3_mat[1].x;
            t_mat[4 * 5 + 4] = -r3_mat[1].y;
            return true;
#endif
        }
        #endregion
    }
}



