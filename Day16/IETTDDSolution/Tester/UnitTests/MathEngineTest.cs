using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathLib;
namespace Tester;

[TestClass]
public class SimpleTest
{
    // Test Scenario
    [TestMethod]
    public void TestAddition1()
    {
        //Addition functionality
        //Input Data: 67,78
        //Exptected Output:154
        //AUT:
        //Class Library(Unit): MathLib.dll
        //MathEngine instance
        //perform Test:
        MathEngine tartget=new MathEngine();
        double expectedResult=145;
        double num1=67;
        double num2=78;

        double actualResult=tartget.Addition(num1,num2);
        Assert.AreEqual(expectedResult,actualResult);
    }


     [TestMethod]
    public void TestAddition2()
    {    
        MathEngine tartget=new MathEngine();
        double expectedResult=8;
        double num1=6;
        double num2=2;
        double actualResult=tartget.Addition(num1,num2);
        Assert.AreEqual(expectedResult,actualResult);
    }
}