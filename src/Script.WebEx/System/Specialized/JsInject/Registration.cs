using System.Runtime.CompilerServices;
namespace System.Specialized.JsInject
{
    [IgnoreNamespace, Imported, ScriptName("JsInject.Registration")]
    public class Registration
    {
        [PreserveCase]
        public void Reused() { }
        [PreserveCase]
        public void Owned() { }
    }
}
