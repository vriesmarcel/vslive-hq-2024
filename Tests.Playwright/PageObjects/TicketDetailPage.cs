using Microsoft.Playwright;
using PlaywrightTests;

namespace Tests.Playwright.PageObjects
{
    internal class TicketDetailPage
    {

        private ContextTestWithArtifact testContext;

        internal TicketDetailPage(ContextTestWithArtifact testContext)
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