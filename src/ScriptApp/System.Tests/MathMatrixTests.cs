using System;
using System.Specialized.JsTestRunner;
using SystemEx;
public class MathMatrixTests
{
    static MathMatrixTests() { TestCaseBuilder.TestCase("MathMatrixTests", typeof(MathMatrixTests).Prototype); }

    public void TestMultiplyMM()
    {
        float[] a = new float[] {
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
        };
        float[] b = new float[] {
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
        };
        float[] ab = new float[16];
        MathMatrix.MultiplyMM(ab, 0, a, 0, b, 0);
        Asserts.AssertEquals(new float[] {
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
        }, ab);
    }
}
