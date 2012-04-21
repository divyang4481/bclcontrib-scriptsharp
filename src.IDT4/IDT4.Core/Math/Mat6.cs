using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idMat6
    {
        public static readonly idMat6 zero = new idMat6(
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 0f));
        public static readonly idMat6 identity = new idMat6(
            new idVec6(1f, 0f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 1f, 0f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 1f, 0f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 1f, 0f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 1f, 0f),
            new idVec6(0f, 0f, 0f, 0f, 0f, 1f));
        idVec6[] mat = new idVec6[6];

        public idMat6() { }
        public idMat6(ref idMat3 m0, ref idMat3 m1, ref idMat3 m2, ref idMat3 m3)
        {
            idVec3[] m0_mat = m0.mat; idVec3[] m1_mat = m1.mat;
            idVec3[] m2_mat = m2.mat; idVec3[] m3_mat = m3.mat;
            mat[0] = new idVec6(m0_mat[0].x, m0_mat[0].y, m0_mat[0].z, m1_mat[0].x, m1_mat[0].y, m1_mat[0].z);
            mat[1] = new idVec6(m0_mat[1].x, m0_mat[1].y, m0_mat[1].z, m1_mat[1].x, m1_mat[1].y, m1_mat[1].z);
            mat[2] = new idVec6(m0_mat[2].x, m0_mat[2].y, m0_mat[2].z, m1_mat[2].x, m1_mat[2].y, m1_mat[2].z);
            mat[3] = new idVec6(m2_mat[0].x, m2_mat[0].y, m2_mat[0].z, m3_mat[0].x, m3_mat[0].y, m3_mat[0].z);
            mat[4] = new idVec6(m2_mat[1].x, m2_mat[1].y, m2_mat[1].z, m3_mat[1].x, m3_mat[1].y, m3_mat[1].z);
            mat[5] = new idVec6(m2_mat[2].x, m2_mat[2].y, m2_mat[2].z, m3_mat[2].x, m3_mat[2].y, m3_mat[2].z);
        }
        public idMat6(idVec6 v0, idVec6 v1, idVec6 v2, idVec6 v3, idVec6 v4, idVec6 v5)
        {
            mat[0] = v0;
            mat[1] = v1;
            mat[2] = v2;
            mat[3] = v3;
            mat[4] = v4;
            mat[5] = v5;
        }
        public idMat6(ref idVec6 v0, ref idVec6 v1, ref idVec6 v2, ref idVec6 v3, ref idVec6 v4, ref idVec6 v5)
        {
            mat[0] = v0;
            mat[1] = v1;
            mat[2] = v2;
            mat[3] = v3;
            mat[4] = v4;
            mat[5] = v5;
        }
        public idMat6(float[] src) { Array.Copy(mat, src, 6 * 6); }
        public void Zero()
        {
            Array.Clear(mat, 0, 6 * 6);
        }
        public int GetDimension() { return 36; }
        public float[] ToArray() { return mat[0].ToArray(); }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public idVec6 this[int index] { get { return mat[index]; } }
        public static idMat6 operator *(idMat6 a, idMat6 b)
        {
            idMat6 dst = new idMat6();
            //float[] m1Ptr = this;
            //float[] m2Ptr = a;
            //float[] dstPtr = null;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //            *dstPtr = m1Ptr[0] * m2Ptr[ 0 * 6 + j ]
                    //                    + m1Ptr[1] * m2Ptr[ 1 * 6 + j ]
                    //                    + m1Ptr[2] * m2Ptr[ 2 * 6 + j ]
                    //                    + m1Ptr[3] * m2Ptr[ 3 * 6 + j ]
                    //                    + m1Ptr[4] * m2Ptr[ 4 * 6 + j ]
                    //                    + m1Ptr[5] * m2Ptr[ 5 * 6 + j ];
                    //            dstPtr++;
                }
                //m1Ptr += 6;
            }
            return dst;
        }
        public static idMat6 operator *(idMat6 a, float b)
        {
            idVec6[] a_mat = a.mat;
            return new idMat6(
                new idVec6(a_mat[0].p[0] * b, a_mat[0].p[1] * b, a_mat[0].p[2] * b, a_mat[0].p[3] * b, a_mat[0].p[4] * b, a_mat[0].p[5] * b),
                new idVec6(a_mat[1].p[0] * b, a_mat[1].p[1] * b, a_mat[1].p[2] * b, a_mat[1].p[3] * b, a_mat[1].p[4] * b, a_mat[1].p[5] * b),
                new idVec6(a_mat[2].p[0] * b, a_mat[2].p[1] * b, a_mat[2].p[2] * b, a_mat[2].p[3] * b, a_mat[2].p[4] * b, a_mat[2].p[5] * b),
                new idVec6(a_mat[3].p[0] * b, a_mat[3].p[1] * b, a_mat[3].p[2] * b, a_mat[3].p[3] * b, a_mat[3].p[4] * b, a_mat[3].p[5] * b),
                new idVec6(a_mat[4].p[0] * b, a_mat[4].p[1] * b, a_mat[4].p[2] * b, a_mat[4].p[3] * b, a_mat[4].p[4] * b, a_mat[4].p[5] * b),
                new idVec6(a_mat[5].p[0] * b, a_mat[5].p[1] * b, a_mat[5].p[2] * b, a_mat[5].p[3] * b, a_mat[5].p[4] * b, a_mat[5].p[5] * b));
        }
        public static idVec6 operator *(idMat6 a, idVec6 vec)
        {
            idVec6[] a_mat = a.mat;
            float[] vec_p = vec.p;
            return new idVec6(
                a_mat[0].p[0] * vec_p[0] + a_mat[0].p[1] * vec_p[1] + a_mat[0].p[2] * vec_p[2] + a_mat[0].p[3] * vec_p[3] + a_mat[0].p[4] * vec_p[4] + a_mat[0].p[5] * vec_p[5],
                a_mat[1].p[0] * vec_p[0] + a_mat[1].p[1] * vec_p[1] + a_mat[1].p[2] * vec_p[2] + a_mat[1].p[3] * vec_p[3] + a_mat[1].p[4] * vec_p[4] + a_mat[1].p[5] * vec_p[5],
                a_mat[2].p[0] * vec_p[0] + a_mat[2].p[1] * vec_p[1] + a_mat[2].p[2] * vec_p[2] + a_mat[2].p[3] * vec_p[3] + a_mat[2].p[4] * vec_p[4] + a_mat[2].p[5] * vec_p[5],
                a_mat[3].p[0] * vec_p[0] + a_mat[3].p[1] * vec_p[1] + a_mat[3].p[2] * vec_p[2] + a_mat[3].p[3] * vec_p[3] + a_mat[3].p[4] * vec_p[4] + a_mat[3].p[5] * vec_p[5],
                a_mat[4].p[0] * vec_p[0] + a_mat[4].p[1] * vec_p[1] + a_mat[4].p[2] * vec_p[2] + a_mat[4].p[3] * vec_p[3] + a_mat[4].p[4] * vec_p[4] + a_mat[4].p[5] * vec_p[5],
                a_mat[5].p[0] * vec_p[0] + a_mat[5].p[1] * vec_p[1] + a_mat[5].p[2] * vec_p[2] + a_mat[5].p[3] * vec_p[3] + a_mat[5].p[4] * vec_p[4] + a_mat[5].p[5] * vec_p[5]);
        }
        public static idMat6 operator +(idMat6 a, idMat6 b)
        {

            idVec6[] a_mat = a.mat;
            idVec6[] b_mat = b.mat;
            return new idMat6(
                new idVec6(a_mat[0].p[0] + b_mat[0].p[0], a_mat[0].p[1] + b_mat[0].p[1], a_mat[0].p[2] + b_mat[0].p[2], a_mat[0].p[3] + b_mat[0].p[3], a_mat[0].p[4] + b_mat[0].p[4], a_mat[0].p[5] + b_mat[0].p[5]),
                new idVec6(a_mat[1].p[0] + b_mat[1].p[0], a_mat[1].p[1] + b_mat[1].p[1], a_mat[1].p[2] + b_mat[1].p[2], a_mat[1].p[3] + b_mat[1].p[3], a_mat[1].p[4] + b_mat[1].p[4], a_mat[1].p[5] + b_mat[1].p[5]),
                new idVec6(a_mat[2].p[0] + b_mat[2].p[0], a_mat[2].p[1] + b_mat[2].p[1], a_mat[2].p[2] + b_mat[2].p[2], a_mat[2].p[3] + b_mat[2].p[3], a_mat[2].p[4] + b_mat[2].p[4], a_mat[2].p[5] + b_mat[2].p[5]),
                new idVec6(a_mat[3].p[0] + b_mat[3].p[0], a_mat[3].p[1] + b_mat[3].p[1], a_mat[3].p[2] + b_mat[3].p[2], a_mat[3].p[3] + b_mat[3].p[3], a_mat[3].p[4] + b_mat[3].p[4], a_mat[3].p[5] + b_mat[3].p[5]),
                new idVec6(a_mat[4].p[0] + b_mat[4].p[0], a_mat[4].p[1] + b_mat[4].p[1], a_mat[4].p[2] + b_mat[4].p[2], a_mat[4].p[3] + b_mat[4].p[3], a_mat[4].p[4] + b_mat[4].p[4], a_mat[4].p[5] + b_mat[4].p[5]),
                new idVec6(a_mat[5].p[0] + b_mat[5].p[0], a_mat[5].p[1] + b_mat[5].p[1], a_mat[5].p[2] + b_mat[5].p[2], a_mat[5].p[3] + b_mat[5].p[3], a_mat[5].p[4] + b_mat[5].p[4], a_mat[5].p[5] + b_mat[5].p[5]));
        }
        public static idMat6 operator -(idMat6 a, idMat6 b)
        {
            idVec6[] a_mat = a.mat;
            idVec6[] b_mat = b.mat;
            return new idMat6(
                new idVec6(a_mat[0].p[0] - b_mat[0].p[0], a_mat[0].p[1] - b_mat[0].p[1], a_mat[0].p[2] - b_mat[0].p[2], a_mat[0].p[3] - b_mat[0].p[3], a_mat[0].p[4] - b_mat[0].p[4], a_mat[0].p[5] - b_mat[0].p[5]),
                new idVec6(a_mat[1].p[0] - b_mat[1].p[0], a_mat[1].p[1] - b_mat[1].p[1], a_mat[1].p[2] - b_mat[1].p[2], a_mat[1].p[3] - b_mat[1].p[3], a_mat[1].p[4] - b_mat[1].p[4], a_mat[1].p[5] - b_mat[1].p[5]),
                new idVec6(a_mat[2].p[0] - b_mat[2].p[0], a_mat[2].p[1] - b_mat[2].p[1], a_mat[2].p[2] - b_mat[2].p[2], a_mat[2].p[3] - b_mat[2].p[3], a_mat[2].p[4] - b_mat[2].p[4], a_mat[2].p[5] - b_mat[2].p[5]),
                new idVec6(a_mat[3].p[0] - b_mat[3].p[0], a_mat[3].p[1] - b_mat[3].p[1], a_mat[3].p[2] - b_mat[3].p[2], a_mat[3].p[3] - b_mat[3].p[3], a_mat[3].p[4] - b_mat[3].p[4], a_mat[3].p[5] - b_mat[3].p[5]),
                new idVec6(a_mat[4].p[0] - b_mat[4].p[0], a_mat[4].p[1] - b_mat[4].p[1], a_mat[4].p[2] - b_mat[4].p[2], a_mat[4].p[3] - b_mat[4].p[3], a_mat[4].p[4] - b_mat[4].p[4], a_mat[4].p[5] - b_mat[4].p[5]),
                new idVec6(a_mat[5].p[0] - b_mat[5].p[0], a_mat[5].p[1] - b_mat[5].p[1], a_mat[5].p[2] - b_mat[5].p[2], a_mat[5].p[3] - b_mat[5].p[3], a_mat[5].p[4] - b_mat[5].p[4], a_mat[5].p[5] - b_mat[5].p[5]));
        }
        public idMat6 opMul(float a)
        {
            mat[0].p[0] *= a; mat[0].p[1] *= a; mat[0].p[2] *= a; mat[0].p[3] *= a; mat[0].p[4] *= a; mat[0].p[5] *= a;
            mat[1].p[0] *= a; mat[1].p[1] *= a; mat[1].p[2] *= a; mat[1].p[3] *= a; mat[1].p[4] *= a; mat[1].p[5] *= a;
            mat[2].p[0] *= a; mat[2].p[1] *= a; mat[2].p[2] *= a; mat[2].p[3] *= a; mat[2].p[4] *= a; mat[2].p[5] *= a;
            mat[3].p[0] *= a; mat[3].p[1] *= a; mat[3].p[2] *= a; mat[3].p[3] *= a; mat[3].p[4] *= a; mat[3].p[5] *= a;
            mat[4].p[0] *= a; mat[4].p[1] *= a; mat[4].p[2] *= a; mat[4].p[3] *= a; mat[4].p[4] *= a; mat[4].p[5] *= a;
            mat[5].p[0] *= a; mat[5].p[1] *= a; mat[5].p[2] *= a; mat[5].p[3] *= a; mat[5].p[4] *= a; mat[5].p[5] *= a;
            return this;
        }
        public idMat6 opMul(ref idMat6 a)
        {
            this = this * a;
            return this;
        }
        public idMat6 opAdd(ref idMat6 a)
        {
            idVec6[] a_mat = a.mat;
            mat[0].p[0] += a_mat[0].p[0]; mat[0].p[1] += a_mat[0].p[1]; mat[0].p[2] += a_mat[0].p[2]; mat[0].p[3] += a_mat[0].p[3]; mat[0].p[4] += a_mat[0].p[4]; mat[0].p[5] += a_mat[0].p[5];
            mat[1].p[0] += a_mat[1].p[0]; mat[1].p[1] += a_mat[1].p[1]; mat[1].p[2] += a_mat[1].p[2]; mat[1].p[3] += a_mat[1].p[3]; mat[1].p[4] += a_mat[1].p[4]; mat[1].p[5] += a_mat[1].p[5];
            mat[2].p[0] += a_mat[2].p[0]; mat[2].p[1] += a_mat[2].p[1]; mat[2].p[2] += a_mat[2].p[2]; mat[2].p[3] += a_mat[2].p[3]; mat[2].p[4] += a_mat[2].p[4]; mat[2].p[5] += a_mat[2].p[5];
            mat[3].p[0] += a_mat[3].p[0]; mat[3].p[1] += a_mat[3].p[1]; mat[3].p[2] += a_mat[3].p[2]; mat[3].p[3] += a_mat[3].p[3]; mat[3].p[4] += a_mat[3].p[4]; mat[3].p[5] += a_mat[3].p[5];
            mat[4].p[0] += a_mat[4].p[0]; mat[4].p[1] += a_mat[4].p[1]; mat[4].p[2] += a_mat[4].p[2]; mat[4].p[3] += a_mat[4].p[3]; mat[4].p[4] += a_mat[4].p[4]; mat[4].p[5] += a_mat[4].p[5];
            mat[5].p[0] += a_mat[5].p[0]; mat[5].p[1] += a_mat[5].p[1]; mat[5].p[2] += a_mat[5].p[2]; mat[5].p[3] += a_mat[5].p[3]; mat[5].p[4] += a_mat[5].p[4]; mat[5].p[5] += a_mat[5].p[5];
            return this;
        }
        public idMat6 opSub(ref idMat6 a)
        {
            idVec6[] a_mat = a.mat;
            mat[0].p[0] -= a_mat[0].p[0]; mat[0].p[1] -= a_mat[0].p[1]; mat[0].p[2] -= a_mat[0].p[2]; mat[0].p[3] -= a_mat[0].p[3]; mat[0].p[4] -= a_mat[0].p[4]; mat[0].p[5] -= a_mat[0].p[5];
            mat[1].p[0] -= a_mat[1].p[0]; mat[1].p[1] -= a_mat[1].p[1]; mat[1].p[2] -= a_mat[1].p[2]; mat[1].p[3] -= a_mat[1].p[3]; mat[1].p[4] -= a_mat[1].p[4]; mat[1].p[5] -= a_mat[1].p[5];
            mat[2].p[0] -= a_mat[2].p[0]; mat[2].p[1] -= a_mat[2].p[1]; mat[2].p[2] -= a_mat[2].p[2]; mat[2].p[3] -= a_mat[2].p[3]; mat[2].p[4] -= a_mat[2].p[4]; mat[2].p[5] -= a_mat[2].p[5];
            mat[3].p[0] -= a_mat[3].p[0]; mat[3].p[1] -= a_mat[3].p[1]; mat[3].p[2] -= a_mat[3].p[2]; mat[3].p[3] -= a_mat[3].p[3]; mat[3].p[4] -= a_mat[3].p[4]; mat[3].p[5] -= a_mat[3].p[5];
            mat[4].p[0] -= a_mat[4].p[0]; mat[4].p[1] -= a_mat[4].p[1]; mat[4].p[2] -= a_mat[4].p[2]; mat[4].p[3] -= a_mat[4].p[3]; mat[4].p[4] -= a_mat[4].p[4]; mat[4].p[5] -= a_mat[4].p[5];
            mat[5].p[0] -= a_mat[5].p[0]; mat[5].p[1] -= a_mat[5].p[1]; mat[5].p[2] -= a_mat[5].p[2]; mat[5].p[3] -= a_mat[5].p[3]; mat[5].p[4] -= a_mat[5].p[4]; mat[5].p[5] -= a_mat[5].p[5];
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idMat6 a)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 6 * 6; i++)
                if (ptr1[i] != ptr2[i])
                    return false;
            return true;
        }
        public bool Compare(ref idMat6 a, float epsilon)
        {
            float[] ptr1 = mat;
            float[] ptr2 = a.mat;
            for (int i = 0; i < 6 * 6; i++)
                if (idMath.Fabs(ptr1[i] - ptr2[i]) > epsilon)
                    return false;
            return true;
        }
        public static bool operator ==(idMat6 a, idMat6 b) { return a.Compare(ref b); }
        public static bool operator !=(idMat6 a, idMat6 b) { return !a.Compare(ref b); }
        #endregion

        #region Identity
        public void Identity() { this = identity; }
        public bool IsIdentity(float epsilon) { return Compare(ref identity, epsilon); }
        public bool IsSymmetric(float epsilon)
        {
            for (int i = 1; i < 6; i++)
                for (int j = 0; j < i; j++)
                    if (idMath.Fabs(mat[i][j] - mat[j][i]) > epsilon)
                        return false;
            return true;
        }
        public bool IsDiagonal(float epsilon)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    if (i != j && idMath.Fabs(mat[i][j]) > epsilon)
                        return false;
            return true;
        }
        #endregion

        #region Transform
        public idMat3 SubMat3(int n)
        {
            Debug.Assert(n >= 0 && n < 4);
            int b0 = ((n & 2) >> 1) * 3;
            int b1 = (n & 1) * 3;
            return new idMat3(
                mat[b0 + 0][b1 + 0], mat[b0 + 0][b1 + 1], mat[b0 + 0][b1 + 2],
                mat[b0 + 1][b1 + 0], mat[b0 + 1][b1 + 1], mat[b0 + 1][b1 + 2],
                mat[b0 + 2][b1 + 0], mat[b0 + 2][b1 + 1], mat[b0 + 2][b1 + 2]);
        }
        public float Trace() { return (mat[0].p[0] + mat[1].p[1] + mat[2].p[2] + mat[3].p[3] + mat[4].p[4] + mat[5].p[5]); }
        public float Determinant()
        {
            // 2x2 sub-determinants required to calculate 6x6 determinant
            float det2_45_01 = mat[4].p[0] * mat[5].p[1] - mat[4].p[1] * mat[5].p[0];
            float det2_45_02 = mat[4].p[0] * mat[5].p[2] - mat[4].p[2] * mat[5].p[0];
            float det2_45_03 = mat[4].p[0] * mat[5].p[3] - mat[4].p[3] * mat[5].p[0];
            float det2_45_04 = mat[4].p[0] * mat[5].p[4] - mat[4].p[4] * mat[5].p[0];
            float det2_45_05 = mat[4].p[0] * mat[5].p[5] - mat[4].p[5] * mat[5].p[0];
            float det2_45_12 = mat[4].p[1] * mat[5].p[2] - mat[4].p[2] * mat[5].p[1];
            float det2_45_13 = mat[4].p[1] * mat[5].p[3] - mat[4].p[3] * mat[5].p[1];
            float det2_45_14 = mat[4].p[1] * mat[5].p[4] - mat[4].p[4] * mat[5].p[1];
            float det2_45_15 = mat[4].p[1] * mat[5].p[5] - mat[4].p[5] * mat[5].p[1];
            float det2_45_23 = mat[4].p[2] * mat[5].p[3] - mat[4].p[3] * mat[5].p[2];
            float det2_45_24 = mat[4].p[2] * mat[5].p[4] - mat[4].p[4] * mat[5].p[2];
            float det2_45_25 = mat[4].p[2] * mat[5].p[5] - mat[4].p[5] * mat[5].p[2];
            float det2_45_34 = mat[4].p[3] * mat[5].p[4] - mat[4].p[4] * mat[5].p[3];
            float det2_45_35 = mat[4].p[3] * mat[5].p[5] - mat[4].p[5] * mat[5].p[3];
            float det2_45_45 = mat[4].p[4] * mat[5].p[5] - mat[4].p[5] * mat[5].p[4];
            // 3x3 sub-determinants required to calculate 6x6 determinant
            float det3_345_012 = mat[3].p[0] * det2_45_12 - mat[3].p[1] * det2_45_02 + mat[3].p[2] * det2_45_01;
            float det3_345_013 = mat[3].p[0] * det2_45_13 - mat[3].p[1] * det2_45_03 + mat[3].p[3] * det2_45_01;
            float det3_345_014 = mat[3].p[0] * det2_45_14 - mat[3].p[1] * det2_45_04 + mat[3].p[4] * det2_45_01;
            float det3_345_015 = mat[3].p[0] * det2_45_15 - mat[3].p[1] * det2_45_05 + mat[3].p[5] * det2_45_01;
            float det3_345_023 = mat[3].p[0] * det2_45_23 - mat[3].p[2] * det2_45_03 + mat[3].p[3] * det2_45_02;
            float det3_345_024 = mat[3].p[0] * det2_45_24 - mat[3].p[2] * det2_45_04 + mat[3].p[4] * det2_45_02;
            float det3_345_025 = mat[3].p[0] * det2_45_25 - mat[3].p[2] * det2_45_05 + mat[3].p[5] * det2_45_02;
            float det3_345_034 = mat[3].p[0] * det2_45_34 - mat[3].p[3] * det2_45_04 + mat[3].p[4] * det2_45_03;
            float det3_345_035 = mat[3].p[0] * det2_45_35 - mat[3].p[3] * det2_45_05 + mat[3].p[5] * det2_45_03;
            float det3_345_045 = mat[3].p[0] * det2_45_45 - mat[3].p[4] * det2_45_05 + mat[3].p[5] * det2_45_04;
            float det3_345_123 = mat[3].p[1] * det2_45_23 - mat[3].p[2] * det2_45_13 + mat[3].p[3] * det2_45_12;
            float det3_345_124 = mat[3].p[1] * det2_45_24 - mat[3].p[2] * det2_45_14 + mat[3].p[4] * det2_45_12;
            float det3_345_125 = mat[3].p[1] * det2_45_25 - mat[3].p[2] * det2_45_15 + mat[3].p[5] * det2_45_12;
            float det3_345_134 = mat[3].p[1] * det2_45_34 - mat[3].p[3] * det2_45_14 + mat[3].p[4] * det2_45_13;
            float det3_345_135 = mat[3].p[1] * det2_45_35 - mat[3].p[3] * det2_45_15 + mat[3].p[5] * det2_45_13;
            float det3_345_145 = mat[3].p[1] * det2_45_45 - mat[3].p[4] * det2_45_15 + mat[3].p[5] * det2_45_14;
            float det3_345_234 = mat[3].p[2] * det2_45_34 - mat[3].p[3] * det2_45_24 + mat[3].p[4] * det2_45_23;
            float det3_345_235 = mat[3].p[2] * det2_45_35 - mat[3].p[3] * det2_45_25 + mat[3].p[5] * det2_45_23;
            float det3_345_245 = mat[3].p[2] * det2_45_45 - mat[3].p[4] * det2_45_25 + mat[3].p[5] * det2_45_24;
            float det3_345_345 = mat[3].p[3] * det2_45_45 - mat[3].p[4] * det2_45_35 + mat[3].p[5] * det2_45_34;
            // 4x4 sub-determinants required to calculate 6x6 determinant
            float det4_2345_0123 = mat[2].p[0] * det3_345_123 - mat[2].p[1] * det3_345_023 + mat[2].p[2] * det3_345_013 - mat[2].p[3] * det3_345_012;
            float det4_2345_0124 = mat[2].p[0] * det3_345_124 - mat[2].p[1] * det3_345_024 + mat[2].p[2] * det3_345_014 - mat[2].p[4] * det3_345_012;
            float det4_2345_0125 = mat[2].p[0] * det3_345_125 - mat[2].p[1] * det3_345_025 + mat[2].p[2] * det3_345_015 - mat[2].p[5] * det3_345_012;
            float det4_2345_0134 = mat[2].p[0] * det3_345_134 - mat[2].p[1] * det3_345_034 + mat[2].p[3] * det3_345_014 - mat[2].p[4] * det3_345_013;
            float det4_2345_0135 = mat[2].p[0] * det3_345_135 - mat[2].p[1] * det3_345_035 + mat[2].p[3] * det3_345_015 - mat[2].p[5] * det3_345_013;
            float det4_2345_0145 = mat[2].p[0] * det3_345_145 - mat[2].p[1] * det3_345_045 + mat[2].p[4] * det3_345_015 - mat[2].p[5] * det3_345_014;
            float det4_2345_0234 = mat[2].p[0] * det3_345_234 - mat[2].p[2] * det3_345_034 + mat[2].p[3] * det3_345_024 - mat[2].p[4] * det3_345_023;
            float det4_2345_0235 = mat[2].p[0] * det3_345_235 - mat[2].p[2] * det3_345_035 + mat[2].p[3] * det3_345_025 - mat[2].p[5] * det3_345_023;
            float det4_2345_0245 = mat[2].p[0] * det3_345_245 - mat[2].p[2] * det3_345_045 + mat[2].p[4] * det3_345_025 - mat[2].p[5] * det3_345_024;
            float det4_2345_0345 = mat[2].p[0] * det3_345_345 - mat[2].p[3] * det3_345_045 + mat[2].p[4] * det3_345_035 - mat[2].p[5] * det3_345_034;
            float det4_2345_1234 = mat[2].p[1] * det3_345_234 - mat[2].p[2] * det3_345_134 + mat[2].p[3] * det3_345_124 - mat[2].p[4] * det3_345_123;
            float det4_2345_1235 = mat[2].p[1] * det3_345_235 - mat[2].p[2] * det3_345_135 + mat[2].p[3] * det3_345_125 - mat[2].p[5] * det3_345_123;
            float det4_2345_1245 = mat[2].p[1] * det3_345_245 - mat[2].p[2] * det3_345_145 + mat[2].p[4] * det3_345_125 - mat[2].p[5] * det3_345_124;
            float det4_2345_1345 = mat[2].p[1] * det3_345_345 - mat[2].p[3] * det3_345_145 + mat[2].p[4] * det3_345_135 - mat[2].p[5] * det3_345_134;
            float det4_2345_2345 = mat[2].p[2] * det3_345_345 - mat[2].p[3] * det3_345_245 + mat[2].p[4] * det3_345_235 - mat[2].p[5] * det3_345_234;
            // 5x5 sub-determinants required to calculate 6x6 determinant
            float det5_12345_01234 = mat[1].p[0] * det4_2345_1234 - mat[1].p[1] * det4_2345_0234 + mat[1].p[2] * det4_2345_0134 - mat[1].p[3] * det4_2345_0124 + mat[1].p[4] * det4_2345_0123;
            float det5_12345_01235 = mat[1].p[0] * det4_2345_1235 - mat[1].p[1] * det4_2345_0235 + mat[1].p[2] * det4_2345_0135 - mat[1].p[3] * det4_2345_0125 + mat[1].p[5] * det4_2345_0123;
            float det5_12345_01245 = mat[1].p[0] * det4_2345_1245 - mat[1].p[1] * det4_2345_0245 + mat[1].p[2] * det4_2345_0145 - mat[1].p[4] * det4_2345_0125 + mat[1].p[5] * det4_2345_0124;
            float det5_12345_01345 = mat[1].p[0] * det4_2345_1345 - mat[1].p[1] * det4_2345_0345 + mat[1].p[3] * det4_2345_0145 - mat[1].p[4] * det4_2345_0135 + mat[1].p[5] * det4_2345_0134;
            float det5_12345_02345 = mat[1].p[0] * det4_2345_2345 - mat[1].p[2] * det4_2345_0345 + mat[1].p[3] * det4_2345_0245 - mat[1].p[4] * det4_2345_0235 + mat[1].p[5] * det4_2345_0234;
            float det5_12345_12345 = mat[1].p[1] * det4_2345_2345 - mat[1].p[2] * det4_2345_1345 + mat[1].p[3] * det4_2345_1245 - mat[1].p[4] * det4_2345_1235 + mat[1].p[5] * det4_2345_1234;
            // determinant of 6x6 matrix
            return mat[0].p[0] * det5_12345_12345 - mat[0].p[1] * det5_12345_02345 + mat[0].p[2] * det5_12345_01345 - mat[0].p[3] * det5_12345_01245 + mat[0].p[4] * det5_12345_01235 - mat[0].p[5] * det5_12345_01234;
        }
        public idMat6 Transpose()
        {
            idMat6 transpose = new idMat6();
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 6; j++)
                    transpose[i][j] = mat[j][i];
            return transpose;
        }
        public idMat6 TransposeSelf()
        {
            for (int i = 0; i < 6; i++)
                for (int j = i + 1; j < 6; j++)
                {
                    float temp = mat[i][j];
                    mat[i][j] = mat[j][i];
                    mat[j][i] = temp;
                }
            return this;
        }
        public idMat6 Inverse()
        {
            idMat6 invMat = this;
            bool r = invMat.InverseSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseSelf()
        {
            // 810+6+36 = 852 multiplications
            //				1 division
            // 2x2 sub-determinants required to calculate 6x6 determinant
            float det2_45_01 = mat[4].p[0] * mat[5].p[1] - mat[4].p[1] * mat[5].p[0];
            float det2_45_02 = mat[4].p[0] * mat[5].p[2] - mat[4].p[2] * mat[5].p[0];
            float det2_45_03 = mat[4].p[0] * mat[5].p[3] - mat[4].p[3] * mat[5].p[0];
            float det2_45_04 = mat[4].p[0] * mat[5].p[4] - mat[4].p[4] * mat[5].p[0];
            float det2_45_05 = mat[4].p[0] * mat[5].p[5] - mat[4].p[5] * mat[5].p[0];
            float det2_45_12 = mat[4].p[1] * mat[5].p[2] - mat[4].p[2] * mat[5].p[1];
            float det2_45_13 = mat[4].p[1] * mat[5].p[3] - mat[4].p[3] * mat[5].p[1];
            float det2_45_14 = mat[4].p[1] * mat[5].p[4] - mat[4].p[4] * mat[5].p[1];
            float det2_45_15 = mat[4].p[1] * mat[5].p[5] - mat[4].p[5] * mat[5].p[1];
            float det2_45_23 = mat[4].p[2] * mat[5].p[3] - mat[4].p[3] * mat[5].p[2];
            float det2_45_24 = mat[4].p[2] * mat[5].p[4] - mat[4].p[4] * mat[5].p[2];
            float det2_45_25 = mat[4].p[2] * mat[5].p[5] - mat[4].p[5] * mat[5].p[2];
            float det2_45_34 = mat[4].p[3] * mat[5].p[4] - mat[4].p[4] * mat[5].p[3];
            float det2_45_35 = mat[4].p[3] * mat[5].p[5] - mat[4].p[5] * mat[5].p[3];
            float det2_45_45 = mat[4].p[4] * mat[5].p[5] - mat[4].p[5] * mat[5].p[4];
            // 3x3 sub-determinants required to calculate 6x6 determinant
            float det3_345_012 = mat[3].p[0] * det2_45_12 - mat[3].p[1] * det2_45_02 + mat[3].p[2] * det2_45_01;
            float det3_345_013 = mat[3].p[0] * det2_45_13 - mat[3].p[1] * det2_45_03 + mat[3].p[3] * det2_45_01;
            float det3_345_014 = mat[3].p[0] * det2_45_14 - mat[3].p[1] * det2_45_04 + mat[3].p[4] * det2_45_01;
            float det3_345_015 = mat[3].p[0] * det2_45_15 - mat[3].p[1] * det2_45_05 + mat[3].p[5] * det2_45_01;
            float det3_345_023 = mat[3].p[0] * det2_45_23 - mat[3].p[2] * det2_45_03 + mat[3].p[3] * det2_45_02;
            float det3_345_024 = mat[3].p[0] * det2_45_24 - mat[3].p[2] * det2_45_04 + mat[3].p[4] * det2_45_02;
            float det3_345_025 = mat[3].p[0] * det2_45_25 - mat[3].p[2] * det2_45_05 + mat[3].p[5] * det2_45_02;
            float det3_345_034 = mat[3].p[0] * det2_45_34 - mat[3].p[3] * det2_45_04 + mat[3].p[4] * det2_45_03;
            float det3_345_035 = mat[3].p[0] * det2_45_35 - mat[3].p[3] * det2_45_05 + mat[3].p[5] * det2_45_03;
            float det3_345_045 = mat[3].p[0] * det2_45_45 - mat[3].p[4] * det2_45_05 + mat[3].p[5] * det2_45_04;
            float det3_345_123 = mat[3].p[1] * det2_45_23 - mat[3].p[2] * det2_45_13 + mat[3].p[3] * det2_45_12;
            float det3_345_124 = mat[3].p[1] * det2_45_24 - mat[3].p[2] * det2_45_14 + mat[3].p[4] * det2_45_12;
            float det3_345_125 = mat[3].p[1] * det2_45_25 - mat[3].p[2] * det2_45_15 + mat[3].p[5] * det2_45_12;
            float det3_345_134 = mat[3].p[1] * det2_45_34 - mat[3].p[3] * det2_45_14 + mat[3].p[4] * det2_45_13;
            float det3_345_135 = mat[3].p[1] * det2_45_35 - mat[3].p[3] * det2_45_15 + mat[3].p[5] * det2_45_13;
            float det3_345_145 = mat[3].p[1] * det2_45_45 - mat[3].p[4] * det2_45_15 + mat[3].p[5] * det2_45_14;
            float det3_345_234 = mat[3].p[2] * det2_45_34 - mat[3].p[3] * det2_45_24 + mat[3].p[4] * det2_45_23;
            float det3_345_235 = mat[3].p[2] * det2_45_35 - mat[3].p[3] * det2_45_25 + mat[3].p[5] * det2_45_23;
            float det3_345_245 = mat[3].p[2] * det2_45_45 - mat[3].p[4] * det2_45_25 + mat[3].p[5] * det2_45_24;
            float det3_345_345 = mat[3].p[3] * det2_45_45 - mat[3].p[4] * det2_45_35 + mat[3].p[5] * det2_45_34;
            // 4x4 sub-determinants required to calculate 6x6 determinant
            float det4_2345_0123 = mat[2].p[0] * det3_345_123 - mat[2].p[1] * det3_345_023 + mat[2].p[2] * det3_345_013 - mat[2].p[3] * det3_345_012;
            float det4_2345_0124 = mat[2].p[0] * det3_345_124 - mat[2].p[1] * det3_345_024 + mat[2].p[2] * det3_345_014 - mat[2].p[4] * det3_345_012;
            float det4_2345_0125 = mat[2].p[0] * det3_345_125 - mat[2].p[1] * det3_345_025 + mat[2].p[2] * det3_345_015 - mat[2].p[5] * det3_345_012;
            float det4_2345_0134 = mat[2].p[0] * det3_345_134 - mat[2].p[1] * det3_345_034 + mat[2].p[3] * det3_345_014 - mat[2].p[4] * det3_345_013;
            float det4_2345_0135 = mat[2].p[0] * det3_345_135 - mat[2].p[1] * det3_345_035 + mat[2].p[3] * det3_345_015 - mat[2].p[5] * det3_345_013;
            float det4_2345_0145 = mat[2].p[0] * det3_345_145 - mat[2].p[1] * det3_345_045 + mat[2].p[4] * det3_345_015 - mat[2].p[5] * det3_345_014;
            float det4_2345_0234 = mat[2].p[0] * det3_345_234 - mat[2].p[2] * det3_345_034 + mat[2].p[3] * det3_345_024 - mat[2].p[4] * det3_345_023;
            float det4_2345_0235 = mat[2].p[0] * det3_345_235 - mat[2].p[2] * det3_345_035 + mat[2].p[3] * det3_345_025 - mat[2].p[5] * det3_345_023;
            float det4_2345_0245 = mat[2].p[0] * det3_345_245 - mat[2].p[2] * det3_345_045 + mat[2].p[4] * det3_345_025 - mat[2].p[5] * det3_345_024;
            float det4_2345_0345 = mat[2].p[0] * det3_345_345 - mat[2].p[3] * det3_345_045 + mat[2].p[4] * det3_345_035 - mat[2].p[5] * det3_345_034;
            float det4_2345_1234 = mat[2].p[1] * det3_345_234 - mat[2].p[2] * det3_345_134 + mat[2].p[3] * det3_345_124 - mat[2].p[4] * det3_345_123;
            float det4_2345_1235 = mat[2].p[1] * det3_345_235 - mat[2].p[2] * det3_345_135 + mat[2].p[3] * det3_345_125 - mat[2].p[5] * det3_345_123;
            float det4_2345_1245 = mat[2].p[1] * det3_345_245 - mat[2].p[2] * det3_345_145 + mat[2].p[4] * det3_345_125 - mat[2].p[5] * det3_345_124;
            float det4_2345_1345 = mat[2].p[1] * det3_345_345 - mat[2].p[3] * det3_345_145 + mat[2].p[4] * det3_345_135 - mat[2].p[5] * det3_345_134;
            float det4_2345_2345 = mat[2].p[2] * det3_345_345 - mat[2].p[3] * det3_345_245 + mat[2].p[4] * det3_345_235 - mat[2].p[5] * det3_345_234;
            // 5x5 sub-determinants required to calculate 6x6 determinant
            float det5_12345_01234 = mat[1].p[0] * det4_2345_1234 - mat[1].p[1] * det4_2345_0234 + mat[1].p[2] * det4_2345_0134 - mat[1].p[3] * det4_2345_0124 + mat[1].p[4] * det4_2345_0123;
            float det5_12345_01235 = mat[1].p[0] * det4_2345_1235 - mat[1].p[1] * det4_2345_0235 + mat[1].p[2] * det4_2345_0135 - mat[1].p[3] * det4_2345_0125 + mat[1].p[5] * det4_2345_0123;
            float det5_12345_01245 = mat[1].p[0] * det4_2345_1245 - mat[1].p[1] * det4_2345_0245 + mat[1].p[2] * det4_2345_0145 - mat[1].p[4] * det4_2345_0125 + mat[1].p[5] * det4_2345_0124;
            float det5_12345_01345 = mat[1].p[0] * det4_2345_1345 - mat[1].p[1] * det4_2345_0345 + mat[1].p[3] * det4_2345_0145 - mat[1].p[4] * det4_2345_0135 + mat[1].p[5] * det4_2345_0134;
            float det5_12345_02345 = mat[1].p[0] * det4_2345_2345 - mat[1].p[2] * det4_2345_0345 + mat[1].p[3] * det4_2345_0245 - mat[1].p[4] * det4_2345_0235 + mat[1].p[5] * det4_2345_0234;
            float det5_12345_12345 = mat[1].p[1] * det4_2345_2345 - mat[1].p[2] * det4_2345_1345 + mat[1].p[3] * det4_2345_1245 - mat[1].p[4] * det4_2345_1235 + mat[1].p[5] * det4_2345_1234;
            // determinant of 6x6 matrix
            double det = mat[0].p[0] * det5_12345_12345 - mat[0].p[1] * det5_12345_02345 + mat[0].p[2] * det5_12345_01345 - mat[0].p[3] * det5_12345_01245 + mat[0].p[4] * det5_12345_01235 - mat[0].p[5] * det5_12345_01234;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_34_01 = mat[3].p[0] * mat[4].p[1] - mat[3].p[1] * mat[4].p[0];
            float det2_34_02 = mat[3].p[0] * mat[4].p[2] - mat[3].p[2] * mat[4].p[0];
            float det2_34_03 = mat[3].p[0] * mat[4].p[3] - mat[3].p[3] * mat[4].p[0];
            float det2_34_04 = mat[3].p[0] * mat[4].p[4] - mat[3].p[4] * mat[4].p[0];
            float det2_34_05 = mat[3].p[0] * mat[4].p[5] - mat[3].p[5] * mat[4].p[0];
            float det2_34_12 = mat[3].p[1] * mat[4].p[2] - mat[3].p[2] * mat[4].p[1];
            float det2_34_13 = mat[3].p[1] * mat[4].p[3] - mat[3].p[3] * mat[4].p[1];
            float det2_34_14 = mat[3].p[1] * mat[4].p[4] - mat[3].p[4] * mat[4].p[1];
            float det2_34_15 = mat[3].p[1] * mat[4].p[5] - mat[3].p[5] * mat[4].p[1];
            float det2_34_23 = mat[3].p[2] * mat[4].p[3] - mat[3].p[3] * mat[4].p[2];
            float det2_34_24 = mat[3].p[2] * mat[4].p[4] - mat[3].p[4] * mat[4].p[2];
            float det2_34_25 = mat[3].p[2] * mat[4].p[5] - mat[3].p[5] * mat[4].p[2];
            float det2_34_34 = mat[3].p[3] * mat[4].p[4] - mat[3].p[4] * mat[4].p[3];
            float det2_34_35 = mat[3].p[3] * mat[4].p[5] - mat[3].p[5] * mat[4].p[3];
            float det2_34_45 = mat[3].p[4] * mat[4].p[5] - mat[3].p[5] * mat[4].p[4];
            float det2_35_01 = mat[3].p[0] * mat[5].p[1] - mat[3].p[1] * mat[5].p[0];
            float det2_35_02 = mat[3].p[0] * mat[5].p[2] - mat[3].p[2] * mat[5].p[0];
            float det2_35_03 = mat[3].p[0] * mat[5].p[3] - mat[3].p[3] * mat[5].p[0];
            float det2_35_04 = mat[3].p[0] * mat[5].p[4] - mat[3].p[4] * mat[5].p[0];
            float det2_35_05 = mat[3].p[0] * mat[5].p[5] - mat[3].p[5] * mat[5].p[0];
            float det2_35_12 = mat[3].p[1] * mat[5].p[2] - mat[3].p[2] * mat[5].p[1];
            float det2_35_13 = mat[3].p[1] * mat[5].p[3] - mat[3].p[3] * mat[5].p[1];
            float det2_35_14 = mat[3].p[1] * mat[5].p[4] - mat[3].p[4] * mat[5].p[1];
            float det2_35_15 = mat[3].p[1] * mat[5].p[5] - mat[3].p[5] * mat[5].p[1];
            float det2_35_23 = mat[3].p[2] * mat[5].p[3] - mat[3].p[3] * mat[5].p[2];
            float det2_35_24 = mat[3].p[2] * mat[5].p[4] - mat[3].p[4] * mat[5].p[2];
            float det2_35_25 = mat[3].p[2] * mat[5].p[5] - mat[3].p[5] * mat[5].p[2];
            float det2_35_34 = mat[3].p[3] * mat[5].p[4] - mat[3].p[4] * mat[5].p[3];
            float det2_35_35 = mat[3].p[3] * mat[5].p[5] - mat[3].p[5] * mat[5].p[3];
            float det2_35_45 = mat[3].p[4] * mat[5].p[5] - mat[3].p[5] * mat[5].p[4];
            // remaining 3x3 sub-determinants
            float det3_234_012 = mat[2].p[0] * det2_34_12 - mat[2].p[1] * det2_34_02 + mat[2].p[2] * det2_34_01;
            float det3_234_013 = mat[2].p[0] * det2_34_13 - mat[2].p[1] * det2_34_03 + mat[2].p[3] * det2_34_01;
            float det3_234_014 = mat[2].p[0] * det2_34_14 - mat[2].p[1] * det2_34_04 + mat[2].p[4] * det2_34_01;
            float det3_234_015 = mat[2].p[0] * det2_34_15 - mat[2].p[1] * det2_34_05 + mat[2].p[5] * det2_34_01;
            float det3_234_023 = mat[2].p[0] * det2_34_23 - mat[2].p[2] * det2_34_03 + mat[2].p[3] * det2_34_02;
            float det3_234_024 = mat[2].p[0] * det2_34_24 - mat[2].p[2] * det2_34_04 + mat[2].p[4] * det2_34_02;
            float det3_234_025 = mat[2].p[0] * det2_34_25 - mat[2].p[2] * det2_34_05 + mat[2].p[5] * det2_34_02;
            float det3_234_034 = mat[2].p[0] * det2_34_34 - mat[2].p[3] * det2_34_04 + mat[2].p[4] * det2_34_03;
            float det3_234_035 = mat[2].p[0] * det2_34_35 - mat[2].p[3] * det2_34_05 + mat[2].p[5] * det2_34_03;
            float det3_234_045 = mat[2].p[0] * det2_34_45 - mat[2].p[4] * det2_34_05 + mat[2].p[5] * det2_34_04;
            float det3_234_123 = mat[2].p[1] * det2_34_23 - mat[2].p[2] * det2_34_13 + mat[2].p[3] * det2_34_12;
            float det3_234_124 = mat[2].p[1] * det2_34_24 - mat[2].p[2] * det2_34_14 + mat[2].p[4] * det2_34_12;
            float det3_234_125 = mat[2].p[1] * det2_34_25 - mat[2].p[2] * det2_34_15 + mat[2].p[5] * det2_34_12;
            float det3_234_134 = mat[2].p[1] * det2_34_34 - mat[2].p[3] * det2_34_14 + mat[2].p[4] * det2_34_13;
            float det3_234_135 = mat[2].p[1] * det2_34_35 - mat[2].p[3] * det2_34_15 + mat[2].p[5] * det2_34_13;
            float det3_234_145 = mat[2].p[1] * det2_34_45 - mat[2].p[4] * det2_34_15 + mat[2].p[5] * det2_34_14;
            float det3_234_234 = mat[2].p[2] * det2_34_34 - mat[2].p[3] * det2_34_24 + mat[2].p[4] * det2_34_23;
            float det3_234_235 = mat[2].p[2] * det2_34_35 - mat[2].p[3] * det2_34_25 + mat[2].p[5] * det2_34_23;
            float det3_234_245 = mat[2].p[2] * det2_34_45 - mat[2].p[4] * det2_34_25 + mat[2].p[5] * det2_34_24;
            float det3_234_345 = mat[2].p[3] * det2_34_45 - mat[2].p[4] * det2_34_35 + mat[2].p[5] * det2_34_34;
            float det3_235_012 = mat[2].p[0] * det2_35_12 - mat[2].p[1] * det2_35_02 + mat[2].p[2] * det2_35_01;
            float det3_235_013 = mat[2].p[0] * det2_35_13 - mat[2].p[1] * det2_35_03 + mat[2].p[3] * det2_35_01;
            float det3_235_014 = mat[2].p[0] * det2_35_14 - mat[2].p[1] * det2_35_04 + mat[2].p[4] * det2_35_01;
            float det3_235_015 = mat[2].p[0] * det2_35_15 - mat[2].p[1] * det2_35_05 + mat[2].p[5] * det2_35_01;
            float det3_235_023 = mat[2].p[0] * det2_35_23 - mat[2].p[2] * det2_35_03 + mat[2].p[3] * det2_35_02;
            float det3_235_024 = mat[2].p[0] * det2_35_24 - mat[2].p[2] * det2_35_04 + mat[2].p[4] * det2_35_02;
            float det3_235_025 = mat[2].p[0] * det2_35_25 - mat[2].p[2] * det2_35_05 + mat[2].p[5] * det2_35_02;
            float det3_235_034 = mat[2].p[0] * det2_35_34 - mat[2].p[3] * det2_35_04 + mat[2].p[4] * det2_35_03;
            float det3_235_035 = mat[2].p[0] * det2_35_35 - mat[2].p[3] * det2_35_05 + mat[2].p[5] * det2_35_03;
            float det3_235_045 = mat[2].p[0] * det2_35_45 - mat[2].p[4] * det2_35_05 + mat[2].p[5] * det2_35_04;
            float det3_235_123 = mat[2].p[1] * det2_35_23 - mat[2].p[2] * det2_35_13 + mat[2].p[3] * det2_35_12;
            float det3_235_124 = mat[2].p[1] * det2_35_24 - mat[2].p[2] * det2_35_14 + mat[2].p[4] * det2_35_12;
            float det3_235_125 = mat[2].p[1] * det2_35_25 - mat[2].p[2] * det2_35_15 + mat[2].p[5] * det2_35_12;
            float det3_235_134 = mat[2].p[1] * det2_35_34 - mat[2].p[3] * det2_35_14 + mat[2].p[4] * det2_35_13;
            float det3_235_135 = mat[2].p[1] * det2_35_35 - mat[2].p[3] * det2_35_15 + mat[2].p[5] * det2_35_13;
            float det3_235_145 = mat[2].p[1] * det2_35_45 - mat[2].p[4] * det2_35_15 + mat[2].p[5] * det2_35_14;
            float det3_235_234 = mat[2].p[2] * det2_35_34 - mat[2].p[3] * det2_35_24 + mat[2].p[4] * det2_35_23;
            float det3_235_235 = mat[2].p[2] * det2_35_35 - mat[2].p[3] * det2_35_25 + mat[2].p[5] * det2_35_23;
            float det3_235_245 = mat[2].p[2] * det2_35_45 - mat[2].p[4] * det2_35_25 + mat[2].p[5] * det2_35_24;
            float det3_235_345 = mat[2].p[3] * det2_35_45 - mat[2].p[4] * det2_35_35 + mat[2].p[5] * det2_35_34;
            float det3_245_012 = mat[2].p[0] * det2_45_12 - mat[2].p[1] * det2_45_02 + mat[2].p[2] * det2_45_01;
            float det3_245_013 = mat[2].p[0] * det2_45_13 - mat[2].p[1] * det2_45_03 + mat[2].p[3] * det2_45_01;
            float det3_245_014 = mat[2].p[0] * det2_45_14 - mat[2].p[1] * det2_45_04 + mat[2].p[4] * det2_45_01;
            float det3_245_015 = mat[2].p[0] * det2_45_15 - mat[2].p[1] * det2_45_05 + mat[2].p[5] * det2_45_01;
            float det3_245_023 = mat[2].p[0] * det2_45_23 - mat[2].p[2] * det2_45_03 + mat[2].p[3] * det2_45_02;
            float det3_245_024 = mat[2].p[0] * det2_45_24 - mat[2].p[2] * det2_45_04 + mat[2].p[4] * det2_45_02;
            float det3_245_025 = mat[2].p[0] * det2_45_25 - mat[2].p[2] * det2_45_05 + mat[2].p[5] * det2_45_02;
            float det3_245_034 = mat[2].p[0] * det2_45_34 - mat[2].p[3] * det2_45_04 + mat[2].p[4] * det2_45_03;
            float det3_245_035 = mat[2].p[0] * det2_45_35 - mat[2].p[3] * det2_45_05 + mat[2].p[5] * det2_45_03;
            float det3_245_045 = mat[2].p[0] * det2_45_45 - mat[2].p[4] * det2_45_05 + mat[2].p[5] * det2_45_04;
            float det3_245_123 = mat[2].p[1] * det2_45_23 - mat[2].p[2] * det2_45_13 + mat[2].p[3] * det2_45_12;
            float det3_245_124 = mat[2].p[1] * det2_45_24 - mat[2].p[2] * det2_45_14 + mat[2].p[4] * det2_45_12;
            float det3_245_125 = mat[2].p[1] * det2_45_25 - mat[2].p[2] * det2_45_15 + mat[2].p[5] * det2_45_12;
            float det3_245_134 = mat[2].p[1] * det2_45_34 - mat[2].p[3] * det2_45_14 + mat[2].p[4] * det2_45_13;
            float det3_245_135 = mat[2].p[1] * det2_45_35 - mat[2].p[3] * det2_45_15 + mat[2].p[5] * det2_45_13;
            float det3_245_145 = mat[2].p[1] * det2_45_45 - mat[2].p[4] * det2_45_15 + mat[2].p[5] * det2_45_14;
            float det3_245_234 = mat[2].p[2] * det2_45_34 - mat[2].p[3] * det2_45_24 + mat[2].p[4] * det2_45_23;
            float det3_245_235 = mat[2].p[2] * det2_45_35 - mat[2].p[3] * det2_45_25 + mat[2].p[5] * det2_45_23;
            float det3_245_245 = mat[2].p[2] * det2_45_45 - mat[2].p[4] * det2_45_25 + mat[2].p[5] * det2_45_24;
            float det3_245_345 = mat[2].p[3] * det2_45_45 - mat[2].p[4] * det2_45_35 + mat[2].p[5] * det2_45_34;
            // remaining 4x4 sub-determinants
            float det4_1234_0123 = mat[1].p[0] * det3_234_123 - mat[1].p[1] * det3_234_023 + mat[1].p[2] * det3_234_013 - mat[1].p[3] * det3_234_012;
            float det4_1234_0124 = mat[1].p[0] * det3_234_124 - mat[1].p[1] * det3_234_024 + mat[1].p[2] * det3_234_014 - mat[1].p[4] * det3_234_012;
            float det4_1234_0125 = mat[1].p[0] * det3_234_125 - mat[1].p[1] * det3_234_025 + mat[1].p[2] * det3_234_015 - mat[1].p[5] * det3_234_012;
            float det4_1234_0134 = mat[1].p[0] * det3_234_134 - mat[1].p[1] * det3_234_034 + mat[1].p[3] * det3_234_014 - mat[1].p[4] * det3_234_013;
            float det4_1234_0135 = mat[1].p[0] * det3_234_135 - mat[1].p[1] * det3_234_035 + mat[1].p[3] * det3_234_015 - mat[1].p[5] * det3_234_013;
            float det4_1234_0145 = mat[1].p[0] * det3_234_145 - mat[1].p[1] * det3_234_045 + mat[1].p[4] * det3_234_015 - mat[1].p[5] * det3_234_014;
            float det4_1234_0234 = mat[1].p[0] * det3_234_234 - mat[1].p[2] * det3_234_034 + mat[1].p[3] * det3_234_024 - mat[1].p[4] * det3_234_023;
            float det4_1234_0235 = mat[1].p[0] * det3_234_235 - mat[1].p[2] * det3_234_035 + mat[1].p[3] * det3_234_025 - mat[1].p[5] * det3_234_023;
            float det4_1234_0245 = mat[1].p[0] * det3_234_245 - mat[1].p[2] * det3_234_045 + mat[1].p[4] * det3_234_025 - mat[1].p[5] * det3_234_024;
            float det4_1234_0345 = mat[1].p[0] * det3_234_345 - mat[1].p[3] * det3_234_045 + mat[1].p[4] * det3_234_035 - mat[1].p[5] * det3_234_034;
            float det4_1234_1234 = mat[1].p[1] * det3_234_234 - mat[1].p[2] * det3_234_134 + mat[1].p[3] * det3_234_124 - mat[1].p[4] * det3_234_123;
            float det4_1234_1235 = mat[1].p[1] * det3_234_235 - mat[1].p[2] * det3_234_135 + mat[1].p[3] * det3_234_125 - mat[1].p[5] * det3_234_123;
            float det4_1234_1245 = mat[1].p[1] * det3_234_245 - mat[1].p[2] * det3_234_145 + mat[1].p[4] * det3_234_125 - mat[1].p[5] * det3_234_124;
            float det4_1234_1345 = mat[1].p[1] * det3_234_345 - mat[1].p[3] * det3_234_145 + mat[1].p[4] * det3_234_135 - mat[1].p[5] * det3_234_134;
            float det4_1234_2345 = mat[1].p[2] * det3_234_345 - mat[1].p[3] * det3_234_245 + mat[1].p[4] * det3_234_235 - mat[1].p[5] * det3_234_234;
            float det4_1235_0123 = mat[1].p[0] * det3_235_123 - mat[1].p[1] * det3_235_023 + mat[1].p[2] * det3_235_013 - mat[1].p[3] * det3_235_012;
            float det4_1235_0124 = mat[1].p[0] * det3_235_124 - mat[1].p[1] * det3_235_024 + mat[1].p[2] * det3_235_014 - mat[1].p[4] * det3_235_012;
            float det4_1235_0125 = mat[1].p[0] * det3_235_125 - mat[1].p[1] * det3_235_025 + mat[1].p[2] * det3_235_015 - mat[1].p[5] * det3_235_012;
            float det4_1235_0134 = mat[1].p[0] * det3_235_134 - mat[1].p[1] * det3_235_034 + mat[1].p[3] * det3_235_014 - mat[1].p[4] * det3_235_013;
            float det4_1235_0135 = mat[1].p[0] * det3_235_135 - mat[1].p[1] * det3_235_035 + mat[1].p[3] * det3_235_015 - mat[1].p[5] * det3_235_013;
            float det4_1235_0145 = mat[1].p[0] * det3_235_145 - mat[1].p[1] * det3_235_045 + mat[1].p[4] * det3_235_015 - mat[1].p[5] * det3_235_014;
            float det4_1235_0234 = mat[1].p[0] * det3_235_234 - mat[1].p[2] * det3_235_034 + mat[1].p[3] * det3_235_024 - mat[1].p[4] * det3_235_023;
            float det4_1235_0235 = mat[1].p[0] * det3_235_235 - mat[1].p[2] * det3_235_035 + mat[1].p[3] * det3_235_025 - mat[1].p[5] * det3_235_023;
            float det4_1235_0245 = mat[1].p[0] * det3_235_245 - mat[1].p[2] * det3_235_045 + mat[1].p[4] * det3_235_025 - mat[1].p[5] * det3_235_024;
            float det4_1235_0345 = mat[1].p[0] * det3_235_345 - mat[1].p[3] * det3_235_045 + mat[1].p[4] * det3_235_035 - mat[1].p[5] * det3_235_034;
            float det4_1235_1234 = mat[1].p[1] * det3_235_234 - mat[1].p[2] * det3_235_134 + mat[1].p[3] * det3_235_124 - mat[1].p[4] * det3_235_123;
            float det4_1235_1235 = mat[1].p[1] * det3_235_235 - mat[1].p[2] * det3_235_135 + mat[1].p[3] * det3_235_125 - mat[1].p[5] * det3_235_123;
            float det4_1235_1245 = mat[1].p[1] * det3_235_245 - mat[1].p[2] * det3_235_145 + mat[1].p[4] * det3_235_125 - mat[1].p[5] * det3_235_124;
            float det4_1235_1345 = mat[1].p[1] * det3_235_345 - mat[1].p[3] * det3_235_145 + mat[1].p[4] * det3_235_135 - mat[1].p[5] * det3_235_134;
            float det4_1235_2345 = mat[1].p[2] * det3_235_345 - mat[1].p[3] * det3_235_245 + mat[1].p[4] * det3_235_235 - mat[1].p[5] * det3_235_234;
            float det4_1245_0123 = mat[1].p[0] * det3_245_123 - mat[1].p[1] * det3_245_023 + mat[1].p[2] * det3_245_013 - mat[1].p[3] * det3_245_012;
            float det4_1245_0124 = mat[1].p[0] * det3_245_124 - mat[1].p[1] * det3_245_024 + mat[1].p[2] * det3_245_014 - mat[1].p[4] * det3_245_012;
            float det4_1245_0125 = mat[1].p[0] * det3_245_125 - mat[1].p[1] * det3_245_025 + mat[1].p[2] * det3_245_015 - mat[1].p[5] * det3_245_012;
            float det4_1245_0134 = mat[1].p[0] * det3_245_134 - mat[1].p[1] * det3_245_034 + mat[1].p[3] * det3_245_014 - mat[1].p[4] * det3_245_013;
            float det4_1245_0135 = mat[1].p[0] * det3_245_135 - mat[1].p[1] * det3_245_035 + mat[1].p[3] * det3_245_015 - mat[1].p[5] * det3_245_013;
            float det4_1245_0145 = mat[1].p[0] * det3_245_145 - mat[1].p[1] * det3_245_045 + mat[1].p[4] * det3_245_015 - mat[1].p[5] * det3_245_014;
            float det4_1245_0234 = mat[1].p[0] * det3_245_234 - mat[1].p[2] * det3_245_034 + mat[1].p[3] * det3_245_024 - mat[1].p[4] * det3_245_023;
            float det4_1245_0235 = mat[1].p[0] * det3_245_235 - mat[1].p[2] * det3_245_035 + mat[1].p[3] * det3_245_025 - mat[1].p[5] * det3_245_023;
            float det4_1245_0245 = mat[1].p[0] * det3_245_245 - mat[1].p[2] * det3_245_045 + mat[1].p[4] * det3_245_025 - mat[1].p[5] * det3_245_024;
            float det4_1245_0345 = mat[1].p[0] * det3_245_345 - mat[1].p[3] * det3_245_045 + mat[1].p[4] * det3_245_035 - mat[1].p[5] * det3_245_034;
            float det4_1245_1234 = mat[1].p[1] * det3_245_234 - mat[1].p[2] * det3_245_134 + mat[1].p[3] * det3_245_124 - mat[1].p[4] * det3_245_123;
            float det4_1245_1235 = mat[1].p[1] * det3_245_235 - mat[1].p[2] * det3_245_135 + mat[1].p[3] * det3_245_125 - mat[1].p[5] * det3_245_123;
            float det4_1245_1245 = mat[1].p[1] * det3_245_245 - mat[1].p[2] * det3_245_145 + mat[1].p[4] * det3_245_125 - mat[1].p[5] * det3_245_124;
            float det4_1245_1345 = mat[1].p[1] * det3_245_345 - mat[1].p[3] * det3_245_145 + mat[1].p[4] * det3_245_135 - mat[1].p[5] * det3_245_134;
            float det4_1245_2345 = mat[1].p[2] * det3_245_345 - mat[1].p[3] * det3_245_245 + mat[1].p[4] * det3_245_235 - mat[1].p[5] * det3_245_234;
            float det4_1345_0123 = mat[1].p[0] * det3_345_123 - mat[1].p[1] * det3_345_023 + mat[1].p[2] * det3_345_013 - mat[1].p[3] * det3_345_012;
            float det4_1345_0124 = mat[1].p[0] * det3_345_124 - mat[1].p[1] * det3_345_024 + mat[1].p[2] * det3_345_014 - mat[1].p[4] * det3_345_012;
            float det4_1345_0125 = mat[1].p[0] * det3_345_125 - mat[1].p[1] * det3_345_025 + mat[1].p[2] * det3_345_015 - mat[1].p[5] * det3_345_012;
            float det4_1345_0134 = mat[1].p[0] * det3_345_134 - mat[1].p[1] * det3_345_034 + mat[1].p[3] * det3_345_014 - mat[1].p[4] * det3_345_013;
            float det4_1345_0135 = mat[1].p[0] * det3_345_135 - mat[1].p[1] * det3_345_035 + mat[1].p[3] * det3_345_015 - mat[1].p[5] * det3_345_013;
            float det4_1345_0145 = mat[1].p[0] * det3_345_145 - mat[1].p[1] * det3_345_045 + mat[1].p[4] * det3_345_015 - mat[1].p[5] * det3_345_014;
            float det4_1345_0234 = mat[1].p[0] * det3_345_234 - mat[1].p[2] * det3_345_034 + mat[1].p[3] * det3_345_024 - mat[1].p[4] * det3_345_023;
            float det4_1345_0235 = mat[1].p[0] * det3_345_235 - mat[1].p[2] * det3_345_035 + mat[1].p[3] * det3_345_025 - mat[1].p[5] * det3_345_023;
            float det4_1345_0245 = mat[1].p[0] * det3_345_245 - mat[1].p[2] * det3_345_045 + mat[1].p[4] * det3_345_025 - mat[1].p[5] * det3_345_024;
            float det4_1345_0345 = mat[1].p[0] * det3_345_345 - mat[1].p[3] * det3_345_045 + mat[1].p[4] * det3_345_035 - mat[1].p[5] * det3_345_034;
            float det4_1345_1234 = mat[1].p[1] * det3_345_234 - mat[1].p[2] * det3_345_134 + mat[1].p[3] * det3_345_124 - mat[1].p[4] * det3_345_123;
            float det4_1345_1235 = mat[1].p[1] * det3_345_235 - mat[1].p[2] * det3_345_135 + mat[1].p[3] * det3_345_125 - mat[1].p[5] * det3_345_123;
            float det4_1345_1245 = mat[1].p[1] * det3_345_245 - mat[1].p[2] * det3_345_145 + mat[1].p[4] * det3_345_125 - mat[1].p[5] * det3_345_124;
            float det4_1345_1345 = mat[1].p[1] * det3_345_345 - mat[1].p[3] * det3_345_145 + mat[1].p[4] * det3_345_135 - mat[1].p[5] * det3_345_134;
            float det4_1345_2345 = mat[1].p[2] * det3_345_345 - mat[1].p[3] * det3_345_245 + mat[1].p[4] * det3_345_235 - mat[1].p[5] * det3_345_234;
            // remaining 5x5 sub-determinants
            float det5_01234_01234 = mat[0].p[0] * det4_1234_1234 - mat[0].p[1] * det4_1234_0234 + mat[0].p[2] * det4_1234_0134 - mat[0].p[3] * det4_1234_0124 + mat[0].p[4] * det4_1234_0123;
            float det5_01234_01235 = mat[0].p[0] * det4_1234_1235 - mat[0].p[1] * det4_1234_0235 + mat[0].p[2] * det4_1234_0135 - mat[0].p[3] * det4_1234_0125 + mat[0].p[5] * det4_1234_0123;
            float det5_01234_01245 = mat[0].p[0] * det4_1234_1245 - mat[0].p[1] * det4_1234_0245 + mat[0].p[2] * det4_1234_0145 - mat[0].p[4] * det4_1234_0125 + mat[0].p[5] * det4_1234_0124;
            float det5_01234_01345 = mat[0].p[0] * det4_1234_1345 - mat[0].p[1] * det4_1234_0345 + mat[0].p[3] * det4_1234_0145 - mat[0].p[4] * det4_1234_0135 + mat[0].p[5] * det4_1234_0134;
            float det5_01234_02345 = mat[0].p[0] * det4_1234_2345 - mat[0].p[2] * det4_1234_0345 + mat[0].p[3] * det4_1234_0245 - mat[0].p[4] * det4_1234_0235 + mat[0].p[5] * det4_1234_0234;
            float det5_01234_12345 = mat[0].p[1] * det4_1234_2345 - mat[0].p[2] * det4_1234_1345 + mat[0].p[3] * det4_1234_1245 - mat[0].p[4] * det4_1234_1235 + mat[0].p[5] * det4_1234_1234;
            float det5_01235_01234 = mat[0].p[0] * det4_1235_1234 - mat[0].p[1] * det4_1235_0234 + mat[0].p[2] * det4_1235_0134 - mat[0].p[3] * det4_1235_0124 + mat[0].p[4] * det4_1235_0123;
            float det5_01235_01235 = mat[0].p[0] * det4_1235_1235 - mat[0].p[1] * det4_1235_0235 + mat[0].p[2] * det4_1235_0135 - mat[0].p[3] * det4_1235_0125 + mat[0].p[5] * det4_1235_0123;
            float det5_01235_01245 = mat[0].p[0] * det4_1235_1245 - mat[0].p[1] * det4_1235_0245 + mat[0].p[2] * det4_1235_0145 - mat[0].p[4] * det4_1235_0125 + mat[0].p[5] * det4_1235_0124;
            float det5_01235_01345 = mat[0].p[0] * det4_1235_1345 - mat[0].p[1] * det4_1235_0345 + mat[0].p[3] * det4_1235_0145 - mat[0].p[4] * det4_1235_0135 + mat[0].p[5] * det4_1235_0134;
            float det5_01235_02345 = mat[0].p[0] * det4_1235_2345 - mat[0].p[2] * det4_1235_0345 + mat[0].p[3] * det4_1235_0245 - mat[0].p[4] * det4_1235_0235 + mat[0].p[5] * det4_1235_0234;
            float det5_01235_12345 = mat[0].p[1] * det4_1235_2345 - mat[0].p[2] * det4_1235_1345 + mat[0].p[3] * det4_1235_1245 - mat[0].p[4] * det4_1235_1235 + mat[0].p[5] * det4_1235_1234;
            float det5_01245_01234 = mat[0].p[0] * det4_1245_1234 - mat[0].p[1] * det4_1245_0234 + mat[0].p[2] * det4_1245_0134 - mat[0].p[3] * det4_1245_0124 + mat[0].p[4] * det4_1245_0123;
            float det5_01245_01235 = mat[0].p[0] * det4_1245_1235 - mat[0].p[1] * det4_1245_0235 + mat[0].p[2] * det4_1245_0135 - mat[0].p[3] * det4_1245_0125 + mat[0].p[5] * det4_1245_0123;
            float det5_01245_01245 = mat[0].p[0] * det4_1245_1245 - mat[0].p[1] * det4_1245_0245 + mat[0].p[2] * det4_1245_0145 - mat[0].p[4] * det4_1245_0125 + mat[0].p[5] * det4_1245_0124;
            float det5_01245_01345 = mat[0].p[0] * det4_1245_1345 - mat[0].p[1] * det4_1245_0345 + mat[0].p[3] * det4_1245_0145 - mat[0].p[4] * det4_1245_0135 + mat[0].p[5] * det4_1245_0134;
            float det5_01245_02345 = mat[0].p[0] * det4_1245_2345 - mat[0].p[2] * det4_1245_0345 + mat[0].p[3] * det4_1245_0245 - mat[0].p[4] * det4_1245_0235 + mat[0].p[5] * det4_1245_0234;
            float det5_01245_12345 = mat[0].p[1] * det4_1245_2345 - mat[0].p[2] * det4_1245_1345 + mat[0].p[3] * det4_1245_1245 - mat[0].p[4] * det4_1245_1235 + mat[0].p[5] * det4_1245_1234;
            float det5_01345_01234 = mat[0].p[0] * det4_1345_1234 - mat[0].p[1] * det4_1345_0234 + mat[0].p[2] * det4_1345_0134 - mat[0].p[3] * det4_1345_0124 + mat[0].p[4] * det4_1345_0123;
            float det5_01345_01235 = mat[0].p[0] * det4_1345_1235 - mat[0].p[1] * det4_1345_0235 + mat[0].p[2] * det4_1345_0135 - mat[0].p[3] * det4_1345_0125 + mat[0].p[5] * det4_1345_0123;
            float det5_01345_01245 = mat[0].p[0] * det4_1345_1245 - mat[0].p[1] * det4_1345_0245 + mat[0].p[2] * det4_1345_0145 - mat[0].p[4] * det4_1345_0125 + mat[0].p[5] * det4_1345_0124;
            float det5_01345_01345 = mat[0].p[0] * det4_1345_1345 - mat[0].p[1] * det4_1345_0345 + mat[0].p[3] * det4_1345_0145 - mat[0].p[4] * det4_1345_0135 + mat[0].p[5] * det4_1345_0134;
            float det5_01345_02345 = mat[0].p[0] * det4_1345_2345 - mat[0].p[2] * det4_1345_0345 + mat[0].p[3] * det4_1345_0245 - mat[0].p[4] * det4_1345_0235 + mat[0].p[5] * det4_1345_0234;
            float det5_01345_12345 = mat[0].p[1] * det4_1345_2345 - mat[0].p[2] * det4_1345_1345 + mat[0].p[3] * det4_1345_1245 - mat[0].p[4] * det4_1345_1235 + mat[0].p[5] * det4_1345_1234;
            float det5_02345_01234 = mat[0].p[0] * det4_2345_1234 - mat[0].p[1] * det4_2345_0234 + mat[0].p[2] * det4_2345_0134 - mat[0].p[3] * det4_2345_0124 + mat[0].p[4] * det4_2345_0123;
            float det5_02345_01235 = mat[0].p[0] * det4_2345_1235 - mat[0].p[1] * det4_2345_0235 + mat[0].p[2] * det4_2345_0135 - mat[0].p[3] * det4_2345_0125 + mat[0].p[5] * det4_2345_0123;
            float det5_02345_01245 = mat[0].p[0] * det4_2345_1245 - mat[0].p[1] * det4_2345_0245 + mat[0].p[2] * det4_2345_0145 - mat[0].p[4] * det4_2345_0125 + mat[0].p[5] * det4_2345_0124;
            float det5_02345_01345 = mat[0].p[0] * det4_2345_1345 - mat[0].p[1] * det4_2345_0345 + mat[0].p[3] * det4_2345_0145 - mat[0].p[4] * det4_2345_0135 + mat[0].p[5] * det4_2345_0134;
            float det5_02345_02345 = mat[0].p[0] * det4_2345_2345 - mat[0].p[2] * det4_2345_0345 + mat[0].p[3] * det4_2345_0245 - mat[0].p[4] * det4_2345_0235 + mat[0].p[5] * det4_2345_0234;
            float det5_02345_12345 = mat[0].p[1] * det4_2345_2345 - mat[0].p[2] * det4_2345_1345 + mat[0].p[3] * det4_2345_1245 - mat[0].p[4] * det4_2345_1235 + mat[0].p[5] * det4_2345_1234;
            //
            mat[0].p[0] = (float)(det5_12345_12345 * invDet);
            mat[0].p[1] = (float)(-det5_02345_12345 * invDet);
            mat[0].p[2] = (float)(det5_01345_12345 * invDet);
            mat[0].p[3] = (float)(-det5_01245_12345 * invDet);
            mat[0].p[4] = (float)(det5_01235_12345 * invDet);
            mat[0].p[5] = (float)(-det5_01234_12345 * invDet);
            //
            mat[1].p[0] = (float)(-det5_12345_02345 * invDet);
            mat[1].p[1] = (float)(det5_02345_02345 * invDet);
            mat[1].p[2] = (float)(-det5_01345_02345 * invDet);
            mat[1].p[3] = (float)(det5_01245_02345 * invDet);
            mat[1].p[4] = (float)(-det5_01235_02345 * invDet);
            mat[1].p[5] = (float)(det5_01234_02345 * invDet);
            //
            mat[2].p[0] = (float)(det5_12345_01345 * invDet);
            mat[2].p[1] = (float)(-det5_02345_01345 * invDet);
            mat[2].p[2] = (float)(det5_01345_01345 * invDet);
            mat[2].p[3] = (float)(-det5_01245_01345 * invDet);
            mat[2].p[4] = (float)(det5_01235_01345 * invDet);
            mat[2].p[5] = (float)(-det5_01234_01345 * invDet);
            //
            mat[3].p[0] = (float)(-det5_12345_01245 * invDet);
            mat[3].p[1] = (float)(det5_02345_01245 * invDet);
            mat[3].p[2] = (float)(-det5_01345_01245 * invDet);
            mat[3].p[3] = (float)(det5_01245_01245 * invDet);
            mat[3].p[4] = (float)(-det5_01235_01245 * invDet);
            mat[3].p[5] = (float)(det5_01234_01245 * invDet);
            //
            mat[4].p[0] = (float)(det5_12345_01235 * invDet);
            mat[4].p[1] = (float)(-det5_02345_01235 * invDet);
            mat[4].p[2] = (float)(det5_01345_01235 * invDet);
            mat[4].p[3] = (float)(-det5_01245_01235 * invDet);
            mat[4].p[4] = (float)(det5_01235_01235 * invDet);
            mat[4].p[5] = (float)(-det5_01234_01235 * invDet);
            //
            mat[5].p[0] = (float)(-det5_12345_01234 * invDet);
            mat[5].p[1] = (float)(det5_02345_01234 * invDet);
            mat[5].p[2] = (float)(-det5_01345_01234 * invDet);
            mat[5].p[3] = (float)(det5_01245_01234 * invDet);
            mat[5].p[4] = (float)(-det5_01235_01234 * invDet);
            mat[5].p[5] = (float)(det5_01234_01234 * invDet);
            return true;
        }
        public idMat6 InverseFast()
        {
            idMat6 invMat = this;
            bool r = invMat.InverseFastSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseFastSelf()
        {
#if false
            // 810+6+36 = 852 multiplications
            //				1 division
            // 2x2 sub-determinants required to calculate 6x6 determinant
            float det2_45_01 = mat[4].p[0] * mat[5].p[1] - mat[4].p[1] * mat[5].p[0];
            float det2_45_02 = mat[4].p[0] * mat[5].p[2] - mat[4].p[2] * mat[5].p[0];
            float det2_45_03 = mat[4].p[0] * mat[5].p[3] - mat[4].p[3] * mat[5].p[0];
            float det2_45_04 = mat[4].p[0] * mat[5].p[4] - mat[4].p[4] * mat[5].p[0];
            float det2_45_05 = mat[4].p[0] * mat[5].p[5] - mat[4].p[5] * mat[5].p[0];
            float det2_45_12 = mat[4].p[1] * mat[5].p[2] - mat[4].p[2] * mat[5].p[1];
            float det2_45_13 = mat[4].p[1] * mat[5].p[3] - mat[4].p[3] * mat[5].p[1];
            float det2_45_14 = mat[4].p[1] * mat[5].p[4] - mat[4].p[4] * mat[5].p[1];
            float det2_45_15 = mat[4].p[1] * mat[5].p[5] - mat[4].p[5] * mat[5].p[1];
            float det2_45_23 = mat[4].p[2] * mat[5].p[3] - mat[4].p[3] * mat[5].p[2];
            float det2_45_24 = mat[4].p[2] * mat[5].p[4] - mat[4].p[4] * mat[5].p[2];
            float det2_45_25 = mat[4].p[2] * mat[5].p[5] - mat[4].p[5] * mat[5].p[2];
            float det2_45_34 = mat[4].p[3] * mat[5].p[4] - mat[4].p[4] * mat[5].p[3];
            float det2_45_35 = mat[4].p[3] * mat[5].p[5] - mat[4].p[5] * mat[5].p[3];
            float det2_45_45 = mat[4].p[4] * mat[5].p[5] - mat[4].p[5] * mat[5].p[4];
            // 3x3 sub-determinants required to calculate 6x6 determinant
            float det3_345_012 = mat[3].p[0] * det2_45_12 - mat[3].p[1] * det2_45_02 + mat[3].p[2] * det2_45_01;
            float det3_345_013 = mat[3].p[0] * det2_45_13 - mat[3].p[1] * det2_45_03 + mat[3].p[3] * det2_45_01;
            float det3_345_014 = mat[3].p[0] * det2_45_14 - mat[3].p[1] * det2_45_04 + mat[3].p[4] * det2_45_01;
            float det3_345_015 = mat[3].p[0] * det2_45_15 - mat[3].p[1] * det2_45_05 + mat[3].p[5] * det2_45_01;
            float det3_345_023 = mat[3].p[0] * det2_45_23 - mat[3].p[2] * det2_45_03 + mat[3].p[3] * det2_45_02;
            float det3_345_024 = mat[3].p[0] * det2_45_24 - mat[3].p[2] * det2_45_04 + mat[3].p[4] * det2_45_02;
            float det3_345_025 = mat[3].p[0] * det2_45_25 - mat[3].p[2] * det2_45_05 + mat[3].p[5] * det2_45_02;
            float det3_345_034 = mat[3].p[0] * det2_45_34 - mat[3].p[3] * det2_45_04 + mat[3].p[4] * det2_45_03;
            float det3_345_035 = mat[3].p[0] * det2_45_35 - mat[3].p[3] * det2_45_05 + mat[3].p[5] * det2_45_03;
            float det3_345_045 = mat[3].p[0] * det2_45_45 - mat[3].p[4] * det2_45_05 + mat[3].p[5] * det2_45_04;
            float det3_345_123 = mat[3].p[1] * det2_45_23 - mat[3].p[2] * det2_45_13 + mat[3].p[3] * det2_45_12;
            float det3_345_124 = mat[3].p[1] * det2_45_24 - mat[3].p[2] * det2_45_14 + mat[3].p[4] * det2_45_12;
            float det3_345_125 = mat[3].p[1] * det2_45_25 - mat[3].p[2] * det2_45_15 + mat[3].p[5] * det2_45_12;
            float det3_345_134 = mat[3].p[1] * det2_45_34 - mat[3].p[3] * det2_45_14 + mat[3].p[4] * det2_45_13;
            float det3_345_135 = mat[3].p[1] * det2_45_35 - mat[3].p[3] * det2_45_15 + mat[3].p[5] * det2_45_13;
            float det3_345_145 = mat[3].p[1] * det2_45_45 - mat[3].p[4] * det2_45_15 + mat[3].p[5] * det2_45_14;
            float det3_345_234 = mat[3].p[2] * det2_45_34 - mat[3].p[3] * det2_45_24 + mat[3].p[4] * det2_45_23;
            float det3_345_235 = mat[3].p[2] * det2_45_35 - mat[3].p[3] * det2_45_25 + mat[3].p[5] * det2_45_23;
            float det3_345_245 = mat[3].p[2] * det2_45_45 - mat[3].p[4] * det2_45_25 + mat[3].p[5] * det2_45_24;
            float det3_345_345 = mat[3].p[3] * det2_45_45 - mat[3].p[4] * det2_45_35 + mat[3].p[5] * det2_45_34;
            // 4x4 sub-determinants required to calculate 6x6 determinant
            float det4_2345_0123 = mat[2].p[0] * det3_345_123 - mat[2].p[1] * det3_345_023 + mat[2].p[2] * det3_345_013 - mat[2].p[3] * det3_345_012;
            float det4_2345_0124 = mat[2].p[0] * det3_345_124 - mat[2].p[1] * det3_345_024 + mat[2].p[2] * det3_345_014 - mat[2].p[4] * det3_345_012;
            float det4_2345_0125 = mat[2].p[0] * det3_345_125 - mat[2].p[1] * det3_345_025 + mat[2].p[2] * det3_345_015 - mat[2].p[5] * det3_345_012;
            float det4_2345_0134 = mat[2].p[0] * det3_345_134 - mat[2].p[1] * det3_345_034 + mat[2].p[3] * det3_345_014 - mat[2].p[4] * det3_345_013;
            float det4_2345_0135 = mat[2].p[0] * det3_345_135 - mat[2].p[1] * det3_345_035 + mat[2].p[3] * det3_345_015 - mat[2].p[5] * det3_345_013;
            float det4_2345_0145 = mat[2].p[0] * det3_345_145 - mat[2].p[1] * det3_345_045 + mat[2].p[4] * det3_345_015 - mat[2].p[5] * det3_345_014;
            float det4_2345_0234 = mat[2].p[0] * det3_345_234 - mat[2].p[2] * det3_345_034 + mat[2].p[3] * det3_345_024 - mat[2].p[4] * det3_345_023;
            float det4_2345_0235 = mat[2].p[0] * det3_345_235 - mat[2].p[2] * det3_345_035 + mat[2].p[3] * det3_345_025 - mat[2].p[5] * det3_345_023;
            float det4_2345_0245 = mat[2].p[0] * det3_345_245 - mat[2].p[2] * det3_345_045 + mat[2].p[4] * det3_345_025 - mat[2].p[5] * det3_345_024;
            float det4_2345_0345 = mat[2].p[0] * det3_345_345 - mat[2].p[3] * det3_345_045 + mat[2].p[4] * det3_345_035 - mat[2].p[5] * det3_345_034;
            float det4_2345_1234 = mat[2].p[1] * det3_345_234 - mat[2].p[2] * det3_345_134 + mat[2].p[3] * det3_345_124 - mat[2].p[4] * det3_345_123;
            float det4_2345_1235 = mat[2].p[1] * det3_345_235 - mat[2].p[2] * det3_345_135 + mat[2].p[3] * det3_345_125 - mat[2].p[5] * det3_345_123;
            float det4_2345_1245 = mat[2].p[1] * det3_345_245 - mat[2].p[2] * det3_345_145 + mat[2].p[4] * det3_345_125 - mat[2].p[5] * det3_345_124;
            float det4_2345_1345 = mat[2].p[1] * det3_345_345 - mat[2].p[3] * det3_345_145 + mat[2].p[4] * det3_345_135 - mat[2].p[5] * det3_345_134;
            float det4_2345_2345 = mat[2].p[2] * det3_345_345 - mat[2].p[3] * det3_345_245 + mat[2].p[4] * det3_345_235 - mat[2].p[5] * det3_345_234;
            // 5x5 sub-determinants required to calculate 6x6 determinant
            float det5_12345_01234 = mat[1].p[0] * det4_2345_1234 - mat[1].p[1] * det4_2345_0234 + mat[1].p[2] * det4_2345_0134 - mat[1].p[3] * det4_2345_0124 + mat[1].p[4] * det4_2345_0123;
            float det5_12345_01235 = mat[1].p[0] * det4_2345_1235 - mat[1].p[1] * det4_2345_0235 + mat[1].p[2] * det4_2345_0135 - mat[1].p[3] * det4_2345_0125 + mat[1].p[5] * det4_2345_0123;
            float det5_12345_01245 = mat[1].p[0] * det4_2345_1245 - mat[1].p[1] * det4_2345_0245 + mat[1].p[2] * det4_2345_0145 - mat[1].p[4] * det4_2345_0125 + mat[1].p[5] * det4_2345_0124;
            float det5_12345_01345 = mat[1].p[0] * det4_2345_1345 - mat[1].p[1] * det4_2345_0345 + mat[1].p[3] * det4_2345_0145 - mat[1].p[4] * det4_2345_0135 + mat[1].p[5] * det4_2345_0134;
            float det5_12345_02345 = mat[1].p[0] * det4_2345_2345 - mat[1].p[2] * det4_2345_0345 + mat[1].p[3] * det4_2345_0245 - mat[1].p[4] * det4_2345_0235 + mat[1].p[5] * det4_2345_0234;
            float det5_12345_12345 = mat[1].p[1] * det4_2345_2345 - mat[1].p[2] * det4_2345_1345 + mat[1].p[3] * det4_2345_1245 - mat[1].p[4] * det4_2345_1235 + mat[1].p[5] * det4_2345_1234;
            // determinant of 6x6 matrix
            double det = mat[0].p[0] * det5_12345_12345 - mat[0].p[1] * det5_12345_02345 + mat[0].p[2] * det5_12345_01345 - mat[0].p[3] * det5_12345_01245 + mat[0].p[4] * det5_12345_01235 - mat[0].p[5] * det5_12345_01234;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            double invDet = 1.0f / det;
            // remaining 2x2 sub-determinants
            float det2_34_01 = mat[3].p[0] * mat[4].p[1] - mat[3].p[1] * mat[4].p[0];
            float det2_34_02 = mat[3].p[0] * mat[4].p[2] - mat[3].p[2] * mat[4].p[0];
            float det2_34_03 = mat[3].p[0] * mat[4].p[3] - mat[3].p[3] * mat[4].p[0];
            float det2_34_04 = mat[3].p[0] * mat[4].p[4] - mat[3].p[4] * mat[4].p[0];
            float det2_34_05 = mat[3].p[0] * mat[4].p[5] - mat[3].p[5] * mat[4].p[0];
            float det2_34_12 = mat[3].p[1] * mat[4].p[2] - mat[3].p[2] * mat[4].p[1];
            float det2_34_13 = mat[3].p[1] * mat[4].p[3] - mat[3].p[3] * mat[4].p[1];
            float det2_34_14 = mat[3].p[1] * mat[4].p[4] - mat[3].p[4] * mat[4].p[1];
            float det2_34_15 = mat[3].p[1] * mat[4].p[5] - mat[3].p[5] * mat[4].p[1];
            float det2_34_23 = mat[3].p[2] * mat[4].p[3] - mat[3].p[3] * mat[4].p[2];
            float det2_34_24 = mat[3].p[2] * mat[4].p[4] - mat[3].p[4] * mat[4].p[2];
            float det2_34_25 = mat[3].p[2] * mat[4].p[5] - mat[3].p[5] * mat[4].p[2];
            float det2_34_34 = mat[3].p[3] * mat[4].p[4] - mat[3].p[4] * mat[4].p[3];
            float det2_34_35 = mat[3].p[3] * mat[4].p[5] - mat[3].p[5] * mat[4].p[3];
            float det2_34_45 = mat[3].p[4] * mat[4].p[5] - mat[3].p[5] * mat[4].p[4];
            float det2_35_01 = mat[3].p[0] * mat[5].p[1] - mat[3].p[1] * mat[5].p[0];
            float det2_35_02 = mat[3].p[0] * mat[5].p[2] - mat[3].p[2] * mat[5].p[0];
            float det2_35_03 = mat[3].p[0] * mat[5].p[3] - mat[3].p[3] * mat[5].p[0];
            float det2_35_04 = mat[3].p[0] * mat[5].p[4] - mat[3].p[4] * mat[5].p[0];
            float det2_35_05 = mat[3].p[0] * mat[5].p[5] - mat[3].p[5] * mat[5].p[0];
            float det2_35_12 = mat[3].p[1] * mat[5].p[2] - mat[3].p[2] * mat[5].p[1];
            float det2_35_13 = mat[3].p[1] * mat[5].p[3] - mat[3].p[3] * mat[5].p[1];
            float det2_35_14 = mat[3].p[1] * mat[5].p[4] - mat[3].p[4] * mat[5].p[1];
            float det2_35_15 = mat[3].p[1] * mat[5].p[5] - mat[3].p[5] * mat[5].p[1];
            float det2_35_23 = mat[3].p[2] * mat[5].p[3] - mat[3].p[3] * mat[5].p[2];
            float det2_35_24 = mat[3].p[2] * mat[5].p[4] - mat[3].p[4] * mat[5].p[2];
            float det2_35_25 = mat[3].p[2] * mat[5].p[5] - mat[3].p[5] * mat[5].p[2];
            float det2_35_34 = mat[3].p[3] * mat[5].p[4] - mat[3].p[4] * mat[5].p[3];
            float det2_35_35 = mat[3].p[3] * mat[5].p[5] - mat[3].p[5] * mat[5].p[3];
            float det2_35_45 = mat[3].p[4] * mat[5].p[5] - mat[3].p[5] * mat[5].p[4];
            // remaining 3x3 sub-determinants
            float det3_234_012 = mat[2].p[0] * det2_34_12 - mat[2].p[1] * det2_34_02 + mat[2].p[2] * det2_34_01;
            float det3_234_013 = mat[2].p[0] * det2_34_13 - mat[2].p[1] * det2_34_03 + mat[2].p[3] * det2_34_01;
            float det3_234_014 = mat[2].p[0] * det2_34_14 - mat[2].p[1] * det2_34_04 + mat[2].p[4] * det2_34_01;
            float det3_234_015 = mat[2].p[0] * det2_34_15 - mat[2].p[1] * det2_34_05 + mat[2].p[5] * det2_34_01;
            float det3_234_023 = mat[2].p[0] * det2_34_23 - mat[2].p[2] * det2_34_03 + mat[2].p[3] * det2_34_02;
            float det3_234_024 = mat[2].p[0] * det2_34_24 - mat[2].p[2] * det2_34_04 + mat[2].p[4] * det2_34_02;
            float det3_234_025 = mat[2].p[0] * det2_34_25 - mat[2].p[2] * det2_34_05 + mat[2].p[5] * det2_34_02;
            float det3_234_034 = mat[2].p[0] * det2_34_34 - mat[2].p[3] * det2_34_04 + mat[2].p[4] * det2_34_03;
            float det3_234_035 = mat[2].p[0] * det2_34_35 - mat[2].p[3] * det2_34_05 + mat[2].p[5] * det2_34_03;
            float det3_234_045 = mat[2].p[0] * det2_34_45 - mat[2].p[4] * det2_34_05 + mat[2].p[5] * det2_34_04;
            float det3_234_123 = mat[2].p[1] * det2_34_23 - mat[2].p[2] * det2_34_13 + mat[2].p[3] * det2_34_12;
            float det3_234_124 = mat[2].p[1] * det2_34_24 - mat[2].p[2] * det2_34_14 + mat[2].p[4] * det2_34_12;
            float det3_234_125 = mat[2].p[1] * det2_34_25 - mat[2].p[2] * det2_34_15 + mat[2].p[5] * det2_34_12;
            float det3_234_134 = mat[2].p[1] * det2_34_34 - mat[2].p[3] * det2_34_14 + mat[2].p[4] * det2_34_13;
            float det3_234_135 = mat[2].p[1] * det2_34_35 - mat[2].p[3] * det2_34_15 + mat[2].p[5] * det2_34_13;
            float det3_234_145 = mat[2].p[1] * det2_34_45 - mat[2].p[4] * det2_34_15 + mat[2].p[5] * det2_34_14;
            float det3_234_234 = mat[2].p[2] * det2_34_34 - mat[2].p[3] * det2_34_24 + mat[2].p[4] * det2_34_23;
            float det3_234_235 = mat[2].p[2] * det2_34_35 - mat[2].p[3] * det2_34_25 + mat[2].p[5] * det2_34_23;
            float det3_234_245 = mat[2].p[2] * det2_34_45 - mat[2].p[4] * det2_34_25 + mat[2].p[5] * det2_34_24;
            float det3_234_345 = mat[2].p[3] * det2_34_45 - mat[2].p[4] * det2_34_35 + mat[2].p[5] * det2_34_34;
            float det3_235_012 = mat[2].p[0] * det2_35_12 - mat[2].p[1] * det2_35_02 + mat[2].p[2] * det2_35_01;
            float det3_235_013 = mat[2].p[0] * det2_35_13 - mat[2].p[1] * det2_35_03 + mat[2].p[3] * det2_35_01;
            float det3_235_014 = mat[2].p[0] * det2_35_14 - mat[2].p[1] * det2_35_04 + mat[2].p[4] * det2_35_01;
            float det3_235_015 = mat[2].p[0] * det2_35_15 - mat[2].p[1] * det2_35_05 + mat[2].p[5] * det2_35_01;
            float det3_235_023 = mat[2].p[0] * det2_35_23 - mat[2].p[2] * det2_35_03 + mat[2].p[3] * det2_35_02;
            float det3_235_024 = mat[2].p[0] * det2_35_24 - mat[2].p[2] * det2_35_04 + mat[2].p[4] * det2_35_02;
            float det3_235_025 = mat[2].p[0] * det2_35_25 - mat[2].p[2] * det2_35_05 + mat[2].p[5] * det2_35_02;
            float det3_235_034 = mat[2].p[0] * det2_35_34 - mat[2].p[3] * det2_35_04 + mat[2].p[4] * det2_35_03;
            float det3_235_035 = mat[2].p[0] * det2_35_35 - mat[2].p[3] * det2_35_05 + mat[2].p[5] * det2_35_03;
            float det3_235_045 = mat[2].p[0] * det2_35_45 - mat[2].p[4] * det2_35_05 + mat[2].p[5] * det2_35_04;
            float det3_235_123 = mat[2].p[1] * det2_35_23 - mat[2].p[2] * det2_35_13 + mat[2].p[3] * det2_35_12;
            float det3_235_124 = mat[2].p[1] * det2_35_24 - mat[2].p[2] * det2_35_14 + mat[2].p[4] * det2_35_12;
            float det3_235_125 = mat[2].p[1] * det2_35_25 - mat[2].p[2] * det2_35_15 + mat[2].p[5] * det2_35_12;
            float det3_235_134 = mat[2].p[1] * det2_35_34 - mat[2].p[3] * det2_35_14 + mat[2].p[4] * det2_35_13;
            float det3_235_135 = mat[2].p[1] * det2_35_35 - mat[2].p[3] * det2_35_15 + mat[2].p[5] * det2_35_13;
            float det3_235_145 = mat[2].p[1] * det2_35_45 - mat[2].p[4] * det2_35_15 + mat[2].p[5] * det2_35_14;
            float det3_235_234 = mat[2].p[2] * det2_35_34 - mat[2].p[3] * det2_35_24 + mat[2].p[4] * det2_35_23;
            float det3_235_235 = mat[2].p[2] * det2_35_35 - mat[2].p[3] * det2_35_25 + mat[2].p[5] * det2_35_23;
            float det3_235_245 = mat[2].p[2] * det2_35_45 - mat[2].p[4] * det2_35_25 + mat[2].p[5] * det2_35_24;
            float det3_235_345 = mat[2].p[3] * det2_35_45 - mat[2].p[4] * det2_35_35 + mat[2].p[5] * det2_35_34;
            float det3_245_012 = mat[2].p[0] * det2_45_12 - mat[2].p[1] * det2_45_02 + mat[2].p[2] * det2_45_01;
            float det3_245_013 = mat[2].p[0] * det2_45_13 - mat[2].p[1] * det2_45_03 + mat[2].p[3] * det2_45_01;
            float det3_245_014 = mat[2].p[0] * det2_45_14 - mat[2].p[1] * det2_45_04 + mat[2].p[4] * det2_45_01;
            float det3_245_015 = mat[2].p[0] * det2_45_15 - mat[2].p[1] * det2_45_05 + mat[2].p[5] * det2_45_01;
            float det3_245_023 = mat[2].p[0] * det2_45_23 - mat[2].p[2] * det2_45_03 + mat[2].p[3] * det2_45_02;
            float det3_245_024 = mat[2].p[0] * det2_45_24 - mat[2].p[2] * det2_45_04 + mat[2].p[4] * det2_45_02;
            float det3_245_025 = mat[2].p[0] * det2_45_25 - mat[2].p[2] * det2_45_05 + mat[2].p[5] * det2_45_02;
            float det3_245_034 = mat[2].p[0] * det2_45_34 - mat[2].p[3] * det2_45_04 + mat[2].p[4] * det2_45_03;
            float det3_245_035 = mat[2].p[0] * det2_45_35 - mat[2].p[3] * det2_45_05 + mat[2].p[5] * det2_45_03;
            float det3_245_045 = mat[2].p[0] * det2_45_45 - mat[2].p[4] * det2_45_05 + mat[2].p[5] * det2_45_04;
            float det3_245_123 = mat[2].p[1] * det2_45_23 - mat[2].p[2] * det2_45_13 + mat[2].p[3] * det2_45_12;
            float det3_245_124 = mat[2].p[1] * det2_45_24 - mat[2].p[2] * det2_45_14 + mat[2].p[4] * det2_45_12;
            float det3_245_125 = mat[2].p[1] * det2_45_25 - mat[2].p[2] * det2_45_15 + mat[2].p[5] * det2_45_12;
            float det3_245_134 = mat[2].p[1] * det2_45_34 - mat[2].p[3] * det2_45_14 + mat[2].p[4] * det2_45_13;
            float det3_245_135 = mat[2].p[1] * det2_45_35 - mat[2].p[3] * det2_45_15 + mat[2].p[5] * det2_45_13;
            float det3_245_145 = mat[2].p[1] * det2_45_45 - mat[2].p[4] * det2_45_15 + mat[2].p[5] * det2_45_14;
            float det3_245_234 = mat[2].p[2] * det2_45_34 - mat[2].p[3] * det2_45_24 + mat[2].p[4] * det2_45_23;
            float det3_245_235 = mat[2].p[2] * det2_45_35 - mat[2].p[3] * det2_45_25 + mat[2].p[5] * det2_45_23;
            float det3_245_245 = mat[2].p[2] * det2_45_45 - mat[2].p[4] * det2_45_25 + mat[2].p[5] * det2_45_24;
            float det3_245_345 = mat[2].p[3] * det2_45_45 - mat[2].p[4] * det2_45_35 + mat[2].p[5] * det2_45_34;
            // remaining 4x4 sub-determinants
            float det4_1234_0123 = mat[1].p[0] * det3_234_123 - mat[1].p[1] * det3_234_023 + mat[1].p[2] * det3_234_013 - mat[1].p[3] * det3_234_012;
            float det4_1234_0124 = mat[1].p[0] * det3_234_124 - mat[1].p[1] * det3_234_024 + mat[1].p[2] * det3_234_014 - mat[1].p[4] * det3_234_012;
            float det4_1234_0125 = mat[1].p[0] * det3_234_125 - mat[1].p[1] * det3_234_025 + mat[1].p[2] * det3_234_015 - mat[1].p[5] * det3_234_012;
            float det4_1234_0134 = mat[1].p[0] * det3_234_134 - mat[1].p[1] * det3_234_034 + mat[1].p[3] * det3_234_014 - mat[1].p[4] * det3_234_013;
            float det4_1234_0135 = mat[1].p[0] * det3_234_135 - mat[1].p[1] * det3_234_035 + mat[1].p[3] * det3_234_015 - mat[1].p[5] * det3_234_013;
            float det4_1234_0145 = mat[1].p[0] * det3_234_145 - mat[1].p[1] * det3_234_045 + mat[1].p[4] * det3_234_015 - mat[1].p[5] * det3_234_014;
            float det4_1234_0234 = mat[1].p[0] * det3_234_234 - mat[1].p[2] * det3_234_034 + mat[1].p[3] * det3_234_024 - mat[1].p[4] * det3_234_023;
            float det4_1234_0235 = mat[1].p[0] * det3_234_235 - mat[1].p[2] * det3_234_035 + mat[1].p[3] * det3_234_025 - mat[1].p[5] * det3_234_023;
            float det4_1234_0245 = mat[1].p[0] * det3_234_245 - mat[1].p[2] * det3_234_045 + mat[1].p[4] * det3_234_025 - mat[1].p[5] * det3_234_024;
            float det4_1234_0345 = mat[1].p[0] * det3_234_345 - mat[1].p[3] * det3_234_045 + mat[1].p[4] * det3_234_035 - mat[1].p[5] * det3_234_034;
            float det4_1234_1234 = mat[1].p[1] * det3_234_234 - mat[1].p[2] * det3_234_134 + mat[1].p[3] * det3_234_124 - mat[1].p[4] * det3_234_123;
            float det4_1234_1235 = mat[1].p[1] * det3_234_235 - mat[1].p[2] * det3_234_135 + mat[1].p[3] * det3_234_125 - mat[1].p[5] * det3_234_123;
            float det4_1234_1245 = mat[1].p[1] * det3_234_245 - mat[1].p[2] * det3_234_145 + mat[1].p[4] * det3_234_125 - mat[1].p[5] * det3_234_124;
            float det4_1234_1345 = mat[1].p[1] * det3_234_345 - mat[1].p[3] * det3_234_145 + mat[1].p[4] * det3_234_135 - mat[1].p[5] * det3_234_134;
            float det4_1234_2345 = mat[1].p[2] * det3_234_345 - mat[1].p[3] * det3_234_245 + mat[1].p[4] * det3_234_235 - mat[1].p[5] * det3_234_234;
            float det4_1235_0123 = mat[1].p[0] * det3_235_123 - mat[1].p[1] * det3_235_023 + mat[1].p[2] * det3_235_013 - mat[1].p[3] * det3_235_012;
            float det4_1235_0124 = mat[1].p[0] * det3_235_124 - mat[1].p[1] * det3_235_024 + mat[1].p[2] * det3_235_014 - mat[1].p[4] * det3_235_012;
            float det4_1235_0125 = mat[1].p[0] * det3_235_125 - mat[1].p[1] * det3_235_025 + mat[1].p[2] * det3_235_015 - mat[1].p[5] * det3_235_012;
            float det4_1235_0134 = mat[1].p[0] * det3_235_134 - mat[1].p[1] * det3_235_034 + mat[1].p[3] * det3_235_014 - mat[1].p[4] * det3_235_013;
            float det4_1235_0135 = mat[1].p[0] * det3_235_135 - mat[1].p[1] * det3_235_035 + mat[1].p[3] * det3_235_015 - mat[1].p[5] * det3_235_013;
            float det4_1235_0145 = mat[1].p[0] * det3_235_145 - mat[1].p[1] * det3_235_045 + mat[1].p[4] * det3_235_015 - mat[1].p[5] * det3_235_014;
            float det4_1235_0234 = mat[1].p[0] * det3_235_234 - mat[1].p[2] * det3_235_034 + mat[1].p[3] * det3_235_024 - mat[1].p[4] * det3_235_023;
            float det4_1235_0235 = mat[1].p[0] * det3_235_235 - mat[1].p[2] * det3_235_035 + mat[1].p[3] * det3_235_025 - mat[1].p[5] * det3_235_023;
            float det4_1235_0245 = mat[1].p[0] * det3_235_245 - mat[1].p[2] * det3_235_045 + mat[1].p[4] * det3_235_025 - mat[1].p[5] * det3_235_024;
            float det4_1235_0345 = mat[1].p[0] * det3_235_345 - mat[1].p[3] * det3_235_045 + mat[1].p[4] * det3_235_035 - mat[1].p[5] * det3_235_034;
            float det4_1235_1234 = mat[1].p[1] * det3_235_234 - mat[1].p[2] * det3_235_134 + mat[1].p[3] * det3_235_124 - mat[1].p[4] * det3_235_123;
            float det4_1235_1235 = mat[1].p[1] * det3_235_235 - mat[1].p[2] * det3_235_135 + mat[1].p[3] * det3_235_125 - mat[1].p[5] * det3_235_123;
            float det4_1235_1245 = mat[1].p[1] * det3_235_245 - mat[1].p[2] * det3_235_145 + mat[1].p[4] * det3_235_125 - mat[1].p[5] * det3_235_124;
            float det4_1235_1345 = mat[1].p[1] * det3_235_345 - mat[1].p[3] * det3_235_145 + mat[1].p[4] * det3_235_135 - mat[1].p[5] * det3_235_134;
            float det4_1235_2345 = mat[1].p[2] * det3_235_345 - mat[1].p[3] * det3_235_245 + mat[1].p[4] * det3_235_235 - mat[1].p[5] * det3_235_234;
            float det4_1245_0123 = mat[1].p[0] * det3_245_123 - mat[1].p[1] * det3_245_023 + mat[1].p[2] * det3_245_013 - mat[1].p[3] * det3_245_012;
            float det4_1245_0124 = mat[1].p[0] * det3_245_124 - mat[1].p[1] * det3_245_024 + mat[1].p[2] * det3_245_014 - mat[1].p[4] * det3_245_012;
            float det4_1245_0125 = mat[1].p[0] * det3_245_125 - mat[1].p[1] * det3_245_025 + mat[1].p[2] * det3_245_015 - mat[1].p[5] * det3_245_012;
            float det4_1245_0134 = mat[1].p[0] * det3_245_134 - mat[1].p[1] * det3_245_034 + mat[1].p[3] * det3_245_014 - mat[1].p[4] * det3_245_013;
            float det4_1245_0135 = mat[1].p[0] * det3_245_135 - mat[1].p[1] * det3_245_035 + mat[1].p[3] * det3_245_015 - mat[1].p[5] * det3_245_013;
            float det4_1245_0145 = mat[1].p[0] * det3_245_145 - mat[1].p[1] * det3_245_045 + mat[1].p[4] * det3_245_015 - mat[1].p[5] * det3_245_014;
            float det4_1245_0234 = mat[1].p[0] * det3_245_234 - mat[1].p[2] * det3_245_034 + mat[1].p[3] * det3_245_024 - mat[1].p[4] * det3_245_023;
            float det4_1245_0235 = mat[1].p[0] * det3_245_235 - mat[1].p[2] * det3_245_035 + mat[1].p[3] * det3_245_025 - mat[1].p[5] * det3_245_023;
            float det4_1245_0245 = mat[1].p[0] * det3_245_245 - mat[1].p[2] * det3_245_045 + mat[1].p[4] * det3_245_025 - mat[1].p[5] * det3_245_024;
            float det4_1245_0345 = mat[1].p[0] * det3_245_345 - mat[1].p[3] * det3_245_045 + mat[1].p[4] * det3_245_035 - mat[1].p[5] * det3_245_034;
            float det4_1245_1234 = mat[1].p[1] * det3_245_234 - mat[1].p[2] * det3_245_134 + mat[1].p[3] * det3_245_124 - mat[1].p[4] * det3_245_123;
            float det4_1245_1235 = mat[1].p[1] * det3_245_235 - mat[1].p[2] * det3_245_135 + mat[1].p[3] * det3_245_125 - mat[1].p[5] * det3_245_123;
            float det4_1245_1245 = mat[1].p[1] * det3_245_245 - mat[1].p[2] * det3_245_145 + mat[1].p[4] * det3_245_125 - mat[1].p[5] * det3_245_124;
            float det4_1245_1345 = mat[1].p[1] * det3_245_345 - mat[1].p[3] * det3_245_145 + mat[1].p[4] * det3_245_135 - mat[1].p[5] * det3_245_134;
            float det4_1245_2345 = mat[1].p[2] * det3_245_345 - mat[1].p[3] * det3_245_245 + mat[1].p[4] * det3_245_235 - mat[1].p[5] * det3_245_234;
            float det4_1345_0123 = mat[1].p[0] * det3_345_123 - mat[1].p[1] * det3_345_023 + mat[1].p[2] * det3_345_013 - mat[1].p[3] * det3_345_012;
            float det4_1345_0124 = mat[1].p[0] * det3_345_124 - mat[1].p[1] * det3_345_024 + mat[1].p[2] * det3_345_014 - mat[1].p[4] * det3_345_012;
            float det4_1345_0125 = mat[1].p[0] * det3_345_125 - mat[1].p[1] * det3_345_025 + mat[1].p[2] * det3_345_015 - mat[1].p[5] * det3_345_012;
            float det4_1345_0134 = mat[1].p[0] * det3_345_134 - mat[1].p[1] * det3_345_034 + mat[1].p[3] * det3_345_014 - mat[1].p[4] * det3_345_013;
            float det4_1345_0135 = mat[1].p[0] * det3_345_135 - mat[1].p[1] * det3_345_035 + mat[1].p[3] * det3_345_015 - mat[1].p[5] * det3_345_013;
            float det4_1345_0145 = mat[1].p[0] * det3_345_145 - mat[1].p[1] * det3_345_045 + mat[1].p[4] * det3_345_015 - mat[1].p[5] * det3_345_014;
            float det4_1345_0234 = mat[1].p[0] * det3_345_234 - mat[1].p[2] * det3_345_034 + mat[1].p[3] * det3_345_024 - mat[1].p[4] * det3_345_023;
            float det4_1345_0235 = mat[1].p[0] * det3_345_235 - mat[1].p[2] * det3_345_035 + mat[1].p[3] * det3_345_025 - mat[1].p[5] * det3_345_023;
            float det4_1345_0245 = mat[1].p[0] * det3_345_245 - mat[1].p[2] * det3_345_045 + mat[1].p[4] * det3_345_025 - mat[1].p[5] * det3_345_024;
            float det4_1345_0345 = mat[1].p[0] * det3_345_345 - mat[1].p[3] * det3_345_045 + mat[1].p[4] * det3_345_035 - mat[1].p[5] * det3_345_034;
            float det4_1345_1234 = mat[1].p[1] * det3_345_234 - mat[1].p[2] * det3_345_134 + mat[1].p[3] * det3_345_124 - mat[1].p[4] * det3_345_123;
            float det4_1345_1235 = mat[1].p[1] * det3_345_235 - mat[1].p[2] * det3_345_135 + mat[1].p[3] * det3_345_125 - mat[1].p[5] * det3_345_123;
            float det4_1345_1245 = mat[1].p[1] * det3_345_245 - mat[1].p[2] * det3_345_145 + mat[1].p[4] * det3_345_125 - mat[1].p[5] * det3_345_124;
            float det4_1345_1345 = mat[1].p[1] * det3_345_345 - mat[1].p[3] * det3_345_145 + mat[1].p[4] * det3_345_135 - mat[1].p[5] * det3_345_134;
            float det4_1345_2345 = mat[1].p[2] * det3_345_345 - mat[1].p[3] * det3_345_245 + mat[1].p[4] * det3_345_235 - mat[1].p[5] * det3_345_234;
            // remaining 5x5 sub-determinants
            float det5_01234_01234 = mat[0].p[0] * det4_1234_1234 - mat[0].p[1] * det4_1234_0234 + mat[0].p[2] * det4_1234_0134 - mat[0].p[3] * det4_1234_0124 + mat[0].p[4] * det4_1234_0123;
            float det5_01234_01235 = mat[0].p[0] * det4_1234_1235 - mat[0].p[1] * det4_1234_0235 + mat[0].p[2] * det4_1234_0135 - mat[0].p[3] * det4_1234_0125 + mat[0].p[5] * det4_1234_0123;
            float det5_01234_01245 = mat[0].p[0] * det4_1234_1245 - mat[0].p[1] * det4_1234_0245 + mat[0].p[2] * det4_1234_0145 - mat[0].p[4] * det4_1234_0125 + mat[0].p[5] * det4_1234_0124;
            float det5_01234_01345 = mat[0].p[0] * det4_1234_1345 - mat[0].p[1] * det4_1234_0345 + mat[0].p[3] * det4_1234_0145 - mat[0].p[4] * det4_1234_0135 + mat[0].p[5] * det4_1234_0134;
            float det5_01234_02345 = mat[0].p[0] * det4_1234_2345 - mat[0].p[2] * det4_1234_0345 + mat[0].p[3] * det4_1234_0245 - mat[0].p[4] * det4_1234_0235 + mat[0].p[5] * det4_1234_0234;
            float det5_01234_12345 = mat[0].p[1] * det4_1234_2345 - mat[0].p[2] * det4_1234_1345 + mat[0].p[3] * det4_1234_1245 - mat[0].p[4] * det4_1234_1235 + mat[0].p[5] * det4_1234_1234;
            float det5_01235_01234 = mat[0].p[0] * det4_1235_1234 - mat[0].p[1] * det4_1235_0234 + mat[0].p[2] * det4_1235_0134 - mat[0].p[3] * det4_1235_0124 + mat[0].p[4] * det4_1235_0123;
            float det5_01235_01235 = mat[0].p[0] * det4_1235_1235 - mat[0].p[1] * det4_1235_0235 + mat[0].p[2] * det4_1235_0135 - mat[0].p[3] * det4_1235_0125 + mat[0].p[5] * det4_1235_0123;
            float det5_01235_01245 = mat[0].p[0] * det4_1235_1245 - mat[0].p[1] * det4_1235_0245 + mat[0].p[2] * det4_1235_0145 - mat[0].p[4] * det4_1235_0125 + mat[0].p[5] * det4_1235_0124;
            float det5_01235_01345 = mat[0].p[0] * det4_1235_1345 - mat[0].p[1] * det4_1235_0345 + mat[0].p[3] * det4_1235_0145 - mat[0].p[4] * det4_1235_0135 + mat[0].p[5] * det4_1235_0134;
            float det5_01235_02345 = mat[0].p[0] * det4_1235_2345 - mat[0].p[2] * det4_1235_0345 + mat[0].p[3] * det4_1235_0245 - mat[0].p[4] * det4_1235_0235 + mat[0].p[5] * det4_1235_0234;
            float det5_01235_12345 = mat[0].p[1] * det4_1235_2345 - mat[0].p[2] * det4_1235_1345 + mat[0].p[3] * det4_1235_1245 - mat[0].p[4] * det4_1235_1235 + mat[0].p[5] * det4_1235_1234;
            float det5_01245_01234 = mat[0].p[0] * det4_1245_1234 - mat[0].p[1] * det4_1245_0234 + mat[0].p[2] * det4_1245_0134 - mat[0].p[3] * det4_1245_0124 + mat[0].p[4] * det4_1245_0123;
            float det5_01245_01235 = mat[0].p[0] * det4_1245_1235 - mat[0].p[1] * det4_1245_0235 + mat[0].p[2] * det4_1245_0135 - mat[0].p[3] * det4_1245_0125 + mat[0].p[5] * det4_1245_0123;
            float det5_01245_01245 = mat[0].p[0] * det4_1245_1245 - mat[0].p[1] * det4_1245_0245 + mat[0].p[2] * det4_1245_0145 - mat[0].p[4] * det4_1245_0125 + mat[0].p[5] * det4_1245_0124;
            float det5_01245_01345 = mat[0].p[0] * det4_1245_1345 - mat[0].p[1] * det4_1245_0345 + mat[0].p[3] * det4_1245_0145 - mat[0].p[4] * det4_1245_0135 + mat[0].p[5] * det4_1245_0134;
            float det5_01245_02345 = mat[0].p[0] * det4_1245_2345 - mat[0].p[2] * det4_1245_0345 + mat[0].p[3] * det4_1245_0245 - mat[0].p[4] * det4_1245_0235 + mat[0].p[5] * det4_1245_0234;
            float det5_01245_12345 = mat[0].p[1] * det4_1245_2345 - mat[0].p[2] * det4_1245_1345 + mat[0].p[3] * det4_1245_1245 - mat[0].p[4] * det4_1245_1235 + mat[0].p[5] * det4_1245_1234;
            float det5_01345_01234 = mat[0].p[0] * det4_1345_1234 - mat[0].p[1] * det4_1345_0234 + mat[0].p[2] * det4_1345_0134 - mat[0].p[3] * det4_1345_0124 + mat[0].p[4] * det4_1345_0123;
            float det5_01345_01235 = mat[0].p[0] * det4_1345_1235 - mat[0].p[1] * det4_1345_0235 + mat[0].p[2] * det4_1345_0135 - mat[0].p[3] * det4_1345_0125 + mat[0].p[5] * det4_1345_0123;
            float det5_01345_01245 = mat[0].p[0] * det4_1345_1245 - mat[0].p[1] * det4_1345_0245 + mat[0].p[2] * det4_1345_0145 - mat[0].p[4] * det4_1345_0125 + mat[0].p[5] * det4_1345_0124;
            float det5_01345_01345 = mat[0].p[0] * det4_1345_1345 - mat[0].p[1] * det4_1345_0345 + mat[0].p[3] * det4_1345_0145 - mat[0].p[4] * det4_1345_0135 + mat[0].p[5] * det4_1345_0134;
            float det5_01345_02345 = mat[0].p[0] * det4_1345_2345 - mat[0].p[2] * det4_1345_0345 + mat[0].p[3] * det4_1345_0245 - mat[0].p[4] * det4_1345_0235 + mat[0].p[5] * det4_1345_0234;
            float det5_01345_12345 = mat[0].p[1] * det4_1345_2345 - mat[0].p[2] * det4_1345_1345 + mat[0].p[3] * det4_1345_1245 - mat[0].p[4] * det4_1345_1235 + mat[0].p[5] * det4_1345_1234;
            float det5_02345_01234 = mat[0].p[0] * det4_2345_1234 - mat[0].p[1] * det4_2345_0234 + mat[0].p[2] * det4_2345_0134 - mat[0].p[3] * det4_2345_0124 + mat[0].p[4] * det4_2345_0123;
            float det5_02345_01235 = mat[0].p[0] * det4_2345_1235 - mat[0].p[1] * det4_2345_0235 + mat[0].p[2] * det4_2345_0135 - mat[0].p[3] * det4_2345_0125 + mat[0].p[5] * det4_2345_0123;
            float det5_02345_01245 = mat[0].p[0] * det4_2345_1245 - mat[0].p[1] * det4_2345_0245 + mat[0].p[2] * det4_2345_0145 - mat[0].p[4] * det4_2345_0125 + mat[0].p[5] * det4_2345_0124;
            float det5_02345_01345 = mat[0].p[0] * det4_2345_1345 - mat[0].p[1] * det4_2345_0345 + mat[0].p[3] * det4_2345_0145 - mat[0].p[4] * det4_2345_0135 + mat[0].p[5] * det4_2345_0134;
            float det5_02345_02345 = mat[0].p[0] * det4_2345_2345 - mat[0].p[2] * det4_2345_0345 + mat[0].p[3] * det4_2345_0245 - mat[0].p[4] * det4_2345_0235 + mat[0].p[5] * det4_2345_0234;
            float det5_02345_12345 = mat[0].p[1] * det4_2345_2345 - mat[0].p[2] * det4_2345_1345 + mat[0].p[3] * det4_2345_1245 - mat[0].p[4] * det4_2345_1235 + mat[0].p[5] * det4_2345_1234;
            //
            mat[0].p[0] = (float)(det5_12345_12345 * invDet);
            mat[0].p[1] = (float)(-det5_02345_12345 * invDet);
            mat[0].p[2] = (float)(det5_01345_12345 * invDet);
            mat[0].p[3] = (float)(-det5_01245_12345 * invDet);
            mat[0].p[4] = (float)(det5_01235_12345 * invDet);
            mat[0].p[5] = (float)(-det5_01234_12345 * invDet);
            //
            mat[1].p[0] = (float)(-det5_12345_02345 * invDet);
            mat[1].p[1] = (float)(det5_02345_02345 * invDet);
            mat[1].p[2] = (float)(-det5_01345_02345 * invDet);
            mat[1].p[3] = (float)(det5_01245_02345 * invDet);
            mat[1].p[4] = (float)(-det5_01235_02345 * invDet);
            mat[1].p[5] = (float)(det5_01234_02345 * invDet);
            //
            mat[2].p[0] = (float)(det5_12345_01345 * invDet);
            mat[2].p[1] = (float)(-det5_02345_01345 * invDet);
            mat[2].p[2] = (float)(det5_01345_01345 * invDet);
            mat[2].p[3] = (float)(-det5_01245_01345 * invDet);
            mat[2].p[4] = (float)(det5_01235_01345 * invDet);
            mat[2].p[5] = (float)(-det5_01234_01345 * invDet);
            //
            mat[3].p[0] = (float)(-det5_12345_01245 * invDet);
            mat[3].p[1] = (float)(det5_02345_01245 * invDet);
            mat[3].p[2] = (float)(-det5_01345_01245 * invDet);
            mat[3].p[3] = (float)(det5_01245_01245 * invDet);
            mat[3].p[4] = (float)(-det5_01235_01245 * invDet);
            mat[3].p[5] = (float)(det5_01234_01245 * invDet);
            //
            mat[4].p[0] = (float)(det5_12345_01235 * invDet);
            mat[4].p[1] = (float)(-det5_02345_01235 * invDet);
            mat[4].p[2] = (float)(det5_01345_01235 * invDet);
            mat[4].p[3] = (float)(-det5_01245_01235 * invDet);
            mat[4].p[4] = (float)(det5_01235_01235 * invDet);
            mat[4].p[5] = (float)(-det5_01234_01235 * invDet);
            //
            mat[5].p[0] = (float)(-det5_12345_01234 * invDet);
            mat[5].p[1] = (float)(det5_02345_01234 * invDet);
            mat[5].p[2] = (float)(-det5_01345_01234 * invDet);
            mat[5].p[3] = (float)(det5_01245_01234 * invDet);
            mat[5].p[4] = (float)(-det5_01235_01234 * invDet);
            mat[5].p[5] = (float)(det5_01234_01234 * invDet);
            return true;
#elif false
            // 6*40 = 240 multiplications
            //			6 divisions
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
            mat[5] *= d;
            d = -d;
            mat[6] *= d;
            mat[12] *= d;
            mat[18] *= d;
            mat[24] *= d;
            mat[30] *= d;
            d = mat[6] * di;
            mat[7] += mat[1] * d;
            mat[8] += mat[2] * d;
            mat[9] += mat[3] * d;
            mat[10] += mat[4] * d;
            mat[11] += mat[5] * d;
            d = mat[12] * di;
            mat[13] += mat[1] * d;
            mat[14] += mat[2] * d;
            mat[15] += mat[3] * d;
            mat[16] += mat[4] * d;
            mat[17] += mat[5] * d;
            d = mat[18] * di;
            mat[19] += mat[1] * d;
            mat[20] += mat[2] * d;
            mat[21] += mat[3] * d;
            mat[22] += mat[4] * d;
            mat[23] += mat[5] * d;
            d = mat[24] * di;
            mat[25] += mat[1] * d;
            mat[26] += mat[2] * d;
            mat[27] += mat[3] * d;
            mat[28] += mat[4] * d;
            mat[29] += mat[5] * d;
            d = mat[30] * di;
            mat[31] += mat[1] * d;
            mat[32] += mat[2] * d;
            mat[33] += mat[3] * d;
            mat[34] += mat[4] * d;
            mat[35] += mat[5] * d;
            di = mat[7];
            s *= di;
            mat[7] = d = 1.0f / di;
            mat[6] *= d;
            mat[8] *= d;
            mat[9] *= d;
            mat[10] *= d;
            mat[11] *= d;
            d = -d;
            mat[1] *= d;
            mat[13] *= d;
            mat[19] *= d;
            mat[25] *= d;
            mat[31] *= d;
            d = mat[1] * di;
            mat[0] += mat[6] * d;
            mat[2] += mat[8] * d;
            mat[3] += mat[9] * d;
            mat[4] += mat[10] * d;
            mat[5] += mat[11] * d;
            d = mat[13] * di;
            mat[12] += mat[6] * d;
            mat[14] += mat[8] * d;
            mat[15] += mat[9] * d;
            mat[16] += mat[10] * d;
            mat[17] += mat[11] * d;
            d = mat[19] * di;
            mat[18] += mat[6] * d;
            mat[20] += mat[8] * d;
            mat[21] += mat[9] * d;
            mat[22] += mat[10] * d;
            mat[23] += mat[11] * d;
            d = mat[25] * di;
            mat[24] += mat[6] * d;
            mat[26] += mat[8] * d;
            mat[27] += mat[9] * d;
            mat[28] += mat[10] * d;
            mat[29] += mat[11] * d;
            d = mat[31] * di;
            mat[30] += mat[6] * d;
            mat[32] += mat[8] * d;
            mat[33] += mat[9] * d;
            mat[34] += mat[10] * d;
            mat[35] += mat[11] * d;
            di = mat[14];
            s *= di;
            mat[14] = d = 1.0f / di;
            mat[12] *= d;
            mat[13] *= d;
            mat[15] *= d;
            mat[16] *= d;
            mat[17] *= d;
            d = -d;
            mat[2] *= d;
            mat[8] *= d;
            mat[20] *= d;
            mat[26] *= d;
            mat[32] *= d;
            d = mat[2] * di;
            mat[0] += mat[12] * d;
            mat[1] += mat[13] * d;
            mat[3] += mat[15] * d;
            mat[4] += mat[16] * d;
            mat[5] += mat[17] * d;
            d = mat[8] * di;
            mat[6] += mat[12] * d;
            mat[7] += mat[13] * d;
            mat[9] += mat[15] * d;
            mat[10] += mat[16] * d;
            mat[11] += mat[17] * d;
            d = mat[20] * di;
            mat[18] += mat[12] * d;
            mat[19] += mat[13] * d;
            mat[21] += mat[15] * d;
            mat[22] += mat[16] * d;
            mat[23] += mat[17] * d;
            d = mat[26] * di;
            mat[24] += mat[12] * d;
            mat[25] += mat[13] * d;
            mat[27] += mat[15] * d;
            mat[28] += mat[16] * d;
            mat[29] += mat[17] * d;
            d = mat[32] * di;
            mat[30] += mat[12] * d;
            mat[31] += mat[13] * d;
            mat[33] += mat[15] * d;
            mat[34] += mat[16] * d;
            mat[35] += mat[17] * d;
            di = mat[21];
            s *= di;
            mat[21] = d = 1.0f / di;
            mat[18] *= d;
            mat[19] *= d;
            mat[20] *= d;
            mat[22] *= d;
            mat[23] *= d;
            d = -d;
            mat[3] *= d;
            mat[9] *= d;
            mat[15] *= d;
            mat[27] *= d;
            mat[33] *= d;
            d = mat[3] * di;
            mat[0] += mat[18] * d;
            mat[1] += mat[19] * d;
            mat[2] += mat[20] * d;
            mat[4] += mat[22] * d;
            mat[5] += mat[23] * d;
            d = mat[9] * di;
            mat[6] += mat[18] * d;
            mat[7] += mat[19] * d;
            mat[8] += mat[20] * d;
            mat[10] += mat[22] * d;
            mat[11] += mat[23] * d;
            d = mat[15] * di;
            mat[12] += mat[18] * d;
            mat[13] += mat[19] * d;
            mat[14] += mat[20] * d;
            mat[16] += mat[22] * d;
            mat[17] += mat[23] * d;
            d = mat[27] * di;
            mat[24] += mat[18] * d;
            mat[25] += mat[19] * d;
            mat[26] += mat[20] * d;
            mat[28] += mat[22] * d;
            mat[29] += mat[23] * d;
            d = mat[33] * di;
            mat[30] += mat[18] * d;
            mat[31] += mat[19] * d;
            mat[32] += mat[20] * d;
            mat[34] += mat[22] * d;
            mat[35] += mat[23] * d;
            di = mat[28];
            s *= di;
            mat[28] = d = 1.0f / di;
            mat[24] *= d;
            mat[25] *= d;
            mat[26] *= d;
            mat[27] *= d;
            mat[29] *= d;
            d = -d;
            mat[4] *= d;
            mat[10] *= d;
            mat[16] *= d;
            mat[22] *= d;
            mat[34] *= d;
            d = mat[4] * di;
            mat[0] += mat[24] * d;
            mat[1] += mat[25] * d;
            mat[2] += mat[26] * d;
            mat[3] += mat[27] * d;
            mat[5] += mat[29] * d;
            d = mat[10] * di;
            mat[6] += mat[24] * d;
            mat[7] += mat[25] * d;
            mat[8] += mat[26] * d;
            mat[9] += mat[27] * d;
            mat[11] += mat[29] * d;
            d = mat[16] * di;
            mat[12] += mat[24] * d;
            mat[13] += mat[25] * d;
            mat[14] += mat[26] * d;
            mat[15] += mat[27] * d;
            mat[17] += mat[29] * d;
            d = mat[22] * di;
            mat[18] += mat[24] * d;
            mat[19] += mat[25] * d;
            mat[20] += mat[26] * d;
            mat[21] += mat[27] * d;
            mat[23] += mat[29] * d;
            d = mat[34] * di;
            mat[30] += mat[24] * d;
            mat[31] += mat[25] * d;
            mat[32] += mat[26] * d;
            mat[33] += mat[27] * d;
            mat[35] += mat[29] * d;
            di = mat[35];
            s *= di;
            mat[35] = d = 1.0f / di;
            mat[30] *= d;
            mat[31] *= d;
            mat[32] *= d;
            mat[33] *= d;
            mat[34] *= d;
            d = -d;
            mat[5] *= d;
            mat[11] *= d;
            mat[17] *= d;
            mat[23] *= d;
            mat[29] *= d;
            d = mat[5] * di;
            mat[0] += mat[30] * d;
            mat[1] += mat[31] * d;
            mat[2] += mat[32] * d;
            mat[3] += mat[33] * d;
            mat[4] += mat[34] * d;
            d = mat[11] * di;
            mat[6] += mat[30] * d;
            mat[7] += mat[31] * d;
            mat[8] += mat[32] * d;
            mat[9] += mat[33] * d;
            mat[10] += mat[34] * d;
            d = mat[17] * di;
            mat[12] += mat[30] * d;
            mat[13] += mat[31] * d;
            mat[14] += mat[32] * d;
            mat[15] += mat[33] * d;
            mat[16] += mat[34] * d;
            d = mat[23] * di;
            mat[18] += mat[30] * d;
            mat[19] += mat[31] * d;
            mat[20] += mat[32] * d;
            mat[21] += mat[33] * d;
            mat[22] += mat[34] * d;
            d = mat[29] * di;
            mat[24] += mat[30] * d;
            mat[25] += mat[31] * d;
            mat[26] += mat[32] * d;
            mat[27] += mat[33] * d;
            mat[28] += mat[34] * d;

            return ( s != 0.0f && !FLOAT_IS_NAN( s ) );
#else
            // 6*27+2*30 = 222 multiplications
            //		2*1  =	 2 divisions
            float[] mat = this;
            // r0 = m0.Inverse();
            float c0 = mat[1 * 6 + 1] * mat[2 * 6 + 2] - mat[1 * 6 + 2] * mat[2 * 6 + 1];
            float c1 = mat[1 * 6 + 2] * mat[2 * 6 + 0] - mat[1 * 6 + 0] * mat[2 * 6 + 2];
            float c2 = mat[1 * 6 + 0] * mat[2 * 6 + 1] - mat[1 * 6 + 1] * mat[2 * 6 + 0];
            float det = mat[0 * 6 + 0] * c0 + mat[0 * 6 + 1] * c1 + mat[0 * 6 + 2] * c2;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            float invDet = 1.0f / det;
            idMat3 r0 = new idMat3(); idVec3[] r0_mat = r0.mat;
            r0_mat[0].x = c0 * invDet;
            r0_mat[0].y = (mat[0 * 6 + 2] * mat[2 * 6 + 1] - mat[0 * 6 + 1] * mat[2 * 6 + 2]) * invDet;
            r0_mat[0].z = (mat[0 * 6 + 1] * mat[1 * 6 + 2] - mat[0 * 6 + 2] * mat[1 * 6 + 1]) * invDet;
            r0_mat[1].x = c1 * invDet;
            r0_mat[1].y = (mat[0 * 6 + 0] * mat[2 * 6 + 2] - mat[0 * 6 + 2] * mat[2 * 6 + 0]) * invDet;
            r0_mat[1].z = (mat[0 * 6 + 2] * mat[1 * 6 + 0] - mat[0 * 6 + 0] * mat[1 * 6 + 2]) * invDet;
            r0_mat[2].x = c2 * invDet;
            r0_mat[2].y = (mat[0 * 6 + 1] * mat[2 * 6 + 0] - mat[0 * 6 + 0] * mat[2 * 6 + 1]) * invDet;
            r0_mat[2].z = (mat[0 * 6 + 0] * mat[1 * 6 + 1] - mat[0 * 6 + 1] * mat[1 * 6 + 0]) * invDet;
            // r1 = r0 * m1;
            idMat3 r1 = new idMat3(); idVec3[] r1_mat = r1.mat;
            r1_mat[0].x = r0_mat[0].x * mat[0 * 6 + 3] + r0_mat[0].y * mat[1 * 6 + 3] + r0_mat[0].z * mat[2 * 6 + 3];
            r1_mat[0].y = r0_mat[0].x * mat[0 * 6 + 4] + r0_mat[0].y * mat[1 * 6 + 4] + r0_mat[0].z * mat[2 * 6 + 4];
            r1_mat[0].z = r0_mat[0].x * mat[0 * 6 + 5] + r0_mat[0].y * mat[1 * 6 + 5] + r0_mat[0].z * mat[2 * 6 + 5];
            r1_mat[1].x = r0_mat[1].x * mat[0 * 6 + 3] + r0_mat[1].y * mat[1 * 6 + 3] + r0_mat[1].z * mat[2 * 6 + 3];
            r1_mat[1].y = r0_mat[1].x * mat[0 * 6 + 4] + r0_mat[1].y * mat[1 * 6 + 4] + r0_mat[1].z * mat[2 * 6 + 4];
            r1_mat[1].z = r0_mat[1].x * mat[0 * 6 + 5] + r0_mat[1].y * mat[1 * 6 + 5] + r0_mat[1].z * mat[2 * 6 + 5];
            r1_mat[2].x = r0_mat[2].x * mat[0 * 6 + 3] + r0_mat[2].y * mat[1 * 6 + 3] + r0_mat[2].z * mat[2 * 6 + 3];
            r1_mat[2].y = r0_mat[2].x * mat[0 * 6 + 4] + r0_mat[2].y * mat[1 * 6 + 4] + r0_mat[2].z * mat[2 * 6 + 4];
            r1_mat[2].z = r0_mat[2].x * mat[0 * 6 + 5] + r0_mat[2].y * mat[1 * 6 + 5] + r0_mat[2].z * mat[2 * 6 + 5];
            // r2 = m2 * r1;
            idMat3 r2 = new idMat3(); idVec3[] r2_mat = r2.mat;
            r2_mat[0].x = mat[3 * 6 + 0] * r1_mat[0].x + mat[3 * 6 + 1] * r1_mat[1].x + mat[3 * 6 + 2] * r1_mat[2].x;
            r2_mat[0].y = mat[3 * 6 + 0] * r1_mat[0].y + mat[3 * 6 + 1] * r1_mat[1].y + mat[3 * 6 + 2] * r1_mat[2].y;
            r2_mat[0].z = mat[3 * 6 + 0] * r1_mat[0].z + mat[3 * 6 + 1] * r1_mat[1].z + mat[3 * 6 + 2] * r1_mat[2].z;
            r2_mat[1].x = mat[4 * 6 + 0] * r1_mat[0].x + mat[4 * 6 + 1] * r1_mat[1].x + mat[4 * 6 + 2] * r1_mat[2].x;
            r2_mat[1].y = mat[4 * 6 + 0] * r1_mat[0].y + mat[4 * 6 + 1] * r1_mat[1].y + mat[4 * 6 + 2] * r1_mat[2].y;
            r2_mat[1].z = mat[4 * 6 + 0] * r1_mat[0].z + mat[4 * 6 + 1] * r1_mat[1].z + mat[4 * 6 + 2] * r1_mat[2].z;
            r2_mat[2].x = mat[5 * 6 + 0] * r1_mat[0].x + mat[5 * 6 + 1] * r1_mat[1].x + mat[5 * 6 + 2] * r1_mat[2].x;
            r2_mat[2].y = mat[5 * 6 + 0] * r1_mat[0].y + mat[5 * 6 + 1] * r1_mat[1].y + mat[5 * 6 + 2] * r1_mat[2].y;
            r2_mat[2].z = mat[5 * 6 + 0] * r1_mat[0].z + mat[5 * 6 + 1] * r1_mat[1].z + mat[5 * 6 + 2] * r1_mat[2].z;
            // r3 = r2 - m3;
            idMat3 r3 = new idMat3(); idVec3[] r3_mat = r3.mat;
            r3_mat[0].x = r2_mat[0].x - mat[3 * 6 + 3];
            r3_mat[0].y = r2_mat[0].y - mat[3 * 6 + 4];
            r3_mat[0].z = r2_mat[0].z - mat[3 * 6 + 5];
            r3_mat[1].x = r2_mat[1].x - mat[4 * 6 + 3];
            r3_mat[1].y = r2_mat[1].y - mat[4 * 6 + 4];
            r3_mat[1].z = r2_mat[1].z - mat[4 * 6 + 5];
            r3_mat[2].x = r2_mat[2].x - mat[5 * 6 + 3];
            r3_mat[2].y = r2_mat[2].y - mat[5 * 6 + 4];
            r3_mat[2].z = r2_mat[2].z - mat[5 * 6 + 5];
            // r3.InverseSelf();
            r2_mat[0].x = r3_mat[1].y * r3_mat[2].z - r3_mat[1].z * r3_mat[2].y;
            r2_mat[1].x = r3_mat[1].z * r3_mat[2].x - r3_mat[1].x * r3_mat[2].z;
            r2_mat[2].x = r3_mat[1].x * r3_mat[2].y - r3_mat[1].y * r3_mat[2].x;
            //
            det = r3_mat[0].x * r2_mat[0].x + r3_mat[0].y * r2_mat[1].x + r3_mat[0].z * r2_mat[2].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON)
                return false;
            invDet = 1.0f / det;
            //
            r2_mat[0].y = r3_mat[0].z * r3_mat[2].y - r3_mat[0].y * r3_mat[2].z;
            r2_mat[0].z = r3_mat[0].y * r3_mat[1].z - r3_mat[0].z * r3_mat[1].y;
            r2_mat[1].y = r3_mat[0].x * r3_mat[2].z - r3_mat[0].z * r3_mat[2].x;
            r2_mat[1].z = r3_mat[0].z * r3_mat[1].x - r3_mat[0].x * r3_mat[1].z;
            r2_mat[2].y = r3_mat[0].y * r3_mat[2].x - r3_mat[0].x * r3_mat[2].y;
            r2_mat[2].z = r3_mat[0].x * r3_mat[1].y - r3_mat[0].y * r3_mat[1].x;
            //
            r3_mat[0].x = r2_mat[0].x * invDet;
            r3_mat[0].y = r2_mat[0].y * invDet;
            r3_mat[0].z = r2_mat[0].z * invDet;
            r3_mat[1].x = r2_mat[1].x * invDet;
            r3_mat[1].y = r2_mat[1].y * invDet;
            r3_mat[1].z = r2_mat[1].z * invDet;
            r3_mat[2].x = r2_mat[2].x * invDet;
            r3_mat[2].y = r2_mat[2].y * invDet;
            r3_mat[2].z = r2_mat[2].z * invDet;
            // r2 = m2 * r0;
            r2_mat[0].x = mat[3 * 6 + 0] * r0_mat[0].x + mat[3 * 6 + 1] * r0_mat[1].x + mat[3 * 6 + 2] * r0_mat[2].x;
            r2_mat[0].y = mat[3 * 6 + 0] * r0_mat[0].y + mat[3 * 6 + 1] * r0_mat[1].y + mat[3 * 6 + 2] * r0_mat[2].y;
            r2_mat[0].z = mat[3 * 6 + 0] * r0_mat[0].z + mat[3 * 6 + 1] * r0_mat[1].z + mat[3 * 6 + 2] * r0_mat[2].z;
            r2_mat[1].x = mat[4 * 6 + 0] * r0_mat[0].x + mat[4 * 6 + 1] * r0_mat[1].x + mat[4 * 6 + 2] * r0_mat[2].x;
            r2_mat[1].y = mat[4 * 6 + 0] * r0_mat[0].y + mat[4 * 6 + 1] * r0_mat[1].y + mat[4 * 6 + 2] * r0_mat[2].y;
            r2_mat[1].z = mat[4 * 6 + 0] * r0_mat[0].z + mat[4 * 6 + 1] * r0_mat[1].z + mat[4 * 6 + 2] * r0_mat[2].z;
            r2_mat[2].x = mat[5 * 6 + 0] * r0_mat[0].x + mat[5 * 6 + 1] * r0_mat[1].x + mat[5 * 6 + 2] * r0_mat[2].x;
            r2_mat[2].y = mat[5 * 6 + 0] * r0_mat[0].y + mat[5 * 6 + 1] * r0_mat[1].y + mat[5 * 6 + 2] * r0_mat[2].y;
            r2_mat[2].z = mat[5 * 6 + 0] * r0_mat[0].z + mat[5 * 6 + 1] * r0_mat[1].z + mat[5 * 6 + 2] * r0_mat[2].z;
            // m2 = r3 * r2;
            mat[3 * 6 + 0] = r3_mat[0].x * r2_mat[0].x + r3_mat[0].y * r2_mat[1].x + r3_mat[0].z * r2_mat[2].x;
            mat[3 * 6 + 1] = r3_mat[0].x * r2_mat[0].y + r3_mat[0].y * r2_mat[1].y + r3_mat[0].z * r2_mat[2].y;
            mat[3 * 6 + 2] = r3_mat[0].x * r2_mat[0].z + r3_mat[0].y * r2_mat[1].z + r3_mat[0].z * r2_mat[2].z;
            mat[4 * 6 + 0] = r3_mat[1].x * r2_mat[0].x + r3_mat[1].y * r2_mat[1].x + r3_mat[1].z * r2_mat[2].x;
            mat[4 * 6 + 1] = r3_mat[1].x * r2_mat[0].y + r3_mat[1].y * r2_mat[1].y + r3_mat[1].z * r2_mat[2].y;
            mat[4 * 6 + 2] = r3_mat[1].x * r2_mat[0].z + r3_mat[1].y * r2_mat[1].z + r3_mat[1].z * r2_mat[2].z;
            mat[5 * 6 + 0] = r3_mat[2].x * r2_mat[0].x + r3_mat[2].y * r2_mat[1].x + r3_mat[2].z * r2_mat[2].x;
            mat[5 * 6 + 1] = r3_mat[2].x * r2_mat[0].y + r3_mat[2].y * r2_mat[1].y + r3_mat[2].z * r2_mat[2].y;
            mat[5 * 6 + 2] = r3_mat[2].x * r2_mat[0].z + r3_mat[2].y * r2_mat[1].z + r3_mat[2].z * r2_mat[2].z;
            // m0 = r0 - r1 * m2;
            mat[0 * 6 + 0] = r0_mat[0].x - r1_mat[0].x * mat[3 * 6 + 0] - r1_mat[0].y * mat[4 * 6 + 0] - r1_mat[0].z * mat[5 * 6 + 0];
            mat[0 * 6 + 1] = r0_mat[0].y - r1_mat[0].x * mat[3 * 6 + 1] - r1_mat[0].y * mat[4 * 6 + 1] - r1_mat[0].z * mat[5 * 6 + 1];
            mat[0 * 6 + 2] = r0_mat[0].z - r1_mat[0].x * mat[3 * 6 + 2] - r1_mat[0].y * mat[4 * 6 + 2] - r1_mat[0].z * mat[5 * 6 + 2];
            mat[1 * 6 + 0] = r0_mat[1].x - r1_mat[1].x * mat[3 * 6 + 0] - r1_mat[1].y * mat[4 * 6 + 0] - r1_mat[1].z * mat[5 * 6 + 0];
            mat[1 * 6 + 1] = r0_mat[1].y - r1_mat[1].x * mat[3 * 6 + 1] - r1_mat[1].y * mat[4 * 6 + 1] - r1_mat[1].z * mat[5 * 6 + 1];
            mat[1 * 6 + 2] = r0_mat[1].z - r1_mat[1].x * mat[3 * 6 + 2] - r1_mat[1].y * mat[4 * 6 + 2] - r1_mat[1].z * mat[5 * 6 + 2];
            mat[2 * 6 + 0] = r0_mat[2].x - r1_mat[2].x * mat[3 * 6 + 0] - r1_mat[2].y * mat[4 * 6 + 0] - r1_mat[2].z * mat[5 * 6 + 0];
            mat[2 * 6 + 1] = r0_mat[2].y - r1_mat[2].x * mat[3 * 6 + 1] - r1_mat[2].y * mat[4 * 6 + 1] - r1_mat[2].z * mat[5 * 6 + 1];
            mat[2 * 6 + 2] = r0_mat[2].z - r1_mat[2].x * mat[3 * 6 + 2] - r1_mat[2].y * mat[4 * 6 + 2] - r1_mat[2].z * mat[5 * 6 + 2];
            // m1 = r1 * r3;
            mat[0 * 6 + 3] = r1_mat[0].x * r3_mat[0].x + r1_mat[0].y * r3_mat[1].x + r1_mat[0].z * r3_mat[2].x;
            mat[0 * 6 + 4] = r1_mat[0].x * r3_mat[0].y + r1_mat[0].y * r3_mat[1].y + r1_mat[0].z * r3_mat[2].y;
            mat[0 * 6 + 5] = r1_mat[0].x * r3_mat[0].z + r1_mat[0].y * r3_mat[1].z + r1_mat[0].z * r3_mat[2].z;
            mat[1 * 6 + 3] = r1_mat[1].x * r3_mat[0].x + r1_mat[1].y * r3_mat[1].x + r1_mat[1].z * r3_mat[2].x;
            mat[1 * 6 + 4] = r1_mat[1].x * r3_mat[0].y + r1_mat[1].y * r3_mat[1].y + r1_mat[1].z * r3_mat[2].y;
            mat[1 * 6 + 5] = r1_mat[1].x * r3_mat[0].z + r1_mat[1].y * r3_mat[1].z + r1_mat[1].z * r3_mat[2].z;
            mat[2 * 6 + 3] = r1_mat[2].x * r3_mat[0].x + r1_mat[2].y * r3_mat[1].x + r1_mat[2].z * r3_mat[2].x;
            mat[2 * 6 + 4] = r1_mat[2].x * r3_mat[0].y + r1_mat[2].y * r3_mat[1].y + r1_mat[2].z * r3_mat[2].y;
            mat[2 * 6 + 5] = r1_mat[2].x * r3_mat[0].z + r1_mat[2].y * r3_mat[1].z + r1_mat[2].z * r3_mat[2].z;
            // m3 = -r3;
            mat[3 * 6 + 3] = -r3_mat[0].x;
            mat[3 * 6 + 4] = -r3_mat[0].y;
            mat[3 * 6 + 5] = -r3_mat[0].z;
            mat[4 * 6 + 3] = -r3_mat[1].x;
            mat[4 * 6 + 4] = -r3_mat[1].y;
            mat[4 * 6 + 5] = -r3_mat[1].z;
            mat[5 * 6 + 3] = -r3_mat[2].x;
            mat[5 * 6 + 4] = -r3_mat[2].y;
            mat[5 * 6 + 5] = -r3_mat[2].z;
            return true;
#endif
        }
    }
}



