using System;
namespace IDT4.Engine.CM
{
    /// <summary>
    /// Trace model vs. polygonal model collision detection.
    ///
    /// Short translations are the least expensive. Retrieving contact points is about as cheap as a short translation. Position tests are more expensive
    /// and rotations are most expensive.
    ///
    /// There is no position test at the start of a translation or rotation. In other words if a translation with start != end or a rotation with angle != 0 starts
    /// in solid, this goes unnoticed and the collision result is undefined.
    ///
    /// A translation with start == end or a rotation with angle == 0 performs a position test and fills in the trace_t structure accordingly.
    /// </summary>
    public interface ICollisionModelManager : IDisposable
    {
        void Dispose();

        // Loads collision models from a map file.
        void LoadMap(idMapFile mapFile);
        // Frees all the collision models.
        void FreeMap();

        // Gets the clip handle for a model.
        CmHandle LoadModel(string modelName, bool precache);
        // Sets up a trace model for collision with other trace models.
        CmHandle SetupTrmModel(ref idTraceModel trm, idMaterial material);
        // Creates a trace model from a collision model, returns true if succesfull.
        bool TrmFromModel(string modelName, ref idTraceModel trm);

        // Gets the name of a model.
        string GetModelName(CmHandle model);
        // Gets the bounds of a model.
        bool GetModelBounds(CmHandle model, ref idBounds bounds);
        // Gets all contents flags of brushes and polygons of a model ored together.
        bool GetModelContents(CmHandle model, ref int contents);
        // Gets a vertex of a model.
        bool GetModelVertex(CmHandle model, int vertexNum, ref idVec3 vertex);
        // Gets an edge of a model.
        bool GetModelEdge(CmHandle model, int edgeNum, ref idVec3 start, ref idVec3 end);
        // Gets a polygon of a model.
        bool GetModelPolygon(CmHandle model, int polygonNum, ref idFixedWinding winding);

        // Translates a trace model and reports the first collision if any.
        void Translation(trace_t results, ref idVec3 start, ref idVec3 end, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis);
        // Rotates a trace model and reports the first collision if any.
        void Rotation(trace_t results, ref idVec3 start, ref idRotation rotation, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis);
        // Returns the contents touched by the trace model or 0 if the trace model is in free space.
        int Contents(ref idVec3 start, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis);
        // Stores all contact points of the trace model with the model, returns the number of contacts.
        int Contacts(ContactInfo contacts, int maxContacts, ref idVec3 start, ref idVec6 dir, float depth, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis);

        // Tests collision detection.
        void DebugOutput(ref idVec3 origin);
        // Draws a model.
        void DrawModel(CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis, ref idVec3 viewOrigin, float radius);
        // Prints model information, use -1 handle for accumulated model info.
        void ModelInfo(CmHandle model);
        // Lists all loaded models.
        void ListModels();
        // Writes a collision model file for the given map entity.
        bool WriteCollisionModelForMapEntity(idMapEntity mapEnt, string filename, bool testTraceModel = true);
    }

    internal partial class CollisionModelManager : ICollisionModelManager
    {
        ////public:
        //    // load collision models from a map file
        //    public void			LoadMap( idMapFile mapFile );
        //    // frees all the collision models
        //    public void			FreeMap( );

        //    // get clip handle for model
        //    public CmHandle		LoadModel( string modelName, bool precache );
        //    // sets up a trace model for collision with other trace models
        //    public CmHandle		SetupTrmModel( ref idTraceModel trm, idMaterial material );
        //    // create trace model from a collision model, returns true if succesfull
        //    public bool			TrmFromModel( string modelName, ref idTraceModel trm );

        //    // name of the model
        //    public string GetModelName( CmHandle model );
        //    // bounds of the model
        //    public bool			GetModelBounds( CmHandle model, ref idBounds bounds ) ;
        //    // all contents flags of brushes and polygons ored together
        //    public bool			GetModelContents( CmHandle model, ref int contents );
        //    // get the vertex of a model
        //    public bool			GetModelVertex( CmHandle model, int vertexNum, ref idVec3 vertex );
        //    // get the edge of a model
        //    public bool			GetModelEdge( CmHandle model, int edgeNum, ref idVec3 start, ref idVec3 end );
        //    // get the polygon of a model
        //    public bool			GetModelPolygon( CmHandle model, int polygonNum, ref idFixedWinding winding );

        //    // translates a trm and reports the first collision if any
        //    public void			Translation( trace_t *results, ref idVec3 start, ref idVec3 end, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis );
        //    // rotates a trm and reports the first collision if any
        //    public void			Rotation( trace_t results, ref idVec3 start, ref idRotation rotation, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis );
        //    // returns the contents the trm is stuck in or 0 if the trm is in free space
        //    public int				Contents( ref idVec3 start, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis );
        //    // stores all contact points of the trm with the model, returns the number of contacts
        //    public int				Contacts( contactInfo contacts, int maxContacts, ref  idVec3 start, ref idVec6 dir, float depth, idTraceModel trm, ref idMat3 trmAxis, int contentMask, CmHandle model, ref idVec3 modelOrigin, ref idMat3 modelAxis );
        //    // test collision detection
        //    public void			DebugOutput( ref idVec3 origin );
        //    // draw a model
        //    public void			DrawModel( CmHandle model, ref idVec3 origin, ref idMat3 axis, ref idVec3 viewOrigin, float radius );
        //    // print model information, use -1 handle for accumulated model info
        //    public void			ModelInfo( CmHandle model );
        //    // list all loaded models
        //    public void			ListModels( );
        //    // write a collision model file for the map entity
        //    public bool			WriteCollisionModelForMapEntity( idMapEntity  mapEnt, string filename, bool testTraceModel = true );

        //private:			// CollisionMap_translate.cpp
        //    int				TranslateEdgeThroughEdge( idVec3 &cross, idPluecker &l1, idPluecker &l2, float *fraction );
        //    void			TranslateTrmEdgeThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *poly, cm_trmEdge_t *trmEdge );
        //    void			TranslateTrmVertexThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *poly, cm_trmVertex_t *v, int bitNum );
        //    void			TranslatePointThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *poly, cm_trmVertex_t *v );
        //    void			TranslateVertexThroughTrmPolygon( cm_traceWork_t *tw, cm_trmPolygon_t *trmpoly, cm_polygon_t *poly, cm_vertex_t *v, idVec3 &endp, idPluecker &pl );
        //    bool			TranslateTrmThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *p );
        //    void			SetupTranslationHeartPlanes( cm_traceWork_t *tw );
        //    void			SetupTrm( cm_traceWork_t *tw, const idTraceModel *trm );

        //private:			// CollisionMap_rotate.cpp
        //    int				CollisionBetweenEdgeBounds( cm_traceWork_t *tw, const idVec3 &va, const idVec3 &vb,
        //                                            const idVec3 &vc, const idVec3 &vd, float tanHalfAngle,
        //                                            idVec3 &collisionPoint, idVec3 &collisionNormal );
        //    int				RotateEdgeThroughEdge( cm_traceWork_t *tw, const idPluecker &pl1,
        //                                            const idVec3 &vc, const idVec3 &vd,
        //                                            const float minTan, float &tanHalfAngle );
        //    int				EdgeFurthestFromEdge( cm_traceWork_t *tw, const idPluecker &pl1,
        //                                            const idVec3 &vc, const idVec3 &vd,
        //                                            float &tanHalfAngle, float &dir );
        //    void			RotateTrmEdgeThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *poly, cm_trmEdge_t *trmEdge );
        //    int				RotatePointThroughPlane( const cm_traceWork_t *tw, const idVec3 &point, const idPlane &plane,
        //                                            const float angle, const float minTan, float &tanHalfAngle );
        //    int				PointFurthestFromPlane( const cm_traceWork_t *tw, const idVec3 &point, const idPlane &plane,
        //                                            const float angle, float &tanHalfAngle, float &dir );
        //    int				RotatePointThroughEpsilonPlane( const cm_traceWork_t *tw, const idVec3 &point, const idVec3 &endPoint,
        //                                            const idPlane &plane, const float angle, const idVec3 &origin,
        //                                            float &tanHalfAngle, idVec3 &collisionPoint, idVec3 &endDir );
        //    void			RotateTrmVertexThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *poly, cm_trmVertex_t *v, int vertexNum);
        //    void			RotateVertexThroughTrmPolygon( cm_traceWork_t *tw, cm_trmPolygon_t *trmpoly, cm_polygon_t *poly,
        //                                            cm_vertex_t *v, idVec3 &rotationOrigin );
        //    bool			RotateTrmThroughPolygon( cm_traceWork_t *tw, cm_polygon_t *p );
        //    void			BoundsForRotation( const idVec3 &origin, const idVec3 &axis, const idVec3 &start, const idVec3 &end, idBounds &bounds );
        //    void			Rotation180( trace_t *results, const idVec3 &rorg, const idVec3 &axis,
        //                                    const float startAngle, const float endAngle, const idVec3 &start,
        //                                    const idTraceModel *trm, const idMat3 &trmAxis, int contentMask,
        //                                    CmHandle model, const idVec3 &origin, const idMat3 &modelAxis );

        //private:			// CollisionMap_contents.cpp
        //    bool			TestTrmVertsInBrush( cm_traceWork_t *tw, cm_brush_t *b );
        //    bool			TestTrmInPolygon( cm_traceWork_t *tw, cm_polygon_t *p );
        //    cm_node_t *		PointNode( const idVec3 &p, cm_model_t *model );
        //    int				PointContents( const idVec3 p, CmHandle model );
        //    int				TransformedPointContents( const idVec3 &p, CmHandle model, const idVec3 &origin, const idMat3 &modelAxis );
        //    int				ContentsTrm( trace_t *results, const idVec3 &start,
        //                                    const idTraceModel *trm, const idMat3 &trmAxis, int contentMask,
        //                                    CmHandle model, const idVec3 &modelOrigin, const idMat3 &modelAxis );

        //private:			// CollisionMap_trace.cpp
        //    void			TraceTrmThroughNode( cm_traceWork_t *tw, cm_node_t *node );
        //    void			TraceThroughAxialBSPTree_r( cm_traceWork_t *tw, cm_node_t *node, float p1f, float p2f, idVec3 &p1, idVec3 &p2);
        //    void			TraceThroughModel( cm_traceWork_t *tw );
        //    void			RecurseProcBSP_r( trace_t *results, int parentNodeNum, int nodeNum, float p1f, float p2f, const idVec3 &p1, const idVec3 &p2 );

        //private:			// CollisionMap_load.cpp
        //    void			Clear( void );
        //    void			FreeTrmModelStructure( void );
        //                    // model deallocation
        //    void			RemovePolygonReferences_r( cm_node_t *node, cm_polygon_t *p );
        //    void			RemoveBrushReferences_r( cm_node_t *node, cm_brush_t *b );
        //    void			FreeNode( cm_node_t *node );
        //    void			FreePolygonReference( cm_polygonRef_t *pref );
        //    void			FreeBrushReference( cm_brushRef_t *bref );
        //    void			FreePolygon( cm_model_t *model, cm_polygon_t *poly );
        //    void			FreeBrush( cm_model_t *model, cm_brush_t *brush );
        //    void			FreeTree_r( cm_model_t *model, cm_node_t *headNode, cm_node_t *node );
        //    void			FreeModel( cm_model_t *model );
        //                    // merging polygons
        //    void			ReplacePolygons( cm_model_t *model, cm_node_t *node, cm_polygon_t *p1, cm_polygon_t *p2, cm_polygon_t *newp );
        //    cm_polygon_t *	TryMergePolygons( cm_model_t *model, cm_polygon_t *p1, cm_polygon_t *p2 );
        //    bool			MergePolygonWithTreePolygons( cm_model_t *model, cm_node_t *node, cm_polygon_t *polygon );
        //    void			MergeTreePolygons( cm_model_t *model, cm_node_t *node );
        //                    // finding internal edges
        //    bool			PointInsidePolygon( cm_model_t *model, cm_polygon_t *p, idVec3 &v );
        //    void			FindInternalEdgesOnPolygon( cm_model_t *model, cm_polygon_t *p1, cm_polygon_t *p2 );
        //    void			FindInternalPolygonEdges( cm_model_t *model, cm_node_t *node, cm_polygon_t *polygon );
        //    void			FindInternalEdges( cm_model_t *model, cm_node_t *node );
        //    void			FindContainedEdges( cm_model_t *model, cm_polygon_t *p );
        //                    // loading of proc BSP tree
        //    void			ParseProcNodes( idLexer *src );
        //    void			LoadProcBSP( const char *name );
        //                    // removal of contained polygons
        //    int				R_ChoppedAwayByProcBSP( int nodeNum, idFixedWinding *w, const idVec3 &normal, const idVec3 &origin, const float radius );
        //    int				ChoppedAwayByProcBSP( const idFixedWinding &w, const idPlane &plane, int contents );
        //    void			ChopWindingListWithBrush( cm_windingList_t *list, cm_brush_t *b );
        //    void			R_ChopWindingListWithTreeBrushes( cm_windingList_t *list, cm_node_t *node );
        //    idFixedWinding *WindingOutsideBrushes( idFixedWinding *w, const idPlane &plane, int contents, int patch, cm_node_t *headNode );
        //                    // creation of axial BSP tree
        //    cm_model_t *	AllocModel( void );
        //    cm_node_t *		AllocNode( cm_model_t *model, int blockSize );
        //    cm_polygonRef_t*AllocPolygonReference( cm_model_t *model, int blockSize );
        //    cm_brushRef_t *	AllocBrushReference( cm_model_t *model, int blockSize );
        //    cm_polygon_t *	AllocPolygon( cm_model_t *model, int numEdges );
        //    cm_brush_t *	AllocBrush( cm_model_t *model, int numPlanes );
        //    void			AddPolygonToNode( cm_model_t *model, cm_node_t *node, cm_polygon_t *p );
        //    void			AddBrushToNode( cm_model_t *model, cm_node_t *node, cm_brush_t *b );
        //    void			SetupTrmModelStructure( void );
        //    void			R_FilterPolygonIntoTree( cm_model_t *model, cm_node_t *node, cm_polygonRef_t *pref, cm_polygon_t *p );
        //    void			R_FilterBrushIntoTree( cm_model_t *model, cm_node_t *node, cm_brushRef_t *pref, cm_brush_t *b );
        //    cm_node_t *		R_CreateAxialBSPTree( cm_model_t *model, cm_node_t *node, const idBounds &bounds );
        //    cm_node_t *		CreateAxialBSPTree( cm_model_t *model, cm_node_t *node );
        //                    // creation of raw polygons
        //    void			SetupHash(void);
        //    void			ShutdownHash(void);
        //    void			ClearHash( idBounds &bounds );
        //    int				HashVec(const idVec3 &vec);
        //    int				GetVertex( cm_model_t *model, const idVec3 &v, int *vertexNum );
        //    int				GetEdge( cm_model_t *model, const idVec3 &v1, const idVec3 &v2, int *edgeNum, int v1num );
        //    void			CreatePolygon( cm_model_t *model, idFixedWinding *w, const idPlane &plane, const idMaterial *material, int primitiveNum );
        //    void			PolygonFromWinding( cm_model_t *model, idFixedWinding *w, const idPlane &plane, const idMaterial *material, int primitiveNum );
        //    void			CalculateEdgeNormals( cm_model_t *model, cm_node_t *node );
        //    void			CreatePatchPolygons( cm_model_t *model, idSurface_Patch &mesh, const idMaterial *material, int primitiveNum );
        //    void			ConvertPatch( cm_model_t *model, const idMapPatch *patch, int primitiveNum );
        //    void			ConvertBrushSides( cm_model_t *model, const idMapBrush *mapBrush, int primitiveNum );
        //    void			ConvertBrush( cm_model_t *model, const idMapBrush *mapBrush, int primitiveNum );
        //    void			PrintModelInfo( const cm_model_t *model );
        //    void			AccumulateModelInfo( cm_model_t *model );
        //    void			RemapEdges( cm_node_t *node, int *edgeRemap );
        //    void			OptimizeArrays( cm_model_t *model );
        //    void			FinishModel( cm_model_t *model );
        //    void			BuildModels( const idMapFile *mapFile );
        //    CmHandle		FindModel( const char *name );
        //    cm_model_t *	CollisionModelForMapEntity( const idMapEntity *mapEnt );	// brush/patch model from .map
        //    cm_model_t *	LoadRenderModel( const char *fileName );					// ASE/LWO models
        //    bool			TrmFromModel_r( idTraceModel &trm, cm_node_t *node );
        //    bool			TrmFromModel( const cm_model_t *model, idTraceModel &trm );

        //private:			// CollisionMap_files.cpp
        //                    // writing
        //    void			WriteNodes( idFile *fp, cm_node_t *node );
        //    int				CountPolygonMemory( cm_node_t *node ) const;
        //    void			WritePolygons( idFile *fp, cm_node_t *node );
        //    int				CountBrushMemory( cm_node_t *node ) const;
        //    void			WriteBrushes( idFile *fp, cm_node_t *node );
        //    void			WriteCollisionModel( idFile *fp, cm_model_t *model );
        //    void			WriteCollisionModelsToFile( const char *filename, int firstModel, int lastModel, unsigned int mapFileCRC );
        //                    // loading
        //    cm_node_t *		ParseNodes( idLexer *src, cm_model_t *model, cm_node_t *parent );
        //    void			ParseVertices( idLexer *src, cm_model_t *model );
        //    void			ParseEdges( idLexer *src, cm_model_t *model );
        //    void			ParsePolygons( idLexer *src, cm_model_t *model );
        //    void			ParseBrushes( idLexer *src, cm_model_t *model );
        //    bool			ParseCollisionModel( idLexer *src );
        //    bool			LoadCollisionModelFile( const char *name, unsigned int mapFileCRC );

        //private:			// CollisionMap_debug
        //    int				ContentsFromString( const char *string ) const;
        //    const char *	StringFromContents( const int contents ) const;
        //    void			DrawEdge( cm_model_t *model, int edgeNum, const idVec3 &origin, const idMat3 &axis );
        //    void			DrawPolygon( cm_model_t *model, cm_polygon_t *p, const idVec3 &origin, const idMat3 &axis,
        //                                const idVec3 &viewOrigin );
        //    void			DrawNodePolygons( cm_model_t *model, cm_node_t *node, const idVec3 &origin, const idMat3 &axis,
        //                                const idVec3 &viewOrigin, const float radius );

        //private:			// collision map data
        string mapName;
        ID_TIME_T mapFileTime;
        int loaded;
        // for multi-check avoidance
        int checkCount;
        // models
        int maxModels;
        int numModels;
        Model[] models;
        // polygons and brush for trm model
        PolygonRef[] trmPolygons = new PolygonRef[MAX_TRACEMODEL_POLYS];
        BrushRef[] trmBrushes = new BrushRef[1];
        idMaterial trmMaterial;
        // for data pruning
        int numProcNodes;
        ProcNode procNodes;
        // for retrieving contact points
        bool getContacts;
        ContactInfo contacts;
        int maxContacts;
        int numContacts;
    }
}
