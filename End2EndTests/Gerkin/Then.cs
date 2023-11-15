using End2EndTests.Solid;

namespace Tests.Playwright.Gerkin
{
    internal class Then
    {
        internal GloboticketDriver driver;
        public Then(GloboticketDriver driver) 
        {
            this.driver = driver;
        }   

        internal bool TheShoppingCartContainsNumberOfItems(int numberOfItems)
        {
            var basket = new ShoppingBasket(driver.CurrentDriver);
            return basket.HasNumberOfItems(numberOfItems);
        }
        internal bool AThankYouMessageIsShown()
        {
            var checkout = new ThankYouPage(driver.CurrentDriver);
            return checkout.IsShowingThankYouMessage();
        }


    }
}
