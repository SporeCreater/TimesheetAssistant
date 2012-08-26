using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PageDrivers.Tests
{
    [TestClass]
    public class PageDriverTests : WatinDriverTestSuite
    {
        [TestMethod]
        public void page_workflow_and_content_verification()
        {
            var loginPage = new LoginPageDriver(_ie);

            loginPage.Verify().Should().BeEmpty();

            loginPage.UserNameField.TypeText(Config.USER_NAME);
            loginPage.PwdField.TypeText(Config.PASSWORD);
            loginPage.LoginButton.Click();

            _ie.Url.Should().ContainEquivalentOf(LoginPageDriver.NextPageName);

            var timeCardPage = new TimeCardPageDriver(_ie);

            timeCardPage.Verify().Should().BeEmpty();
        }
    }
}
