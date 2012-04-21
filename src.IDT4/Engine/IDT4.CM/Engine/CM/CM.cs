namespace IDT4.Engine.CM
{
    public static class CM
    {
        public static ICollisionModelManager collisionModelManager;
        public const float CM_CLIP_EPSILON = 0.25f;		// always stay this distance away from any model
        public const float CM_BOX_EPSILON = 1.0f;		// should always be larger than clip epsilon
        public const float CM_MAX_TRACE_DIST = 4096.0f;	// maximum distance a trace model may be traced, point traces are unlimited
        //
        internal const float MIN_NODE_SIZE = 64.0f;
        internal const int MAX_NODE_POLYGONS = 128;
        internal const int CM_MAX_POLYGON_EDGES = 64;
        internal const float CIRCLE_APPROXIMATION_LENGTH = 64.0f;
        //
        internal const int MAX_SUBMODELS = 2048;
        internal const int TRACE_MODEL_HANDLE = MAX_SUBMODELS;
        //
        internal const int VERTEX_HASH_BOXSIZE = (1 << 6);	// must be power of 2
        internal const int VERTEX_HASH_SIZE = (VERTEX_HASH_BOXSIZE * VERTEX_HASH_BOXSIZE);
        internal const int EDGE_HASH_SIZE = (1 << 14);
        //
        internal const int NODE_BLOCK_SIZE_SMALL = 8;
        internal const int NODE_BLOCK_SIZE_LARGE = 256;
        internal const int REFERENCE_BLOCK_SIZE_SMALL = 8;
        internal const int REFERENCE_BLOCK_SIZE_LARGE = 256;
        //
        internal const int MAX_WINDING_LIST = 128;  // quite a few are generated at times
        internal const float INTEGRAL_EPSILON = 0.01f;
        internal const float VERTEX_EPSILON = 0.1f;
        internal const float CHOP_EPSILON = 0.1f;
        //
        internal idCVar DebugCollision;
    }
}
