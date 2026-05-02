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
            driver.Navigate().GoToUrl("https://www.google.com");
            Thread.Sleep(2000);
            IWebElement ele = driver.FindElement(By.Name("q"));
            ele.SendKeys("Ravi Tambade");
            Thread.Sleep(2000);
            IWebElement ele1 = driver.FindElement(By.Name("btnK"));
            ele1.Click();
            Thread.Sleep(3000);
            Console.ReadLine();
            driver.Close();
        }
    }
}
