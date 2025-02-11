using OpenQA.Selenium;
using SymphonySolutions.UI.Helpers.BrowserFactory.Managers;
using SymphonySolutions.Core.Configuration.Interfaces;

namespace SymphonySolutions.UI.Helpers.BrowserFactory
{
    public class DriverFactory
    {
        private readonly IBrowserSettings _settings;

        public DriverFactory(IBrowserSettings settings)
        {
            _settings = settings;
        }

        public IWebDriver CreateInstance()
        {
            var browserName = _settings.Browser.ToUpperInvariant();

            BrowsersEnum browser;
            if(!Enum.TryParse(browserName, out browser))
            {
                throw new ArgumentException($"Wrong browser name: {browserName}");
            }

            return browser switch
            {
                BrowsersEnum.CHROME => 
                    new ChromeDriverManager(_settings).CreateDriver(),
                BrowsersEnum.EDGE => 
                    new EdgeDriverManager(_settings).CreateDriver(),
                BrowsersEnum.FIREFOX => new FirefoxDriverManager(_settings).CreateDriver(),
                _ => throw new NotImplementedException($"Unable to create WebDriver for {browserName}")
            };
        }
    }
}
