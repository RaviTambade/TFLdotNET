using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using AuthLib;

namespace Tester;

[TestClass]
public class AuthTest
{
    // Test Scenario
    [TestMethod]
    public void TestAuthentication()
    {

        bool actualResult=true;

        Assert.IsTrue(actualResult);

    }

    [TestMethod]
    public void TestUserCredentials()
    {
        bool actualResult=true;
        //Prepare your test enviornment -----TestBed
        //1.Prepare data (Sample Data)
        List<Credential> credentials=new List<Credential>();
        credentials.Add( new Credential{ UserId="ravi.tambade@transflower.in", Pass="tfl@123"});
        credentials.Add( new Credential{ UserId="manoj.jadhav@iet.com", Pass="tfl@123"});
        AuthService service=new AuthService();  //AUT
        actualResult=service.Authenticate(credentials[0]);
        Assert.IsTrue(actualResult);
    }



}
