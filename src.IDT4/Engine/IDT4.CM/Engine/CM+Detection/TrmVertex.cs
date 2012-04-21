namespace IDT4.Engine.CM
{
    internal class TrmVertex
    {
        public int used;				// true if this vertex is used for collision detection
        public idVec3 p;				// vertex position
        public idVec3 endp;				// end point of vertex after movement
        public int polygonSide;			// side of polygon this vertex is on (rotational collision)
        public idPluecker pl;			// pluecker coordinate for vertex movement
        public idVec3 rotationOrigin;	// rotation origin for this vertex
        public idBounds rotationBounds; // rotation bounds for this vertex
    }
}
