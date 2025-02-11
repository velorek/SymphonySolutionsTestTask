using OpenQA.Selenium;

namespace SymphonySolutions.UI.Helpers.BrowserFactory
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver();
    }
}
