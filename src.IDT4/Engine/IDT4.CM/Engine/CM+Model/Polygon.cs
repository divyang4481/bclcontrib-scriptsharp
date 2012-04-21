namespace IDT4.Engine.CM
{
    internal class Polygon
    {
        public idBounds bounds;		// polygon bounds
        public int checkcount;		// for multi-check avoidance
        public int contents;		// contents behind polygon
        public idMaterial material;	// material
        public idPlane plane;		// polygon plane
        public int numEdges;		// number of edges
        public int[] edges = new int[1];    // variable sized, indexes into cm_edge_t list
    }
}
