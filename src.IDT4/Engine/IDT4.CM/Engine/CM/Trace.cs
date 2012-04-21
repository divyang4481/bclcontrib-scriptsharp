namespace IDT4.Engine.CM
{
    // trace result
    public class Trace
    {
        public float fraction;	// fraction of movement completed, 1.0 = didn't hit anything
        public idVec3 endpos;	// final position of trace model
        public idMat3 endAxis;	// final axis of trace model
        public ContactInfo c;	// contact information, only valid if fraction < 1.0
    }
}
