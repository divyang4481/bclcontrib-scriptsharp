#if !CODE_ANALYSIS
using System.Interop.InternalCSyntax;
namespace System.Interop
#else
using System.Runtime.CompilerServices;
using SystemEx.Interop.InternalCSyntax;
namespace SystemEx.Interop
#endif
{
    public partial class CSyntax
    {
#if CODE_ANALYSIS
        [AlternateSignature]
        extern public static string Sprintf(string fmt);
#else
        public static string Sprintf(string fmt) { return Sprintf(fmt, null); }
#endif
        public static string Sprintf(string fmt, object[] vargs)
        {
            return ((vargs == null) || (vargs.Length == 0) ? fmt : new PrintfFormat(fmt).SprintfArray(vargs));
        }

        public static string SprintfInt32(string fmt, int x) { return new PrintfFormat(fmt).SprintfInt32(x); }
        public static string SprintfInt64(string fmt, long x) { return new PrintfFormat(fmt).SprintfInt64(x); }
        public static string SprintfDouble(string fmt, double x) { return new PrintfFormat(fmt).SprintfDouble(x); }
        public static string SprintfString(string fmt, string x) { return new PrintfFormat(fmt).SprintfString(x); }
        public static string SprintfObject(string fmt, object x) { return new PrintfFormat(fmt).SprintfObject(x); }
    }
}
