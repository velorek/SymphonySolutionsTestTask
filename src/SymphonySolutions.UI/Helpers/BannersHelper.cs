using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.PageObjects.PageElements;
using Cookie = OpenQA.Selenium.Cookie;

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

        public void UpdateLocalStorageAndCookies()
        {
            logger.Info("> Start updating Local Storage");
            driverWraper.ExecuteJavaScript("localStorage.clear();");
            Thread.Sleep(100);
            driverWraper.ExecuteJavaScript($"localStorage.setItem('promoPopupShown', 'true')"); // Promo banner
            logger.Info("> Local storage updated");

            Thread.Sleep(300);
            var driver = driverWraper.GetWebDriverInstance();

            logger.Info("> Start updaing cookies");
            var cookie = driver.Manage().Cookies.GetCookieNamed("cookieyes-consent");
            if (cookie is not null)
            {
                var updatedValue = UpdateConsentCookieValue(cookie.Value);

                driver.Manage().Cookies.DeleteAllCookies();
                Thread.Sleep(100);
                var newCookie = new Cookie(
                    name: cookie.Name,
                    value: updatedValue,
                    domain: cookie.Domain,
                    path: cookie.Path,
                    expiry: cookie.Expiry,
                    secure: cookie.Secure,
                    isHttpOnly: cookie.IsHttpOnly,
                    sameSite: cookie.SameSite);

                try
                {
                    driver.Manage().Cookies.AddCookie(newCookie);
                }
                catch(UnableToSetCookieException)
                {
                    logger.Warn("> Can't update cookies");
                }
                
                logger.Info("> Cookies were updated");
            }
            else
            {
                logger.Warn("> Can't get 'cookieyes-consent' cookie");
            }
            Thread.Sleep(2000);
        }

        private string UpdateConsentCookieValue(string cookieValue)
        {
            var keyValuePairs = cookieValue.Split(',').Select(pair => pair.Split(':')).ToDictionary(pair => pair[0], pair => pair[1]);
            keyValuePairs["action"] = "yes";
            return string.Join(",", keyValuePairs.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        }

        public void TryClosePrivacyPolicyBanner()
        {
            logger.Info("> Try close PrivacyPolicy Banner on UI");
            IWebElement? baseElement = TryGetBaseElementForBanner(privacyPolicyBaseElementLocator);

            if(baseElement is not null)
            {
                var baner = new PrivacyPolicyBanner(driverWraper, baseElement);

                baner.WaitUntilDisplayed();
                baner.RejectAllButttonClick();
                logger.Info("> PrivacyPolicy Banner was closed");
            }
            else
            {
                logger.Info("> PrivacyPolicy Banner was not displayed");
            }
        }

        public void TryClosePromoBanner()
        {
            logger.Info("> Try close Promo Banner ");
            IWebElement? baseElement = TryGetBaseElementForBanner(promoBaseElementLocator);
            if (baseElement is not null)
            {
                var baner = new PromoBanner(driverWraper, baseElement);

                baner.WaitUntilDisplayed();
                baner.ClickCloseButton();
                logger.Info("> Promo Banner was closed");
            }
            else
            {
                logger.Info("> Promo Banner was not displayed");
            }
        }

        private IWebElement? TryGetBaseElementForBanner(By locator)
        {
            IWebElement? baseElement = null;
            try
            {
                baseElement = driverWraper.FindElement(locator);
                if (baseElement.Displayed)
                {
                    return baseElement;
                }
            }
            catch (NoSuchElementException)
            {
                logger.Info(">> Base element for banner was not found");
            }
            catch (WebDriverException ex) when (ex is ElementNotVisibleException || ex is ElementNotInteractableException)
            {
                logger.Error($">> Base element for banner is not Interactable: {ex.GetType()}");
            }
            catch(WebDriverException ex)
            {
                logger.Error(">> Exception was throws while trying to gate base element for banner", ex);
            }

            return null;
        }
    }
}