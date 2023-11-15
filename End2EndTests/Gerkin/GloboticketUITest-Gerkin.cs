using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Playwright.Gerkin;

namespace End2EndTests.Gerkin
{
    [TestClass]
    public class GloboticketUITest_Gerkin
    {
        private GloboticketDriver driver;

        [TestInitialize]
        public void Init()
        {
            driver = new GloboticketDriver();
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.CleanUp();
        }


        [TestMethod]
        public void BuyOneProduct()
        {
            
            var Given = new Given(driver);
            var When = new When(driver);
            var Then = new Then(driver);

            Given.IHaveACleanDatabaseWithProducts()
                  .And()
                  .GloboticketWebsiteIsAvailable();

            When.IAddTheProductToTheShoppingCart("John Egbert")
                 .And()
                 .IAddTheProductToTheShoppingCart("John Egbert");

            Then.TheShoppingCartContainsNumberOfItems(2);
        }
    }
}
