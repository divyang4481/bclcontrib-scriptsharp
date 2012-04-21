using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idMat4
    {
        public readonly static idMat4 zero = new idMat4(
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f,
            0f, 0f, 0f, 0f);
        public readonly static idMat4 identity = new idMat4(
            1f, 0f, 0f, 0f,
            0f, 1f, 0f, 0f,
            0f, 0f, 1f, 0f,
            0f, 0f, 0f, 1f);
        idVec4[] mat = new idVec4[4];

        public idMat4() { }
        public idMat4(ref idVec4 x, ref idVec4 y, ref idVec4 z, ref idVec4 w)
        {
            mat[0] = x;
            mat[1] = y;
            mat[2] = z;
            mat[3] = w;
        }
        public idMat4(
            float xx, float xy, float xz, float xw,
            float yx, float yy, float yz, float yw,
            float zx, float zy, float zz, float zw,
            float wx, float wy, float wz, float ww)
        {
            mat[0].x = xx; mat[0].y = xy; mat[0].z = xz; mat[0].w = xw;
            mat[1].x = yx; mat[1].y = yy; mat[1].z = yz; mat[1].w = yw;
            mat[2].x = zx; mat[2].y = zy; mat[2].z = zz; mat[2].w = zw;
            mat[3].x = wx; mat[3].y = wy; mat[3].z = wz; mat[3].w = ww;

        }
        public idMat4(ref idMat3 rotation, ref idVec3 translation)
        {
            idVec3[] rotation_mat = rotation.mat;
            // NOTE: idMat3 is transposed because it is column-major
            mat[0].x = rotation_mat[0].x;
            mat[0].y = rotation_mat[1].x;
            mat[0].z = rotation_mat[2].x;
            mat[0].w = translation.x;
            mat[1].x = rotation_mat[0].y;
            mat[1].y = rotation_mat[1].y;
            mat[1].z = rotation_mat[2].y;
            mat[1].w = translation.y;
            mat[2].x = rotation_mat[0].z;
            mat[2].y = rotation_mat[1].z;
            mat[2].z = rotation_mat[2].z;
            mat[2].w = translation.z;
            mat[3].x = 0.0f;
            mat[3].y = 0.0f;
            mat[3].z = 0.0f;
            mat[3].w = 1.0f;
        }
        public idMat4(float[][] src) { Array.Copy(mat, src, 4 * 4); }
        public void Zero()
        {
            Array.Clear(mat, 0, 4 * 4);
        }
        public int GetDimension() { return 16; }
        public float[] ToArray() { return mat[0].ToArray(); }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public idVec4 this[int index] { get { return mat[index]; } }
        public static idMat4 operator *(idMat4 a, float b)
        {
            idVec4[] a_mat = a.mat;
            return new idMat4(
                a_mat[0].x * b, a_mat[0].y * b, a_mat[0].z * b, a_mat[0].w * b,
                a_mat[1].x * b, a_mat[1].y * b, a_mat[1].z * b, a_mat[1].w * b,
                a_mat[2].x * b, a_mat[2].y * b, a_mat[2].z * b, a_mat[2].w * b,
                a_mat[3].x * b, a_mat[3].y * b, a_mat[3].z * b, a_mat[3].w * b);
        }
        public static idVec4 operator *(idMat4 a, idVec4 vec)
        {
            idVec4[] a_mat = a.mat;
            return new idVec4(
                a_mat[0].x * vec.x + a_mat[0].y * vec.y + a_mat[0].z * vec.z + a_mat[0].x * vec.w,
                a_mat[1].x * vec.x + a_mat[1].y * vec.y + a_mat[1].z * vec.z + a_mat[1].x * vec.w,
                a_mat[2].x * vec.x + a_mat[2].y * vec.y + a_mat[2].z * vec.z + a_mat[2].x * vec.w,
                a_mat[3].x * vec.x + a_mat[3].y * vec.y + a_mat[3].z * vec.z + a_mat[3].x * vec.w);
        }
        public static idVec3 operator *(idMat4 a, idVec3 vec)
        {
            idVec4[] a_mat = a.mat;
            float s = a_mat[3].x * vec.x + a_mat[3].y * vec.y + a_mat[3].z * vec.z + a_mat[3].w;
            if (s == 0.0f) new idVec3(0.0f, 0.0f, 0.0f);
            if (s == 1.0f)
                return new idVec3(
                    a_mat[0].x * vec.x + a_mat[0].y * vec.y + a_mat[0].z * vec.z + a_mat[0].w,
                    a_mat[1].x * vec.x + a_mat[1].y * vec.y + a_mat[1].z * vec.z + a_mat[1].w,
                    a_mat[2].x * vec.x + a_mat[2].y * vec.y + a_mat[2].z * vec.z + a_mat[2].w);
            float invS = 1.0f / s;
            return new idVec3(
                (a_mat[0].x * vec.x + a_mat[0].y * vec.y + a_mat[0].z * vec.z + a_mat[0].w) * invS,
                (a_mat[1].x * vec.x + a_mat[1].y * vec.y + a_mat[1].z * vec.z + a_mat[1].w) * invS,
                (a_mat[2].x * vec.x + a_mat[2].y * vec.y + a_mat[2].z * vec.z + a_mat[2].w) * invS);
        }
        public static idMat4 operator *(idMat4 a, idMat4 b)
        {
            //    m1Ptr = reinterpret_cast<const float *>(this);
            //    m2Ptr = reinterpret_cast<const float *>(&a);
            //    dstPtr = reinterpret_cast<float *>(&dst);
            idMat4 dst = new idMat4();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //            *dstPtr = m1Ptr[0] * m2Ptr[ 0 * 4 + j ]
                    //                    + m1Ptr[1] * m2Ptr[ 1 * 4 + j ]
                    //                    + m1Ptr[2] * m2Ptr[ 2 * 4 + j ]
                    //                    + m1Ptr[3] * m2Ptr[ 3 * 4 + j ];
                    //            dstPtr++;
                }
                //        m1Ptr += 4;
            }
            return dst;
        }
        public static idMat4 operator +(idMat4 a, idMat4 b)
        {
            idVec4[] a_mat = a.mat;
            idVec4[] b_mat = b.mat;
            return new idMat4(
                a_mat[0].x + b_mat[0].x, a_mat[0].y + b_mat[0].y, a_mat[0].z + b_mat[0].z, a_mat[0].w + b_mat[0].w,
                a_mat[1].x + b_mat[1].x, a_mat[1].y + b_mat[1].y, a_mat[1].z + b_mat[1].z, a_mat[1].w + b_mat[1].w,
                a_mat[2].x + b_mat[2].x, a_mat[2].y + b_mat[2].y, a_mat[2].z + b_mat[2].z, a_mat[2].w + b_mat[2].w,
                a_mat[3].x + b_mat[3].x, a_mat[3].y + b_mat[3].y, a_mat[3].z + b_mat[3].z, a_mat[3].w + b_mat[3].w);
        }
        public static idMat4 operator -(idMat4 a, idMat4 b)
        {
            idVec4[] a_mat = a.mat;
            idVec4[] b_mat = b.mat;
            return new idMat4(
                a_mat[0].x - b_mat[0].x, a_mat[0].y - b_mat[0].y, a_mat[0].z - b_mat[0].z, a_mat[0].w - b_mat[0].w,
                a_mat[1].x - b_mat[1].x, a_mat[1].y - b_mat[1].y, a_mat[1].z - b_mat[1].z, a_mat[1].w - b_mat[1].w,
                a_mat[2].x - b_mat[2].x, a_mat[2].y - b_mat[2].y, a_mat[2].z - b_mat[2].z, a_mat[2].w - b_mat[2].w,
                a_mat[3].x - b_mat[3].x, a_mat[3].y - b_mat[3].y, a_mat[3].z - b_mat[3].z, a_mat[3].w - b_mat[3].w);
        }
        public idMat4 opMul(float a)
        {
            mat[0].x *= a; mat[0].y *= a; mat[0].z *= a; mat[0].w *= a;
            mat[1].x *= a; mat[1].y *= a; mat[1].z *= a; mat[1].w *= a;
            mat[2].x *= a; mat[2].y *= a; mat[2].z *= a; mat[2].w *= a;
            mat[3].x *= a; mat[3].y *= a; mat[3].z *= a; mat[3].w *= a;
            return this;
        }
        public idMat4 opMul(ref idMat4 a)
        {
            this = this * a;
            return this;
        }
        public idMat4 opAdd(ref idMat4 a)
        {
            idVec4[] a_mat = a.mat;
            mat[0].x += a_mat[0].x; mat[0].y += a_mat[0].y; mat[0].z += a_mat[0].z; mat[0].w += a_mat[0].w;
            mat[1].x += a_mat[1].x; mat[1].y += a_mat[1].y; mat[1].z += a_mat[1].z; mat[1].w += a_mat[1].w;
            mat[2].x += a_mat[2].x; mat[2].y += a_mat[2].y; mat[2].z += a_mat[2].z; mat[2].w += a_mat[2].w;
            mat[3].x += a_mat[3].x; mat[3].y += a_mat[3].y; mat[3].z += a_mat[3].z; mat[3].w += a_mat[3].w;
            return this;
        }
        public idMat4 opSub(ref idMat4 a)
        {
            idVec4[] a_mat = a.mat;
            mat[0].x -= a_mat[0].x; mat[0].y -= a_mat[0].y; mat[0].z -= a_mat[0].z; mat[0].w -= a_mat[0].w;
            mat[1].x -= a_mat[1].x; mat[1].y -= a_mat[1].y; mat[1].z -= a_mat[1].z; mat[1].w -= a_mat[1].w;
            mat[2].x -= a_mat[2].x; mat[2].y -= a_mat[2].y; mat[2].z -= a_mat[2].z; mat[2].w -= a_mat[2].w;
            mat[3].x -= a_mat[3].x; mat[3].y -= a_mat[3].y; mat[3].z -= a_mat[3].z; mat[3].w -= a_mat[3].w;
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idMat4 a)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 4 * 4; i++)
                if (ptr1[i] != ptr2[i])
                    return false;
            return true;
        }
        public bool Compare(ref idMat4 a, float epsilon)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 4 * 4; i++)
                if (idMath.Fabs(ptr1[i] - ptr2[i]) > epsilon)
                    return false;
            return true;
        }
        public static bool operator ==(idMat4 a, idMat4 b) { return a.Compare(ref b); }
        public static bool operator !=(idMat4 a, idMat4 b) { return !a.Compare(ref b); }
        #endregion

        #region Identity
        public void Identity() { this = identity; }
        public bool IsIdentity(float epsilon) { return Compare(ref identity, epsilon); }
        public bool IsSymmetric(float epsilon)
        {
            for (int i = 1; i < 4; i++)
                for (int j = 0; j < i; j++)
                    if (idMath.Fabs(mat[i][j] - mat[j][i]) > epsilon)
                        return false;
            return true;
        }
        public bool IsDiagonal(float epsilon)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (i != j && idMath.Fabs(mat[i][j]) > epsilon)
                        return false;
            return true;
        }
        public bool IsRotated()
        {
            return !(
                mat[0].y != 0f && mat[0].z != 0f &&
                mat[1].x != 0f && mat[1].z != 0f &&
                mat[2].x != 0f && mat[2].y != 0f);
        }
        #endregion

        #region Project
        public void ProjectVector(ref idVec4 src, ref idVec4 dst)
        {
            dst.x = src * mat[0];
            dst.y = src * mat[1];
            dst.z = src * mat[2];
            dst.w = src * mat[3];
        }
        public void UnprojectVector(ref idVec4 src, ref idVec4 dst)
        {
            dst = mat[0] * src.x + mat[1] * src.y + mat[2] * src.z + mat[3] * src.w;
        }
        #endregion

        #region Transform
        public float Trace() { return (mat[0].x + mat[1].y + mat[2].z + mat[3].w); }
        public float Determinant()
        {
            // 2x2 sub-determinants
            float det2_01_01 = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            float det2_01_02 = mat[0].x * mat[1].z - mat[0].z * mat[1].x;
            float det2_01_03 = mat[0].x * mat[1].w - mat[0].w * mat[1].x;
            float det2_01_12 = mat[0].y * mat[1].z - mat[0].z * mat[1].y;
            float det2_01_13 = mat[0].y * mat[1].w - mat[0].w * mat[1].y;
            float det2_01_23 = mat[0].z * mat[1].w - mat[0].w * mat[1].z;
            // 3x3 sub-determinants
            float det3_201_012 = mat[2].x * det2_01_12 - mat[2].y * det2_01_02 + mat[2].z * det2_01_01;
            float det3_201_013 = mat[2].x * det2_01_13 - mat[2].y * det2_01_03 + mat[2].w * det2_01_01;
            float det3_201_023 = mat[2].x * det2_01_23 - mat[2].z * det2_01_03 + mat[2].w * det2_01_02;
            float det3_201_123 = mat[2].y * det2_01_23 - mat[2].z * det2_01_13 + mat[2].w * det2_01_12;
            return (-det3_201_123 * mat[3].x + det3_201_023 * mat[3].y - det3_201_013 * mat[3].z + det3_201_012 * mat[3].w);
        }
        public idMat4 Transpose()
        {
            idMat4 transpose = new idMat4();
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    transpose[i][j] = mat[j][i];
            return transpose;
        }
        public idMat4 TransposeSelf()
        {
            for (int i = 0; i < 4; i++)
                for (int j = i + 1; j < 4; j++)
                {
                    float temp = mat[i][j];
                    mat[i][j] = mat[j][i];
                    mat[j][i] = temp;
                }
            return this;
        }
        public idMat4 Inverse()
        {
            idMat4 invMat = this;
            bool r = invMat.InverseSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseSelf()
        {
            // 84+4+16 = 104 multiplications
            //			   1 division
            // 2x2 sub-determinants required to calculate 4x4 determinant
            float det2_01_01 = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            float det2_01_02 = mat[0].x * mat[1].z - mat[0].z * mat[1].x;
            float det2_01_03 = mat[0].x * mat[1].w - mat[0].w * mat[1].x;
            float det2_01_12 = mat[0].y * mat[1].z - mat[0].z * mat[1].y;
            float det2_01_13 = mat[0].y * mat[1].w - mat[0].w * mat[1].y;
            float det2_01_23 = mat[0].z * mat[1].w - mat[0].w * mat[1].z;
            // 3x3 sub-determinants required to calculate 4x4 determinant
            float det3_201_012 = mat[2].x * det2_01_12 - mat[2].y * det2_01_02 + mat[2].z * det2_01_01;
            float det3_201_013 = mat[2].x * det2_01_13 - mat[2].y * det2_01_03 + mat[2].w * det2_01_01;
            float det3_201_023 = mat[2].x * det2_01_23 - mat[2].z * det2_01_03 + mat[2].w * det2_01_02;
            float det3_201_123 = mat[2].y * det2_01_23 - mat[2].z * det2_01_13 + mat[2].w * det2_01_12;
            double det = (-det3_201_123 * mat[3].x + det3_201_023 * mat[3].y - det3_201_013 * mat[3].z + det3_201_012 * mat[3].w);
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_03_01 = mat[0].x * mat[3].y - mat[0].y * mat[3].x;
            float det2_03_02 = mat[0].x * mat[3].z - mat[0].z * mat[3].x;
            float det2_03_03 = mat[0].x * mat[3].w - mat[0].w * mat[3].x;
            float det2_03_12 = mat[0].y * mat[3].z - mat[0].z * mat[3].y;
            float det2_03_13 = mat[0].y * mat[3].w - mat[0].w * mat[3].y;
            float det2_03_23 = mat[0].z * mat[3].w - mat[0].w * mat[3].z;
            //
            float det2_13_01 = mat[1].x * mat[3].y - mat[1].y * mat[3].x;
            float det2_13_02 = mat[1].x * mat[3].z - mat[1].z * mat[3].x;
            float det2_13_03 = mat[1].x * mat[3].w - mat[1].w * mat[3].x;
            float det2_13_12 = mat[1].y * mat[3].z - mat[1].z * mat[3].y;
            float det2_13_13 = mat[1].y * mat[3].w - mat[1].w * mat[3].y;
            float det2_13_23 = mat[1].z * mat[3].w - mat[1].w * mat[3].z;
            // remaining 3x3 sub-determinants
            float det3_203_012 = mat[2].x * det2_03_12 - mat[2].y * det2_03_02 + mat[2].z * det2_03_01;
            float det3_203_013 = mat[2].x * det2_03_13 - mat[2].y * det2_03_03 + mat[2].w * det2_03_01;
            float det3_203_023 = mat[2].x * det2_03_23 - mat[2].z * det2_03_03 + mat[2].w * det2_03_02;
            float det3_203_123 = mat[2].y * det2_03_23 - mat[2].z * det2_03_13 + mat[2].w * det2_03_12;
            //
            float det3_213_012 = mat[2].x * det2_13_12 - mat[2].y * det2_13_02 + mat[2].z * det2_13_01;
            float det3_213_013 = mat[2].x * det2_13_13 - mat[2].y * det2_13_03 + mat[2].w * det2_13_01;
            float det3_213_023 = mat[2].x * det2_13_23 - mat[2].z * det2_13_03 + mat[2].w * det2_13_02;
            float det3_213_123 = mat[2].y * det2_13_23 - mat[2].z * det2_13_13 + mat[2].w * det2_13_12;
            //
            float det3_301_012 = mat[3].x * det2_01_12 - mat[3].y * det2_01_02 + mat[3].z * det2_01_01;
            float det3_301_013 = mat[3].x * det2_01_13 - mat[3].y * det2_01_03 + mat[3].w * det2_01_01;
            float det3_301_023 = mat[3].x * det2_01_23 - mat[3].z * det2_01_03 + mat[3].w * det2_01_02;
            float det3_301_123 = mat[3].y * det2_01_23 - mat[3].z * det2_01_13 + mat[3].w * det2_01_12;
            //
            mat[0].x = (float)(-det3_213_123 * invDet);
            mat[1].x = (float)(+det3_213_023 * invDet);
            mat[2].x = (float)(-det3_213_013 * invDet);
            mat[3].x = (float)(+det3_213_012 * invDet);
            //
            mat[0].y = (float)(+det3_203_123 * invDet);
            mat[1].y = (float)(-det3_203_023 * invDet);
            mat[2].y = (float)(+det3_203_013 * invDet);
            mat[3].y = (float)(-det3_203_012 * invDet);
            //
            mat[0].z = (float)(+det3_301_123 * invDet);
            mat[1].z = (float)(-det3_301_023 * invDet);
            mat[2].z = (float)(+det3_301_013 * invDet);
            mat[3].z = (float)(-det3_301_012 * invDet);
            //
            mat[0].w = (float)(-det3_201_123 * invDet);
            mat[1].w = (float)(+det3_201_023 * invDet);
            mat[2].w = (float)(-det3_201_013 * invDet);
            mat[3].w = (float)(+det3_201_012 * invDet);
            return true;
        }
        public idMat4 InverseFast()
        {
            idMat4 invMat = this;
            bool r = invMat.InverseFastSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseFastSelf()
        {
#if false
            // 84+4+16 = 104 multiplications
            //			   1 division
            // 2x2 sub-determinants required to calculate 4x4 determinant
            float det2_01_01 = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            float det2_01_02 = mat[0].x * mat[1].z - mat[0].z * mat[1].x;
            float det2_01_03 = mat[0].x * mat[1].w - mat[0].w * mat[1].x;
            float det2_01_12 = mat[0].y * mat[1].z - mat[0].z * mat[1].y;
            float det2_01_13 = mat[0].y * mat[1].w - mat[0].w * mat[1].y;
            float det2_01_23 = mat[0].z * mat[1].w - mat[0].w * mat[1].z;
            // 3x3 sub-determinants required to calculate 4x4 determinant
            float det3_201_012 = mat[2].x * det2_01_12 - mat[2].y * det2_01_02 + mat[2].z * det2_01_01;
            float det3_201_013 = mat[2].x * det2_01_13 - mat[2].y * det2_01_03 + mat[2].w * det2_01_01;
            float det3_201_023 = mat[2].x * det2_01_23 - mat[2].z * det2_01_03 + mat[2].w * det2_01_02;
            float det3_201_123 = mat[2].y * det2_01_23 - mat[2].z * det2_01_13 + mat[2].w * det2_01_12;
            double det = (-det3_201_123 * mat[3].x + det3_201_023 * mat[3].y - det3_201_013 * mat[3].z + det3_201_012 * mat[3].w);
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_03_01 = mat[0].x * mat[3].y - mat[0].y * mat[3].x;
            float det2_03_02 = mat[0].x * mat[3].z - mat[0].z * mat[3].x;
            float det2_03_03 = mat[0].x * mat[3].w - mat[0].w * mat[3].x;
            float det2_03_12 = mat[0].y * mat[3].z - mat[0].z * mat[3].y;
            float det2_03_13 = mat[0].y * mat[3].w - mat[0].w * mat[3].y;
            float det2_03_23 = mat[0].z * mat[3].w - mat[0].w * mat[3].z;
            //
            float det2_13_01 = mat[1].x * mat[3].y - mat[1].y * mat[3].x;
            float det2_13_02 = mat[1].x * mat[3].z - mat[1].z * mat[3].x;
            float det2_13_03 = mat[1].x * mat[3].w - mat[1].w * mat[3].x;
            float det2_13_12 = mat[1].y * mat[3].z - mat[1].z * mat[3].y;
            float det2_13_13 = mat[1].y * mat[3].w - mat[1].w * mat[3].y;
            float det2_13_23 = mat[1].z * mat[3].w - mat[1].w * mat[3].z;
            // remaining 3x3 sub-determinants
            float det3_203_012 = mat[2].x * det2_03_12 - mat[2].y * det2_03_02 + mat[2].z * det2_03_01;
            float det3_203_013 = mat[2].x * det2_03_13 - mat[2].y * det2_03_03 + mat[2].w * det2_03_01;
            float det3_203_023 = mat[2].x * det2_03_23 - mat[2].z * det2_03_03 + mat[2].w * det2_03_02;
            float det3_203_123 = mat[2].y * det2_03_23 - mat[2].z * det2_03_13 + mat[2].w * det2_03_12;
            //
            float det3_213_012 = mat[2].x * det2_13_12 - mat[2].y * det2_13_02 + mat[2].z * det2_13_01;
            float det3_213_013 = mat[2].x * det2_13_13 - mat[2].y * det2_13_03 + mat[2].w * det2_13_01;
            float det3_213_023 = mat[2].x * det2_13_23 - mat[2].z * det2_13_03 + mat[2].w * det2_13_02;
            float det3_213_123 = mat[2].y * det2_13_23 - mat[2].z * det2_13_13 + mat[2].w * det2_13_12;
            //
            float det3_301_012 = mat[3].x * det2_01_12 - mat[3].y * det2_01_02 + mat[3].z * det2_01_01;
            float det3_301_013 = mat[3].x * det2_01_13 - mat[3].y * det2_01_03 + mat[3].w * det2_01_01;
            float det3_301_023 = mat[3].x * det2_01_23 - mat[3].z * det2_01_03 + mat[3].w * det2_01_02;
            float det3_301_123 = mat[3].y * det2_01_23 - mat[3].z * det2_01_13 + mat[3].w * det2_01_12;
            //
            mat[0].x = (float)(-det3_213_123 * invDet);
            mat[1].x = (float)(+det3_213_023 * invDet);
            mat[2].x = (float)(-det3_213_013 * invDet);
            mat[3].x = (float)(+det3_213_012 * invDet);
            //
            mat[0].y = (float)(+det3_203_123 * invDet);
            mat[1].y = (float)(-det3_203_023 * invDet);
            mat[2].y = (float)(+det3_203_013 * invDet);
            mat[3].y = (float)(-det3_203_012 * invDet);
            //
            mat[0].z = (float)(+det3_301_123 * invDet);
            mat[1].z = (float)(-det3_301_023 * invDet);
            mat[2].z = (float)(+det3_301_013 * invDet);
            mat[3].z = (float)(-det3_301_012 * invDet);
            //
            mat[0].w = (float)(-det3_201_123 * invDet);
            mat[1].w = (float)(+det3_201_023 * invDet);
            mat[2].w = (float)(-det3_201_013 * invDet);
            mat[3].w = (float)(+det3_201_012 * invDet);
            return true;
#elif false
	// 4*18 = 72 multiplications
	//		   4 divisions
	float *mat = reinterpret_cast<float *>(this);
	float s;
	double d, di;

	di = mat[0];
	s = di;
	mat[0] = d = 1.0f / di;
	mat[1] *= d;
	mat[2] *= d;
	mat[3] *= d;
	d = -d;
	mat[4] *= d;
	mat[8] *= d;
	mat[12] *= d;
	d = mat[4] * di;
	mat[5] += mat[1] * d;
	mat[6] += mat[2] * d;
	mat[7] += mat[3] * d;
	d = mat[8] * di;
	mat[9] += mat[1] * d;
	mat[10] += mat[2] * d;
	mat[11] += mat[3] * d;
	d = mat[12] * di;
	mat[13] += mat[1] * d;
	mat[14] += mat[2] * d;
	mat[15] += mat[3] * d;
	di = mat[5];
	s *= di;
	mat[5] = d = 1.0f / di;
	mat[4] *= d;
	mat[6] *= d;
	mat[7] *= d;
	d = -d;
	mat[1] *= d;
	mat[9] *= d;
	mat[13] *= d;
	d = mat[1] * di;
	mat[0] += mat[4] * d;
	mat[2] += mat[6] * d;
	mat[3] += mat[7] * d;
	d = mat[9] * di;
	mat[8] += mat[4] * d;
	mat[10] += mat[6] * d;
	mat[11] += mat[7] * d;
	d = mat[13] * di;
	mat[12] += mat[4] * d;
	mat[14] += mat[6] * d;
	mat[15] += mat[7] * d;
	di = mat[10];
	s *= di;
	mat[10] = d = 1.0f / di;
	mat[8] *= d;
	mat[9] *= d;
	mat[11] *= d;
	d = -d;
	mat[2] *= d;
	mat[6] *= d;
	mat[14] *= d;
	d = mat[2] * di;
	mat[0] += mat[8] * d;
	mat[1] += mat[9] * d;
	mat[3] += mat[11] * d;
	d = mat[6] * di;
	mat[4] += mat[8] * d;
	mat[5] += mat[9] * d;
	mat[7] += mat[11] * d;
	d = mat[14] * di;
	mat[12] += mat[8] * d;
	mat[13] += mat[9] * d;
	mat[15] += mat[11] * d;
	di = mat[15];
	s *= di;
	mat[15] = d = 1.0f / di;
	mat[12] *= d;
	mat[13] *= d;
	mat[14] *= d;
	d = -d;
	mat[3] *= d;
	mat[7] *= d;
	mat[11] *= d;
	d = mat[3] * di;
	mat[0] += mat[12] * d;
	mat[1] += mat[13] * d;
	mat[2] += mat[14] * d;
	d = mat[7] * di;
	mat[4] += mat[12] * d;
	mat[5] += mat[13] * d;
	mat[6] += mat[14] * d;
	d = mat[11] * di;
	mat[8] += mat[12] * d;
	mat[9] += mat[13] * d;
	mat[10] += mat[14] * d;

	return ( s != 0.0f && !FLOAT_IS_NAN( s ) );
#else
            //	6*8+2*6 = 60 multiplications
            //		2*1 =  2 divisions
            float[] mat = this;
            // r0 = m0.Inverse();
            float det = mat[0 * 4 + 0] * mat[1 * 4 + 1] - mat[0 * 4 + 1] * mat[1 * 4 + 0];
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            float invDet = 1.0f / det;
            idMat2 r0 = new idMat2(); idVec2[] r0_mat = r0.mat;
            r0_mat[0].x = mat[1 * 4 + 1] * invDet;
            r0_mat[0].y = -mat[0 * 4 + 1] * invDet;
            r0_mat[1].x = -mat[1 * 4 + 0] * invDet;
            r0_mat[1].y = mat[0 * 4 + 0] * invDet;
            // r1 = r0 * m1;
            idMat2 r1 = new idMat2(); idVec2[] r1_mat = r1.mat;
            r1_mat[0].x = r0_mat[0].x * mat[0 * 4 + 2] + r0_mat[0].y * mat[1 * 4 + 2];
            r1_mat[0].y = r0_mat[0].x * mat[0 * 4 + 3] + r0_mat[0].y * mat[1 * 4 + 3];
            r1_mat[1].x = r0_mat[1].x * mat[0 * 4 + 2] + r0_mat[1].y * mat[1 * 4 + 2];
            r1_mat[1].y = r0_mat[1].x * mat[0 * 4 + 3] + r0_mat[1].y * mat[1 * 4 + 3];
            // r2 = m2 * r1;
            idMat2 r2 = new idMat2(); idVec2[] r2_mat = r2.mat;
            r2_mat[0].x = mat[2 * 4 + 0] * r1_mat[0].x + mat[2 * 4 + 1] * r1_mat[1].x;
            r2_mat[0].y = mat[2 * 4 + 0] * r1_mat[0].y + mat[2 * 4 + 1] * r1_mat[1].y;
            r2_mat[1].x = mat[3 * 4 + 0] * r1_mat[0].x + mat[3 * 4 + 1] * r1_mat[1].x;
            r2_mat[1].y = mat[3 * 4 + 0] * r1_mat[0].y + mat[3 * 4 + 1] * r1_mat[1].y;
            // r3 = r2 - m3;
            idMat2 r3 = new idMat2(); idVec2[] r3_mat = r3.mat;
            r3_mat[0].x = r2_mat[0].x - mat[2 * 4 + 2];
            r3_mat[0].y = r2_mat[0].y - mat[2 * 4 + 3];
            r3_mat[1].x = r2_mat[1].x - mat[3 * 4 + 2];
            r3_mat[1].y = r2_mat[1].y - mat[3 * 4 + 3];
            // r3.InverseSelf();
            det = r3_mat[0].x * r3_mat[1].y - r3_mat[0].y * r3_mat[1].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            invDet = 1.0f / det;
            float a = r3_mat[0].x;
            r3_mat[0].x = r3_mat[1].y * invDet;
            r3_mat[0].y = -r3_mat[0].y * invDet;
            r3_mat[1].x = -r3_mat[1].x * invDet;
            r3_mat[1].y = a * invDet;
            // r2 = m2 * r0;
            r2_mat[0].x = mat[2 * 4 + 0] * r0_mat[0].x + mat[2 * 4 + 1] * r0_mat[1].x;
            r2_mat[0].y = mat[2 * 4 + 0] * r0_mat[0].y + mat[2 * 4 + 1] * r0_mat[1].y;
            r2_mat[1].x = mat[3 * 4 + 0] * r0_mat[0].x + mat[3 * 4 + 1] * r0_mat[1].x;
            r2_mat[1].y = mat[3 * 4 + 0] * r0_mat[0].y + mat[3 * 4 + 1] * r0_mat[1].y;
            // m2 = r3 * r2;
            mat[2 * 4 + 0] = r3_mat[0].x * r2_mat[0].x + r3_mat[0].y * r2_mat[1].x;
            mat[2 * 4 + 1] = r3_mat[0].x * r2_mat[0].y + r3_mat[0].y * r2_mat[1].y;
            mat[3 * 4 + 0] = r3_mat[1].x * r2_mat[0].x + r3_mat[1].y * r2_mat[1].x;
            mat[3 * 4 + 1] = r3_mat[1].x * r2_mat[0].y + r3_mat[1].y * r2_mat[1].y;
            // m0 = r0 - r1 * m2;
            mat[0 * 4 + 0] = r0_mat[0].x - r1_mat[0].x * mat[2 * 4 + 0] - r1_mat[0].y * mat[3 * 4 + 0];
            mat[0 * 4 + 1] = r0_mat[0].y - r1_mat[0].x * mat[2 * 4 + 1] - r1_mat[0].y * mat[3 * 4 + 1];
            mat[1 * 4 + 0] = r0_mat[1].x - r1_mat[1].x * mat[2 * 4 + 0] - r1_mat[1].y * mat[3 * 4 + 0];
            mat[1 * 4 + 1] = r0_mat[1].y - r1_mat[1].x * mat[2 * 4 + 1] - r1_mat[1].y * mat[3 * 4 + 1];
            // m1 = r1 * r3;
            mat[0 * 4 + 2] = r1_mat[0].x * r3_mat[0].x + r1_mat[0].x * r3_mat[1].x;
            mat[0 * 4 + 3] = r1_mat[0].x * r3_mat[0].y + r1_mat[0].x * r3_mat[1].y;
            mat[1 * 4 + 2] = r1_mat[1].x * r3_mat[0].x + r1_mat[1].x * r3_mat[1].x;
            mat[1 * 4 + 3] = r1_mat[1].x * r3_mat[0].y + r1_mat[1].x * r3_mat[1].y;
            // m3 = -r3;
            mat[2 * 4 + 2] = -r3_mat[0].x;
            mat[2 * 4 + 3] = -r3_mat[0].y;
            mat[3 * 4 + 2] = -r3_mat[1].x;
            mat[3 * 4 + 3] = -r3_mat[1].y;
            return true;
#endif
        }
        #endregion
    }
}



