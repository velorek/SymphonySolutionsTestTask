using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SymphonySolutions.Core.Configuration.Interfaces;

namespace SymphonySolutions.UI.Helpers.BrowserFactory.Managers
{
    public class ChromeDriverManager : IDriverFactory
    {
        private readonly IBrowserSettings _settings;

        public ChromeDriverManager(IBrowserSettings settings)
        {
            _settings = settings;
        }

        public IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            options.AddArgument("--incognito");
            options.AddArgument("--disable-extensions");
            options.AddExcludedArgument("disable-popup-blocking");
            //options.AddArgument("disable-popup-blocking");
            //options.AddArgument("disable-infobars");
            //options.AddArgument("--no-sandbox");
            if (_settings.IsHeadless)
                options.AddArgument("--headless");

            return new ChromeDriver(options);
        }
    }
}
