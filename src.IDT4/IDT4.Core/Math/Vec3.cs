using System;
namespace IDT4.Math2
{
    public struct idVec3
    {
        public readonly static idVec3 origin = new idVec3(0f, 0f, 0f);
        public float x;
        public float y;
        public float z;

        public idVec3() { x = y = z = 0f; }
        public idVec3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public void Set(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
        public void Zero() { x = y = z = 0f; }
        public int GetDimension() { return 3; }
        public idVec2 ToVec2() { return new idVec2(x, y); }
        public float[] ToArray() { return new float[] { x, y, z }; }
        public string ToString(int precision) { return StringEx.ToString(ToArray(), precision); }
        public static void Lerp(out idVec2 t, idVec2 v1, idVec2 v2, float l)
        {
            if (l <= 0.0f) t = v1;
            else if (l >= 1.0f) t = v2;
            else t = v1 + (v2 - v1) * l;
        }

        #region Operator
        //public float this[int index] { get { throw new NotSupportedException(); } }
        public static idVec3 operator -(idVec3 a) { return new idVec3(-a.x, -a.y, -a.z); }
        public static idVec3 operator -(idVec3 a, idVec3 b) { return new idVec3(a.x - b.x, a.y - b.y, a.z - b.z); }
        public static float operator *(idVec3 a, idVec3 b) { return a.x * b.x + a.y * b.y + a.z * b.z; }
        public static idVec3 operator *(idVec3 a, float b) { return new idVec3(a.x * b, a.y * b, a.z * b); }
        public static idVec3 operator /(idVec3 a, float b) { float invb = 1.0f / b; return new idVec3(a.x * invb, a.y * invb, a.z * invb); }
        public static idVec3 operator +(idVec3 a, idVec3 b) { return new idVec3(a.x + b.x, a.y + b.y, a.z + b.z); }
        public idVec3 opAdd(idVec3 a) { x += a.x; y += a.y; z += a.z; return this; }
        public idVec3 opSub(idVec3 a) { x -= a.x; y -= a.y; z -= a.z; return this; }
        public idVec3 opDiv(idVec3 a) { x /= a.x; y /= a.y; z /= a.z; return this; }
        public idVec3 opMul(float a) { x *= a; y *= a; z *= a; return this; }
        public idVec3 opDiv(float a) { float inva = 1.0f / a; x *= inva; y *= inva; z *= inva; return this; }
        #endregion

        #region Compare
        public bool Compare(ref idVec3 a) { return (x == a.x && y == a.y && z == a.z); }
        public bool Compare(ref idVec3 a, float epsilon) { return (idMath.Fabs(x - a.x) <= epsilon && idMath.Fabs(y - a.y) <= epsilon && idMath.Fabs(z - a.z) <= epsilon); }
        public static bool operator ==(idVec3 a, idVec3 b) { return a.Compare(ref b); }
        public static bool operator !=(idVec3 a, idVec3 b) { return !a.Compare(ref b); }
        #endregion

        public idVec3 Cross(idVec3 a) { return new idVec3(y * a.z - z * a.y, z * a.x - x * a.z, x * a.y - y * a.x); }
        public idVec3 Cross(idVec3 a, idVec3 b)
        {
            x = a.y * b.z - a.z * b.y;
            y = a.z * b.x - a.x * b.z;
            z = a.x * b.y - a.y * b.x;
            return this;
        }
        public float Length() { return (float)Math.Sqrt(x * x + y * y + z * z); }
        public float LengthFast() { float sqrLength = x * x + y * y + z * z; return sqrLength * idMath.RSqrt(sqrLength); }
        public float LengthSqr() { return (x * x + y * y + z * z); }
        public float Normalize()
        {
            float sqrLength = x * x + y * y + z * z;
            float invLength = idMath.InvSqrt(sqrLength);
            x *= invLength;
            y *= invLength;
            z *= invLength;
            return invLength * sqrLength;
        }
        public float NormalizeFast()
        {
            float sqrLength = x * x + y * y + z * z;
            float invLength = idMath.RSqrt(sqrLength);
            x *= invLength;
            y *= invLength;
            z *= invLength;
            return invLength * sqrLength;
        }
        public idVec3 Truncate(float length)
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
                    z *= ilength;
                }
            }
            return this;
        }
        public void Clamp(idVec3 min, idVec3 max)
        {
            if (x < min.x) x = min.x;
            else if (x > max.x) x = max.x;
            if (y < min.y) y = min.y;
            else if (y > max.y) y = max.y;
            if (z < min.z) z = min.z;
            else if (z > max.y) z = max.z;
        }
        public void Snap()
        {
            x = (float)Math.Floor(x + 0.5f);
            y = (float)Math.Floor(y + 0.5f);
            z = (float)Math.Floor(z + 0.5f);
        }
        public void SnapInt()
        {
            //x = float( int( x ) );
            //y = float( int( y ) );
            //z = float( int( z ) );
        }

        #region Other

        private const float LERP_DELTA = 1e-6f;
        public static void SLerp(out idVec3 t, idVec3 v1, idVec3 v2, float l)
        {
            if (l <= 0.0f) { t = v1; return; }
            else if (l >= 1.0f) { t = v2; return; }
            float cosom = v1 * v2;
            float scale0;
            float scale1;
            if ((1.0f - cosom) > LERP_DELTA)
            {
                float omega = (float)Math.Acos(cosom);
                float sinom = (float)Math.Sin(omega);
                scale0 = (float)Math.Sin((1.0f - l) * omega) / sinom;
                scale1 = (float)Math.Sin(l * omega) / sinom;
            }
            else
            {
                scale0 = 1.0f - l;
                scale1 = l;
            }
            t = (v1 * scale0 + v2 * scale1);
        }

        public bool FixDegenerateNormal()
        {
            if (x == 0.0f)
            {
                if (y == 0.0f)
                {
                    if (z > 0.0f)
                    {
                        if (z != 1.0f)
                        {
                            z = 1.0f;
                            return true;
                        }
                    }
                    else
                    {
                        if (z != -1.0f)
                        {
                            z = -1.0f;
                            return true;
                        }
                    }
                    return false;
                }
                else if (z == 0.0f)
                {
                    if (y > 0.0f)
                    {
                        if (y != 1.0f)
                        {
                            y = 1.0f;
                            return true;
                        }
                    }
                    else
                    {
                        if (y != -1.0f)
                        {
                            y = -1.0f;
                            return true;
                        }
                    }
                    return false;
                }
            }
            else if (y == 0.0f)
            {
                if (z == 0.0f)
                {
                    if (x > 0.0f)
                    {
                        if (x != 1.0f)
                        {
                            x = 1.0f;
                            return true;
                        }
                    }
                    else
                    {
                        if (x != -1.0f)
                        {
                            x = -1.0f;
                            return true;
                        }
                    }
                    return false;
                }
            }
            if (idMath.Fabs(x) == 1.0f)
            {
                if (y != 0.0f || z != 0.0f)
                {
                    y = z = 0.0f;
                    return true;
                }
                return false;
            }
            else if (idMath.Fabs(y) == 1.0f)
            {
                if (x != 0.0f || z != 0.0f)
                {
                    x = z = 0.0f;
                    return true;
                }
                return false;
            }
            else if (idMath.Fabs(z) == 1.0f)
            {
                if (x != 0.0f || y != 0.0f)
                {
                    x = y = 0.0f;
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool FixDenormals()
        {
            bool denormal = false;
            if (Math.Abs(x) < 1e-30f)
            {
                x = 0.0f;
                denormal = true;
            }
            if (Math.Abs(y) < 1e-30f)
            {
                y = 0.0f;
                denormal = true;
            }
            if (Math.Abs(z) < 1e-30f)
            {
                z = 0.0f;
                denormal = true;
            }
            return denormal;
        }

        public void NormalVectors(ref idVec3 left, ref idVec3 down)
        {
            float d = x * x + y * y;
            if (d == 0)
            {
                left.x = 1;
                left.y = 0;
                left.z = 0;
            }
            else
            {
                d = idMath.InvSqrt(d);
                left.x = -y * d;
                left.y = x * d;
                left.z = 0;
            }
            down = left.Cross(this);
        }

        public void OrthogonalBasis(idVec3 left, idVec3 up)
        {
            if (idMath.Fabs(z) > 0.7f)
            {
                float l = y * y + z * z;
                float s = idMath.InvSqrt(l);
                up.x = 0;
                up.y = z * s;
                up.z = -y * s;
                left.x = l * s;
                left.y = -x * up[2];
                left.z = x * up[1];
            }
            else
            {
                float l = x * x + y * y;
                float s = idMath.InvSqrt(l);
                left.x = -y * s;
                left.y = x * s;
                left.z = 0;
                up.x = -z * left[1];
                up.y = z * left[0];
                up.z = l * s;
            }
        }

        public static void ProjectOntoPlane(ref idVec3 t, idVec3 normal, float overBounce)
        {
            float backoff = t * normal;
            if (overBounce != 1.0)
            {
                if (backoff < 0)
                    backoff *= overBounce;
                else
                    backoff /= overBounce;
            }
            t.opSub(normal * backoff);
        }

        public static bool ProjectAlongPlane(idVec3 t, idVec3 normal, float epsilon, float overBounce)
        {
            idVec3 cross = t.Cross(normal).Cross(t);
            // normalize so a fixed epsilon can be used
            cross.Normalize();
            float len = normal * cross;
            if (idMath.Fabs(len) < epsilon)
                return false;
            cross.opMul(overBounce * (normal * t) / len);
            t.opSub(cross);
            return true;
        }


        //float idVec3::ToYaw( void ) const {
        //    float yaw;

        //    if ( ( y == 0.0f ) && ( x == 0.0f ) ) {
        //        yaw = 0.0f;
        //    } else {
        //        yaw = RAD2DEG( atan2( y, x ) );
        //        if ( yaw < 0.0f ) {
        //            yaw += 360.0f;
        //        }
        //    }

        //    return yaw;
        //}

        //float idVec3::ToPitch( void ) const {
        //    float	forward;
        //    float	pitch;

        //    if ( ( x == 0.0f ) && ( y == 0.0f ) ) {
        //        if ( z > 0.0f ) {
        //            pitch = 90.0f;
        //        } else {
        //            pitch = 270.0f;
        //        }
        //    } else {
        //        forward = ( float )idMath::Sqrt( x * x + y * y );
        //        pitch = RAD2DEG( atan2( z, forward ) );
        //        if ( pitch < 0.0f ) {
        //            pitch += 360.0f;
        //        }
        //    }

        //    return pitch;
        //}

        //idAngles idVec3::ToAngles( void ) const {
        //    float forward;
        //    float yaw;
        //    float pitch;

        //    if ( ( x == 0.0f ) && ( y == 0.0f ) ) {
        //        yaw = 0.0f;
        //        if ( z > 0.0f ) {
        //            pitch = 90.0f;
        //        } else {
        //            pitch = 270.0f;
        //        }
        //    } else {
        //        yaw = RAD2DEG( atan2( y, x ) );
        //        if ( yaw < 0.0f ) {
        //            yaw += 360.0f;
        //        }

        //        forward = ( float )idMath::Sqrt( x * x + y * y );
        //        pitch = RAD2DEG( atan2( z, forward ) );
        //        if ( pitch < 0.0f ) {
        //            pitch += 360.0f;
        //        }
        //    }

        //    return idAngles( -pitch, yaw, 0.0f );
        //}

        //idPolar3 idVec3::ToPolar( void ) const {
        //    float forward;
        //    float yaw;
        //    float pitch;

        //    if ( ( x == 0.0f ) && ( y == 0.0f ) ) {
        //        yaw = 0.0f;
        //        if ( z > 0.0f ) {
        //            pitch = 90.0f;
        //        } else {
        //            pitch = 270.0f;
        //        }
        //    } else {
        //        yaw = RAD2DEG( atan2( y, x ) );
        //        if ( yaw < 0.0f ) {
        //            yaw += 360.0f;
        //        }

        //        forward = ( float )idMath::Sqrt( x * x + y * y );
        //        pitch = RAD2DEG( atan2( z, forward ) );
        //        if ( pitch < 0.0f ) {
        //            pitch += 360.0f;
        //        }
        //    }
        //    return idPolar3( idMath::Sqrt( x * x + y * y + z * z ), yaw, -pitch );
        //}

        //idMat3 idVec3::ToMat3( void ) const {
        //    idMat3	mat;
        //    float	d;

        //    mat[0] = *this;
        //    d = x * x + y * y;
        //    if ( !d ) {
        //        mat[1][0] = 1.0f;
        //        mat[1][1] = 0.0f;
        //        mat[1][2] = 0.0f;
        //    } else {
        //        d = idMath::InvSqrt( d );
        //        mat[1][0] = -y * d;
        //        mat[1][1] = x * d;
        //        mat[1][2] = 0.0f;
        //    }
        //    mat[2] = Cross( mat[1] );

        //    return mat;
        //}

        // Projects the z component onto a sphere.
        public void ProjectSelfOntoSphere(float radius)
        {
            float rsqr = radius * radius;
            float len = Length();
            z = (float)(len < rsqr * 0.5f ? Math.Sqrt(rsqr - len) : rsqr / (2.0f * Math.Sqrt(len)));
        }

        #endregion
    }
}






