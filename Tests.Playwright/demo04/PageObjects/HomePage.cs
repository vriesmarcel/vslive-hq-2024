using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace Tests.Playwright.PageObjects
{
    internal class HomePage
    {
        PageTest testContext;
        public static HomePage GetHomePage(PageTest testContext, string homepageurl)
        {
            testContext.Page.GotoAsync(homepageurl).Wait();
            return new HomePage(testContext);
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