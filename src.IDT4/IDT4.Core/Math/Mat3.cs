using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idMat3
    {
        public readonly static idMat3 zero = new idMat3(
            0f, 0f, 0f,
            0f, 0f, 0f,
            0f, 0f, 0f);
        public readonly static idMat3 identity = new idMat3(
            1f, 0f, 0f,
            0f, 1f, 0f,
            0f, 0f, 1f);
        internal idVec3[] mat = new idVec3[3];

        public idMat3() { }
        public idMat3(ref idVec2 x, ref idVec2 y, ref idVec2 z)
        {
            mat[0].x = x.x; mat[0].y = x.y; mat[0].z = x.z;
            mat[1].x = y.x; mat[1].y = y.y; mat[1].z = y.z;
            mat[2].x = z.x; mat[2].y = z.y; mat[2].z = z.z;
        }
        public idMat3(float xx, float xy, float xz, float yx, float yy, float yz, float zx, float zy, float zz)
        {
            mat[0].x = xx; mat[0].y = xy; mat[0].z = xz;
            mat[1].x = yx; mat[1].y = yy; mat[1].z = yz;
            mat[2].x = zx; mat[2].y = zy; mat[2].z = zz;
        }
        public idMat3(float[][] src) { Array.Copy(mat, src, 3 * 3); }
        public void Zero()
        {
            Array.Clear(mat, 0, 3 * 3);
        }
        public int GetDimension() { return 9; }
        public float[] ToArray() { return mat[0].ToArray(); }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        //public idVec3 this[int index] { get { return mat[index]; } }
        public static idMat3 operator -(idMat3 a)
        {
            idVec3[] a_mat = a.mat;
            return new idMat3(
                -a_mat[0].x, -a_mat[0].y, -a_mat[0].z,
                -a_mat[1].x, -a_mat[1].y, -a_mat[1].z,
                -a_mat[2].x, -a_mat[2].y, -a_mat[2].z);
        }
        public static idVec3 operator *(idMat3 a, idVec3 vec)
        {
            idVec3[] a_mat = a.mat;
            return new idVec3(
                a_mat[0].x * vec.x + a_mat[1].x * vec.y + a_mat[2].x * vec.z,
                a_mat[0].y * vec.x + a_mat[1].y * vec.y + a_mat[2].y * vec.z,
                a_mat[0].z * vec.x + a_mat[1].z * vec.y + a_mat[2].z * vec.z);
        }
        public static idMat3 operator *(idMat3 a, idMat3 b)
        {
            //m1Ptr = reinterpret_cast<const float *>(this);
            //m2Ptr = reinterpret_cast<const float *>(&a);
            //dstPtr = reinterpret_cast<float *>(&dst);
            idMat3 dst = new idMat3();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //        *dstPtr = m1Ptr[0] * m2Ptr[ 0 * 3 + j ]
                    //                + m1Ptr[1] * m2Ptr[ 1 * 3 + j ]
                    //                + m1Ptr[2] * m2Ptr[ 2 * 3 + j ];
                    //        dstPtr++;
                }
                //m1Ptr += 3;
            }
            return dst;
        }
        public static idMat3 operator *(idMat3 a, float b)
        {
            idVec3[] a_mat = a.mat;
            return new idMat3(
                a_mat[0].x * b, a_mat[0].y * b, a_mat[0].z * b,
                a_mat[1].x * b, a_mat[1].y * b, a_mat[1].z * b,
                a_mat[2].x * b, a_mat[2].y * b, a_mat[2].z * b);
        }
        public static idMat3 operator +(idMat3 a, idMat3 b)
        {
            idVec3[] a_mat = a.mat;
            idVec3[] b_mat = b.mat;
            return new idMat3(
                a_mat[0].x + b_mat[0].x, a_mat[0].y + b_mat[0].y, a_mat[0].z + +b_mat[0].z,
                a_mat[1].x + b_mat[1].x, a_mat[1].y + b_mat[1].y, a_mat[1].z + +b_mat[1].z,
                a_mat[2].x + b_mat[2].x, a_mat[2].y + b_mat[2].y, a_mat[2].z + +b_mat[2].z);
        }
        public static idMat3 operator -(idMat3 a, idMat3 b)
        {
            idVec3[] a_mat = a.mat;
            idVec3[] b_mat = b.mat;
            return new idMat3(
                a_mat[0].x - b_mat[0].x, a_mat[0].y - b_mat[0].y, a_mat[0].z - b_mat[0].z,
                a_mat[1].x - b_mat[1].x, a_mat[1].y - b_mat[1].y, a_mat[1].z - b_mat[1].z,
                a_mat[2].x - b_mat[2].x, a_mat[2].y - b_mat[2].y, a_mat[2].z - b_mat[2].z);
        }
        public idMat3 opMul(float a)
        {
            mat[0].x *= a; mat[0].y *= a; mat[0].z *= a;
            mat[1].x *= a; mat[1].y *= a; mat[1].z *= a;
            mat[2].x *= a; mat[2].y *= a; mat[2].z *= a;
            return this;
        }
        public idMat3 opMul(idMat3 a)
        {
            //m1Ptr = reinterpret_cast<float *>(this);
            //m2Ptr = reinterpret_cast<const float *>(&a);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    //        dst[j]  = m1Ptr[0] * m2Ptr[ 0 * 3 + j ]
                    //                + m1Ptr[1] * m2Ptr[ 1 * 3 + j ]
                    //                + m1Ptr[2] * m2Ptr[ 2 * 3 + j ];
                }
                //    m1Ptr[0] = dst[0]; m1Ptr[1] = dst[1]; m1Ptr[2] = dst[2];
                //    m1Ptr += 3;
            }
            return this;
        }
        public idMat3 opAdd(idMat3 a)
        {
            idVec3[] a_mat = a.mat;
            mat[0].x += a_mat[0].x; mat[0].y += a_mat[0].y; mat[0].z += a_mat[0].z;
            mat[1].x += a_mat[1].x; mat[1].y += a_mat[1].y; mat[1].z += a_mat[1].z;
            mat[2].x += a_mat[2].x; mat[2].y += a_mat[2].y; mat[2].z += a_mat[2].z;
            return this;
        }
        public idMat3 opSub(idMat3 a)
        {
            idVec3[] a_mat = a.mat;
            mat[0].x -= a_mat[0].x; mat[0].y -= a_mat[0].y; mat[0].z -= a_mat[0].z;
            mat[1].x -= a_mat[1].x; mat[1].y -= a_mat[1].y; mat[1].z -= a_mat[1].z;
            mat[2].x -= a_mat[2].x; mat[2].y -= a_mat[2].y; mat[2].z -= a_mat[2].z;
            return this;
        }
        // extra
        public idVec3 opMul(ref idVec3 vec, ref idMat3 mat)
        {
            idVec3[] mat_mat = mat.mat;
            float x = mat_mat[0].x * vec.x + mat_mat[1].x * vec.y + mat_mat[2].x * vec.z;
            float y = mat_mat[0].y * vec.x + mat_mat[1].y * vec.y + mat_mat[2].y * vec.z;
            vec.z = mat_mat[0].z * vec.x + mat_mat[1].z * vec.y + mat_mat[2].z * vec.z;
            vec.x = x;
            vec.y = y;
            return vec;
        }
        #endregion

        #region Compare
        public bool Compare(ref idMat3 a)
        {
            return (
                mat[0].Compare(ref a.mat[0]) &&
                mat[1].Compare(ref a.mat[1]) &&
                mat[2].Compare(ref a.mat[2]));
        }
        public bool Compare(ref idMat3 a, float epsilon)
        {
            return (
                mat[0].Compare(ref a.mat[0], epsilon) &&
                mat[1].Compare(ref a.mat[1], epsilon) &&
                mat[2].Compare(ref a.mat[2], epsilon));
        }
        public static bool operator ==(idMat3 a, idMat3 b) { return a.Compare(ref b); }
        public static bool operator !=(idMat3 a, idMat3 b) { return !a.Compare(ref b); }
        #endregion

        #region Identity
        public void Identity() { this = identity; }
        public bool IsIdentity(float epsilon) { return Compare(ref identity, epsilon); }
        public bool IsSymmetric(float epsilon)
        {
            return !(
                (idMath.Fabs(mat[0].y - mat[1].x) > epsilon) ||
                (idMath.Fabs(mat[0].z - mat[2].x) > epsilon) ||
                (idMath.Fabs(mat[1].z - mat[2].y) > epsilon));

        }
        public bool IsDiagonal(float epsilon)
        {
            return !(
                idMath.Fabs(mat[0].y) > epsilon ||
                idMath.Fabs(mat[0].z) > epsilon ||
                idMath.Fabs(mat[1].x) > epsilon ||
                idMath.Fabs(mat[1].z) > epsilon ||
                idMath.Fabs(mat[2].x) > epsilon ||
                idMath.Fabs(mat[2].y) > epsilon);
        }
        public bool IsRotated() { return !Compare(ref identity); }
        #endregion

        #region Project
        public void ProjectVector(ref idVec3 src, ref idVec3 dst)
        {
            dst.x = src * mat[0];
            dst.y = src * mat[1];
            dst.z = src * mat[2];
        }
        public void UnprojectVector(ref idVec3 src, ref idVec3 dst)
        {
            dst = mat[0] * src.x + mat[1] * src.y + mat[2] * src.z;
        }
        #endregion

        public bool FixDegeneracies()
        {
            bool r = mat[0].FixDegenerateNormal();
            r |= mat[1].FixDegenerateNormal();
            r |= mat[2].FixDegenerateNormal();
            return r;
        }
        public bool FixDenormals()
        {
            bool r = mat[0].FixDenormals();
            r |= mat[1].FixDenormals();
            r |= mat[2].FixDenormals();
            return r;
        }

        #region Transform
        public float Trace() { return (mat[0].x + mat[1].y + mat[2].z); }
        public float Determinant()
        {
            float det2_12_01 = mat[1].x * mat[2].y - mat[1].y * mat[2].x;
            float det2_12_02 = mat[1].x * mat[2].z - mat[1].z * mat[2].x;
            float det2_12_12 = mat[1].y * mat[2].z - mat[1].z * mat[2].y;
            return mat[0].x * det2_12_12 - mat[0].y * det2_12_02 + mat[0].x * det2_12_01;
        }
        public idMat3 OrthoNormalize()
        {
            idMat3 ortho = this;
            ortho.mat[0].Normalize();
            ortho.mat[2].Cross(mat[0], mat[1]);
            ortho.mat[2].Normalize();
            ortho.mat[1].Cross(mat[2], mat[0]);
            ortho.mat[1].Normalize();
            return ortho;
        }
        public idMat3 OrthoNormalizeSelf()
        {
            mat[0].Normalize();
            mat[2].Cross(mat[0], mat[1]);
            mat[2].Normalize();
            mat[1].Cross(mat[2], mat[0]);
            mat[1].Normalize();
            return this;
        }
        public idMat3 Transpose()
        {
            return new idMat3(
                mat[0].x, mat[1].x, mat[2].x,
                mat[0].y, mat[1].y, mat[2].y,
                mat[0].z, mat[1].z, mat[2].z);
        }
        public idMat3 TransposeSelf()
        {
            float tmp0 = mat[0].y;
            mat[0].y = mat[1].x;
            mat[1].x = tmp0;
            float tmp1 = mat[0].z;
            mat[0].z = mat[2].x;
            mat[2].x = tmp1;
            float tmp2 = mat[1].z;
            mat[1].z = mat[2].y;
            mat[2].y = tmp2;
            return this;
        }
        public idMat3 Inverse()
        {
            idMat3 invMat = this;
            bool r = invMat.InverseSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseSelf()
        {
            // 18+3+9 = 30 multiplications
            //			 1 division
            idMat3 inverse = new idMat3(); idVec3[] inverse_mat = inverse.mat;
            inverse_mat[0].x = mat[1].y * mat[2].z - mat[1].z * mat[2].y;
            inverse_mat[1].x = mat[1].z * mat[2].x - mat[1].x * mat[2].z;
            inverse_mat[2].x = mat[1].x * mat[2].y - mat[1].y * mat[2].x;
            double det = mat[0].x * inverse_mat[0].x + mat[0].y * inverse_mat[1].x + mat[0].z * inverse_mat[2].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON) return false;
            double invDet = 1.0f / det;
            inverse_mat[0].y = mat[0].z * mat[2].y - mat[0].y * mat[2].z;
            inverse_mat[0].z = mat[0].y * mat[1].z - mat[0].z * mat[1].y;
            inverse_mat[1].y = mat[0].x * mat[2].z - mat[0].z * mat[2].x;
            inverse_mat[1].z = mat[0].z * mat[1].x - mat[0].x * mat[1].z;
            inverse_mat[2].y = mat[0].y * mat[2].x - mat[0].x * mat[2].y;
            inverse_mat[2].z = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            mat[0].x = (float)(inverse_mat[0].x * invDet);
            mat[0].y = (float)(inverse_mat[0].y * invDet);
            mat[0].z = (float)(inverse_mat[0].z * invDet);
            mat[1].x = (float)(inverse_mat[1].x * invDet);
            mat[1].y = (float)(inverse_mat[1].y * invDet);
            mat[1].z = (float)(inverse_mat[1].z * invDet);
            mat[2].x = (float)(inverse_mat[2].x * invDet);
            mat[2].y = (float)(inverse_mat[2].y * invDet);
            mat[2].z = (float)(inverse_mat[2].z * invDet);
            return true;
        }
        public idMat3 InverseFast()
        {
            idMat3 invMat = this;
            bool r = invMat.InverseFastSelf();
            Debug.Assert(r);
            return invMat;
        }
        public bool InverseFastSelf()
        {
#if true
            // 18+3+9 = 30 multiplications
            //			 1 division
            idMat3 inverse = new idMat3(); idVec3[] inverse_mat = inverse.mat;
            inverse_mat[0].x = mat[1].y * mat[2].z - mat[1].z * mat[2].y;
            inverse_mat[1].x = mat[1].z * mat[2].x - mat[1].x * mat[2].z;
            inverse_mat[2].x = mat[1].x * mat[2].y - mat[1].y * mat[2].x;
            double det = mat[0].x * inverse_mat[0].x + mat[0].y * inverse_mat[1].x + mat[0].z * inverse_mat[2].x;
            if (idMath.Fabs(det) < MATRIX_INVERSE_EPSILON) return false;
            double invDet = 1.0f / det;
            inverse_mat[0].y = mat[0].z * mat[2].y - mat[0].y * mat[2].z;
            inverse_mat[0].z = mat[0].y * mat[1].z - mat[0].z * mat[1].y;
            inverse_mat[1].y = mat[0].x * mat[2].z - mat[0].z * mat[2].x;
            inverse_mat[1].z = mat[0].z * mat[1].x - mat[0].x * mat[1].z;
            inverse_mat[2].y = mat[0].y * mat[2].x - mat[0].x * mat[2].y;
            inverse_mat[2].z = mat[0].x * mat[1].y - mat[0].y * mat[1].x;
            mat[0].x = (float)(inverse_mat[0].x * invDet);
            mat[0].y = (float)(inverse_mat[0].y * invDet);
            mat[0].z = (float)(inverse_mat[0].z * invDet);
            mat[1].x = (float)(inverse_mat[1].x * invDet);
            mat[1].y = (float)(inverse_mat[1].y * invDet);
            mat[1].z = (float)(inverse_mat[1].z * invDet);
            mat[2].x = (float)(inverse_mat[2].x * invDet);
            mat[2].y = (float)(inverse_mat[2].y * invDet);
            mat[2].z = (float)(inverse_mat[2].z * invDet);
            return true;
#elif false
	// 3*10 = 30 multiplications
	//		   3 divisions
	float *mat = reinterpret_cast<float *>(this);
	float s;
	double d, di;

	di = mat[0];
	s = di;
	mat[0] = d = 1.0f / di;
	mat[1] *= d;
	mat[2] *= d;
	d = -d;
	mat[3] *= d;
	mat[6] *= d;
	d = mat[3] * di;
	mat[4] += mat[1] * d;
	mat[5] += mat[2] * d;
	d = mat[6] * di;
	mat[7] += mat[1] * d;
	mat[8] += mat[2] * d;
	di = mat[4];
	s *= di;
	mat[4] = d = 1.0f / di;
	mat[3] *= d;
	mat[5] *= d;
	d = -d;
	mat[1] *= d;
	mat[7] *= d;
	d = mat[1] * di;
	mat[0] += mat[3] * d;
	mat[2] += mat[5] * d;
	d = mat[7] * di;
	mat[6] += mat[3] * d;
	mat[8] += mat[5] * d;
	di = mat[8];
	s *= di;
	mat[8] = d = 1.0f / di;
	mat[6] *= d;
	mat[7] *= d;
	d = -d;
	mat[2] *= d;
	mat[5] *= d;
	d = mat[2] * di;
	mat[0] += mat[6] * d;
	mat[1] += mat[7] * d;
	d = mat[5] * di;
	mat[3] += mat[6] * d;
	mat[4] += mat[7] * d;

	return ( s != 0.0f && !FLOAT_IS_NAN( s ) );
#else
	//	4*2+4*4 = 24 multiplications
	//		2*1 =  2 divisions
	idMat2 r0;
	float r1[2], r2[2], r3;
	float det, invDet;
	float *mat = reinterpret_cast<float *>(this);

	// r0 = m0.Inverse();	// 2x2
	det = mat[0*3+0] * mat[1*3+1] - mat[0*3+1] * mat[1*3+0];

	if ( idMath::Fabs( det ) < MATRIX_INVERSE_EPSILON ) {
		return false;
	}

	invDet = 1.0f / det;

	r0[0][0] =   mat[1*3+1] * invDet;
	r0[0][1] = - mat[0*3+1] * invDet;
	r0[1][0] = - mat[1*3+0] * invDet;
	r0[1][1] =   mat[0*3+0] * invDet;

	// r1 = r0 * m1;		// 2x1 = 2x2 * 2x1
	r1[0] = r0[0][0] * mat[0*3+2] + r0[0][1] * mat[1*3+2];
	r1[1] = r0[1][0] * mat[0*3+2] + r0[1][1] * mat[1*3+2];

	// r2 = m2 * r1;		// 1x1 = 1x2 * 2x1
	r2[0] = mat[2*3+0] * r1[0] + mat[2*3+1] * r1[1];

	// r3 = r2 - m3;		// 1x1 = 1x1 - 1x1
	r3 = r2[0] - mat[2*3+2];

	// r3.InverseSelf();
	if ( idMath::Fabs( r3 ) < MATRIX_INVERSE_EPSILON ) {
		return false;
	}

	r3 = 1.0f / r3;

	// r2 = m2 * r0;		// 1x2 = 1x2 * 2x2
	r2[0] = mat[2*3+0] * r0[0][0] + mat[2*3+1] * r0[1][0];
	r2[1] = mat[2*3+0] * r0[0][1] + mat[2*3+1] * r0[1][1];

	// m2 = r3 * r2;		// 1x2 = 1x1 * 1x2
	mat[2*3+0] = r3 * r2[0];
	mat[2*3+1] = r3 * r2[1];

	// m0 = r0 - r1 * m2;	// 2x2 - 2x1 * 1x2
	mat[0*3+0] = r0[0][0] - r1[0] * mat[2*3+0];
	mat[0*3+1] = r0[0][1] - r1[0] * mat[2*3+1];
	mat[1*3+0] = r0[1][0] - r1[1] * mat[2*3+0];
	mat[1*3+1] = r0[1][1] - r1[1] * mat[2*3+1];

	// m1 = r1 * r3;		// 2x1 = 2x1 * 1x1
	mat[0*3+2] = r1[0] * r3;
	mat[1*3+2] = r1[1] * r3;

	// m3 = -r3;
	mat[2*3+2] = -r3;

	return true;
#endif
        }
        public idMat3 TransposeMultiply(ref idMat3 b)
        {
            return new idMat3(
                mat[0].x * b.mat[0].x + mat[1].x * b.mat[1].x + mat[2].x * b.mat[2].x,
                mat[0].x * b.mat[0].y + mat[1].x * b.mat[1].y + mat[2].x * b.mat[2].y,
                mat[0].x * b.mat[0].z + mat[1].x * b.mat[1].z + mat[2].x * b.mat[2].z,
                mat[0].y * b.mat[0].x + mat[1].y * b.mat[1].x + mat[2].y * b.mat[2].x,
                mat[0].y * b.mat[0].y + mat[1].y * b.mat[1].y + mat[2].y * b.mat[2].y,
                mat[0].y * b.mat[0].z + mat[1].y * b.mat[1].z + mat[2].y * b.mat[2].z,
                mat[0].z * b.mat[0].x + mat[1].z * b.mat[1].x + mat[2].z * b.mat[2].x,
                mat[0].z * b.mat[0].y + mat[1].z * b.mat[1].y + mat[2].z * b.mat[2].y,
                mat[0].z * b.mat[0].z + mat[1].z * b.mat[1].z + mat[2].z * b.mat[2].z);
        }
        public void TransposeMultiply(ref idMat3 transpose, ref idMat3 b, ref idMat3 dst)
        {
            dst.mat[0].x = transpose.mat[0].x * b.mat[0].x + transpose.mat[1].x * b.mat[1].x + transpose.mat[2].x * b.mat[2].x;
            dst.mat[0].y = transpose.mat[0].x * b.mat[0].y + transpose.mat[1].x * b.mat[1].y + transpose.mat[2].x * b.mat[2].y;
            dst.mat[0].z = transpose.mat[0].x * b.mat[0].z + transpose.mat[1].x * b.mat[1].z + transpose.mat[2].x * b.mat[2].z;
            dst.mat[1].x = transpose.mat[0].y * b.mat[0].x + transpose.mat[1].y * b.mat[1].x + transpose.mat[2].y * b.mat[2].x;
            dst.mat[1].y = transpose.mat[0].y * b.mat[0].y + transpose.mat[1].y * b.mat[1].y + transpose.mat[2].y * b.mat[2].y;
            dst.mat[1].z = transpose.mat[0].y * b.mat[0].z + transpose.mat[1].y * b.mat[1].z + transpose.mat[2].y * b.mat[2].z;
            dst.mat[2].x = transpose.mat[0].z * b.mat[0].x + transpose.mat[1].z * b.mat[1].x + transpose.mat[2].z * b.mat[2].x;
            dst.mat[2].y = transpose.mat[0].z * b.mat[0].y + transpose.mat[1].z * b.mat[1].y + transpose.mat[2].z * b.mat[2].y;
            dst.mat[2].z = transpose.mat[0].z * b.mat[0].z + transpose.mat[1].z * b.mat[1].z + transpose.mat[2].z * b.mat[2].z;
        }
        public idMat3 SkewSymmetric(ref idVec3 src)
        {
            return new idMat3(0.0f, -src.z, src.y, src.z, 0.0f, -src.x, -src.y, src.x, 0.0f);
        }
        #endregion

        #region Inertia
        public idMat3 InertiaTranslate(float mass, ref idVec3 centerOfMass, ref idVec3 translation)
        {
            idVec3 newCenter = centerOfMass + translation;
            idMat3 m = new idMat3(); idVec3[] m_mat = m.mat;
            m_mat[0].x = mass * ((centerOfMass.y * centerOfMass.y + centerOfMass.z * centerOfMass.z) - (newCenter.y * newCenter.y + newCenter.z * newCenter.z));
            m_mat[1].y = mass * ((centerOfMass.x * centerOfMass.x + centerOfMass.z * centerOfMass.z) - (newCenter.x * newCenter.x + newCenter.z * newCenter.z));
            m_mat[2].z = mass * ((centerOfMass.x * centerOfMass.x + centerOfMass.y * centerOfMass.y) - (newCenter.x * newCenter.x + newCenter.y * newCenter.y));
            m_mat[0].y = m_mat[1].x = mass * (newCenter.x * newCenter.y - centerOfMass.x * centerOfMass.y);
            m_mat[1].z = m_mat[2].y = mass * (newCenter.y * newCenter.z - centerOfMass.y * centerOfMass.z);
            m_mat[0].z = m_mat[2].x = mass * (newCenter.x * newCenter.z - centerOfMass.x * centerOfMass.z);
            return this + m;
        }
        public idMat3 InertiaTranslateSelf(float mass, ref idVec3 centerOfMass, ref idVec3 translation)
        {
            idVec3 newCenter = centerOfMass + translation;
            idMat3 m = new idMat3(); idVec3[] m_mat = m.mat;
            m_mat[0].x = mass * ((centerOfMass.y * centerOfMass.y + centerOfMass.z * centerOfMass.z) - (newCenter.y * newCenter.y + newCenter.z * newCenter.z));
            m_mat[1].y = mass * ((centerOfMass.x * centerOfMass.x + centerOfMass.z * centerOfMass.z) - (newCenter.x * newCenter.x + newCenter.z * newCenter.z));
            m_mat[2].z = mass * ((centerOfMass.x * centerOfMass.x + centerOfMass.y * centerOfMass.y) - (newCenter.x * newCenter.x + newCenter.y * newCenter.y));
            m_mat[0].y = m_mat[1].x = mass * (newCenter.x * newCenter.y - centerOfMass.x * centerOfMass.y);
            m_mat[1].z = m_mat[2].y = mass * (newCenter.y * newCenter.z - centerOfMass.y * centerOfMass.z);
            m_mat[0].z = m_mat[2].x = mass * (newCenter.x * newCenter.z - centerOfMass.x * centerOfMass.y);
            this.opAdd(m);
            return this;
        }
        public idMat3 InertiaRotate(ref idMat3 rotation)
        {
            // NOTE: the rotation matrix is stored column-major
            return rotation.Transpose() * this * rotation;
        }
        public idMat3 InertiaRotateSelf(ref idMat3 rotation)
        {
            // NOTE: the rotation matrix is stored column-major
            this = rotation.Transpose() * this * rotation;
            return this;
        }
        #endregion

        #region Convert
        public idMat4 ToMat4()
        {
            // NOTE: idMat3 is transposed because it is column-major
            return new idMat4(
                mat[0].x, mat[1].x, mat[2].x, 0.0f,
                mat[0].y, mat[1].y, mat[2].y, 0.0f,
                mat[0].z, mat[1].z, mat[2].z, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
        }
        public idAngles ToAngles()
        {
            float sp = mat[0][2];
            // cap off our sin value so that we don't get any NANs
            if (sp > 1.0f)
                sp = 1.0f;
            else if (sp < -1.0f)
                sp = -1.0f;
            double theta = -asin(sp);
            double cp = cos(theta);
            idAngles angles = new idAngles();
            if (cp > 8192.0f * idMath.FLT_EPSILON)
            {
                angles.pitch = RAD2DEG(theta);
                angles.yaw = RAD2DEG(atan2(mat[0][1], mat[0][0]));
                angles.roll = RAD2DEG(atan2(mat[1][2], mat[2][2]));
            }
            else
            {
                angles.pitch = RAD2DEG(theta);
                angles.yaw = RAD2DEG(-atan2(mat[1][0], mat[1][1]));
                angles.roll = 0;
            }
            return angles;
        }

        static readonly int[] convertNext = { 1, 2, 0 };
        public idQuat ToQuat()
        {
            idQuat q = new idQuat();
            float trace = mat[0][0] + mat[1][1] + mat[2][2];
            if (trace > 0.0f)
            {
                float t = trace + 1.0f;
                float s = idMath.InvSqrt(t) * 0.5f;
                q[3] = s * t;
                q[0] = (mat[2][1] - mat[1][2]) * s;
                q[1] = (mat[0][2] - mat[2][0]) * s;
                q[2] = (mat[1][0] - mat[0][1]) * s;
            }
            else
            {
                int i = 0;
                if (mat[1][1] > mat[0][0])
                    i = 1;
                if (mat[2][2] > mat[i][i])
                    i = 2;
                int j = convertNext[i];
                int k = convertNext[j];
                float t = (mat[i][i] - (mat[j][j] + mat[k][k])) + 1.0f;
                float s = idMath.InvSqrt(t) * 0.5f;
                q[i] = s * t;
                q[3] = (mat[k][j] - mat[j][k]) * s;
                q[j] = (mat[j][i] + mat[i][j]) * s;
                q[k] = (mat[k][i] + mat[i][k]) * s;
            }
            return q;
        }
        public idCQuat ToCQuat()
        {
            idQuat q = ToQuat();
            return (q.w < 0.0f ? idCQuat(-q.x, -q.y, -q.z) : idCQuat(q.x, q.y, q.z));
        }
        public idRotation ToRotation()
        {
            idRotation r = new idRotation(); idVec3 r_vec = r.vec;
            float trace = mat[0][0] + mat[1][1] + mat[2][2];
            if (trace > 0.0f)
            {
                float t = trace + 1.0f;
                float s = idMath.InvSqrt(t) * 0.5f;
                r.angle = s * t;
                r_vec[0] = (mat[2][1] - mat[1][2]) * s;
                r_vec[1] = (mat[0][2] - mat[2][0]) * s;
                r_vec[2] = (mat[1][0] - mat[0][1]) * s;
            }
            else
            {
                int i = 0;
                if (mat[1][1] > mat[0][0])
                    i = 1;
                if (mat[2][2] > mat[i][i])
                    i = 2;
                int j = convertNext[i];
                int k = convertNext[j];
                float t = (mat[i][i] - (mat[j][j] + mat[k][k])) + 1.0f;
                float s = idMath.InvSqrt(t) * 0.5f;
                r_vec[i] = s * t;
                r.angle = (mat[k][j] - mat[j][k]) * s;
                r_vec[j] = (mat[j][i] + mat[i][j]) * s;
                r_vec[k] = (mat[k][i] + mat[i][k]) * s;
            }
            r.angle = idMath.ACos(r.angle);
            if (idMath.Fabs(r.angle) < 1e-10f)
            {
                r_vec.Set(0.0f, 0.0f, 1.0f);
                r.angle = 0.0f;
            }
            else
            {
                //vec *= (1.0f / sin( angle ));
                r_vec.Normalize();
                r_vec.FixDegenerateNormal();
                r.angle *= 2.0f * idMath.M_RAD2DEG;
            }
            r.origin.Zero();
            r.axis = *this;
            r.axisValid = true;
            return r;
        }
        idVec3 ToAngularVelocity()
        {
            idRotation rotation = ToRotation();
            return rotation.GetVec() * DEG2RAD(rotation.GetAngle());
        }
        #endregion
    }
}



