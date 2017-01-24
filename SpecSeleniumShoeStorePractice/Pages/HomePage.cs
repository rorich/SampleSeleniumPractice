using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace SpecSeleniumShoeStorePractice.Pages
{
    class HomePage
    {
        public HomePage(IWebDriver driver)
        {
           PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@id='header_nav']/nav/ul/li/a")]
        public IList<IWebElement> Months { get; set; }

        [FindsBy(How = How.Id, Using = "remind_email_input")]
        public IWebElement EmailAddress { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='remind_email_form']/div/input[@type ='submit']")]
        public IWebElement SubmitButton { get; set; }

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }
    }
}
