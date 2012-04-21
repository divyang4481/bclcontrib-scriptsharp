namespace IDT4.Engine.CM
{
    internal class Edge
    {
        public int checkcount;	// for multi-check avoidance
        public ushort @internal;// a trace model can never collide with internal edges
        public ushort numUsers;	// number of polygons using this edge
        public ulong side;		// each bit tells at which side of this edge one of the trace model vertices passes
        public ulong sideSet;	// each bit tells if sidedness for the trace model vertex has been calculated yet
        public int[] vertexNum;	// start and end point of edge
        public idVec3 normal;	// edge normal
    }
}
