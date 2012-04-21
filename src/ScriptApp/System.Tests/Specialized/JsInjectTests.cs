using System;
using System.Specialized.JsTestRunner;
using System.Specialized.JsInject;
namespace Html
{
    public class Bar { }
    public class Foo
    {
        private Bar _bar;
        public Foo(Bar bar) { _bar = bar; }
        public Bar GetBar() { return _bar; }
    }
    public class Rabbit
    {
        private int _weight;
        public Rabbit(int weight) { _weight = weight; }
        public int GetWeight() { return _weight; }
    }

    public class JsInjectTests
    {
        static JsInjectTests() { TestCaseBuilder.TestCase("JsInjectTests", typeof(JsInjectTests).Prototype); }

        public void TestInstantiation()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Register("bar", delegate(Container c) { return new Bar(); });
            containerBuilder.Register("foo", delegate(Container c) { return new Foo((Bar)c.Resolve("bar")); });
            Container container = containerBuilder.Create();
            //
            Foo foo = (Foo)container.Resolve("foo");
            //
            Asserts.AssertNotNull(foo);
            Asserts.AssertNotNull(foo.GetBar());
        }

        public void TestInstanceIsReusedWithinContainer()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Register("bar", delegate(Container c) { return new Bar(); }).Reused();
            Container container = containerBuilder.Create();
            //
            object bar1 = container.Resolve("bar");
            object bar2 = container.Resolve("bar");
            //
            Asserts.AssertSame(bar1, bar2);
        }

        public void TestResolveWithParameter()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Register("Rabbit", delegate(Container c, object weight) { return new Rabbit((int)weight); });
            Container container = containerBuilder.Create();
            //
            Rabbit rabbit = (Rabbit)container.Resolve("Rabbit", 55);
            //
            Asserts.AssertNotNull(rabbit);
            Asserts.AssertEquals(55, rabbit.GetWeight());
        }
    }
}