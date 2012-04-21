namespace IDT4.Engine.CM
{
    internal class Model
    {
        public string name;		// model name
        public idBounds bounds;	// model bounds
        public int contents;	// all contents of the model ored together
        public bool isConvex;	// set if model is convex
        // model geometry
        public int maxVertices;	// size of vertex array
        public int numVertices;	// number of vertices
        public Vertex[] vertices;	// array with all vertices used by the model
        public int maxEdges;	// size of edge array
        public int numEdges;	// number of edges
        public Edge[] edges;		// array with all edges used by the model
        public Node node;		// first node of spatial subdivision
        // blocks with allocated memory
        public NodeBlock nodeBlocks;			// list with blocks of nodes
        public PolygonRefBlock polygonRefBlocks;// list with blocks of polygon references
        public BrushRefBlock brushRefBlocks;	// list with blocks of brush references
        public PolygonBlock polygonBlock;	    // memory block with all polygons
        public BrushBlock brushBlock;		    // memory block with all brushes
        // statistics
        public int numPolygons;
        public int polygonMemory;
        public int numBrushes;
        public int brushMemory;
        public int numNodes;
        public int numBrushRefs;
        public int numPolygonRefs;
        public int numInternalEdges;
        public int numSharpEdges;
        public int numRemovedPolys;
        public int numMergedPolys;
        public int usedMemory;
    }
}
