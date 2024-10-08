﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace End2EndTests.Solid
{
    internal class ShoppingBasket
    {
        private IWebDriver driver;

        internal ShoppingBasket(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void SetQuantity(string productName, int quantity)
        {
            var tableRows = driver.FindElements(By.XPath("/html/body/div/main/table/tbody/tr"));
            foreach (var row in tableRows)
            {
                if (row.Text.Contains(productName))
                {
                    var ticketAmount = row.FindElement(By.Name("TicketAmount"));
                    ticketAmount.FindElement(By.XPath($"//option[. = '{quantity}']")).Click();

                    var updateButton = row.FindElement(By.TagName("button"));
                    updateButton.Click();
                    break;
                }
            }
          
        }

        public void RemoveItem(string productName)
        {
            var tableRows = driver.FindElements(By.XPath("/html/body/div/main/table/tbody/tr"));
            foreach (var row in tableRows)
            {
                if (row.Text.Contains(productName))
                {
                    var removeButton = row.FindElement(By.ClassName("cancelIcon"));
                    removeButton.Click();
                    break;
                }
            }
        }

        public bool HasNumberOfItems(int quantity)
        {
            var value = driver.FindElement(By.Id("ticketAmount")).Text;
            return (string.Equals(value,quantity.ToString()));  
        }
        public void BackToEventCatalog()
        {
            driver.FindElement(By.LinkText("Back to event catalog")).Click();
        }

        public void GotoCheckout()
        {
            driver.FindElement(By.ClassName("btn-primary")).Click();
        }
    }
}
