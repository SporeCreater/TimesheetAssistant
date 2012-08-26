using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interactors.Tests
{
    [TestClass]
    public class LoginInteractorTests
    {
        private Mock<ILoginPage> _page;
        private LoginInteractor _interactor;

        private string _userName;
        private string _password;
        private bool _expectedLoginResult;
        private string _expectedErrorMessage;

        [TestInitialize]
        public void Setup()
        {
            _userName = null;
            _password = null;
            _expectedLoginResult = false;
            _expectedErrorMessage = null;

            _page = new Mock<ILoginPage>();
            _interactor = new LoginInteractor(_page.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            _page.Verify();
        }
        
        [TestMethod]
        public void handles_login_into_timesheet_system()
        {
            LoginRequest request = assume_login_credentials("user", "password").with_successful_login().get_request();

            LoginResponse response = _interactor.Login(request);

            response.WasSuccessful.Should().BeTrue();
            response.ErrorMessage.Should().BeEmpty();
        }

        [TestMethod]
        public void produces_error_message_on_login_failure()
        {
            LoginRequest request = assume_login_credentials("user", "password").with_failed_login().with_error_message("error message").get_request();

            LoginResponse response = _interactor.Login(request);

            response.WasSuccessful.Should().BeFalse();
            response.ErrorMessage.Should().Be("error message");
        }

        [TestMethod]
        public void on_login_selects_the_current_week()
        {
            LoginRequest request = assume_login_credentials("user", "password").with_successful_login().get_request();

            var today = new DateTime(2012, 1, 24);
            const string nextSaturday = "1/28/2012";
            const string actualSelectedDate = "1/21/2012";

            _page.Setup(p => p.SelectCurrentWeek(nextSaturday)).Verifiable();
            _page.Setup(p => p.CurrentWeek).Returns(actualSelectedDate).Verifiable();

            _interactor.Clock = get_clock_fixed_on_date(today);

            LoginResponse response = _interactor.Login(request);

            response.CurrentWeek.Should().Be(actualSelectedDate);
        }

        private IClock get_clock_fixed_on_date(DateTime today)
        {
            var clock = new Mock<IClock>();
            clock.Setup(c => c.Now()).Returns(today);
            return clock.Object;
        }

        private LoginRequest get_request()
        {
            var request = new LoginRequest { UserName = _userName, Password = _password };

            _page.Setup(p => p.Login(_userName, _password)).Returns(_expectedLoginResult).Verifiable();
            _page.Setup(p => p.LastErrorMessage).Returns(_expectedErrorMessage);

            return request;
        }

        private LoginInteractorTests assume_login_credentials(string userName, string password)
        {
            _userName = userName;
            _password = password;
            return this;
        }

        private LoginInteractorTests with_successful_login()
        {
            _expectedLoginResult = true;
            return this;
        }

        private LoginInteractorTests with_failed_login()
        {
            _expectedLoginResult = false;
            return this;
        }

        private LoginInteractorTests with_error_message(string errorMessage)
        {
            _expectedErrorMessage = errorMessage;
            return this;
        }
    }
}
