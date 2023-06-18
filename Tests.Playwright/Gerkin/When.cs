using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Playwright.PageObjects;

namespace Tests.Playwright.Gerkin
{

    internal class When
    {
        internal GloboticketDriver driver;

        internal When(GloboticketDriver driver)
        {
            this.driver = driver;
        }

        internal When IAddTheProductToTheShoppingCart(string productName)
        {
            driver.GotoHomepage();

            var element = driver.CurrentPage.GetByRole(AriaRole.Row)
                .Filter(new() { HasText = productName });
            element.GetByRole(AriaRole.Cell, new() { Name = "PURCHASE DETAILS" }).ClickAsync().RunSynchronously();

            element = driver.CurrentPage.GetByRole(AriaRole.Button, new() { Name = "PLACE ORDER" });
            element.ClickAsync().RunSynchronously();

            return this;
        }

        internal When And() { return this; }
    }
}
