using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace Tests.Playwright.PageObjects
{
    internal class TicketDetailPage
    {

        private PageTest testContext;

        internal TicketDetailPage(PageTest testContext)
        {

            this.testContext = testContext;
 
        }

        public ShopingBasket BuyTicket()
        {
            var element = testContext.Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" });
            element.ClickAsync().Wait();

            element = testContext.Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" });
             element.ClickAsync().Wait();
            return new ShopingBasket(testContext);
        }
    }
}