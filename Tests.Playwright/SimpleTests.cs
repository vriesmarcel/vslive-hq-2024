using Microsoft.Playwright;
using Tests.Playwright.Gerkin;
using Tests.Playwright.PageObjects;
using static System.Net.WebRequestMethods;

namespace Tests.Playwright
{
    [TestClass]
    public class SimpleTests
    {
        public TestContext? TestContext;
        public string StartPage = "https://globoticket.azurewebsites.net";
        [TestInitialize]
        public void Initialize()
        {
            var homepage = System.Environment.GetEnvironmentVariable("HomePage");
            if(!string.IsNullOrWhiteSpace(homepage))
                StartPage = homepage.Trim();

            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
            if (exitCode != 0)
            {
                Console.WriteLine("Failed to install browsers");
                Environment.Exit(exitCode);
            }
        }


        [TestMethod] 
        public async Task SimpleTest_demo01()
        {
            // Arange
            var homepageUrl = "https://localhost:7274/";

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false
            });

            // Act
            var page = await browser.NewPageAsync();
            await page.GotoAsync(homepageUrl);

            var element = page.GetByRole(AriaRole.Row)
                .Filter(new() { HasText = "John Egbert" });
            await element.GetByRole(AriaRole.Cell, new() { Name = "PURCHASE DETAILS" }).ClickAsync();

            element = page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" });
            await element.ClickAsync();

            element = page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" });
            await element.ClickAsync();
    
            await page.Locator("id=Name").FillAsync("Marcel de Vries");
            await page.Locator("id=Address").FillAsync("Kerkhofweg 12");
            await page.Locator("id=Town").FillAsync("Warnsveld");
            await page.Locator("id=PostalCode").FillAsync("7231RJ");
            await page.Locator("id=CreditCardDate").FillAsync("05/28");
            await page.Locator("id=Email").FillAsync("vriesmarcel@hotmail.com");
            await page.Locator("id=CreditCard").FillAsync("1111222233334444");

            var button = page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" });
            await button.ClickAsync();
            
            // Assert
            var header = page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" });
            Assert.IsTrue(await header.IsVisibleAsync());

        }

        [TestMethod]
        public async Task SimpleTest_demo02()
        {
            var homepageUrl = "https://localhost:7274/";

            var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false
            });
            // Act

            var webSite = new WebSite(browser, homepageUrl);
            await webSite.NavigateToHomepage();
            await webSite.AddItemToBasket("John Egbert");
            await webSite.CheckOut();
            await webSite.ConfirmPurchase();
            // Assert 
            Assert.IsTrue(await webSite.IsPurchaseConfirmed());



        }


        [TestMethod]
        public void SimpleTest_demo03()
        {
            var Given = new Given(new GloboticketDriver());
            var When = new When(new GloboticketDriver());
            var Then = new Then(new GloboticketDriver());

            Given.IHaveACleanDatabaseWithProducts()
                 .And()
                 .GloboticketWebsiteIsAvailable();

            When.IAddTheProductToTheShoppingCart("John Egbert")
                .And()
                .IAddTheProductToTheShoppingCart("John Egbert");

            Then.TheShoppingCartContainsNumberOfItems(2);
        }

        [TestMethod]
        public async Task SimpleTest()
        {
            var BuyticketResult = HomePage.GetHomePage(StartPage,false).Result
                .SelectTicket("John Egbert").Result
                .BuyTicket().Result
                .Checkout(new CustomerNico()).Result
                .IsOrderPlaced();
            Assert.IsTrue(await BuyticketResult);
        }
    }
}