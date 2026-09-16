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
        {     System.Environment.SetEnvironmentVariable("PLAYWRIGHT_SERVICE_URL", "wss://eastus.api.playwright.microsoft.com/playwrightworkspaces/d05a92cd-8766-4a5f-9bf5-b9da11128c05/browsers");
}
    }
}
