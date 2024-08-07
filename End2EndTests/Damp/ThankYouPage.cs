using OpenQA.Selenium;

namespace End2EndTests.Damp
{
    internal class ThankYouPage
    {
        private IWebDriver driver;

        internal ThankYouPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool IsThankYouMessageShown()
        {
            return true;
        }
    }
}
