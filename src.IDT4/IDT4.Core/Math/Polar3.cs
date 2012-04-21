using System;
using System.Diagnostics;
namespace IDT4.Math2
{
    public struct idPolar3
    {
        public float radius;
        public float theta;
        public float phi;

        public idPolar3() { radius = theta = phi = 0f; }
        public idPolar3(float radius, float theta, float phi) { Debug.Assert(radius > 0); this.radius = radius; this.theta = theta; this.phi = phi; }
        public void Set(float radius, float theta, float phi) { Debug.Assert(radius > 0); this.radius = radius; this.theta = theta; this.phi = phi; }
        public void Zero() { radius = theta = phi = 0f; }
        public idVec3 ToVec3()
        {
            float sp, cp;
            idMath.SinCos(phi, out sp, out cp);
            float st, ct;
            idMath.SinCos(theta, out st, out ct);
            return new idVec3(cp * radius * ct, cp * radius * st, radius * sp);
        }

        public float this[int index] { get { throw new NotSupportedException(); } }
        public static idPolar3 operator -(idPolar3 a) { return new idPolar3(a.radius, -a.theta, -a.phi); }
    }
}
