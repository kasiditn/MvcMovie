using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace MvcMovie
{  
    class SeleniumTest
    {
        private string _websiteURL = "https://localhost:5001/";
        private RemoteWebDriver _browserDriver;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initializer()
        {
            _websiteURL = (string)TestContext.Properties["webAppUrl"];

        }

        [TestMethod]
        [TestCategory("Selenium")]
        [DataRow("Superman", "011120", "Action", "19.99")]
        [DataRow("Spiderman", "031520", "Action", "29.00")]
        public void CreateMovie(String title, string releaseDate, string genre, string price)
        {
            //Arrange
            _browserDriver = new ChromeDriver();
            _browserDriver.Manage().Window.Maximize();
            _browserDriver.Navigate().GoToUrl(_websiteURL + "Movies/Create");
            //_browserDriver.Manage().Timeouts().ImplicitlyWait(10, TimeUnit.SECONDS);

            _browserDriver.FindElementById("Title").SendKeys(title);
            _browserDriver.FindElementById("ReleaseDate").SendKeys(releaseDate);
            _browserDriver.FindElementById("Genre").SendKeys(genre);
            _browserDriver.FindElementById("Price").SendKeys(price);

            //Create Screenshot
            var screenshot = _browserDriver.GetScreenshot();
            var filename = $"{title}.jpg";
            screenshot.SaveAsFile(filename, ScreenshotImageFormat.Jpeg);
            TestContext.AddResultFile(filename);

            //Act
            _browserDriver.FindElement(By.CssSelector("btn btn-primary")).Click();
            // _browserDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));

            //Assert
            Assert.IsTrue(_browserDriver.PageSource.Contains(title));
            Assert.IsTrue(_browserDriver.PageSource.Contains(releaseDate));
            Assert.IsTrue(_browserDriver.PageSource.Contains(genre));
            Assert.IsTrue(_browserDriver.PageSource.Contains(price));

        }
    }
}
