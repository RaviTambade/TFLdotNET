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

            //identify search text box of Google page
            IWebElement ele = driver.FindElement(By.Name("q"));
            //Enter  the value into Google Searh Text Box
            ele.SendKeys("Transflower");
            Thread.Sleep(2000);

            //Identify The google search button.
            IWebElement ele1 = driver.FindElement(By.Name("btnK"));

            //Automate button Click
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


            //identify username text box from facebook page
            IWebElement ele = driver.FindElement(By.Id("email"));
            //enter the username value
            ele.SendKeys("ravi_tambade@hotmail.com");
            Thread.Sleep(2000);
            //identify the password text box from facebook page
            IWebElement ele1 = driver.FindElement(By.Name("pass"));

            //enter the password value
            ele1.SendKeys("vcbcvbcvbcvb");

            //Get the element button login of facebook page
            IWebElement ele2 = driver.FindElement(By.Name("login"));
            ele2.Click();
            Thread.Sleep(3000);
            //Click on the login button

            driver.Quit();
        }
    }
}
