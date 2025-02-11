using System.Collections.ObjectModel;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SymphonySolutions.Core.Configuration.Interfaces;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers.BrowserFactory;

namespace SymphonySolutions.UI.Helpers
{
    public sealed class DriverWraper : IDriverWraper
    {
        private readonly Logger logger = new(nameof(DriverWraper));

        private readonly IWebDriver driver;
        private readonly DefaultWait<IWebDriver> wait;
        private readonly IWebDriverSettings driverSettings;

        public DriverWraper(IWebDriverSettings settings)
        {
            driverSettings = settings;

            var driverFactory = new DriverFactory(settings);
            driver = driverFactory.CreateInstance();

            wait = new(driver)
            {
                Timeout =
                    TimeSpan.FromSeconds(
                        settings.DefaultTimeoutSeconds),
                PollingInterval =
                    TimeSpan.FromMilliseconds(
                        settings.DefaultPollingIntervalMs),
            };
        }

        public IWebElement FindElement(By by)
        {
            IWebElement element = null;
            var retryCount = 0;
            var maxRetries = driverSettings.MaxRetriesCount;
            var delay = driverSettings.DefaultPollingIntervalMs;

            while (retryCount < maxRetries)
            {
                try
                {
                    element = driver.FindElement(by);
                    if (element != null)
                    {
                        return element;
                    }
                }
                catch (NoSuchElementException)
                {
                    // Element not found, retry after delay
                    Thread.Sleep(delay);
                    delay *= 2;
                }

                retryCount++;
            }

            throw new NoSuchElementException($"Element not found after {maxRetries} retries.");
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return driver.FindElements(by);
        }

        public void ExecuteJsScript(string script)
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); 

            logger.Info($"> start JS script execution '{script}'");
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(script);

            stopwatch.Stop();
            logger.Info($"> JS script execution finished within {stopwatch.Elapsed:mm\\:ss\\.ff}");
        }

        public IWebDriver GetWebDriverInstance() => driver;

        public void QuitWebDriver()
        {
            logger.Info("Try to stop WebDriver instance");
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
            logger.Info("WebDriver stoped");
        }

        public void WaitUntil(By by, Func<bool> condition, IEnumerable<Type> excludedExceptions)
        {

            WebDriverWait w = new(driver, TimeSpan.FromSeconds(30));
            w.IgnoreExceptionTypes(excludedExceptions.ToArray());

            w.Until((d) => { var element = driver.FindElement(by); return condition(); });
        }

        public void WaitUntil(IWebElement element, Func<bool> condition, IEnumerable<Type> excludedExceptions)
        {
            WebDriverWait w = new(driver, TimeSpan.FromSeconds(30));
            w.IgnoreExceptionTypes(excludedExceptions.ToArray());

            w.Until<bool>((d) => { return condition.Invoke(); });
        }

        public Screenshot TakeScreenshot()
        {
            logger.Info("Take a screenshot");
            return ((ITakesScreenshot)driver).GetScreenshot();
        }

        public void HoverOnElement(IWebElement element)
        {
            logger.Info($"Perform hover on element");
            new Actions(driver).MoveToElement(element).Perform();
        }

        public void HoverAndClickOnElement(IWebElement element)
        {
            logger.Info($"Perform hover and click on element");
            new Actions(driver).MoveToElement(element).Click().Perform();
        }
    }
}
