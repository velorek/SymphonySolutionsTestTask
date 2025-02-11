using OpenQA.Selenium;
using SymphonySolutions.Core.Logging;
using SymphonySolutions.UI.Helpers;

namespace SymphonySolutions.UI.PageObjects.PageElements.Vacancies
{
    public sealed class VacancyArticle : BasePageElement
    {
        private readonly Logger logger = new(nameof(VacancyArticle));

        private readonly By titleLocator = By.TagName("h3");
        private readonly By checkMoreButtonLocation = By.CssSelector(".item-vacancy--actions > a");

        public VacancyArticle(IDriverWraper driverWraper, IWebElement baseElement) : base(driverWraper, baseElement)
        {
        }

        public string GetVacancyTitle()
        {
            return baseElement.FindElement(titleLocator).Text;
        }

        public VacancyPage ClickOnCheckMoreButton()
        {
            baseElement.FindElement(checkMoreButtonLocation).Click();
            return new VacancyPage(driverWraper);
        }
    }
}
