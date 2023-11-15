using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace End2EndTests
{
    [TestClass]
    public class GloboticketUITests
    {
        [TestMethod]
        public void BuyOneProduct()
        {
            using (IWebDriver driver = new EdgeDriver())
            {
                StartDriver(driver);
                
            }
        }

        [TestMethod]
        public void BuyOneProductAfterAddingAndRemovingAnotherProduct()
        {
            using (IWebDriver driver = new EdgeDriver())
            {
                StartDriver(driver);


                driver.Close();
            }
        }

        private static void StartDriver(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("http://localhost:5266/");
            driver.Manage().Window.Maximize();
        }
    }
}