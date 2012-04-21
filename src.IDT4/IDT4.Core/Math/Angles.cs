using System;
namespace IDT4.Math2
{
    public struct idAngles
    {
        public const int PITCH = 0;		// up / down
        public const int YAW = 1;		// left / right
        public const int ROLL = 2;		// fall over
        public static readonly idAngles zero = new idAngles(0.0f, 0.0f, 0.0f);
        public float pitch;
        public float yaw;
        public float roll;

        public idAngles() { pitch = yaw = roll = 0.0f; }
        public idAngles(float pitch, float yaw, float roll)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
        }
        public idAngles(ref idVec3 v)
        {
            this.pitch = v.x;
            this.yaw = v.y;
            this.roll = v.z;
        }
        public idAngles Zero() { pitch = yaw = roll = 0.0f; return this; }
        public void Set(float pitch, float yaw, float roll)
        {
            this.pitch = pitch;
            this.yaw = yaw;
            this.roll = roll;
        }
        public int GetDimension() { return 3; }
        public float[] ToArray() { return null; }
        public string ToString(int precision = 2) { return StringEx.ToString(ToArray(), precision); }

        #region Operator
        public float this[int index] { get { return 0; } set { } }
        public static idAngles operator -(idAngles a) { return new idAngles(-a.pitch, -a.yaw, -a.roll); }
        //public static idAngles operator=( idAngles a ,idAngles b ) {
        //    a.pitch	= b.pitch;
        //    a.yaw		= b.yaw;
        //    a.roll	= b.roll;
        //    return a;
        //}
        public static idAngles operator +(idAngles a, idAngles b) { return new idAngles(a.pitch + b.pitch, a.yaw + b.yaw, a.roll + b.roll); }
        public static idAngles operator -(idAngles a, idAngles b) { return new idAngles(a.pitch - b.pitch, a.yaw - b.yaw, a.roll - b.roll); }
        public static idAngles operator *(idAngles a, float b) { return new idAngles(a.pitch * b, a.yaw * b, a.roll * b); }
        public static idAngles operator /(idAngles a, float b) { float inva = 1.0f / b; return new idAngles(a.pitch * inva, a.yaw * inva, a.roll * inva); }
        public idAngles opAdd(ref idAngles a)
        {
            pitch += a.pitch;
            yaw += a.yaw;
            roll += a.roll;
            return this;
        }
        public idAngles opSub(idAngles a)
        {
            pitch -= a.pitch;
            yaw -= a.yaw;
            roll -= a.roll;
            return this;
        }
        public idAngles opMul(float a)
        {
            pitch *= a;
            yaw *= a;
            roll *= a;
            return this;
        }
        public idAngles opDiv(float a)
        {
            float inva = 1.0f / a;
            pitch *= inva;
            yaw *= inva;
            roll *= inva;
            return this;
        }
        #endregion

        #region Compare
        public bool Compare(ref idAngles a) { return (a.pitch == pitch && a.yaw == yaw && a.roll == roll); }
        public bool Compare(ref idAngles a, float epsilon)
        {
            return !(
                (idMath.Fabs(pitch - a.pitch) > epsilon) ||
                (idMath.Fabs(yaw - a.yaw) > epsilon) ||
                (idMath.Fabs(roll - a.roll) > epsilon));
        }
        public static bool operator ==(idAngles a, idAngles b) { return a.Compare(ref b); }
        public static bool operator !=(idAngles a, idAngles b) { return !a.Compare(ref b); }
        #endregion

        #region Normalize
        // returns angles normalized to the range [0 <= angle < 360]
        public idAngles Normalize360()
        {
            for (int i = 0; i < 3; i++)
                if (this[i] >= 360.0f || this[i] < 0.0f)
                {
                    this[i] -= (float)Math.Floor(this[i] / 360.0f) * 360.0f;
                    if (this[i] >= 360.0f) this[i] -= 360.0f;
                    if (this[i] < 0.0f) this[i] += 360.0f;
                }
            return this;
        }
        // returns angles normalized to the range [-180 < angle <= 180]
        public idAngles Normalize180()
        {
            Normalize360();
            if (pitch > 180.0f) pitch -= 360.0f;
            if (yaw > 180.0f) yaw -= 360.0f;
            if (roll > 180.0f) roll -= 360.0f;
            return this;
        }
        #endregion

        public void Clamp(ref idAngles min, ref idAngles max)
        {
            if (pitch < min.pitch) pitch = min.pitch;
            else if (pitch > max.pitch) pitch = max.pitch;
            if (yaw < min.yaw) yaw = min.yaw;
            else if (yaw > max.yaw) yaw = max.yaw;
            if (roll < min.roll) roll = min.roll;
            else if (roll > max.roll) roll = max.roll;
        }

        #region Convert
        public void ToVectors(ref idVec3 forward, ref idVec3 right, ref idVec3 up)
        {
            float sr, sp, sy, cr, cp, cy;
            idMath.SinCos(DEG2RAD(yaw), out sy, out cy);
            idMath.SinCos(DEG2RAD(pitch), out sp, out cp);
            idMath.SinCos(DEG2RAD(roll), out sr, out cr);
            if (forward != null) forward.Set(cp * cy, cp * sy, -sp);
            if (right != null) right.Set(-sr * sp * cy + cr * sy, -sr * sp * sy + -cr * cy, -sr * cp);
            if (up != null) up.Set(cr * sp * cy + -sr * -sy, cr * sp * sy + -sr * cy, cr * cp);
        }
        public idVec3 ToForward()
        {
            float sp, sy, cp, cy;
            idMath.SinCos(DEG2RAD(yaw), out sy, out cy);
            idMath.SinCos(DEG2RAD(pitch), out sp, out cp);
            return idVec3(cp * cy, cp * sy, -sp);
        }
        public idQuat ToQuat()
        {
            float sx, cx, sy, cy, sz, cz;
            float sxcy, cxcy, sxsy, cxsy;
            idMath.SinCos(DEG2RAD(yaw) * 0.5f, out sz, out cz);
            idMath.SinCos(DEG2RAD(pitch) * 0.5f, out sy, out cy);
            idMath.SinCos(DEG2RAD(roll) * 0.5f, out sx, out cx);
            sxcy = sx * cy;
            cxcy = cx * cy;
            sxsy = sx * sy;
            cxsy = cx * sy;
            return new idQuat(cxsy * sz - sxcy * cz, -cxsy * cz - sxcy * sz, sxsy * cz - cxcy * sz, cxcy * cz + sxsy * sz);
        }
        public idRotation ToRotation()
        {
            if (pitch == 0.0f)
            {
                if (yaw == 0.0f) return new idRotation(idvec3.origin, new idVec3(-1.0f, 0.0f, 0.0f), roll);
                if (roll == 0.0f) return new idRotation(idVec3.origin, new idVec3(0.0f, 0.0f, -1.0f), yaw);
            }
            else if (yaw == 0.0f && roll == 0.0f) return new idRotation(idVec3.origin, new idVec3(0.0f, -1.0f, 0.0f), pitch);
            float sx, cx, sy, cy, sz, cz;
            idMath.SinCos(DEG2RAD(yaw) * 0.5f, out sz, out cz);
            idMath.SinCos(DEG2RAD(pitch) * 0.5f, out sy, out cy);
            idMath.SinCos(DEG2RAD(roll) * 0.5f, out sx, out cx);
            float sxcy = sx * cy;
            float cxcy = cx * cy;
            float sxsy = sx * sy;
            float cxsy = cx * sy;
            //
            idVec3 vec = new idVec3();
            vec.x = cxsy * sz - sxcy * cz;
            vec.y = -cxsy * cz - sxcy * sz;
            vec.z = sxsy * cz - cxcy * sz;
            float w = cxcy * cz + sxsy * sz;
            float angle = idMath.ACos(w);
            if (angle == 0.0f)
                vec.Set(0.0f, 0.0f, 1.0f);
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
            float sr, sp, sy, cr, cp, cy;
            idMath.SinCos(DEG2RAD(yaw), out sy, out cy);
            idMath.SinCos(DEG2RAD(pitch), out sp, out cp);
            idMath.SinCos(DEG2RAD(roll), out sr, out cr);
            idMat3 mat = new idMat3(); idVec3[] mat_mat = mat.mat;
            mat_mat[0].Set(cp * cy, cp * sy, -sp);
            mat_mat[1].Set(sr * sp * cy + cr * -sy, sr * sp * sy + cr * cy, sr * cp);
            mat_mat[2].Set(cr * sp * cy + -sr * -sy, cr * sp * sy + -sr * cy, cr * cp);
            return mat;
        }
        public idMat4 ToMat4() { return ToMat3().ToMat4(); }
        public idVec3 ToAngularVelocity()
        {
            idRotation rotation = ToRotation();
            return rotation.GetVec() * DEG2RAD(rotation.GetAngle());
        }
        #endregion
    }
}
