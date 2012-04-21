using System.Runtime.CompilerServices;
using System.Collections;
namespace System.Specialized.JsTestRunner
{
    [IgnoreNamespace, Imported, ScriptName("window")]
    public class Asserts
    {
        /// <summary>
        /// Throws a JavaScript Error with given message string.
        /// </summary>
        public static void Fail() { }
        /// <summary>
        /// Throws a JavaScript Error with given message string.
        /// </summary>
        public static void Fail(string message) { }

        public static void Assert(string actual) { }
        public static void Assert(string message, object actual) { }

        /// <summary>
        /// Fails if the result isn't truthy. To use a message, add it as the first parameter.
        /// </summary>
        public static void AssertTrue(bool actual) { }
        /// <summary>
        /// Fails if the result isn't truthy. To use a message, add it as the first parameter.
        /// </summary>
        public static void AssertTrue(string message, bool actual) { }
        /// <summary>
        /// Fails if the result isn't falsy.
        /// </summary>
        public static void AssertFalse(bool actual) { }
        /// <summary>
        /// Fails if the result isn't falsy.
        /// </summary>
        public static void AssertFalse(string message, bool actual) { }

        /// <summary>
        /// Fails if the expected and actual values can not be compared to be equal.
        /// </summary>
        public static void AssertEquals(object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual values can not be compared to be equal.
        /// </summary>
        public static void AssertEquals(string message, object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual values can be compared to be equal.
        /// </summary>
        public static void AssertNotEquals(object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual values can be compared to be equal.
        /// </summary>
        public static void AssertNotEquals(string message, object expected, object actual) { }

        /// <summary>
        /// Fails if the expected and actual values are not references to the same object.
        /// </summary>
        public static void AssertSame(object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual values are not references to the same object.
        /// </summary>
        public static void AssertSame(string message, object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual are references to the same object.
        /// </summary>
        public static void AssertNotSame(object expected, object actual) { }
        /// <summary>
        /// Fails if the expected and actual are references to the same object.
        /// </summary>
        public static void AssertNotSame(string message, object expected, object actual) { }

        /// <summary>
        /// Fails if the given value is not exactly null.
        /// </summary>
        public static void AssertNull(object actual) { }
        /// <summary>
        /// Fails if the given value is not exactly null.
        /// </summary>
        public static void AssertNull(string message, object actual) { }
        /// <summary>
        /// Fails if the given value is exactly null.
        /// </summary>
        public static void AssertNotNull(object actual) { }
        /// <summary>
        /// Fails if the given value is exactly null.
        /// </summary>
        public static void AssertNotNull(string message, object actual) { }

        /// <summary>
        /// Fails if the given value is not undefined.
        /// </summary>
        public static void AssertUndefined(object actual) { }
        /// <summary>
        /// Fails if the given value is not undefined.
        /// </summary>
        public static void AssertUndefined(string message, object actual) { }
        /// <summary>
        /// Fails if the given value is undefined.
        /// </summary>
        public static void AssertNotUndefined(object actual) { }
        /// <summary>
        /// Fails if the given value is undefined.
        /// </summary>
        public static void AssertNotUndefined(string message, object actual) { }

        /// <summary>
        /// Fails if the given value is not a NaN.
        /// </summary>
        public static void AssertNaN(object actual) { }
        /// <summary>
        /// Fails if the given value is not a NaN.
        /// </summary>
        public static void AssertNaN(string message, object actual) { }
        /// <summary>
        /// Fails if the given value is a NaN.
        /// </summary>
        public static void AssertNotNaN(object actual) { }
        /// <summary>
        /// Fails if the given value is a NaN.
        /// </summary>
        public static void AssertNotNaN(string message, object actual) { }

        /// <summary>
        /// Fails if the code in the callback does not throw the given error.
        /// </summary>
        public static void AssertException(Action callback, object error) { }
        /// <summary>
        /// Fails if the code in the callback does not throw the given error.
        /// </summary>
        public static void AssertException(string message, Action callback, object error) { }
        /// <summary>
        /// Fails if the code in the callback throws an error.
        /// </summary>
        public static void AssertNoException(Action callback, object error) { }
        /// <summary>
        /// Fails if the code in the callback throws an error.
        /// </summary>
        public static void AssertNoException(string message, Action callback, object error) { }

        /// <summary>
        /// Fails if the given value is not an Array.
        /// </summary>
        public static void AssertArray(object actual) { }
        /// <summary>
        /// Fails if the given value is not an Array.
        /// </summary>
        public static void AssertArray(string message, object actual) { }

        /// <summary>
        /// Fails if the JavaScript type of the value isn't the expected string.
        /// </summary>
        public static void AssertTypeOf(object expected, object value) { }
        /// <summary>
        /// Fails if the JavaScript type of the value isn't the expected string.
        /// </summary>
        public static void AssertTypeOf(string message, object expected, object value) { }

        /// <summary>
        /// Fails if the given value is not a Boolean. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertBoolean(bool actual) { }
        /// <summary>
        /// Fails if the given value is not a Boolean. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertBoolean(string message, bool actual) { }

        /// <summary>
        /// Fails if the given value is not a Function. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertFunction(object actual) { }
        /// <summary>
        /// Fails if the given value is not a Function. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertFunction(string message, object actual) { }

        /// <summary>
        /// Fails if the given value is not an Object. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertObject(object actual) { }
        /// <summary>
        /// Fails if the given value is not an Object. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertObject(string message, object actual) { }

        /// <summary>
        /// Fails if the given value is not a Number. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertNumber(object actual) { }
        /// <summary>
        /// Fails if the given value is not a Number. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertNumber(string message, object actual) { }

        /// <summary>
        /// Fails if the given value is not a String. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertString(string actual) { }
        /// <summary>
        /// Fails if the given value is not a String. Convenience function to assertTypeOf.
        /// </summary>
        public static void AssertString(string message, string actual) { }

        /// <summary>
        /// Fails if the given value does not match the given regular expression.
        /// </summary>
        public static void AssertMatch(string regexp, object actual) { }
        /// <summary>
        /// Fails if the given value does not match the given regular expression.
        /// </summary>
        public static void AssertMatch(string message, string regexp, object actual) { }
        /// <summary>
        /// Fails if the given value matches the given regular expression.
        /// </summary>
        public static void AssertNoMatch(string regexp, object actual) { }
        /// <summary>
        /// Fails if the given value matches the given regular expression.
        /// </summary>
        public static void AssertNoMatch(string message, string regexp, object actual) { }

        /// <summary>
        /// Fails if the given DOM element is not of given tagName.
        /// </summary>
        public static void AssertTagName(string tagName, object element) { }
        /// <summary>
        /// Fails if the given DOM element is not of given tagName.
        /// </summary>
        public static void AssertTagName(string message, string tagName, object element) { }

        /// <summary>
        /// Fails if the given DOM element does not have given CSS class name.
        /// </summary>
        public static void AssertClassName(string className, object element) { }
        /// <summary>
        /// Fails if the given DOM element does not have given CSS class name.
        /// </summary>
        public static void AssertClassName(string message, string className, object element) { }

        /// <summary>
        /// Fails if the given DOM element does not have given ID.    
        /// </summary>
        public static void AssertElementId(string id, object element) { }
        /// <summary>
        /// Fails if the given DOM element does not have given ID.    
        /// </summary>
        public static void AssertElementId(string message, string id, object element) { }

        /// <summary>
        /// Fails if the given object is not an instance of given constructor.
        /// </summary>
        public static void AssertInstanceOf(object constructor, object actual) { }
        /// <summary>
        /// Fails if the given object is not an instance of given constructor.
        /// </summary>
        public static void AssertInstanceOf(string message, object constructor, object actual) { }
        /// <summary>
        /// Fails if the given object is an instance of given constructor. 
        /// </summary>
        public static void AssertNotInstanceOf(object constructor, object actual) { }
        /// <summary>
        /// Fails if the given object is an instance of given constructor. 
        /// </summary>
        public static void AssertNotInstanceOf(string message, object constructor, object actual) { }
    }
}
