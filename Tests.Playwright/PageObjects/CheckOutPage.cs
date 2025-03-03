using Microsoft.Playwright;
using PlaywrightTests;

namespace Tests.Playwright.PageObjects
{
    public class CheckOutPage
    {
        private ContextTestWithArtifact testContext;

        public CheckOutPage(ContextTestWithArtifact testContext)
        {

            this.testContext = testContext;
        }

        public bool IsOrderPlaced()
        {
            return testContext.Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" }).IsVisibleAsync().Result;
        }
    }
}