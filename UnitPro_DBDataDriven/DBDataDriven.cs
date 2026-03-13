// Removed unused MSTest import to avoid mixing test frameworks
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using NUnit.Framework;

namespace UnitPro_DBDataDriven
{
    [TestFixture]
    public class DBDataDriven
    {
        IWebDriver _driver;
        [SetUp]
        public void DriverInit()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://adactinhotelapp.com/");
            _driver.Manage().Window.Maximize();
        }
        public static IEnumerable<TestCaseData> LoginData()
        {
            var _data = DBDataSource.GetDBData();
            foreach(var details in _data)
            {
                yield return new TestCaseData(details.Username, details.Password);
            }
        }
        [Test,TestCaseSource(nameof(LoginData))]        
        public void TC_DBDataDrivenPass(string _username, string _password)
        {
            _driver.FindElement(By.Id("username")).SendKeys(_username);
            _driver.FindElement(By.Id("password")).SendKeys(_password);
            _driver.FindElement(By.Id("login")).Click();
            Assert.That(_driver.Title, Is.EqualTo("Adactin.com - Search Hotel"));
        }
        [Test,TestCaseSource(nameof(LoginData))]
        public void TC_DBDataDrivenFail(string _username, string _password)
        {
            _driver.FindElement(By.Id("username")).SendKeys(_username);
            _driver.FindElement(By.Id("password")).SendKeys(_password);
            _driver.FindElement(By.Id("login")).Click();
            string _errorMessage = _driver.FindElement(By.ClassName("auth_error")).Text;
            //Assert.That(_errorMessage,Is.EqualTo("Invalid Login details or Your Password might have expired. Click here to reset your password"));
            NUnit.Framework.Assert.That(_driver.FindElement(By.Id("username_span")).Text, Is.EqualTo("Invalid Login details or Your Password might have expired. Click here to reset your password"));
        }
        [TearDown]
        public void DriverClose()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}
