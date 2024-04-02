using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Membership;

namespace BankTests
{
    [TestClass]
   public  class ValidationTests
    {

        [TestMethod]
        public void ValidCredentialTest()
        {
           
            //Arrange
            string email = "ravi.tambade@transflower.in";
            string password = "tfl@7867";
            bool expected = true;

            //Act
            bool actual = AccountManager.Validate(email, password);

            //Assert
            Assert.AreEqual(expected, actual, "Validaation should be successfull");
        }
    }
}
