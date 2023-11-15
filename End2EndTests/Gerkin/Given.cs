
namespace Tests.Playwright.Gerkin
{
    internal class Given
    {
        private GloboticketDriver driver;

        internal Given(GloboticketDriver diver)
        {
            this.driver = diver;
        }

        internal Given And()
        {
            return this;
        }

        internal Given GloboticketWebsiteIsAvailable()
        {
            driver.GotoHomepage();
            return this;
        }

        internal Given IHaveACleanUserDatabase()
        {
            return this;
        }

        internal Given IHaveACleanDatabaseWithProducts()
        {
            return this;
        }
    }
}
