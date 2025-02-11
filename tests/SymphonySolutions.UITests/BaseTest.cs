using System.Diagnostics;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using SymphonySolutions.Core.Configuration;
using SymphonySolutions.UI.Helpers;
using SymphonySolutions.UI.PageObjects;
using SymphonySolutions.UI.PageObjects.PageElements;
using Logger = SymphonySolutions.Core.Logging.Logger;

namespace SymphonySolutionsUITests
{
    [AllureNUnit]
    [TestFixture]
    //[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
    [Parallelizable]
    public abstract class BaseTest
    {
        protected IDriverWraper driverWraper;
        private readonly Settings settings = ConfigurationProvider.LoadSettings();
        private Logger logger;

        [OneTimeSetUp]
        [AllureBefore("Test preparation")]
        public void OneTimeSetUp()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());

            Logger.InitLogger();
            logger = new(nameof(BaseTest));

            logger.Info("OneTimeSetUp started");
            driverWraper = new DriverWraper(settings);

            driverWraper.GetWebDriverInstance().Manage().Window.Maximize();
        }

        [SetUp]
        public void Setup() {
            var driver = driverWraper.GetWebDriverInstance();

            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(settings.BaseHost);


            var bannerHelper = new BannersHelper(driverWraper);
            bannerHelper.ClosePrivacyPolicyBanner();
            bannerHelper.ClosePromoBanner();

            /*driver.ClearLocalStorageData();
            driver.AddRecordToLocalStorage("promoPopupShown", "true");

            var consentCookie = driver.Manage().Cookies.GetCookieNamed("cookieyes-consent");
            if (consentCookie != null)
            {
                driver.Manage().Cookies.DeleteAllCookies();

                var updatedConsentCookieValue = UpdateConsentCookieValue(consentCookie.Value);
                driver.Manage().Cookies.AddCookie(
                    new Cookie(
                        name: consentCookie.Name,
                        value: updatedConsentCookieValue,
                        domain: consentCookie.Domain,
                        path: consentCookie.Path,
                        expiry: consentCookie.Expiry,
                        secure: consentCookie.Secure,
                        isHttpOnly: consentCookie.IsHttpOnly,
                        sameSite: consentCookie.SameSite));
            }

            Thread.Sleep(1000);
            driver.Navigate().Refresh();*/
        }

        private string UpdateConsentCookieValue(string cookie)
        {
            var keyValuePairs = cookie.Split(',').Select(pair => pair.Split(':')).ToDictionary(pair => pair[0], pair => pair[1]);
            keyValuePairs["action"] = "yes";
            return string.Join(",", keyValuePairs.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        }

        [TearDown]
        public void TearDown()
        {
            var driver = driverWraper.GetWebDriverInstance();
            driver.Manage().Cookies.DeleteAllCookies();

            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            if (testStatus == TestStatus.Failed)
            {
                var screenshot = driverWraper.TakeScreenshot();
                AllureApi.AddAttachment("Screenshots", "image/jpeg", screenshot.AsByteArray, "jpeg");
            }
        }

        [OneTimeTearDown]
        [AllureAfter("End tests")]
        public void OneTimeTearDown() 
        {
            var driver = driverWraper.GetWebDriverInstance();
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
