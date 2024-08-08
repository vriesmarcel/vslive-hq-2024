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
                driver.Navigate().GoToUrl("http://localhost:5266/");
                driver.Manage().Window.Maximize();

                BuyTwoProducts(driver);
                CheckOut(driver);

            }
        }

        private static void CheckOut(IWebDriver driver)
        {
            driver.FindElement(By.Id("Name")).SendKeys("marcel");
            driver.FindElement(By.Id("Email")).SendKeys("vriesmarcel@hotmail.com");
            driver.FindElement(By.Id("Address")).SendKeys("Kerkhofweg 12");
            driver.FindElement(By.Id("Town")).Click();
            driver.FindElement(By.Id("Town")).SendKeys("warnsveld");
            driver.FindElement(By.Id("PostalCode")).SendKeys("1213vb");
            driver.FindElement(By.Id("CreditCard")).SendKeys("1111222233334444");
            driver.FindElement(By.Id("CreditCardDate")).Click();
            driver.FindElement(By.Id("CreditCardDate")).SendKeys("12/24");
            driver.FindElement(By.CssSelector(".btn")).Click();
        }

        private static void BuyTwoProducts(IWebDriver driver)
        {
            driver.FindElement(By.CssSelector("tr:nth-child(2) .btn-primary")).Click();
            driver.FindElement(By.Name("TicketAmount")).Click();
            {
                var dropdown = driver.FindElement(By.Name("TicketAmount"));
                dropdown.FindElement(By.XPath("//option[. = '2']")).Click();
            }
            driver.FindElement(By.CssSelector(".btn")).Click();
            driver.FindElement(By.LinkText("CHECKOUT")).Click();
            driver.FindElement(By.Id("Name")).Click();
        }

        [TestMethod]
        public void BuyOneProductAfterAddingAndRemovingAnotherProduct()
        {
            using (IWebDriver driver = new EdgeDriver())
            {
                StartDriver(driver);
                driver.Navigate().GoToUrl("http://localhost:5266/");
                driver.Manage().Window.Size = new System.Drawing.Size(1552, 832);
                driver.FindElement(By.LinkText("PURCHASE DETAILS")).Click();
                driver.FindElement(By.CssSelector(".btn")).Click();
                driver.FindElement(By.LinkText("Back to event catalog")).Click();
                driver.FindElement(By.CssSelector("tr:nth-child(2) .btn-primary")).Click();
                driver.FindElement(By.CssSelector(".btn")).Click();
                driver.FindElement(By.LinkText("CHECKOUT")).Click();
                driver.FindElement(By.Id("Name")).Click();
                driver.FindElement(By.Id("Name")).SendKeys("m");
                driver.FindElement(By.Id("Email")).SendKeys("m@m.com");
                driver.FindElement(By.Id("Address")).SendKeys("a");
                driver.FindElement(By.Id("Town")).SendKeys("b");
                driver.FindElement(By.Id("PostalCode")).SendKeys("1212vb");
                driver.FindElement(By.Id("CreditCard")).SendKeys("1111222223333344444");
                driver.FindElement(By.Id("CreditCardDate")).SendKeys("12/24");
                driver.FindElement(By.Id("Email")).Click();
                driver.FindElement(By.CssSelector(".btn")).Click();
                driver.FindElement(By.Id("CreditCard")).Click();
                driver.FindElement(By.Id("CreditCard")).SendKeys("1111222233334444");
                driver.FindElement(By.CssSelector(".btn")).Click();


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