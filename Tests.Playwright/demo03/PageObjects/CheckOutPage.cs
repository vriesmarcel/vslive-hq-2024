using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace Tests.Playwright.PageObjects.demo03
{
    public class CheckOutPage
    {
        private PageTest testContext;

        public CheckOutPage(PageTest testContext)
        {

            this.testContext = testContext;
        }

        public bool IsOrderPlaced()
        {
            return testContext.Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" }).IsVisibleAsync().Result;
        }
    }
}