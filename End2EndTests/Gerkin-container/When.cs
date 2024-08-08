using End2EndTests.Solid;

namespace Tests.Gerkin.Container
{

    internal class When
    {
        internal GloboticketDriver driver;

        internal When(GloboticketDriver driver)
        {
            this.driver = driver;
        }

        internal When IAddTheProductToTheShoppingCart(string productName)
        {
             var homePage = new HomePage(driver.CurrentDriver);
            homePage.GotoProductPageWithName(productName);

            var productPage = new ProductPage(driver.CurrentDriver);
            productPage.PlaceOrder();
            return this;
        }

        internal When And() { return this; }
    }
}
