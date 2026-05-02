using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class GoogledotnetTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  [SetUp]
  public void SetUp() {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  protected void TearDown() {
    driver.Quit();
  }
  [Test]
  public void googledotnet() {
    driver.Navigate().GoToUrl("https://www.google.com/");
    driver.Manage().Window.Size = new System.Drawing.Size(542, 728);
    {
      var element = driver.FindElement(By.CssSelector(".gb_n"));
      Actions builder = new Actions(driver);
      builder.MoveToElement(element).Perform();
    }
    {
      var element = driver.FindElement(By.tagName("body"));
      Actions builder = new Actions(driver);
      builder.MoveToElement(element, 0, 0).Perform();
    }
    driver.FindElement(By.Id("APjFqb")).Click();
    driver.FindElement(By.Id("APjFqb")).SendKeys("dotnet");
    driver.FindElement(By.CssSelector(".eKjLze .LC20lb")).Click();
    js.ExecuteScript("window.scrollTo(0,106)");
    driver.FindElement(By.LinkText("Get started")).Click();
    driver.FindElement(By.CssSelector("#microservices > .media-heading")).Click();
    driver.FindElement(By.LinkText("Install Docker")).Click();
    driver.FindElement(By.LinkText("Continue")).Click();
    driver.FindElement(By.LinkText("Continue")).Click();
    driver.FindElement(By.LinkText("Next steps")).Click();
  }
}
