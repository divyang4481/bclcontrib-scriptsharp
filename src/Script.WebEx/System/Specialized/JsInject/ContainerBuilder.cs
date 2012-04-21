using System.Runtime.CompilerServices;
namespace System.Specialized.JsInject
{
    [IgnoreNamespace, Imported]
    public delegate object Factory(Container c);
    [IgnoreNamespace, Imported]
    public delegate object Factory2(Container c, object arg1);
    [IgnoreNamespace, Imported]
    public delegate object Factory3(Container c, object arg1, object arg2);
    [IgnoreNamespace, Imported]
    public delegate object Factory4(Container c, object arg1, object arg2, object arg3);

    [IgnoreNamespace, Imported, ScriptName("JsInject.ContainerBuilder")]
    public class ContainerBuilder
    {
        [PreserveCase]
        public Registration Register(string name, Factory factory) { return null; }
        [PreserveCase]
        public Registration Register(string name, Factory2 factory) { return null; }
        [PreserveCase]
        public Registration Register(string name, Factory3 factory) { return null; }
        [PreserveCase]
        public Registration Register(string name, Factory4 factory) { return null; }
        [PreserveCase]
        public Container Create() { return null; }
    }
}
