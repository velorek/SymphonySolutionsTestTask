using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SymphonySolutions.Core.Configuration.Interfaces;

namespace SymphonySolutions.UI.Helpers.BrowserFactory.Managers
{
    public class FirefoxDriverManager : IDriverFactory
    {
        private readonly IBrowserSettings _settings;

        public FirefoxDriverManager(IBrowserSettings settings)
        {
            _settings = settings;
        }

        public IWebDriver CreateDriver()
        {
            var options = new FirefoxOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-extensions");
            options.AddArgument("disable-popup-blocking");
            if (_settings.IsHeadless)
                options.AddArgument("--headless");

            return new FirefoxDriver(options);
        }
    }
}
