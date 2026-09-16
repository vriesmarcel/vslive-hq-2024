using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace Tests.Playwright.PageObjects.demo03
{
    internal class HomePage
    {
        PageTest testContext;
        public static void GetHomePage(PageTest testContext, string homepageurl)
        {
            testContext.Page.GotoAsync(homepageurl).Wait();
        }

        protected HomePage(PageTest testContext) {

            this.testContext = testContext;
        }
        public TicketDetailPage SelectTicket(string concertName)
        {
            var element = this.testContext.Page.GetByRole(AriaRole.Row)
                .Filter(new() { HasText = concertName });
            element.GetByRole(AriaRole.Cell, new() { Name = "PURCHASE DETAILS" }).ClickAsync().Wait();

            return new TicketDetailPage(testContext);
        }
    }
}