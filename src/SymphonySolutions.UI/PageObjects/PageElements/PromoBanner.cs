using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers;

namespace SymphonySolutions.UI.PageObjects.PageElements
{
    public class PromoBanner : BasePageElement
    {
        private readonly Logger logger = new(nameof(PrivacyPolicyBanner));
        private readonly By closeButtonLocator = By.CssSelector("a.close"); // #promoPopup 

        public PromoBanner(IDriverWraper driverWraper, IWebElement baseElement) : base(driverWraper, baseElement)
        {
        }

        public void ClickCloseButton()
        {
            logger.Info($"Click on close button");
            baseElement.FindElement(closeButtonLocator).Click();
        }

        public override void WaitUntilDisplayed()
        {
            driverWraper.WaitUntil(
                baseElement.FindElement(closeButtonLocator), () => {return baseElement.FindElement(closeButtonLocator).Enabled; }, []);
        }
    }
}
