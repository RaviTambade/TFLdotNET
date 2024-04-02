using Microsoft.VisualStudio.TestTools.UnitTesting;
using AUT;

namespace Prime.UnitTests.Services
{
    [TestClass]
    public class PrimeService_IsPrimeShould
    {

        //1.Set Up TestBed

        private readonly PrimeService  _primeService;
        public PrimeService_IsPrimeShould()
        {
            _primeService = new PrimeService();
        }

        [TestMethod]
        public void IsPrime_InputIs1_ReturnFalse()
        {
            bool result = _primeService.IsPrime(1);
            Assert.IsFalse(result, "1 should not be prime");
        }
    }
}