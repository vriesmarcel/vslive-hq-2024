using DotLiquid;
using DotLiquid.Util;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Tests.Playwright.PageObjects;

namespace Tests.Playwright.demo03
{
    [TestFixture] 
    public class SimpleTests : PageTest
    {
        public string StartPage = "https://globoticket-frontend-dpfbe7hxa6d2bdab.westeurope-01.azurewebsites.net/";


        [Test] 
        public void SimpleTest()
        {
            var homepage = System.Environment.GetEnvironmentVariable("homepage");
            if (!string.IsNullOrWhiteSpace(homepage))
                StartPage = homepage.Trim();

            var BuyticketResult = HomePage.GetHomePage(this, StartPage);
            BuyticketResult.SelectTicket("John Egbert");
            var ticketDetailPage = new TicketDetailPage(this);
            ticketDetailPage.BuyTicket();

            var shopingBasket = new ShopingBasket(this);
            shopingBasket.Checkout(new CustomerNico());

            var checkOutPage = new CheckOutPage(this);
            

            Assert.That(checkOutPage.IsOrderPlaced());
        }
    }
}