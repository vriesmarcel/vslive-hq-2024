using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Playwright.Gerkin
{
    internal class GloboticketDriver
    {
        internal string homepageUrl = "http://localhost:5266/";
        internal IWebDriver? driver;
        internal bool isInitialized = false;
        public void GotoHomepage()
        {
            if(!isInitialized)
            {
                Initialize();
            }
            driver?.Navigate().GoToUrl(homepageUrl);

        }

        public void Initialize()
        {

            driver = new EdgeDriver();
            driver?.Navigate().GoToUrl(homepageUrl);
            driver.Manage().Window.Maximize();

        }

        internal void CleanUp()
        {
            driver.Close();
            driver.Dispose();
        }

        public IWebDriver CurrentDriver
        {
            get
            {
                return driver;
            }
        }
    }
}
