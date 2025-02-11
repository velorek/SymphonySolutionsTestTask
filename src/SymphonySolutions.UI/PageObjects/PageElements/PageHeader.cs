using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers;

namespace SymphonySolutions.UI.PageObjects.PageElements
{
    public sealed class PageHeader : BasePageElement
    {
        private readonly Logger logger = new(nameof(PageHeader));

        public PageHeader(IDriverWraper driverWraper, IWebElement baseElement) : base(driverWraper, baseElement) 
        {
        }

        public PageHeader HoverOnMenuItemByName(string menuItemName)
        {
            logger.Info($"Try to hover on Main Menu Item {menuItemName}");
            var menuItem = GetMenuItemElementByName(menuItemName);
            driverWraper.HoverOnElement(menuItem);

            return this;
        }

        public void HoverAndClickOnMenuItemByName(string menuItemName)
        {
            logger.Info($"Try to hover and click on Main Menu Item {menuItemName}");
            var menuItem = GetMenuItemElementByName(menuItemName);
            driverWraper.HoverAndClickOnElement(menuItem);
        }

        private IWebElement GetMenuItemElementByName(string menuItemName)
        {
            var menuItem = 
                baseElement.FindElement(By.XPath($"//a[contains(text(),'{menuItemName}')]"));

            return menuItem;
        }
    }
}
