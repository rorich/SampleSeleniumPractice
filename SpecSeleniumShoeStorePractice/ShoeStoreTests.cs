using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;
using SpecSeleniumShoeStorePractice.Pages;

namespace SpecSeleniumShoeStorePractice
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    [TestFixture(typeof(FirefoxDriver))]

    public class ShoeStoreTests<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver _driver;
        private const string URL = "http://shoestore-manheim.rhcloud.com/";

        [SetUp]
        public void CreateDriver()
        {
            this._driver = new TWebDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(URL);
        }

        [Test]
        public void CheckMonthlyShoes()
        {
            HomePage page = new HomePage(_driver);
            string selectedMonth;

            for (int index = 0; index < page.Months.Count; index++)
            {
                var month = page.Months[index];
                selectedMonth = month.Text;
                month.Click();
                WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                var isTitleMatchingMonth = wait.Until(ExpectedConditions.TitleContains(selectedMonth));
                Assert.True(isTitleMatchingMonth);
                MonthPage monthPage = new MonthPage(_driver);
               

                var shoeList = monthPage.ShoeResults;
                if (shoeList.Count > 0)
                {
                    foreach (var shoe in shoeList)
                    {
                        var releaseMonth = shoe.FindElement(By.ClassName("shoe_release_month")).Text;
                        Assert.True(selectedMonth == releaseMonth);

                        //verify that there is a description
                        var shoeDescription = shoe.FindElement(By.ClassName("shoe_description"));
                        Assert.True(shoeDescription.Displayed && shoeDescription.Text !="");

                        //verify image of shoe is present
                        var shoeImage = shoe.FindElement(By.ClassName("shoe_image"));
                        Assert.True(shoeImage.Displayed && shoeImage.GetAttribute("src")!="");

                        //check that there is a price
                        var shoePrice = shoe.FindElement(By.ClassName("shoe_price"));
                        Assert.True(shoePrice.Displayed && shoePrice.Text !="");
                    }
                }
            }
        }

        [Test]
        public void SubmitEmailForReminder()
        {
            string emailForSubscription = "testCustomer@email.com";

            HomePage page = new HomePage(_driver);
            page.EmailAddress.SendKeys(emailForSubscription);

            page.ClickSubmitButton();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement successMsgElement = wait.Until(ExpectedConditions.ElementExists(By.ClassName("notice")));
            string expectedMessage = $"Thanks! We will notify you of our new shoes at this email: {emailForSubscription}";
            Assert.True(successMsgElement.Text == expectedMessage);

        }

        [TearDown]
        public void TestFixtureTearnDown()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception randomException)
            {
                Console.WriteLine($"Failed to quit the driver {randomException.Message}");
            }
            finally
            {
                _driver.Dispose();
            }

        }
    }
}
