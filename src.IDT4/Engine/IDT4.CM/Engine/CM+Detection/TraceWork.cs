namespace IDT4.Engine.CM
{
    internal class TraceWork
    {
        public int numVerts;
        public TrmVertex[] vertices = new TrmVertex[CM.MAX_TRACEMODEL_VERTS];	// trm vertices
        public int numEdges;
        public TrmEdge[] edges = new TrmEdge[CM.MAX_TRACEMODEL_EDGES + 1];		// trm edges
        public int numPolys;
        public TrmPolygon[] polys = new TrmPolygon[CM.MAX_TRACEMODEL_POLYS];	    // trm polygons
        public Model model;		// model colliding with
        public idVec3 start;	// start of trace
        public idVec3 end;		// end of trace
        public idVec3 dir;		// trace direction
        public idBounds bounds;	// bounds of full trace
        public idBounds size;	// bounds of transformed trm relative to start
        public idVec3 extents;	// largest of abs(size[0]) and abs(size[1]) for BSP trace
        public int contents;	// ignore polygons that do not have any of these contents flags
        public Trace trace;		// collision detection result
        //
        public bool rotation;	// true if calculating rotational collision
        public bool pointTrace;	// true if only tracing a point
        public bool positionTest;// true if not tracing but doing a position test
        public bool isConvex;	// true if the trace model is convex
        public bool axisIntersectsTrm;	// true if the rotation axis intersects the trace model
        public bool getContacts;// true if retrieving contacts
        public bool quickExit;	// set to quickly stop the collision detection calculations
        //
        public idVec3 origin;	// origin of rotation in model space
        public idVec3 axis;		// rotation axis in model space
        public idMat3 matrix;	// rotates axis of rotation to the z-axis
        public float angle;		// angle for rotational collision
        public float maxTan;	// max tangent of half the positive angle used instead of fraction
        public float radius;	// rotation radius of trm start
        public idRotation modelVertexRotation;	// inverse rotation for model vertices
        //
        public ContactInfo contacts;// array with contacts
        public int maxContacts;		// max size of contact array
        public int numContacts;		// number of contacts found
        //
        public idPlane heartPlane1;	// polygons should be near anough the trace heart planes
        public float maxDistFromHeartPlane1;
        public idPlane heartPlane2;
        public float maxDistFromHeartPlane2;
        public idPluecker[] polygonEdgePlueckerCache = new idPluecker[CM.CM_MAX_POLYGON_EDGES];
        public idPluecker[] polygonVertexPlueckerCache = new idPluecker[CM.CM_MAX_POLYGON_EDGES];
        public idVec3[] polygonRotationOriginCache = new idVec3[CM.CM_MAX_POLYGON_EDGES];
    }
}
