namespace IDT4.Engine.CM
{
    internal class TrmEdge
    {
        public int used;					// true when vertex is used for collision detection
        public idVec3 start;				// start of edge
        public idVec3 end;					// end of edge
        public int[] vertexNum = new int[2];// indexes into cm_traceWork_t->vertices
        public idPluecker pl;				// pluecker coordinate for edge
        public idVec3 cross;				// (z,-y,x) of cross product between edge dir and movement dir
        public idBounds rotationBounds;		// rotation bounds for this edge
        public idPluecker plzaxis;			// pluecker coordinate for rotation about the z-axis
        public ushort bitNum;				// vertex bit number
    }
}
