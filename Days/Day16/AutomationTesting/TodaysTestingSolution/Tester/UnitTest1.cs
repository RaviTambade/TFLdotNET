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
        PrimeService obj=new PrimeService();
        int candidate=1;
        bool result=obj.IsPrime(candidate);
         Assert.IsFalse(result, "1 should not be prime");
    }
    [TestMethod]
    public void TestMethod2()
    {
        PrimeService obj=new PrimeService();
        int candidate=78;
        bool result=obj.IsPrime(candidate);
         Assert.IsFalse(result, "1 should not be prime");
    }
    [TestMethod]
    public void TestMethod3()
    {
        PrimeService obj=new PrimeService();
        int candidate=67;
        bool result=obj.IsPrime(candidate);
         Assert.IsFalse(result, "1 should not be prime");
    }
     [TestMethod]
    public void MathEngineAdditionTest3(){
        double num1=67;
        double num2=3;
        double resultExpected=70;
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }
    [TestMethod]
    public void MathEngineAdditionTest1(){
        double num1=45;
        double num2=45;
        double resultExpected=90;
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }
    [TestMethod]
    public void MathEngineAdditionTest2(){
        double num1=23;
        double num2=23;
        double resultExpected=46;
        MathEngine  math=new MathEngine();
        double actualResult=math.Addition(num1,num2);
        Assert.AreEqual(resultExpected,actualResult);
    }
}
