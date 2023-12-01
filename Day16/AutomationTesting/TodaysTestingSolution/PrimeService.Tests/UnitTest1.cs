using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PrimeService.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        bool statusActual=false;
        Assert.IsFalse(statusActual); 
    }
}