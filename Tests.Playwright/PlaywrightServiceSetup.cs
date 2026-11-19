using Azure.Developer.Playwright.NUnit;
using Azure.Identity;
using NUnit.Framework;

namespace Tests.Playwright
{
    // One-time setup that authenticates against the Azure Playwright Testing workspace.
    [SetUpFixture]
    public class PlaywrightServiceNUnitSetup : PlaywrightServiceBrowserNUnit
    {
        public PlaywrightServiceNUnitSetup() : base(
            credential: new DefaultAzureCredential()
        )
        {     System.Environment.SetEnvironmentVariable("PLAYWRIGHT_SERVICE_URL", "wss://eastus.api.playwright.microsoft.com/playwrightworkspaces/54a85971-932c-4240-b460-56ab8ba1e730/browsers");
}

        // Publishes {runId}/index.html so the Playwright Workspaces portal can show the run's report.
        [OneTimeTearDown]
        public async Task PublishRunReport()
        {
            await RunReport.PublishAsync(TestContext.Parameters.Get("TraceStorageAccountName"));
        }
    }
}
