using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecSeleniumShoeStorePractice.Pages
{
    class MonthPage
    {
        public MonthPage(IWebDriver driver)
        {
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.ClassName, Using = "shoe_result")]
        public IList<IWebElement> ShoeResults { get; set; }
    }
}
