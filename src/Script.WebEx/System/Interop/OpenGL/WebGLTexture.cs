using System.Runtime.CompilerServices;
namespace System.Interop.OpenGL
{
    /// <summary>
    /// The WebGLTexture interface represents an OpenGL Texture Object. The underlying object is created as if by calling glGenTextures (OpenGL ES 2.0 �3.7.13, man page)  , bound as if by calling glBindTexture (OpenGL ES 2.0 �3.7.13, man page)  and destroyed as if by calling glDeleteTextures (OpenGL ES 2.0 �3.7.13, man page). 
    /// </summary>
#if CODE_ANALYSIS
    [IgnoreNamespace, Imported]
#endif
    public class WebGLTexture : WebGLObject
    {
        protected WebGLTexture() { }
    }
}