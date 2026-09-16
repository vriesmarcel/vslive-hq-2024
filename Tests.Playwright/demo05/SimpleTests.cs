using DotLiquid;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Runtime.InteropServices;
using Tests.Playwright.PageObjects;

namespace Tests.Playwright.services
{
    // Runs the same test against multiple cloud-hosted OS combinations concurrently.
    // Browser coverage (chromium/firefox/webkit) is set process-wide via .runsettings and
    // requires a separate `dotnet test --settings` invocation per browser run in parallel.
    [TestFixture("linux")]
    [TestFixture("windows")]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SimpleTests : ServicePageTest
    {
        public string StartPage = "https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/";

        private readonly string _os;

        public SimpleTests(string os)
        {
            _os = os;
            ServiceOs = os.Equals("windows", StringComparison.OrdinalIgnoreCase) ? OSPlatform.Windows : OSPlatform.Linux;
        }

        [SetUp]
        public async Task StartTracing()
        {
            await Context.Tracing.StartAsync(new()
            {
                Title = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        [TearDown]
        public async Task StopTracing()
        {
            var blobName = $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.{_os}.zip";
            var traceFile = Path.Combine(
                TestContext.CurrentContext.WorkDirectory,
                "playwright-traces",
                blobName);
            await Context.Tracing.StopAsync(new() { Path = traceFile });

            await TraceUploader.UploadAsync(
                TestContext.Parameters.Get("TraceStorageAccountName"),
                traceFile,
                blobName);
        }

        [Test] 
        public void SimpleTest()
        {
                    
            var homepage = System.Environment.GetEnvironmentVariable("homepage");
            if (!string.IsNullOrWhiteSpace(homepage))
                StartPage = homepage.Trim();

            var BuyticketResult = HomePage.GetHomePage(this, StartPage)
                .SelectTicket("John Egbert")
                .BuyTicket()
                .Checkout(new CustomerNico())
                .IsOrderPlaced();
            Assert.That(BuyticketResult);
        }
    }
}