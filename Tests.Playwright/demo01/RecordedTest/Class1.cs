using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using Tests.Playwright;
namespace Demo01 
{ 
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class RecordedTests : PageTest
    {
        public override BrowserNewContextOptions ContextOptions()
        {
            return new()
            {
                Locale = "en-US",
                ColorScheme = ColorScheme.Light,
                RecordVideoDir = ".videos",
                IgnoreHTTPSErrors = true,
            };
        }

        [Test]
        public async Task BuyOneProduct()
        {
             await Page.GotoAsync("https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/");
            await Page.GetByRole(AriaRole.Row, new() { Name = "Artist pic 05/17/2027 John" }).GetByRole(AriaRole.Link).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" }).ClickAsync();
            await Page.GetByRole(AriaRole.Link).ClickAsync();
        }
    }
}