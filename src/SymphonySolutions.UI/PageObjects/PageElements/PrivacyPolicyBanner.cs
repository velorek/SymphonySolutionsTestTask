using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers;

namespace SymphonySolutions.UI.PageObjects.PageElements
{
    public sealed class PrivacyPolicyBanner : BasePageElement
    {
        private readonly Logger logger = new(nameof(PrivacyPolicyBanner));
        private readonly By rejectButtonLocator = By.CssSelector(".cky-consent-bar button.cky-btn-reject");

        public PrivacyPolicyBanner(IDriverWraper driverWraper, IWebElement baseElement) : base(driverWraper, baseElement)
        {
        }

        public void RejectAllButttonClick()
        {
            logger.Info($"Click on Reject All button");
            baseElement.FindElement(rejectButtonLocator).Click();
        }

        public override void WaitUntilDisplayed()
        {
        }
    }
}
