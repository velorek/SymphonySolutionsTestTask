using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace SymphonySolutions.UI.Helpers
{
    public interface IDriverWraper
    {
        IWebDriver GetWebDriverInstance();

        void QuitWebDriver();

        void ExecuteJavaScript(string script);

        IWebElement FindElement(By by);

        ReadOnlyCollection<IWebElement> FindElements(By by);

        void WaitUntil(By by, Func<bool> condition, IEnumerable<Type> excludedExceptions);
        void WaitUntil(IWebElement element, Func<bool> condition, IEnumerable<Type> excludedExceptions);

        Screenshot TakeScreenshot();

        void HoverOnElement(IWebElement element);

        void HoverAndClickOnElement(IWebElement element);
    }
}
