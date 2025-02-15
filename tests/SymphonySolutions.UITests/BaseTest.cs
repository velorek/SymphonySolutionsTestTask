﻿using System.Diagnostics;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using SymphonySolutions.Core.Configuration;
using SymphonySolutions.Core.Helpers;
using SymphonySolutions.UI.Helpers;
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
            logger.Info("Setup starts");
            var driver = driverWraper.GetWebDriverInstance();

            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(settings.BaseHost);

            var bannerHelper = new BannersHelper(driverWraper);
            logger.Info("Start updating local storage and cookies");
            bannerHelper.UpdateLocalStorageAndCookies();
            logger.Info("local storage and cookies were updated");

            driver.Navigate().Refresh();

            logger.Info("Check and close banners on UI if they still displayed");
            bannerHelper.TryClosePrivacyPolicyBanner();
            bannerHelper.TryClosePromoBanner();
            logger.Info("Setup ends");
        }

        [TearDown]
        public void TearDown()
        {
            var driver = driverWraper.GetWebDriverInstance();
            driver.Manage().Cookies.DeleteAllCookies();

            var testResultsFolderPath = Path.Combine(DirectoryHelper.TryGetSolutionDirectory(), settings.TestResults);

            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            if (testStatus == TestStatus.Failed)
            {
                var screenshot = driverWraper.TakeScreenshot();
                AllureApi.AddAttachment("Screenshots", "image/jpeg", screenshot.AsByteArray, "jpeg");

                var imagePath = Path.Combine(
                    testResultsFolderPath,
                    $"Fail_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}_{TestDataHelper.GenerateRandomName()}_screenshot.png");
                screenshot.SaveAsFile(imagePath);
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
