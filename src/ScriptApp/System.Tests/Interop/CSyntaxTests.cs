using System;
using SystemEx.Html;
using System.Specialized.JsTestRunner;
using SystemEx.Interop;
namespace Interop
{
    public class CSyntaxTests
    {
        static CSyntaxTests() { TestCaseBuilder.TestCase("CSyntaxTests", typeof(CSyntaxTests).Prototype); }

        public void Test_simple_format()
        {
            Asserts.AssertEquals("format", CSyntax.Sprintf("format"));
        }

        public void Test_single_decimal_format()
        {
            Asserts.AssertEquals("test \u0000", CSyntax.Sprintf("test %d", new object[] { 1 }));
        }

        public void TestSprintfInt32_passes()
        {
            Asserts.AssertEquals("test \u0000", CSyntax.SprintfInt32("test %d", 1));
        }
    }
}