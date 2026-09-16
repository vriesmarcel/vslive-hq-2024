using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using Tests.Playwright;

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
    public async Task MyTest()
    {
        await Page.GotoAsync("https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Row, new() { Name = "Artist pic 03/15/2027 John" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" }).ClickAsync();
        await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { "4" });
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).FillAsync("SAVE25");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
    
    }
    
    [Test]
    public async Task Checkout()
    {
        await Page.GotoAsync("https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Row, new() { Name = "Artist pic 03/15/2027 John" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" }).ClickAsync();
        await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { "4" });
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).FillAsync("SAVE25");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Promo Code:" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Apply" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).FillAsync("marcel de vries");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("vriesmarcel@hotmail.com");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Address" }).FillAsync("Kerkhofweg 12");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Address" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Town" }).FillAsync("Warnsveld");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Town" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Postal Code" }).FillAsync("7231RJ");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Postal Code" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Credit Card" }).FillAsync("1111222233334444");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Credit Card" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Expiry Date" }).FillAsync("12/12");
        await Page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" })).ToBeVisibleAsync();
    }

    [Test]
       public async Task AddRemoveItemsInCart()
    {
        await Page.GotoAsync("https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/");
        
        await Page.GetByRole(AriaRole.Row, new() { Name = "Artist pic 03/15/2027 John" }).GetByRole(AriaRole.Link).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" }).ClickAsync();
        await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { "2" });
        await Page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
        await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { "4" });
        await Page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
        await Page.Locator("#z0__Quantity").SelectOptionAsync(new[] { "1" });
        await Page.GetByRole(AriaRole.Button, new() { Name = "Update" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();

        await Page.GetByRole(AriaRole.Link, new() { Name = "CHECKOUT" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).FillAsync("marcel de vries");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).FillAsync("vriesmarcel@hotmail.com");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Address" }).FillAsync("Kerkhofweg 12");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Address" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Town" }).FillAsync("Warnsveld");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Town" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Postal Code" }).FillAsync("7231RJ");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Postal Code" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Credit Card" }).FillAsync("1111222233334444");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Credit Card" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Expiry Date" }).FillAsync("12/12");
        await Page.GetByRole(AriaRole.Button, new() { Name = "SUBMIT ORDER" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Thank you for your order!" })).ToBeVisibleAsync();

    }

}
