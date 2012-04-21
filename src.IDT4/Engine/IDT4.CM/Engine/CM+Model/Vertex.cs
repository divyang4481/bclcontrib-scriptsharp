namespace IDT4.Engine.CM
{
    internal class Vertex
    {
        public idVec3 p;		// vertex point
        public int checkcount;	// for multi-check avoidance
        public ulong side;		// each bit tells at which side this vertex passes one of the trace model edges
        public ulong sideSet;	// each bit tells if sidedness for the trace model edge has been calculated yet
    }
}
