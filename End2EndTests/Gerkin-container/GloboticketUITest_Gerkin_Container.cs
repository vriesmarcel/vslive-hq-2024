using OpenQA.Selenium.Edge;
using SeleniumTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Playwright.Gerkin;

namespace Tests.Gerkin.Container
{
    [TestClass]
    public class GloboticketUITest_Gerkin_Container
    {
        private GloboticketDriver ?driver;

        [TestInitialize]
        public void Init()
        {
            driver = new GloboticketDriver();
        }

        [TestCleanup]
        public async Task CleanUp()
        {
            if (driver != null)
            {
                driver.CleanUp();
                await ContainerManager.CleanupContainers();
            }
        }


        [TestMethod]
        public void BuyOneProduct()
        {
            if (driver != null)
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
            else
            {
                throw new Exception("driver not initialized");
            }
        }

        [TestMethod]
        public void BuyOneProductStateOfTrance()
        {
            if (driver != null)
            {
                var Given = new Given(driver);
                var When = new When(driver);
                var Then = new Then(driver);

                Given.IHaveACleanDatabaseWithStateOfTranceInCatalog()
                      .And()
                      .GloboticketWebsiteIsAvailable();

                When.IAddTheProductToTheShoppingCart("State of Trance");
                Then.TheShoppingCartContainsNumberOfItems(1);
            }
            else
            {
                throw new Exception("driver not initialized");
            }
        }
    }
}
