using System.Runtime.CompilerServices;
using System.Collections;
namespace System.Specialized.JsTestRunner
{
    [IgnoreNamespace, Imported, ScriptName("window")]
    public class TestCaseBuilder
    {
        [PreserveCase]
        public static void TestCase(string name, Dictionary prototype) { }
        [PreserveCase]
        public static void AsyncTestCase(string name, Dictionary prototype) { }
        [PreserveCase]
        public static void ConditionalTestCase(string name, Dictionary prototype) { }
    }
}
