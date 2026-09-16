using Azure.Developer.Playwright;
using Azure.Identity;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Runtime.InteropServices;

namespace Tests.Playwright
{
    // PageTest that connects to cloud-hosted browsers via Azure Playwright Testing.
    public class ServicePageTest : PageTest
    {
        // Overridable per test-fixture so multiple OS combinations can run concurrently in one process.
        public OSPlatform ServiceOs { get; set; } = OSPlatform.Linux;

        public override async Task<(string, BrowserTypeConnectOptions?)?> ConnectOptionsAsync()
        {
            var client = new PlaywrightServiceBrowserClient(credential: new DefaultAzureCredential());
            var connectOptions = await client.GetConnectOptionsAsync<BrowserTypeConnectOptions>(os: ServiceOs);
            return (connectOptions.WsEndpoint, connectOptions.Options);
        }
    }
}
