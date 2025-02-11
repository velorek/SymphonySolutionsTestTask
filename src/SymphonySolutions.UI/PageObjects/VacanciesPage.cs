using OpenQA.Selenium;
using SymphonySolutions.UI.Helpers;
using SymphonySolutions.UI.PageObjects.PageElements.Vacancies;

namespace SymphonySolutions.UI.PageObjects
{
    public sealed class VacanciesPage : BasePage
    {
        //private readonly IDriverWraper driverWraper;
        private readonly By vacanciesListLocator = By.CssSelector("main article");
        private readonly List<VacancyArticle> vacanciesList;

        public VacanciesPage(IDriverWraper driverWraper) : base(driverWraper)
        {
            //this.driverWraper = driverWraper;

            var vacanciesBaseElements = driverWraper.FindElements(vacanciesListLocator);
            vacanciesList = new List<VacancyArticle>(vacanciesBaseElements.Count);
            foreach(var vacancyBaseElement in vacanciesBaseElements)
            {
                var vacancy = new VacancyArticle(driverWraper, vacancyBaseElement);
                vacanciesList.Add(vacancy);
            }
        }

        public int GetVacanciesCount => vacanciesList.Count;

        public VacancyArticle GetVacancyArticleByPosition(int position)
        {
            return vacanciesList[position];
        }
    }
}
