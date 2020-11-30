using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UITestHTML
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDir = "C:\\seleniumDrivers2";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDir);
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        //[ClassCleanup]
        //public static void TearDown()
        //{
        //    _driver.Dispose();
        //}

        [TestMethod]
        public void TestGetMethod()
        {
            _driver.Navigate().GoToUrl("http://localhost:3000/");
            Assert.AreEqual("SmartCanteen", _driver.Title);

            IWebElement buttonElement = _driver.FindElement(By.Id("getAllCustomersButton"));
            buttonElement.Click();

            IWebElement buttonElement2 = _driver.FindElement(By.Id("clearAllCustomerData"));
            buttonElement2.Click();
        }
    }
}
