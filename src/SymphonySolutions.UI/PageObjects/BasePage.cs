using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers;
using SymphonySolutions.UI.PageObjects.PageElements;

namespace SymphonySolutions.UI.PageObjects
{
    public abstract class BasePage
    {
        private readonly Logger logger = new(nameof(BasePage));

        protected readonly IDriverWraper driverWraper;

        public PageHeader header;
        private IWebElement pageHeaderElement;

        public BasePage(IDriverWraper driverWraper)
        {
            this.driverWraper = driverWraper;

            pageHeaderElement = driverWraper.FindElement(By.Id("siteHeader"));
            header = new PageHeader(driverWraper, pageHeaderElement);
        }

        public string GetPageHeader1Text => driverWraper.FindElement(By.TagName("h1")).Text.Trim();

        public void NavigateTo(string url)
        {
            driverWraper.GetWebDriverInstance().Navigate().GoToUrl(url);
        }
    }
}
