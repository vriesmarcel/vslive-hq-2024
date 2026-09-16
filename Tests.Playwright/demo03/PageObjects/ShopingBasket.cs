using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace Tests.Playwright.PageObjects.demo03
{
    internal class ShopingBasket
    {
    
        private PageTest testContext;

        public ShopingBasket(PageTest testContext)
        {

            this.testContext = testContext;
        }

        public void Checkout(CustomerNico customer)
        {
            testContext.Page.Locator("id=Name").FillAsync(customer.name).Wait();
            testContext.Page.Locator("id=Address").FillAsync(customer.street).Wait();
            testContext.Page.Locator("id=Town").FillAsync(customer.town).Wait();
            testContext.Page.Locator("id=PostalCode").FillAsync(customer.postalcode).Wait();
            testContext.Page.Locator("id=CreditCardDate").FillAsync(customer.expdate).Wait();
            testContext.Page.Locator("id=Email").FillAsync(customer.email).Wait();
            testContext.Page.Locator("id=CreditCard").FillAsync(customer.cc).Wait();

            var button = testContext.Page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" });
            button.ClickAsync().Wait();
         
        }
    }
}