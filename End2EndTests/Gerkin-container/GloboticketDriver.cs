using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Gerkin.Container
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
            // now refresh the page untill we see items on the page.
            // indiator we are not ready yet is the message "No events were found"
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            //wait for the list of events to appear
            IWebElement table = null;
            table = wait.Until(x =>
            {
                driver?.Navigate().GoToUrl(homepageUrl);
                return x.FindElement(By.CssSelector("body > div.container > main > div > table"));
            });
            if (table == null)
            {
                throw new Exception("Could not find any events on the page");
            }
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
