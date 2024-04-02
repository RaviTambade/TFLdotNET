namespace Tester;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUT;
using Mathematics;


[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        //Testbed preparatio
        //Target is prepared for testing
        PrimeService obj=new PrimeService();
        //Set up data to test unit test
        int candidate=1;
        //Invoke method
        bool result=obj.IsPrime(candidate);
        //Collect result
        //Check the result is as per expectation
         Assert.IsFalse(result, "1 should not be prime");
    }

    [TestMethod]
    public void TestMethod2()
    {
        //Testbed preparatio
        //Target is prepared for testing
        PrimeService obj=new PrimeService();
        //Set up data to test unit test
        int candidate=78;
        //Invoke method
        bool result=obj.IsPrime(candidate);
        //Collect result
        //Check the result is as per expectation
         Assert.IsFalse(result, "1 should not be prime");
    }

    [TestMethod]
    public void TestMethod3()
    {
        //Testbed preparatio
        //Target is prepared for testing
        PrimeService obj=new PrimeService();
        //Set up data to test unit test
        int candidate=67;
        //Invoke method
        bool result=obj.IsPrime(candidate);
        //Collect result
        //Check the result is as per expectation
         Assert.IsFalse(result, "1 should not be prime");
    }


     [TestMethod]
    public void MathEngineAdditionTest3(){
        //Input Data
        double num1=67;
        double num2=3;

        double resultExpected=70;

        //AUT
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }


    [TestMethod]
    public void MathEngineAdditionTest1(){
        //Input Data
        double num1=45;
        double num2=45;

        double resultExpected=90;
        
        //AUT
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }

    [TestMethod]
    public void MathEngineAdditionTest2(){
        //Input Data
        double num1=23;
        double num2=23;

        double resultExpected=46;
        
        //AUT
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }

}