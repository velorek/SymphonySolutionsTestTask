using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using SymphonySolutions.Core.Configuration.Interfaces;

namespace SymphonySolutions.UI.Helpers.BrowserFactory.Managers
{
    public class EdgeDriverManager : IDriverFactory
    {
        private readonly IBrowserSettings _settings;

        public EdgeDriverManager(IBrowserSettings settings)
        {
            _settings = settings;
        }

        public IWebDriver CreateDriver()
        {
            var options = new EdgeOptions();
            options.AddArgument("start-maximized");
            options.AddArgument("disable-extensions");
            options.AddExcludedArgument("disable-popup-blocking");
            if (_settings.IsHeadless)
                options.AddArgument("--headless");

            return new EdgeDriver(options);
        }
    }
}
