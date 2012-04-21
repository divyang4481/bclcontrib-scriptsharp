namespace IDT4.Engine.CM
{
    internal class Brush
    {
        public int checkcount;		// for multi-check avoidance
        public idBounds bounds;		// brush bounds
        public int contents;		// contents of brush
        public idMaterial material;	// material
        public int primitiveNum;	// number of brush primitive
        public int numPlanes;		// number of bounding planes
        public idPlane[] planes = new idPlane[1];   // variable sized
    }
}
