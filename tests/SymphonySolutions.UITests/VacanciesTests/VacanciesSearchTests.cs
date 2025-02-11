using Allure.NUnit.Attributes;
using SymphonySolutions.Core.Helpers;
using SymphonySolutions.UI.PageObjects;
using SymphonySolutionsUITests;

namespace SymphonySolutions.UITests.VacanciesTests
{
    [TestFixture]
    [AllureFeature("Vacancies search tests")]

    public class VacanciesSearchTests : BaseTest
    {
        [Test]
        [AllureName("Check vacancy has valid title")]
        [AllureTag("smoke")]
        public void VacancySearch_ShouldHasValidTitle()
        {
            var mainPage = new MainPage(driverWraper);
            mainPage.header
                .HoverOnMenuItemByName("Career")
                .HoverAndClickOnMenuItemByName("Vacancies");

            var vacanciesPage = new VacanciesPage(driverWraper);
            var vacanciesCount = vacanciesPage.GetVacanciesCount;
            var randomVacancyPosition = TestDataHelper.GenerateRandomInt(0, vacanciesCount - 1);
            var vacancyArticle = vacanciesPage.GetVacancyArticleByPosition(randomVacancyPosition);
            var expectedVacancyTitle = vacancyArticle.GetVacancyTitle();
            var vacancyPage = vacancyArticle.ClickOnCheckMoreButton();

            var actualVacancyTitle = vacancyPage.GetPageHeader1Text;

            Assert.That(actualVacancyTitle, Is.EqualTo(expectedVacancyTitle), 
                $"Wrong title for Vacancy #{randomVacancyPosition}");
        }
    }
}
