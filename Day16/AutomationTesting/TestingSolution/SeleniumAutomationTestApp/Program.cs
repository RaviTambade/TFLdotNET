using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace SeleniumAutomationTestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            IWebDriver driver;
            driver = new ChromeDriver(@"D:\drv\");
            /* driver.Url = "http://www.google.co.in";
               string title = driver.Title;
             */

            driver.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(2000);

            //identify search text box of Google page
            IWebElement ele = driver.FindElement(By.Name("q"));
            //Enter  the value into Google Searh Text Box
            ele.SendKeys("Ravi Tambade");
            Thread.Sleep(2000);

            //Identify The google search button.
            IWebElement ele1 = driver.FindElement(By.Name("btnK"));

            //Automate button Click
            ele1.Click();
            Thread.Sleep(3000);

            Console.ReadLine();
            driver.Close();
        }
    }
}
