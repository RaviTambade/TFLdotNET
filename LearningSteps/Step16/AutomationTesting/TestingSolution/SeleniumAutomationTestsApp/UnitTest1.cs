using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace SeleniumAutomationTestsApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SeleinumAutomationTest()
        {
            IWebDriver driver;
            driver = new ChromeDriver(@"D:\drv\");
            driver.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(2000);
            IWebElement ele = driver.FindElement(By.Name("q"));
            ele.SendKeys("Transflower");
            Thread.Sleep(2000);
            IWebElement ele1 = driver.FindElement(By.Name("btnK"));
            ele1.Click();
            Thread.Sleep(3000);
            driver.Close();
        }
        [TestMethod]
        public void SeleniumAutomationFacebookTest()
        {
            IWebDriver driver;
            driver = new ChromeDriver(@"D:\drv\");
            driver = new ChromeDriver(@"D:\drv\");
            driver.Url = "https://www.facebook.com/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);
            IWebElement ele = driver.FindElement(By.Id("email"));
            ele.SendKeys("ravi_tambade@hotmail.com");
            Thread.Sleep(2000);
            IWebElement ele1 = driver.FindElement(By.Name("pass"));
            ele1.SendKeys("vcbcvbcvbcvb");
            IWebElement ele2 = driver.FindElement(By.Name("login"));
            ele2.Click();
            Thread.Sleep(3000);
            driver.Quit();
        }
    }
}
