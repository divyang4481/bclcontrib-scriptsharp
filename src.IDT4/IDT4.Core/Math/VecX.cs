#define VECX_SIMD
//#define VECX_QUAD( x )		( ( ( ( x ) + 3 ) & ~3 ) * sizeof( float ) )
//#define VECX_CLEAREND()		int s = size; while( s < ( ( s + 3) & ~3 ) ) { p[s++] = 0.0f; }
//#define VECX_ALLOCA( n )	( (float *) _alloca16( VECX_QUAD( n ) ) )
using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idVecX
    {
        const int VECX_MAX_TEMP = 1024;
        static readonly float[] temp = new float[VECX_MAX_TEMP + 4];	// used to store intermediate results
        static int tempIndex;				// index into memory pool, wraps around
        int size;					// size of the vector
        int alloced;				// if -1 p points to data set with SetData
        float[] p;						// memory the vector is stored

        public idVecX() { size = alloced = 0; p = null; }
        public idVecX(int length) { size = alloced = 0; p = null; SetSize(length); }
        public idVecX(float[] data) { size = alloced = 0; p = null; Set(data); }
        public void Set(float[] data)
        {
            p = data;
            size = data.Length;
            alloced = -1;
#if VECX_SIMD
            VECX_CLEAREND();
#endif
        }
        public void Zero()
        {
#if VECX_SIMD
            SIMDProcessor.Zero16(p, size);
#else
            Array.Clear(p, 0, size);
#endif
        }
        public int GetDimension() { return size; }
        public idVec3 SubVec3(int index) { Debug.Assert(index >= 0 && index * 3 + 3 <= size); int i = index * 3; return new idVec3(p[i], p[i + 1], p[i + 2]); }
        public idVec6 SubVec6(int index) { Debug.Assert(index >= 0 && index * 6 + 6 <= size); int i = index * 6; return new idVec6(p[i], p[i + 1], p[i + 2], p[i + 3], p[i + 4], p[i + 5]); }
        public float[] ToArray() { return p; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        public float this[int index] { get { Debug.Assert(index >= 0 && index < size); return p[index]; } set { p[index] = value; } }
        public static idVecX operator -(idVecX a)
        {
            idVecX m = new idVecX();
            m.SetTempSize(a.size);
            for (int i = 0; i < a.size; i++)
                m.p[i] = -m.p[i];
            return m;
        }

        public static idVecX operator +(idVecX a, idVecX b)
        {
            Debug.Assert(a.size == b.size);
            idVecX m = new idVecX();
            m.SetTempSize(a.size);
#if VECX_SIMD
            SIMDProcessor.Add16(m.p, a.p, b.p, a.size);
#else
            for (int i = 0; i < a.size; i++)
                m.p[i] = a.p[i] + b.p[i];
#endif
            return m;
        }

        public static idVecX operator -(idVecX a, idVecX b)
        {
            Debug.Assert(a.size == b.size);
            idVecX m = new idVecX();
            m.SetTempSize(a.size);
#if VECX_SIMD
            SIMDProcessor.Sub16(m.p, p, a.p, size);
#else
            for (int i = 0; i < a.size; i++)
                m.p[i] = a.p[i] - b.p[i];
#endif
            return m;
        }

        public static idVecX operator *(idVecX a, float b)
        {
            idVecX m = new idVecX();
            m.SetTempSize(a.size);
#if VECX_SIMD
            SIMDProcessor.Mul16(m.p, p, a, size);
#else
            for (int i = 0; i < a.size; i++)
                m.p[i] = a.p[i] * b;
#endif
            return m;
        }

        public static float operator *(idVecX a, idVecX b)
        {
            Debug.Assert(a.size == b.size);
            float sum = 0.0f;
            for (int i = 0; i < a.size; i++)
                sum += a.p[i] * a.p[i];
            return sum;
        }

        public static idVecX operator /(idVecX a, float b)
        {
            Debug.Assert(b != 0.0f);
            return a * (1.0f / b);
        }

        public idVecX opAdd(idVecX a)
        {
            Debug.Assert(size == a.size);
#if VECX_SIMD
            SIMDProcessor.AddAssign16(p, a.p, size);
#else
            for (int i = 0; i < size; i++)
                p[i] += a.p[i];
#endif
            tempIndex = 0;
            return this;
        }

        public idVecX opSub(idVecX a)
        {
            Debug.Assert(size == a.size);
#if VECX_SIMD
            SIMDProcessor.SubAssign16(p, a.p, size);
#else
            int i;
            for (i = 0; i < size; i++)
                p[i] -= a.p[i];
#endif
            tempIndex = 0;
            return this;
        }

        public idVecX opMul(float a)
        {
#if VECX_SIMD
            SIMDProcessor.MulAssign16(p, a, size);
#else
            for (int i = 0; i < size; i++)
                p[i] *= a;
#endif
            return this;
        }

        public idVecX opDiv(float a)
        {
            Debug.Assert(a != 0.0f);
            opMul(1.0f / a);
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idVecX a)
        {
            Debug.Assert(size == a.size);
            for (int i = 0; i < size; i++)
                if (p[i] != a.p[i])
                    return false;
            return true;
        }
        public bool Compare(ref idVecX a, float epsilon)
        {
            Debug.Assert(size == a.size);
            for (int i = 0; i < size; i++)
                if (idMath.Fabs(p[i] - a.p[i]) > epsilon)
                    return false;
            return true;
        }
        public static bool operator ==(idVecX a, idVecX b) { return a.Compare(ref b); }
        public static bool operator !=(idVecX a, idVecX b) { return !a.Compare(ref b); }
        #endregion

        public void SetSize(int newSize)
        {
            int alloc = (newSize + 3) & ~3;
            if (alloc > alloced && alloced != -1)
            {
                p = new float[alloc];
                alloced = alloc;
            }
            size = newSize;
#if VECX_SIMD
            VECX_CLEAREND();
#endif
        }
        public void ChangeSize(int newSize, bool makeZero)
        {
            int alloc = (newSize + 3) & ~3;
            if (alloc > alloced && alloced != -1)
            {
                float[] oldVec = p;
                p = new float[alloc];
                alloced = alloc;
                if (oldVec != null)
                    for (int i = 0; i < size; i++)
                        p[i] = oldVec[i];
                if (makeZero)
                    // zero any new elements
                    for (int i = size; i < newSize; i++)
                        p[i] = 0.0f;
            }
            size = newSize;
#if VECX_SIMD
            VECX_CLEAREND();
#endif
        }

        public void SetTempSize(int newSize)
        {
            size = newSize;
            alloced = (newSize + 3) & ~3;
            //Debug.Assert(alloced < VECX_MAX_TEMP);
            //if (tempIndex + alloced > VECX_MAX_TEMP)
            //    tempIndex = 0;
            //p = temp[tempIndex];
            //tempIndex += alloced;
            p = new float[alloced];
#if VECX_SIMD
            VECX_CLEAREND();
#endif
        }

        public void Zero(int length)
        {
            SetSize(length);
#if VECX_SIMD
            SIMDProcessor.Zero16(p, length);
#else
            Array.Clear(p, 0, size);
#endif
        }

        public void Random(int seed, float l, float u)
        {
            idRandom rnd = new idRandom(seed);
            float c = u - l;
            for (int i = 0; i < size; i++)
                p[i] = l + rnd.RandomFloat() * c;
        }

        public void Random(int length, int seed, float l, float u)
        {
            idRandom rnd = new idRandom(seed);
            SetSize(length);
            float c = u - l;
            for (int i = 0; i < size; i++)
                p[i] = l + rnd.RandomFloat() * c;
        }

        public void Negate()
        {
#if VECX_SIMD
            SIMDProcessor.Negate16(p, size);
#else
            for (int i = 0; i < size; i++)
                p[i] = -p[i];
#endif
        }

        public void Clamp(float min, float max)
        {
            for (int i = 0; i < size; i++)
                if (p[i] < min)
                    p[i] = min;
                else if (p[i] > max)
                    p[i] = max;
        }

        public idVecX SwapElements(int e1, int e2)
        {
            float tmp = p[e1];
            p[e1] = p[e2];
            p[e2] = tmp;
            return this;
        }

        public float Length()
        {
            float sum = 0.0f;
            for (int i = 0; i < size; i++)
                sum += p[i] * p[i];
            return (float)Math.Sqrt(sum);
        }

        public float LengthSqr()
        {
            float sum = 0.0f;
            for (int i = 0; i < size; i++)
                sum += p[i] * p[i];
            return sum;
        }

        public idVecX Normalize()
        {
            idVecX m = new idVecX();
            float sum = 0.0f;
            m.SetTempSize(size);
            for (int i = 0; i < size; i++)
                sum += p[i] * p[i];
            float invSqrt = idMath.InvSqrt(sum);
            for (int i = 0; i < size; i++)
                m.p[i] = p[i] * invSqrt;
            return m;
        }

        public float NormalizeSelf()
        {
            float sum = 0.0f;
            for (int i = 0; i < size; i++)
                sum += p[i] * p[i];
            float invSqrt = idMath.InvSqrt(sum);
            for (int i = 0; i < size; i++)
                p[i] *= invSqrt;
            return invSqrt * sum;
        }
    }
}
