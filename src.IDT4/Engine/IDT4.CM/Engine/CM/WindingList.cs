namespace IDT4.Engine.CM
{
    internal class WindingList
    {
        public int numWindings; // number of windings
        public idFixedWinding[] w = new idFixedWinding[CM.MAX_WINDING_LIST];	// windings
        public idVec3 normal;	// normal for all windings
        public idBounds bounds; // bounds of all windings in list
        public idVec3 origin;	// origin for radius
        public float radius;	// radius relative to origin for all windings
        public int contents;	// winding surface contents
        public int primitiveNum;// number of primitive the windings came from
    }
}
