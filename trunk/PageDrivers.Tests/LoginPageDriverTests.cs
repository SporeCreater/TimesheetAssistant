﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PageDrivers.Tests
{
    [TestClass]
    public class LoginPageDriverTests : WatinDriverTestSuite
    {
        [TestMethod]
        public void navigates_to_next_page_when_clicking_login()
        {
            var p = new LoginPageDriver(_ie);

            p.UserNameField.TypeText(Config.USER_NAME);
            p.PwdField.TypeText(Config.PASSWORD);
            p.LoginButton.Click();

            _ie.Url.Should().ContainEquivalentOf(LoginPageDriver.NextPageName);
        }
    }
}
