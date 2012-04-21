using System.Runtime.CompilerServices;
namespace System.Specialized.JsInject
{
    [IgnoreNamespace, Imported, ScriptName("JsInject.Container")]
    public class Container
    {
        [PreserveCase]
        public object Resolve(string name, params object[] arg) { return null; }
        [PreserveCase]
        public object TryResolve(string name, params object[] arg) { return null; }
        [PreserveCase]
        public void Dispose() { }
    }
}
