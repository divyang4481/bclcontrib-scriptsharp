namespace IDT4.Engine.CM
{
    internal class Node
    {
        public int planeType;		// node axial plane type
        public float planeDist;		// node plane distance
        public PolygonRef polygons;	// polygons in node
        public BrushRef brushes;	// brushes in node
        public Node parent;			// parent of this node
        public Node[] children = new Node[2];   // node children
    }
}
