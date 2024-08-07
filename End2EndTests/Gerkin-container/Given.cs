
using SeleniumTests;

namespace Tests.Gerkin.Container
{
    internal class Given
    {
        private GloboticketDriver driver;
        string container;
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


        internal Given IHaveACleanDatabaseWithProducts()
        {
            var container = ContainerManager.StartDefaultDatabaseContainer("globoticket_database");
            return this;
        }

        internal Given IHaveACleanDatabaseWithStateOfTranceInCatalog()
        {
            var container = ContainerManager.StartStateOfTranceDatabaseContainer("globoticket_database");
            return this;
        }
    }
}
