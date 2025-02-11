using OpenQA.Selenium;
using SymphonySolutions.UI.Helpers;

namespace SymphonySolutions.UI.PageObjects.PageElements
{
    public abstract class BasePageElement
    {
        protected readonly IDriverWraper driverWraper;
        protected IWebElement baseElement;

        public BasePageElement(IDriverWraper driverWraper, IWebElement baseElement)
        {
            this.driverWraper = driverWraper;
            this.baseElement = baseElement;
        }

        public virtual void WaitUntilDisplayed()
        {

        }
    }
}
