using DotLiquid;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Tests.Playwright.PageObjects;
using static System.Net.WebRequestMethods;

namespace Tests.Playwright
{
    [TestFixture] 
    public class SimpleTests : PageTest
    {
        public string StartPage = "https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/";

        [SetUp] 
        public async Task TestInitialize()
        {
            var homepage = System.Environment.GetEnvironmentVariable("homepage");
            if (!string.IsNullOrWhiteSpace(homepage))
                StartPage = homepage.Trim();

            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install" });
            if (exitCode != 0)
            {
                Console.WriteLine("Failed to install browsers");
                Environment.Exit(exitCode);
            }
            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown] 
        public async Task TestCleanup()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                    Environment.CurrentDirectory,
                    "playwright-traces",
                    $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
                )
            });
        }

        [Test] 
        public void SimpleTest()
        {
            var BuyticketResult = HomePage.GetHomePage(StartPage, false)
                .SelectTicket("John Egbert")
                .BuyTicket()
                .Checkout(new CustomerNico())
                .IsOrderPlaced();
            Assert.That(BuyticketResult);
        }
    }
}