using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PageDrivers.Tests
{
    [TestClass]
    public class AppDriverTests
    {
        [TestMethod]
        public void performs_login_into_the_application()
        {
            using (var p = new TimeSheetAppDriver(Config.APP_URL))
            {
                p.Login(Config.USER_NAME, Config.PASSWORD).Should().BeTrue();
                p.LastErrorMessage.Should().BeEmpty();
            }
        }

        [TestMethod]
        public void fails_gracefully_when_cant_connect_to_web_app()
        {
            using(var p = new TimeSheetAppDriver(Config.APP_URL + "foo"))
            {
                p.Login(Config.USER_NAME, Config.PASSWORD).Should().BeFalse();
                p.LastErrorMessage.Should().Contain("Page doesn't contain expected controls:");
            }
        }

        [TestMethod]
        public void fails_gracefully_when_authentication_fails()
        {
            using(var p = new TimeSheetAppDriver(Config.APP_URL))
            {
                p.Login("wrong user name", Config.PASSWORD).Should().BeFalse();
                p.LastErrorMessage.Should().Contain("Login failed");
            }
        }

        [TestMethod]
        public void last_error_is_empty_by_default()
        {
            using (var p = new TimeSheetAppDriver(Config.APP_URL))
            {
                p.LastErrorMessage.Should().BeEmpty();
            }
        }

        [TestMethod]
        public void selects_existing_value_in_the_weekend_list()
        {
            using (var p = new TimeSheetAppDriver(Config.APP_URL))
            {
                p.Login(Config.USER_NAME, Config.PASSWORD);

                p.SelectCurrentWeek("9/15/2012");

                p.GetTimeCard().CurrentWeek.Should().Be("9/15/2012");
            }
        }

        [TestMethod]
        public void selects_non_existent_value_in_weekend_list()
        {
            using (var p = new TimeSheetAppDriver(Config.APP_URL))
            {
                p.Login(Config.USER_NAME, Config.PASSWORD);

                p.SelectCurrentWeek("9/99/9999");

                p.GetTimeCard().CurrentWeek.Should().Be(p.WeekEndings[p.WeekEndings.Count - 1]); 
            }
        }
    }
}