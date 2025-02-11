using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.PageObjects.PageElements;

namespace SymphonySolutions.UI.Helpers
{
    public sealed class BannersHelper
    {
        private readonly Logger logger = new(nameof(BannersHelper));
        private readonly IDriverWraper driverWraper;

        private readonly By privacyPolicyBaseElementLocator = By.CssSelector(".cky-consent-container");
        private readonly By promoBaseElementLocator = By.Id("promoPopup");

        public BannersHelper(IDriverWraper driverWraper)
        {
            this.driverWraper = driverWraper;
        }

        public void WaitUntilDisplayed()
        {
            logger.Info("Wait for privacy policy and promo banners");
            driverWraper.WaitUntil(
                privacyPolicyBaseElementLocator, 
                () => { return driverWraper.FindElement(privacyPolicyBaseElementLocator).Displayed; }, 
                [typeof(NotFoundException)]);

            driverWraper.WaitUntil(
                privacyPolicyBaseElementLocator,
                () => { return driverWraper.FindElement(promoBaseElementLocator).Displayed; },
                [typeof(NotFoundException)]);
            logger.Info("End wait");
        }

        public void ClosePrivacyPolicyBanner()
        {
            logger.Info("Close PrivacyPolicy Banner");
            var baseElement = driverWraper.FindElement(privacyPolicyBaseElementLocator);
            var baner = new PrivacyPolicyBanner(driverWraper, baseElement);

            baner.WaitUntilDisplayed();
            baner.RejectAllButttonClick();
            logger.Info("PrivacyPolicy Banner was closed");
        }

        public void ClosePromoBanner()
        {
            logger.Info("Close Promo Banner");
            var baseElement = driverWraper.FindElement(promoBaseElementLocator);
            var baner = new PromoBanner(driverWraper, baseElement);

            baner.WaitUntilDisplayed();
            baner.ClickCloseButton();
            logger.Info("Promo Banner was closed");
        }
    }
}
