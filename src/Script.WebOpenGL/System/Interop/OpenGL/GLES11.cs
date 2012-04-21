using System.Runtime.CompilerServices;
//[Khronos GLES1.1]http://www.khronos.org/registry/gles/api/1.1/gl.h
//[Khronos GLES1.0]http://www.khronos.org/registry/gles/api/1.0/gl.h
namespace System.Interop.OpenGL
{
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public abstract class GLES11
    {
        /* ClearBufferMask */
        public const uint DEPTH_BUFFER_BIT = 0x00000100;
        public const uint STENCIL_BUFFER_BIT = 0x00000400;
        public const uint COLOR_BUFFER_BIT = 0x00004000;

        /* BeginMode */
        public const uint POINTS = 0x0000;
        public const uint LINES = 0x0001;
        public const uint LINE_LOOP = 0x0002;
        public const uint LINE_STRIP = 0x0003;
        public const uint TRIANGLES = 0x0004;
        public const uint TRIANGLE_STRIP = 0x0005;
        public const uint TRIANGLE_FAN = 0x0006;

        /* AlphaFunction */
        public const uint NEVER = 0x0200;
        public const uint LESS = 0x0201;
        public const uint EQUAL = 0x0202;
        public const uint LEQUAL = 0x0203;
        public const uint GREATER = 0x0204;
        public const uint NOTEQUAL = 0x0205;
        public const uint GEQUAL = 0x0206;
        public const uint ALWAYS = 0x0207;

        /* BlendingFactorDest */
        public const uint ZERO = 0;
        public const uint ONE = 1;
        public const uint SRC_COLOR = 0x0300;
        public const uint ONE_MINUS_SRC_COLOR = 0x0301;
        public const uint SRC_ALPHA = 0x0302;
        public const uint ONE_MINUS_SRC_ALPHA = 0x0303;
        public const uint DST_ALPHA = 0x0304;
        public const uint ONE_MINUS_DST_ALPHA = 0x0305;

        /* BlendingFactorSrc */
        /* ZERO */
        /* ONE */
        public const uint DST_COLOR = 0x0306;
        public const uint ONE_MINUS_DST_COLOR = 0x0307;
        public const uint SRC_ALPHA_SATURATE = 0x0308;
        /* SRC_ALPHA */
        /* ONE_MINUS_SRC_ALPHA */
        /* DST_ALPHA */
        /* ONE_MINUS_DST_ALPHA */

        /* ClipPlaneName */
        public const uint CLIP_PLANE0 = 0x3000;
        public const uint CLIP_PLANE1 = 0x3001;
        public const uint CLIP_PLANE2 = 0x3002;
        public const uint CLIP_PLANE3 = 0x3003;
        public const uint CLIP_PLANE4 = 0x3004;
        public const uint CLIP_PLANE5 = 0x3005;

        /* ColorMaterialFace */
        /* FRONT_AND_BACK */

        /* ColorMaterialParameter */
        /* AMBIENT_AND_DIFFUSE */

        /* ColorPointerType */
        /* UNSIGNED_BYTE */
        /* FLOAT */
        /* FIXED */

        /* CullFaceMode */
        public const uint FRONT = 0x0404;
        public const uint BACK = 0x0405;
        public const uint FRONT_AND_BACK = 0x0408;

        /* DepthFunction */
        /* NEVER */
        /* LESS */
        /* EQUAL */
        /* LEQUAL */
        /* GREATER */
        /* NOTEQUAL */
        /* GEQUAL */
        /* ALWAYS */

        /* EnableCap */
        public const uint FOG = 0x0B60;
        public const uint LIGHTING = 0x0B50;
        public const uint TEXTURE_2D = 0x0DE1;
        public const uint CULL_FACE = 0x0B44;
        public const uint ALPHA_TEST = 0x0BC0;
        public const uint BLEND = 0x0BE2;
        public const uint COLOR_LOGIC_OP = 0x0BF2;
        public const uint DITHER = 0x0BD0;
        public const uint STENCIL_TEST = 0x0B90;
        public const uint DEPTH_TEST = 0x0B71;
        /* LIGHT0 */
        /* LIGHT1 */
        /* LIGHT2 */
        /* LIGHT3 */
        /* LIGHT4 */
        /* LIGHT5 */
        /* LIGHT6 */
        /* LIGHT7 */
        public const uint POINT_SMOOTH = 0x0B10;
        public const uint LINE_SMOOTH = 0x0B20;
        public const uint CISSOR_TEST = 0x0C11;
        public const uint COLOR_MATERIAL = 0x0B57;
        public const uint NORMALIZE = 0x0BA1;
        public const uint RESCALE_NORMAL = 0x803A;
        /* POLYGON_OFFSET_FILL */
        public const uint VERTEX_ARRAY = 0x8074;
        public const uint NORMAL_ARRAY = 0x8075;
        public const uint COLOR_ARRAY = 0x8076;
        public const uint TEXTURE_COORD_ARRAY = 0x8078;
        public const uint MULTISAMPLE = 0x809D;
        public const uint SAMPLE_ALPHA_TO_COVERAGE = 0x809E;
        public const uint SAMPLE_ALPHA_TO_ONE = 0x809F;
        public const uint SAMPLE_COVERAGE = 0x80A0;

        /* ErrorCode */
        public const uint NO_ERROR = 0;
        public const uint INVALID_ENUM = 0x0500;
        public const uint INVALID_VALUE = 0x0501;
        public const uint INVALID_OPERATION = 0x0502;
        public const uint OUT_OF_MEMORY = 0x0505;

        /* FogMode */
        /* LINEAR */
        public const uint EXP = 0x0800;
        public const uint EXP2 = 0x0801;

        /* FogParameter */
        public const uint FOG_DENSITY = 0x0B62;
        public const uint FOG_START = 0x0B63;
        public const uint FOG_END = 0x0B64;
        public const uint FOG_MODE = 0x0B65;
        public const uint FOG_COLOR = 0x0B66;

        /* FrontFaceDirection */
        public const uint CW = 0x0900;
        public const uint CCW = 0x0901;

        /* GetPName */
        public const uint CURRENT_COLOR = 0x0B00;
        public const uint CURRENT_NORMAL = 0x0B02;
        public const uint CURRENT_TEXTURE_COORDS = 0x0B03;
        public const uint POINT_SIZE = 0x0B11;
        public const uint POINT_SIZE_MIN = 0x8126;
        public const uint POINT_SIZE_MAX = 0x8127;
        public const uint POINT_FADE_THRESHOLD_SIZE = 0x8128;
        public const uint POINT_DISTANCE_ATTENUATION = 0x8129;
        public const uint SMOOTH_POINT_SIZE_RANGE = 0x0B12;
        public const uint LINE_WIDTH = 0x0B21;
        public const uint SMOOTH_LINE_WIDTH_RANGE = 0x0B22;
        public const uint ALIASED_POINT_SIZE_RANGE = 0x846D;
        public const uint ALIASED_LINE_WIDTH_RANGE = 0x846E;
        public const uint CULL_FACE_MODE = 0x0B45;
        public const uint FRONT_FACE = 0x0B46;
        public const uint SHADE_MODEL = 0x0B54;
        public const uint DEPTH_RANGE = 0x0B70;
        public const uint DEPTH_WRITEMASK = 0x0B72;
        public const uint DEPTH_CLEAR_VALUE = 0x0B73;
        public const uint DEPTH_FUNC = 0x0B74;
        public const uint STENCIL_CLEAR_VALUE = 0x0B91;
        public const uint STENCIL_FUNC = 0x0B92;
        public const uint STENCIL_VALUE_MASK = 0x0B93;
        public const uint STENCIL_FAIL = 0x0B94;
        public const uint STENCIL_PASS_DEPTH_FAIL = 0x0B95;
        public const uint STENCIL_PASS_DEPTH_PASS = 0x0B96;
        public const uint STENCIL_REF = 0x0B97;
        public const uint STENCIL_WRITEMASK = 0x0B98;
        public const uint MATRIX_MODE = 0x0BA0;
        public const uint VIEWPORT = 0x0BA2;
        public const uint MODELVIEW_STACK_DEPTH = 0x0BA3;
        public const uint PROJECTION_STACK_DEPTH = 0x0BA4;
        public const uint TEXTURE_STACK_DEPTH = 0x0BA5;
        public const uint MODELVIEW_MATRIX = 0x0BA6;
        public const uint PROJECTION_MATRIX = 0x0BA7;
        public const uint TEXTURE_MATRIX = 0x0BA8;
        public const uint ALPHA_TEST_FUNC = 0x0BC1;
        public const uint ALPHA_TEST_REF = 0x0BC2;
        public const uint BLEND_DST = 0x0BE0;
        public const uint BLEND_SRC = 0x0BE1;
        public const uint LOGIC_OP_MODE = 0x0BF0;
        public const uint SCISSOR_BOX = 0x0C10;
        public const uint SCISSOR_TEST = 0x0C11;
        public const uint COLOR_CLEAR_VALUE = 0x0C22;
        public const uint COLOR_WRITEMASK = 0x0C23;
        public const uint UNPACK_ALIGNMENT = 0x0CF5;
        public const uint PACK_ALIGNMENT = 0x0D05;
        public const uint MAX_LIGHTS = 0x0D31;
        public const uint MAX_CLIP_PLANES = 0x0D32;
        public const uint MAX_TEXTURE_SIZE = 0x0D33;
        public const uint MAX_MODELVIEW_STACK_DEPTH = 0x0D36;
        public const uint MAX_PROJECTION_STACK_DEPTH = 0x0D38;
        public const uint MAX_TEXTURE_STACK_DEPTH = 0x0D39;
        public const uint MAX_VIEWPORT_DIMS = 0x0D3A;
        public const uint MAX_TEXTURE_UNITS = 0x84E2;
        public const uint SUBPIXEL_BITS = 0x0D50;
        public const uint RED_BITS = 0x0D52;
        public const uint GREEN_BITS = 0x0D53;
        public const uint BLUE_BITS = 0x0D54;
        public const uint ALPHA_BITS = 0x0D55;
        public const uint DEPTH_BITS = 0x0D56;
        public const uint STENCIL_BITS = 0x0D57;
        public const uint POLYGON_OFFSET_UNITS = 0x2A00;
        public const uint POLYGON_OFFSET_FILL = 0x8037;
        public const uint POLYGON_OFFSET_FACTOR = 0x8038;
        public const uint TEXTURE_BINDING_2D = 0x8069;
        public const uint VERTEX_ARRAY_SIZE = 0x807A;
        public const uint VERTEX_ARRAY_TYPE = 0x807B;
        public const uint VERTEX_ARRAY_STRIDE = 0x807C;
        public const uint NORMAL_ARRAY_TYPE = 0x807E;
        public const uint NORMAL_ARRAY_STRIDE = 0x807F;
        public const uint COLOR_ARRAY_SIZE = 0x8081;
        public const uint COLOR_ARRAY_TYPE = 0x8082;
        public const uint COLOR_ARRAY_STRIDE = 0x8083;
        public const uint TEXTURE_COORD_ARRAY_SIZE = 0x8088;
        public const uint TEXTURE_COORD_ARRAY_TYPE = 0x8089;
        public const uint TEXTURE_COORD_ARRAY_STRIDE = 0x808A;
        public const uint VERTEX_ARRAY_POINTER = 0x808E;
        public const uint NORMAL_ARRAY_POINTER = 0x808F;
        public const uint COLOR_ARRAY_POINTER = 0x8090;
        public const uint TEXTURE_COORD_ARRAY_POINTER = 0x8092;
        public const uint SAMPLE_BUFFERS = 0x80A8;
        public const uint SAMPLES = 0x80A9;
        public const uint SAMPLE_COVERAGE_VALUE = 0x80AA;
        public const uint SAMPLE_COVERAGE_INVERT = 0x80AB;

        /* GetTextureParameter */
        /* TEXTURE_MAG_FILTER */
        /* TEXTURE_MIN_FILTER */
        /* TEXTURE_WRAP_S */
        /* TEXTURE_WRAP_T */

        public const uint NUM_COMPRESSED_TEXTURE_FORMATS = 0x86A2;
        public const uint COMPRESSED_TEXTURE_FORMATS = 0x86A3;

        /* HintMode */
        public const uint DONT_CARE = 0x1100;
        public const uint FASTEST = 0x1101;
        public const uint NICEST = 0x1102;

        /* HintTarget */
        public const uint PERSPECTIVE_CORRECTION_HINT = 0x0C50;
        public const uint POINT_SMOOTH_HINT = 0x0C51;
        public const uint LINE_SMOOTH_HINT = 0x0C52;
        public const uint POLYGON_SMOOTH_HINT = 0x0C53;
        public const uint FOG_HINT = 0x0C54;

        /* LightModelParameter */
        public const uint LIGHT_MODEL_AMBIENT = 0x0B53;
        public const uint LIGHT_MODEL_TWO_SIDE = 0x0B52;

        /* LightParameter */
        public const uint AMBIENT = 0x1200;
        public const uint DIFFUSE = 0x1201;
        public const uint SPECULAR = 0x1202;
        public const uint POSITION = 0x1203;
        public const uint SPOT_DIRECTION = 0x1204;
        public const uint SPOT_EXPONENT = 0x1205;
        public const uint SPOT_CUTOFF = 0x1206;
        public const uint CONSTANT_ATTENUATION = 0x1207;
        public const uint LINEAR_ATTENUATION = 0x1208;
        public const uint QUADRATIC_ATTENUATION = 0x1209;

        /* DataType */
        public const uint BYTE = 0x1400;
        public const uint UNSIGNED_BYTE = 0x1401;
        public const uint SHORT = 0x1402;
        public const uint UNSIGNED_SHORT = 0x1403;
        public const uint INT = 0x1404;
        public const uint UNSIGNED_INT = 0x1405;
        public const uint FLOAT = 0x1406;
        public const uint FIXED = 0x140C;

        /* LogicOp */
        public const uint CLEAR = 0x1500;
        public const uint AND = 0x1501;
        public const uint AND_REVERSE = 0x1502;
        public const uint COPY = 0x1503;
        public const uint AND_INVERTED = 0x1504;
        public const uint NOOP = 0x1505;
        public const uint XOR = 0x1506;
        public const uint OR = 0x1507;
        public const uint NOR = 0x1508;
        public const uint EQUIV = 0x1509;
        public const uint INVERT = 0x150A;
        public const uint OR_REVERSE = 0x150B;
        public const uint COPY_INVERTED = 0x150C;
        public const uint OR_INVERTED = 0x150D;
        public const uint NAND = 0x150E;
        public const uint SET = 0x150F;

        /* MaterialFace */
        /* FRONT_AND_BACK */

        /* MaterialParameter */
        public const uint EMISSION = 0x1600;
        public const uint SHININESS = 0x1601;
        public const uint AMBIENT_AND_DIFFUSE = 0x1602;
        /* AMBIENT */
        /* DIFFUSE */
        /* SPECULAR */

        /* MatrixMode */
        public const uint MODELVIEW = 0x1700;
        public const uint PROJECTION = 0x1701;
        public const uint TEXTURE = 0x1702;

        /* NormalPointerType */
        /* BYTE */
        /* SHORT */
        /* FLOAT */
        /* FIXED */

        /* PixelFormat */
        public const uint DEPTH_COMPONENT = 0x1902;
        public const uint ALPHA = 0x1906;
        public const uint RGB = 0x1907;
        public const uint RGBA = 0x1908;
        public const uint LUMINANCE = 0x1909;
        public const uint LUMINANCE_ALPHA = 0x190A;

        /* PixelType */
        /* UNSIGNED_BYTE */
        public const uint UNSIGNED_SHORT_4_4_4_4 = 0x8033;
        public const uint UNSIGNED_SHORT_5_5_5_1 = 0x8034;
        public const uint UNSIGNED_SHORT_5_6_5 = 0x8363;

        /* ShadingModel */
        public const uint FLAT = 0x1D00;
        public const uint SMOOTH = 0x1D01;

        /* StencilFunction */
        /* NEVER */
        /* LESS */
        /* EQUAL */
        /* LEQUAL */
        /* GREATER */
        /* NOTEQUAL */
        /* GEQUAL */
        /* ALWAYS */

        /* StencilOp */
        /* ZERO */
        public const uint KEEP = 0x1E00;
        public const uint REPLACE = 0x1E01;
        public const uint INCR = 0x1E02;
        public const uint DECR = 0x1E03;
        /* INVERT */

        /* StringName */
        public const uint VENDOR = 0x1F00;
        public const uint RENDERER = 0x1F01;
        public const uint VERSION = 0x1F02;
        public const uint EXTENSIONS = 0x1F03;

        /* TexCoordPointerType */
        /* SHORT */
        /* FLOAT */
        /* FIXED */
        /* BYTE */

        /* TextureEnvMode */
        public const uint MODULATE = 0x2100;
        public const uint DECAL = 0x2101;
        /* BLEND */
        public const uint ADD = 0x0104;
        /* REPLACE */

        /* TextureEnvParameter */
        public const uint TEXTURE_ENV_MODE = 0x2200;
        public const uint TEXTURE_ENV_COLOR = 0x2201;

        /* TextureEnvTarget */
        public const uint TEXTURE_ENV = 0x2300;

        /* TextureMagFilter */
        public const uint NEAREST = 0x2600;
        public const uint LINEAR = 0x2601;

        /* TextureMinFilter */
        /* NEAREST */
        /* LINEAR */
        public const uint NEAREST_MIPMAP_NEAREST = 0x2700;
        public const uint LINEAR_MIPMAP_NEAREST = 0x2701;
        public const uint NEAREST_MIPMAP_LINEAR = 0x2702;
        public const uint LINEAR_MIPMAP_LINEAR = 0x2703;

        /* TextureParameterName */
        public const uint TEXTURE_MAG_FILTER = 0x2800;
        public const uint TEXTURE_MIN_FILTER = 0x2801;
        public const uint TEXTURE_WRAP_S = 0x2802;
        public const uint TEXTURE_WRAP_T = 0x2803;
        public const uint GENERATE_MIPMAP = 0x8191;

        /* TextureTarget */
        /* TEXTURE_2D */

        /* TextureUnit */
        public const uint TEXTURE0 = 0x84C0;
        public const uint TEXTURE1 = 0x84C1;
        public const uint TEXTURE2 = 0x84C2;
        public const uint TEXTURE3 = 0x84C3;
        public const uint TEXTURE4 = 0x84C4;
        public const uint TEXTURE5 = 0x84C5;
        public const uint TEXTURE6 = 0x84C6;
        public const uint TEXTURE7 = 0x84C7;
        public const uint TEXTURE8 = 0x84C8;
        public const uint TEXTURE9 = 0x84C9;
        public const uint TEXTURE10 = 0x84CA;
        public const uint TEXTURE11 = 0x84CB;
        public const uint TEXTURE12 = 0x84CC;
        public const uint TEXTURE13 = 0x84CD;
        public const uint TEXTURE14 = 0x84CE;
        public const uint TEXTURE15 = 0x84CF;
        public const uint TEXTURE16 = 0x84D0;
        public const uint TEXTURE17 = 0x84D1;
        public const uint TEXTURE18 = 0x84D2;
        public const uint TEXTURE19 = 0x84D3;
        public const uint TEXTURE20 = 0x84D4;
        public const uint TEXTURE21 = 0x84D5;
        public const uint TEXTURE22 = 0x84D6;
        public const uint TEXTURE23 = 0x84D7;
        public const uint TEXTURE24 = 0x84D8;
        public const uint TEXTURE25 = 0x84D9;
        public const uint TEXTURE26 = 0x84DA;
        public const uint TEXTURE27 = 0x84DB;
        public const uint TEXTURE28 = 0x84DC;
        public const uint TEXTURE29 = 0x84DD;
        public const uint TEXTURE30 = 0x84DE;
        public const uint TEXTURE31 = 0x84DF;
        public const uint ACTIVE_TEXTURE = 0x84E0;
        public const uint CLIENT_ACTIVE_TEXTURE = 0x84E1;

        /* TextureWrapMode */
        public const uint REPEAT = 0x2901;
        public const uint CLAMP_TO_EDGE = 0x812F;

        /* PixelInternalFormat */
        public const uint PALETTE4_RGB8_OES = 0x8B90;
        public const uint PALETTE4_RGBA8_OES = 0x8B91;
        public const uint PALETTE4_R5_G6_B5_OES = 0x8B92;
        public const uint PALETTE4_RGBA4_OES = 0x8B93;
        public const uint PALETTE4_RGB5_A1_OES = 0x8B94;
        public const uint PALETTE8_RGB8_OES = 0x8B95;
        public const uint PALETTE8_RGBA8_OES = 0x8B96;
        public const uint PALETTE8_R5_G6_B5_OES = 0x8B97;
        public const uint PALETTE8_RGBA4_OES = 0x8B98;
        public const uint PALETTE8_RGB5_A1_OES = 0x8B99;

        /* VertexPointerType */
        /* SHORT */
        /* FLOAT */
        /* FIXED */
        /* BYTE */

        /* LightName */
        public const uint LIGHT0 = 0x4000;
        public const uint LIGHT1 = 0x4001;
        public const uint LIGHT2 = 0x4002;
        public const uint LIGHT3 = 0x4003;
        public const uint LIGHT4 = 0x4004;
        public const uint LIGHT5 = 0x4005;
        public const uint LIGHT6 = 0x4006;
        public const uint LIGHT7 = 0x4007;

        /* Buffer Objects */
        public const uint ARRAY_BUFFER = 0x8892;
        public const uint ELEMENT_ARRAY_BUFFER = 0x8893;
        public const uint ARRAY_BUFFER_BINDING = 0x8894;
        public const uint ELEMENT_ARRAY_BUFFER_BINDING = 0x8895;
        public const uint VERTEX_ARRAY_BUFFER_BINDING = 0x8896;
        public const uint NORMAL_ARRAY_BUFFER_BINDING = 0x8897;
        public const uint COLOR_ARRAY_BUFFER_BINDING = 0x8898;
        public const uint TEXTURE_COORD_ARRAY_BUFFER_BINDING = 0x889A;
        public const uint STATIC_DRAW = 0x88E4;
        public const uint DYNAMIC_DRAW = 0x88E8;
        public const uint BUFFER_SIZE = 0x8764;
        public const uint BUFFER_USAGE = 0x8765;

        /* Texture combine + dot3 */
        public const uint SUBTRACT = 0x84E7;
        public const uint COMBINE = 0x8570;
        public const uint COMBINE_RGB = 0x8571;
        public const uint COMBINE_ALPHA = 0x8572;
        public const uint RGB_SCALE = 0x8573;
        public const uint ADD_SIGNED = 0x8574;
        public const uint INTERPOLATE = 0x8575;
        public const uint CONSTANT = 0x8576;
        public const uint PRIMARY_COLOR = 0x8577;
        public const uint PREVIOUS = 0x8578;
        public const uint OPERAND0_RGB = 0x8590;
        public const uint OPERAND1_RGB = 0x8591;
        public const uint OPERAND2_RGB = 0x8592;
        public const uint OPERAND0_ALPHA = 0x8598;
        public const uint OPERAND1_ALPHA = 0x8599;
        public const uint OPERAND2_ALPHA = 0x859A;

        public const uint ALPHA_SCALE = 0x0D1C;

        public const uint SRC0_RGB = 0x8580;
        public const uint SRC1_RGB = 0x8581;
        public const uint SRC2_RGB = 0x8582;
        public const uint SRC0_ALPHA = 0x8588;
        public const uint SRC1_ALPHA = 0x8589;
        public const uint SRC2_ALPHA = 0x858A;

        public const uint DOT3_RGB = 0x86AE;
        public const uint DOT3_RGBA = 0x86AF;
    }
}