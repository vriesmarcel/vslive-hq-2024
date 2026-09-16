using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Tests.Playwright.DryTest
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class RecordedDryTests : PageTest
    {
        private const string StartUrl = "https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/";
        private const string ConcertRowName = "Artist pic 03/15/2027 John";

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

   [SetUp]
    public async Task Setup()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }
        [TearDown]
        public async Task TearDown()
        {
            await Context.Tracing.StopAsync();
        }
        [Test]
        public async Task MyTest()
        {
            await GoToHomePageAsync();
            await SelectConcertAndPlaceOrderAsync();
            await SetQuantityAsync("4");
            await ApplyPromoCodeAsync("SAVE25");
            await GoToCheckoutAsync();
        }

        [Test]
        public async Task Checkout()
        {
            await GoToHomePageAsync();
            await SelectConcertAndPlaceOrderAsync();
            await SetQuantityAsync("4");
            await ApplyPromoCodeAsync("SAVE25");
            await GoToCheckoutAsync();
            await FillCustomerDetailsAsync();
            await SubmitOrderAsync();
            await AssertOrderPlacedAsync();
        }

        [Test]
        public async Task AddRemoveItemsInCart()
        {
            await GoToHomePageAsync();
            await SelectConcertAndPlaceOrderAsync();
            await UpdateQuantityAsync("2");
            await UpdateQuantityAsync("4");
            await UpdateQuantityAsync("1");
            await GoToCheckoutAsync();
            await GoToCheckoutAsync();
            await FillCustomerDetailsAsync();
            await SubmitOrderAsync();
            await AssertOrderPlacedAsync();
        }

        private async Task GoToHomePageAsync()
        {
            await Page.GotoAsync(StartUrl);
        }

        private async Task SelectConcertAndPlaceOrderAsync()
        {
            await Page.GetByRole(AriaRole.Row, new() { Name = ConcertRowName }).GetByRole(AriaRole.Link).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" }).ClickAsync();
        }

        private async Task SetQuantityAsync(string quantity)
        {
            await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { quantity });
        }

        private async Task UpdateQuantityAsync(string quantity)
        {
            await SetQuantityAsync(quantity);
            await Page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
        }

        private async Task ApplyPromoCodeAsync(string promoCode)
        {
            var promoTextbox = Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" });
            await promoTextbox.ClickAsync();
            await promoTextbox.FillAsync(promoCode);
            await promoTextbox.PressAsync("Tab");
            await Page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
        }

        private async Task GoToCheckoutAsync()
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
        }

        private async Task FillCustomerDetailsAsync()
        {
            var name = Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
            await name.ClickAsync();
            await name.FillAsync("marcel de vries");
            await name.PressAsync("Tab");

            var email = Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
            await email.FillAsync("vriesmarcel@hotmail.com");
            await email.PressAsync("Tab");

            var address = Page.GetByRole(AriaRole.Textbox, new() { Name = "Address" });
            await address.FillAsync("Kerkhofweg 12");
            await address.PressAsync("Tab");

            var town = Page.GetByRole(AriaRole.Textbox, new() { Name = "Town" });
            await town.FillAsync("Warnsveld");
            await town.PressAsync("Tab");

            var postalCode = Page.GetByRole(AriaRole.Textbox, new() { Name = "Postal Code" });
            await postalCode.FillAsync("7231RJ");
            await postalCode.PressAsync("Tab");

            var creditCard = Page.GetByRole(AriaRole.Textbox, new() { Name = "Credit Card" });
            await creditCard.FillAsync("1111222233334444");
            await creditCard.PressAsync("Tab");

            await Page.GetByRole(AriaRole.Textbox, new() { Name = "Expiry Date" }).FillAsync("12/12");
        }

        private async Task SubmitOrderAsync()
        {
            await Page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" }).ClickAsync();
        }

        private async Task AssertOrderPlacedAsync()
        {
            await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" })).ToBeVisibleAsync();
        }
    }
}
