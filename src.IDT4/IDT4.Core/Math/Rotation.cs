using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    /// <summary>
    /// Describes a complete rotation in degrees about an abritray axis. 
    /// A local rotation matrix is stored for fast rotation of multiple points.
    /// </summary>
    public class idRotation
    {
        idVec3 origin;	// origin of rotation
        internal idVec3 vec;	    // normalized vector to rotate around
        float angle;	// angle of rotation in degrees
        idMat3 axis;	// rotation axis
        bool axisValid;	// true if rotation axis is valid

        public idRotation() { }
        public idRotation(idVec3 rotationOrigin, idVec3 rotationVec, float rotationAngle)
        {
            origin = rotationOrigin;
            vec = rotationVec;
            angle = rotationAngle;
            axisValid = false;
        }
        public idAngles ToAngles() { return ToMat3().ToAngles(); }

        public idQuat ToQuat()
        {
            float a = angle * (idMath.M_DEG2RAD * 0.5f);
            float s, c;
            idMath.SinCos(a, out s, out c);
            return idQuat(vec.x * s, vec.y * s, vec.z * s, c);
        }

        public idMat3 ToMat3()
        {
            if (axisValid)
                return axis;
            float a = angle * (idMath.M_DEG2RAD * 0.5f);
            float c, s;
            idMath.SinCos(a, out s, out c);
            float x = vec[0] * s;
            float y = vec[1] * s;
            float z = vec[2] * s;
            float x2 = x + x;
            float y2 = y + y;
            float z2 = z + z;
            float xx = x * x2;
            float xy = x * y2;
            float xz = x * z2;
            float yy = y * y2;
            float yz = y * z2;
            float zz = z * z2;
            float wx = c * x2;
            float wy = c * y2;
            float wz = c * z2;
            axis[0][0] = 1.0f - (yy + zz);
            axis[0][1] = xy - wz;
            axis[0][2] = xz + wy;
            axis[1][0] = xy + wz;
            axis[1][1] = 1.0f - (xx + zz);
            axis[1][2] = yz - wx;
            axis[2][0] = xz - wy;
            axis[2][1] = yz + wx;
            axis[2][2] = 1.0f - (xx + yy);
            axisValid = true;
            return axis;
        }

        public idMat4 ToMat4() { return ToMat3().ToMat4(); }

        public idVec3 ToAngularVelocity() { return vec * DEG2RAD(angle); }

        public void Set(idVec3 rotationOrigin, idVec3 rotationVec, float rotationAngle)
        {
            origin = rotationOrigin;
            vec = rotationVec;
            angle = rotationAngle;
            axisValid = false;
        }

        public void SetOrigin(idVec3 rotationOrigin)
        {
            origin = rotationOrigin;
        }

        public void SetVec(idVec3 rotationVec)
        {
            vec = rotationVec;
            axisValid = false;
        }

        public void SetVec(float x, float y, float z)
        {
            vec.x = x;
            vec.y = y;
            vec.z = z;
            axisValid = false;
        }

        public void SetAngle(float rotationAngle)
        {
            angle = rotationAngle;
            axisValid = false;
        }

        public void Scale(float s)
        {
            angle *= s;
            axisValid = false;
        }

        public void ReCalculateMatrix()
        {
            axisValid = false;
            ToMat3();
        }

        public idVec3 GetOrigin() { return origin; }
        public idVec3 GetVec() { return vec; }
        public float GetAngle() { return angle; }
        public static idRotation operator -(idRotation a) { return new idRotation(a.origin, a.vec, -a.angle); }
        public static idRotation operator *(idRotation a, float s) { return new idRotation(a.origin, a.vec, a.angle * s); }
        public static idRotation operator /(idRotation a, float s) { Debug.Assert(s != 0.0f); return new idRotation(a.origin, a.vec, a.angle / s); }
        public static idVec3 operator *(idRotation a, idVec3 v) { if (!a.axisValid)  a.ToMat3(); return ((v - a.origin) * a.axis + a.origin); }
        public idVec3 operator *(idVec3 v, idRotation r) { return r * v; }
        public idRotation opMul(float s) { angle *= s; axisValid = false; return this; }
        public idRotation opDiv(float s) { Debug.Assert(s != 0.0f); angle /= s; axisValid = false; return this; }
        public idVec3 opMul(idVec3 v, idRotation r) { v = r * v; return v; }

        public void RotatePoint(ref idVec3 point)
        {
            if (!axisValid)
                ToMat3();
            point = ((point - origin) * axis + origin);
        }

        public void Normalize180()
        {
            angle -= (float)Math.Floor(angle / 360.0f) * 360.0f;
            if (angle > 180.0f)
                angle -= 360.0f;
            else if (angle < -180.0f)
                angle += 360.0f;
        }

        public void Normalize360()
        {
            angle -= (float)Math.Floor(angle / 360.0f) * 360.0f;
            if (angle > 360.0f)
                angle -= 360.0f;
            else if (angle < 0.0f)
                angle += 360.0f;
        }
    }
}



