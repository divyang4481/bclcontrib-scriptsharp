#define MATX_SIMD
//#define MATX_QUAD( x )		( ( ( ( x ) + 3 ) & ~3 ) * sizeof( float ) )
//#define MATX_CLEAREND()		int s = numRows * numColumns; while( s < ( ( s + 3 ) & ~3 ) ) { mat[s++] = 0.0f; }
//#define MATX_ALLOCA( n )	( (float *) _alloca16( MATX_QUAD( n ) ) )
using System;
namespace IDT4.Math2
{
    public struct idMatX
    {
        const int MATX_MAX_TEMP		= 1024;
	static readonly float[]	temp = new float[MATX_MAX_TEMP+4];	// used to store intermediate results
	//static float[]	tempPtr;				// pointer to 16 byte aligned temporary memory
	static int		tempIndex;				// index into memory pool, wraps around

	int				numRows;				// number of rows
	int				numColumns;				// number of columns
	int				alloced;				// floats allocated, if -1 then mat points to data set with SetData
	float[]			mat;					// memory the matrix is stored

        
public idMatX( ) { numRows = numColumns = alloced = 0; mat = null; }

public idMatX( int rows, int columns ) { numRows = numColumns = alloced = 0; mat = null; SetSize( rows, columns ); }
public idMatX( int rows, int columns, float[] src ) { numRows = numColumns = alloced = 0; mat = null; SetData( rows, columns, src ); }

public void Dispose( ) {
	// if not temp memory
	if ( mat != null && ( mat < tempPtr || mat > tempPtr + MATX_MAX_TEMP ) && alloced != -1 ) {
		mat = null;
	}
}

void Set( int rows, int columns, float[] src ) {
	SetSize( rows, columns );
	Array.Copy( this.mat, src, rows * columns);
}

void Set( ref idMat3 m1, ref idMat3 m2 ) {
	SetSize( 3, 6 );
	for (int i = 0; i < 3; i++ ) {
		for (int j = 0; j < 3; j++ ) {
			mat[(i+0) * numColumns + (j+0)] = m1[i][j];
			mat[(i+0) * numColumns + (j+3)] = m2[i][j];
		}
	}
}

void Set( ref idMat3 m1, ref idMat3 m2, ref idMat3 m3, ref idMat3 m4 ) {
	SetSize( 6, 6 );
	for (int i = 0; i < 3; i++ ) {
		for (int j = 0; j < 3; j++ ) {
			mat[(i+0) * numColumns + (j+0)] = m1[i][j];
			mat[(i+0) * numColumns + (j+3)] = m2[i][j];
			mat[(i+3) * numColumns + (j+0)] = m3[i][j];
			mat[(i+3) * numColumns + (j+3)] = m4[i][j];
		}
	}
}

static const float[] operator[]( int index ) const {
	assert( ( index >= 0 ) && ( index < numRows ) );
	return mat + index * numColumns;
}

ID_INLINE float *operator[]( int index ) {
	assert( ( index >= 0 ) && ( index < numRows ) );
	return mat + index * numColumns;
}

idMatX operator=( const idMatX &a ) {
	SetSize( a.numRows, a.numColumns );
#if MATX_SIMD
	SIMDProcessor->Copy16( mat, a.mat, a.numRows * a.numColumns );
#else
	memcpy( mat, a.mat, a.numRows * a.numColumns * sizeof( float ) );
#endif
	tempIndex = 0;
	return *this;
}

idMatX operator*( float a ) {
	idMatX m;

	m.SetTempSize( numRows, numColumns );
#if MATX_SIMD
	SIMDProcessor->Mul16( m.mat, mat, a, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		m.mat[i] = mat[i] * a;
	}
#endif
	return m;
}

ID_INLINE idVecX operator*( ref idVecX vec ) {
	idVecX dst;

	assert( numColumns == vec.GetSize() );

	dst.SetTempSize( numRows );
#if MATX_SIMD
	SIMDProcessor->MatX_MultiplyVecX( dst, *this, vec );
#else
	Multiply( dst, vec );
#endif
	return dst;
}

idMatX operator*( ref idMatX a ) {
	idMatX dst;

	Debug.Assert( numColumns == a.numRows );
	dst.SetTempSize( numRows, a.numColumns );
#if MATX_SIMD
	SIMDProcessor->MatX_MultiplyMatX( dst, *this, a );
#else
	Multiply( dst, a );
#endif
	return dst;
}

idMatX operator+( ref idMatX a ) {
	idMatX m;

	assert( numRows == a.numRows && numColumns == a.numColumns );
	m.SetTempSize( numRows, numColumns );
#if MATX_SIMD
	SIMDProcessor->Add16( m.mat, mat, a.mat, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		m.mat[i] = mat[i] + a.mat[i];
	}
#endif
	return m;
}

idMatX operator-( ref idMatX a ) {
	idMatX m;
	assert( numRows == a.numRows && numColumns == a.numColumns );
	m.SetTempSize( numRows, numColumns );
#if MATX_SIMD
	SIMDProcessor->Sub16( m.mat, mat, a.mat, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		m.mat[i] = mat[i] - a.mat[i];
	}
#endif
	return m;
}

idMatX &operator*=( float a ) {
#if MATX_SIMD
	SIMDProcessor->MulAssign16( mat, a, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] *= a;
	}
#endif
	tempIndex = 0;
	return *this;
}

ID_INLINE idMatX &operator*=( const idMatX &a ) {
	*this = *this * a;
	tempIndex = 0;
	return *this;
}

ID_INLINE idMatX &operator+=( const idMatX &a ) {
	assert( numRows == a.numRows && numColumns == a.numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->AddAssign16( mat, a.mat, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] += a.mat[i];
	}
#endif
	tempIndex = 0;
	return *this;
}

ID_INLINE idMatX &operator-=( const idMatX &a ) {
	assert( numRows == a.numRows && numColumns == a.numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->SubAssign16( mat, a.mat, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] -= a.mat[i];
	}
#endif
	tempIndex = 0;
	return *this;
}

ID_INLINE idMatX operator*( const float a, idMatX const &m ) {
	return m * a;
}

ID_INLINE idVecX operator*( const idVecX &vec, const idMatX &m ) {
	return m * vec;
}

ID_INLINE idVecX &operator*=( idVecX &vec, const idMatX &m ) {
	vec = m * vec;
	return vec;
}

ID_INLINE bool Compare( const idMatX &a ) const {
	int i, s;

	assert( numRows == a.numRows && numColumns == a.numColumns );

	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		if ( mat[i] != a.mat[i] ) {
			return false;
		}
	}
	return true;
}

ID_INLINE bool Compare( const idMatX &a, const float epsilon ) const {
	int i, s;

	assert( numRows == a.numRows && numColumns == a.numColumns );

	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		if ( idMath::Fabs( mat[i] - a.mat[i] ) > epsilon ) {
			return false;
		}
	}
	return true;
}

ID_INLINE bool operator==( const idMatX &a ) const {
	return Compare( a );
}

ID_INLINE bool operator!=( const idMatX &a ) const {
	return !Compare( a );
}

ID_INLINE void SetSize( int rows, int columns ) {
	assert( mat < tempPtr || mat > tempPtr + MATX_MAX_TEMP );
	int alloc = ( rows * columns + 3 ) & ~3;
	if ( alloc > alloced && alloced != -1 ) {
		if ( mat != NULL ) {
			Mem_Free16( mat );
		}
		mat = (float *) Mem_Alloc16( alloc * sizeof( float ) );
		alloced = alloc;
	}
	numRows = rows;
	numColumns = columns;
	MATX_CLEAREND();
}

ID_INLINE void SetTempSize( int rows, int columns ) {
	int newSize;

	newSize = ( rows * columns + 3 ) & ~3;
	assert( newSize < MATX_MAX_TEMP );
	if ( tempIndex + newSize > MATX_MAX_TEMP ) {
		tempIndex = 0;
	}
	mat = tempPtr + tempIndex;
	tempIndex += newSize;
	alloced = newSize;
	numRows = rows;
	numColumns = columns;
	MATX_CLEAREND();
}

ID_INLINE void SetData( int rows, int columns, float *data ) {
	assert( mat < tempPtr || mat > tempPtr + MATX_MAX_TEMP );
	if ( mat != NULL && alloced != -1 ) {
		Mem_Free16( mat );
	}
	assert( ( ( (int) data ) & 15 ) == 0 ); // data must be 16 byte aligned
	mat = data;
	alloced = -1;
	numRows = rows;
	numColumns = columns;
	MATX_CLEAREND();
}

ID_INLINE void Zero( void ) {
#ifdef MATX_SIMD
	SIMDProcessor->Zero16( mat, numRows * numColumns );
#else
	memset( mat, 0, numRows * numColumns * sizeof( float ) );
#endif
}

ID_INLINE void Zero( int rows, int columns ) {
	SetSize( rows, columns );
#ifdef MATX_SIMD
	SIMDProcessor->Zero16( mat, numRows * numColumns );
#else
	memset( mat, 0, rows * columns * sizeof( float ) );
#endif
}

ID_INLINE void Identity( void ) {
	assert( numRows == numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->Zero16( mat, numRows * numColumns );
#else
	memset( mat, 0, numRows * numColumns * sizeof( float ) );
#endif
	for ( int i = 0; i < numRows; i++ ) {
		mat[i * numColumns + i] = 1.0f;
	}
}

ID_INLINE void Identity( int rows, int columns ) {
	assert( rows == columns );
	SetSize( rows, columns );
	Identity();
}

ID_INLINE void Diag( const idVecX &v ) {
	Zero( v.GetSize(), v.GetSize() );
	for ( int i = 0; i < v.GetSize(); i++ ) {
		mat[i * numColumns + i] = v[i];
	}
}

ID_INLINE void Random( int seed, float l, float u ) {
	int i, s;
	float c;
	idRandom rnd(seed);

	c = u - l;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] = l + rnd.RandomFloat() * c;
	}
}

ID_INLINE void Random( int rows, int columns, int seed, float l, float u ) {
	int i, s;
	float c;
	idRandom rnd(seed);

	SetSize( rows, columns );
	c = u - l;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] = l + rnd.RandomFloat() * c;
	}
}

ID_INLINE void Negate( void ) {
#ifdef MATX_SIMD
	SIMDProcessor->Negate16( mat, numRows * numColumns );
#else
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		mat[i] = -mat[i];
	}
#endif
}

ID_INLINE void Clamp( float min, float max ) {
	int i, s;
	s = numRows * numColumns;
	for ( i = 0; i < s; i++ ) {
		if ( mat[i] < min ) {
			mat[i] = min;
		} else if ( mat[i] > max ) {
			mat[i] = max;
		}
	}
}

ID_INLINE idMatX &SwapRows( int r1, int r2 ) {
	float *ptr;

	ptr = (float *) _alloca16( numColumns * sizeof( float ) );
	memcpy( ptr, mat + r1 * numColumns, numColumns * sizeof( float ) );
	memcpy( mat + r1 * numColumns, mat + r2 * numColumns, numColumns * sizeof( float ) );
	memcpy( mat + r2 * numColumns, ptr, numColumns * sizeof( float ) );

	return *this;
}

ID_INLINE idMatX &SwapColumns( int r1, int r2 ) {
	int i;
	float tmp, *ptr;

	for ( i = 0; i < numRows; i++ ) {
		ptr = mat + i * numColumns;
		tmp = ptr[r1];
		ptr[r1] = ptr[r2];
		ptr[r2] = tmp;
	}

	return *this;
}

ID_INLINE idMatX &SwapRowsColumns( int r1, int r2 ) {

	SwapRows( r1, r2 );
	SwapColumns( r1, r2 );
	return *this;
}

ID_INLINE void ClearUpperTriangle( void ) {
	assert( numRows == numColumns );
	for ( int i = numRows-2; i >= 0; i-- ) {
		memset( mat + i * numColumns + i + 1, 0, (numColumns - 1 - i) * sizeof(float) );
	}
}

ID_INLINE void ClearLowerTriangle( void ) {
	assert( numRows == numColumns );
	for ( int i = 1; i < numRows; i++ ) {
		memset( mat + i * numColumns, 0, i * sizeof(float) );
	}
}

ID_INLINE void SquareSubMatrix( const idMatX &m, int size ) {
	int i;
	assert( size <= m.numRows && size <= m.numColumns );
	SetSize( size, size );
	for ( i = 0; i < size; i++ ) {
		memcpy( mat + i * numColumns, m.mat + i * m.numColumns, size * sizeof( float ) );
	}
}

ID_INLINE float MaxDifference( const idMatX &m ) const {
	int i, j;
	float diff, maxDiff;

	assert( numRows == m.numRows && numColumns == m.numColumns );

	maxDiff = -1.0f;
	for ( i = 0; i < numRows; i++ ) {
		for ( j = 0; j < numColumns; j++ ) {
			diff = idMath::Fabs( mat[ i * numColumns + j ] - m[i][j] );
			if ( maxDiff < 0.0f || diff > maxDiff ) {
				maxDiff = diff;
			}
		}
	}
	return maxDiff;
}

ID_INLINE bool IsZero( const float epsilon ) const {
	// returns true if (*this) == Zero
	for ( int i = 0; i < numRows; i++ ) {
		for ( int j = 0; j < numColumns; j++ ) {
			if ( idMath::Fabs( mat[i * numColumns + j] ) > epsilon ) {
				return false;
			}
		}
	}
	return true;
}

ID_INLINE bool IsIdentity( const float epsilon ) const {
	// returns true if (*this) == Identity
	assert( numRows == numColumns );
	for ( int i = 0; i < numRows; i++ ) {
		for ( int j = 0; j < numColumns; j++ ) {
			if ( idMath::Fabs( mat[i * numColumns + j] - (float)( i == j ) ) > epsilon ) {
				return false;
			}
		}
	}
	return true;
}

ID_INLINE bool IsDiagonal( const float epsilon ) const {
	// returns true if all elements are zero except for the elements on the diagonal
	assert( numRows == numColumns );
	for ( int i = 0; i < numRows; i++ ) {
		for ( int j = 0; j < numColumns; j++ ) {
			if ( i != j && idMath::Fabs( mat[i * numColumns + j] ) > epsilon ) {
				return false;
			}
		}
	}
	return true;
}

ID_INLINE bool IsTriDiagonal( const float epsilon ) const {
	// returns true if all elements are zero except for the elements on the diagonal plus or minus one column

	if ( numRows != numColumns ) {
		return false;
	}
	for ( int i = 0; i < numRows-2; i++ ) {
		for ( int j = i+2; j < numColumns; j++ ) {
			if ( idMath::Fabs( (*this)[i][j] ) > epsilon ) {
				return false;
			}
			if ( idMath::Fabs( (*this)[j][i] ) > epsilon ) {
				return false;
			}
		}
	}
	return true;
}

ID_INLINE bool IsSymmetric( const float epsilon ) const {
	// (*this)[i][j] == (*this)[j][i]
	if ( numRows != numColumns ) {
		return false;
	}
	for ( int i = 0; i < numRows; i++ ) {
		for ( int j = 0; j < numColumns; j++ ) {
			if ( idMath::Fabs( mat[ i * numColumns + j ] - mat[ j * numColumns + i ] ) > epsilon ) {
				return false;
			}
		}
	}
	return true;
}

ID_INLINE float Trace( void ) const {
	float trace = 0.0f;

	assert( numRows == numColumns );

	// sum of elements on the diagonal
	for ( int i = 0; i < numRows; i++ ) {
		trace += mat[i * numRows + i];
	}
	return trace;
}

ID_INLINE float Determinant( void ) const {

	assert( numRows == numColumns );

	switch( numRows ) {
		case 1:
			return mat[0];
		case 2:
			return reinterpret_cast<const idMat2 *>(mat)->Determinant();
		case 3:
			return reinterpret_cast<const idMat3 *>(mat)->Determinant();
		case 4:
			return reinterpret_cast<const idMat4 *>(mat)->Determinant();
		case 5:
			return reinterpret_cast<const idMat5 *>(mat)->Determinant();
		case 6:
			return reinterpret_cast<const idMat6 *>(mat)->Determinant();
		default:
			return DeterminantGeneric();
	}
	return 0.0f;
}

ID_INLINE idMatX Transpose( void ) const {
	idMatX transpose;
	int i, j;

	transpose.SetTempSize( numColumns, numRows );

	for ( i = 0; i < numRows; i++ ) {
		for ( j = 0; j < numColumns; j++ ) {
			transpose.mat[j * transpose.numColumns + i] = mat[i * numColumns + j];
		}
	}

	return transpose;
}

ID_INLINE idMatX &TransposeSelf( void ) {
	*this = Transpose();
	return *this;
}

ID_INLINE idMatX Inverse( void ) const {
	idMatX invMat;

	invMat.SetTempSize( numRows, numColumns );
	memcpy( invMat.mat, mat, numRows * numColumns * sizeof( float ) );
	int r = invMat.InverseSelf();
	assert( r );
	return invMat;
}

ID_INLINE bool InverseSelf( void ) {

	assert( numRows == numColumns );

	switch( numRows ) {
		case 1:
			if ( idMath::Fabs( mat[0] ) < MATRIX_INVERSE_EPSILON ) {
				return false;
			}
			mat[0] = 1.0f / mat[0];
			return true;
		case 2:
			return reinterpret_cast<idMat2 *>(mat)->InverseSelf();
		case 3:
			return reinterpret_cast<idMat3 *>(mat)->InverseSelf();
		case 4:
			return reinterpret_cast<idMat4 *>(mat)->InverseSelf();
		case 5:
			return reinterpret_cast<idMat5 *>(mat)->InverseSelf();
		case 6:
			return reinterpret_cast<idMat6 *>(mat)->InverseSelf();
		default:
			return InverseSelfGeneric();
	}
}

ID_INLINE idMatX InverseFast( void ) const {
	idMatX invMat;

	invMat.SetTempSize( numRows, numColumns );
	memcpy( invMat.mat, mat, numRows * numColumns * sizeof( float ) );
	int r = invMat.InverseFastSelf();
	assert( r );
	return invMat;
}

ID_INLINE bool InverseFastSelf( void ) {

	assert( numRows == numColumns );

	switch( numRows ) {
		case 1:
			if ( idMath::Fabs( mat[0] ) < MATRIX_INVERSE_EPSILON ) {
				return false;
			}
			mat[0] = 1.0f / mat[0];
			return true;
		case 2:
			return reinterpret_cast<idMat2 *>(mat)->InverseFastSelf();
		case 3:
			return reinterpret_cast<idMat3 *>(mat)->InverseFastSelf();
		case 4:
			return reinterpret_cast<idMat4 *>(mat)->InverseFastSelf();
		case 5:
			return reinterpret_cast<idMat5 *>(mat)->InverseFastSelf();
		case 6:
			return reinterpret_cast<idMat6 *>(mat)->InverseFastSelf();
		default:
			return InverseSelfGeneric();
	}
	return false;
}

ID_INLINE idVecX Multiply( const idVecX &vec ) const {
	idVecX dst;

	assert( numColumns == vec.GetSize() );

	dst.SetTempSize( numRows );
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplyVecX( dst, *this, vec );
#else
	Multiply( dst, vec );
#endif
	return dst;
}

ID_INLINE idMatX Multiply( const idMatX &a ) const {
	idMatX dst;

	assert( numColumns == a.numRows );

	dst.SetTempSize( numRows, a.numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplyMatX( dst, *this, a );
#else
	Multiply( dst, a );
#endif
	return dst;
}

ID_INLINE idVecX TransposeMultiply( const idVecX &vec ) const {
	idVecX dst;

	assert( numRows == vec.GetSize() );

	dst.SetTempSize( numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplyVecX( dst, *this, vec );
#else
	TransposeMultiply( dst, vec );
#endif
	return dst;
}

ID_INLINE idMatX TransposeMultiply( const idMatX &a ) const {
	idMatX dst;

	assert( numRows == a.numRows );

	dst.SetTempSize( numColumns, a.numColumns );
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplyMatX( dst, *this, a );
#else
	TransposeMultiply( dst, a );
#endif
	return dst;
}

ID_INLINE void Multiply( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplyVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	mPtr = mat;
	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numRows; i++ ) {
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numColumns; j++ ) {
			sum += mPtr[j] * vPtr[j];
		}
		dstPtr[i] = sum;
		mPtr += numColumns;
	}
#endif
}

ID_INLINE void MultiplyAdd( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplyAddVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	mPtr = mat;
	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numRows; i++ ) {
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numColumns; j++ ) {
			sum += mPtr[j] * vPtr[j];
		}
		dstPtr[i] += sum;
		mPtr += numColumns;
	}
#endif
}

ID_INLINE void MultiplySub( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplySubVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	mPtr = mat;
	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numRows; i++ ) {
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numColumns; j++ ) {
			sum += mPtr[j] * vPtr[j];
		}
		dstPtr[i] -= sum;
		mPtr += numColumns;
	}
#endif
}

ID_INLINE void TransposeMultiply( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplyVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numColumns; i++ ) {
		mPtr = mat + i;
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numRows; j++ ) {
			mPtr += numColumns;
			sum += mPtr[0] * vPtr[j];
		}
		dstPtr[i] = sum;
	}
#endif
}

ID_INLINE void TransposeMultiplyAdd( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplyAddVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numColumns; i++ ) {
		mPtr = mat + i;
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numRows; j++ ) {
			mPtr += numColumns;
			sum += mPtr[0] * vPtr[j];
		}
		dstPtr[i] += sum;
	}
#endif
}

ID_INLINE void TransposeMultiplySub( idVecX &dst, const idVecX &vec ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplySubVecX( dst, *this, vec );
#else
	int i, j;
	const float *mPtr, *vPtr;
	float *dstPtr;

	vPtr = vec.ToFloatPtr();
	dstPtr = dst.ToFloatPtr();
	for ( i = 0; i < numColumns; i++ ) {
		mPtr = mat + i;
		float sum = mPtr[0] * vPtr[0];
		for ( j = 1; j < numRows; j++ ) {
			mPtr += numColumns;
			sum += mPtr[0] * vPtr[j];
		}
		dstPtr[i] -= sum;
	}
#endif
}

ID_INLINE void Multiply( idMatX &dst, const idMatX &a ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_MultiplyMatX( dst, *this, a );
#else
	int i, j, k, l, n;
	float *dstPtr;
	const float *m1Ptr, *m2Ptr;
	double sum;

	assert( numColumns == a.numRows );

	dstPtr = dst.ToFloatPtr();
	m1Ptr = ToFloatPtr();
	m2Ptr = a.ToFloatPtr();
	k = numRows;
	l = a.GetNumColumns();

	for ( i = 0; i < k; i++ ) {
		for ( j = 0; j < l; j++ ) {
			m2Ptr = a.ToFloatPtr() + j;
			sum = m1Ptr[0] * m2Ptr[0];
			for ( n = 1; n < numColumns; n++ ) {
				m2Ptr += l;
				sum += m1Ptr[n] * m2Ptr[0];
			}
			*dstPtr++ = sum;
		}
		m1Ptr += numColumns;
	}
#endif
}

ID_INLINE void TransposeMultiply( idMatX &dst, const idMatX &a ) const {
#ifdef MATX_SIMD
	SIMDProcessor->MatX_TransposeMultiplyMatX( dst, *this, a );
#else
	int i, j, k, l, n;
	float *dstPtr;
	const float *m1Ptr, *m2Ptr;
	double sum;

	assert( numRows == a.numRows );

	dstPtr = dst.ToFloatPtr();
	m1Ptr = ToFloatPtr();
	k = numColumns;
	l = a.numColumns;

	for ( i = 0; i < k; i++ ) {
		for ( j = 0; j < l; j++ ) {
			m1Ptr = ToFloatPtr() + i;
			m2Ptr = a.ToFloatPtr() + j;
			sum = m1Ptr[0] * m2Ptr[0];
			for ( n = 1; n < numRows; n++ ) {
				m1Ptr += numColumns;
				m2Ptr += a.numColumns;
				sum += m1Ptr[0] * m2Ptr[0];
			}
			*dstPtr++ = sum;
		}
	}
#endif
}

ID_INLINE int GetDimension( void ) const {
	return numRows * numColumns;
}

ID_INLINE const idVec6 &SubVec6( int row ) const {
	assert( numColumns >= 6 && row >= 0 && row < numRows );
	return *reinterpret_cast<const idVec6 *>(mat + row * numColumns);
}

ID_INLINE idVec6 &SubVec6( int row ) {
	assert( numColumns >= 6 && row >= 0 && row < numRows );
	return *reinterpret_cast<idVec6 *>(mat + row * numColumns);
}

ID_INLINE const idVecX SubVecX( int row ) const {
	idVecX v;
	assert( row >= 0 && row < numRows );
	v.SetData( numColumns, mat + row * numColumns );
	return v;
}

ID_INLINE idVecX SubVecX( int row ) {
	idVecX v;
	assert( row >= 0 && row < numRows );
	v.SetData( numColumns, mat + row * numColumns );
	return v;
}

ID_INLINE const float *ToFloatPtr( void ) const {
	return mat;
}

ID_INLINE float *ToFloatPtr( void ) {
	return mat;
}

    }
}



