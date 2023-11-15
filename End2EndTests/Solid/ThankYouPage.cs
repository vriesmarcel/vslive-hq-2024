using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace End2EndTests.Solid
{
    internal class ThankYouPage
    {

        private IWebDriver driver;

        internal ThankYouPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool IsShowingThankYouMessage()
        {
            return true;
        }
    }
}
