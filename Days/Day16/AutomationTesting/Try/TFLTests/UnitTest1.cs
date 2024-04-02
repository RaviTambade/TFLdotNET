using Membership;
namespace TFLTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        string sampleEmail="sachin.jadhav@transflower.in";
        string samplePassword="432";
         bool result = AccountManager.Login(sampleEmail,samplePassword);
          Assert.AreEqual(result, true);
    }

    /*[TestMethod]
    public void TestMethod2()
    {
        string sampleEmail="ravi.tambade@transflower.in";
        string samplePassword="123";
         bool result = AccountManager.Login(sampleEmail,samplePassword);
          Assert.AreEqual(result, true);
    }

    */
}