namespace IDT4.Engine.CM
{
    internal class TrmPolygon
    {
        public int used;
        public idPlane plane;           // polygon plane
        public int numEdges;			// number of edges
        public int[] edges = new int[CM.MAX_TRACEMODEL_POLYEDGES];   // index into cm_traceWork_t->edges
        public idBounds rotationBounds;	// rotation bounds for this polygon
    }
}
