using System;
using Boundaries;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interactors.Tests
{
    [TestClass]
    public class LoginInteractorTests
    {
        private Mock<ILoginPage> _page;
        private Mock<ILoginView> _view;
        private LoginInteractor _interactor;

        [TestInitialize]
        public void Setup()
        {
            _page = new Mock<ILoginPage>();
            _view = new Mock<ILoginView>();

            _interactor = new LoginInteractor(_view.Object, _page.Object);
        }

        [TestCleanup]
        public void TearDown()
        {
            _page.Verify();
            _view.Verify();
        }
        
        [TestMethod]
        public void interacts_with_webapp_to_handle_login()
        {
            var request = new LoginRequest { UserName = "user", Password = "password" };

            _page.Setup(p => p.Login(request.UserName, request.Password)).Returns(true).Verifiable();

            _interactor.Login(request);
        }

        [TestMethod]
        public void when_login_fails_forwards_error_message_to_view()
        {
            _page.Setup(p => p.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            _page.Setup(p => p.LastErrorMessage).Returns("error message");

            _view.Setup(v => v.ShowErrorMessage("error message")).Verifiable();

            _interactor.Login(new LoginRequest());
        }

        [TestMethod]
        public void when_login_succeeds_forwards_current_week_to_view()
        {
            _page.Setup(p => p.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _page.Setup(p => p.LastErrorMessage).Returns("");

            var today = new DateTime(2012, 1, 24);
            const string nextSaturday = "1/28/2012";
            const string actualSelectedDate = "1/21/2012";

            _page.Setup(p => p.SelectCurrentWeek(nextSaturday)).Verifiable();
            _page.Setup(p => p.CurrentWeek).Returns(actualSelectedDate).Verifiable();

            _view.Setup(v => v.SetCurrentWeek(actualSelectedDate)).Verifiable();

            _interactor.Clock = get_clock_fixed_on_date(today);

            _interactor.Login(new LoginRequest());
        }

        private IClock get_clock_fixed_on_date(DateTime today)
        {
            var clock = new Mock<IClock>();
            clock.Setup(c => c.Now()).Returns(today);
            return clock.Object;
        }
    }
}
