using System.Collections.Generic;
using Boundaries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Interactors.Tests
{
    [TestClass]
    public class SubmitInteractorTests
    {
        private Mock<ITimeCardPage> _page;
        private Mock<ITimeCardView> _view;

        private SubmitInteractor _interactor;

        [TestInitialize]
        public void Setup()
        {
            _page = new Mock<ITimeCardPage>();
            _view = new Mock<ITimeCardView>();

            _interactor = new SubmitInteractor(_view.Object, _page.Object);
        }

        [TestMethod]
        public void prepares_time_card_for_submision()
        {                     
            var request = new SubmitRequest
                              {
                                  Hours = "8",
                                  CurrentWeek = "11/24/2012",
                                  DaysOfWeek = new List<string> {"Monday", "Friday"},
                                  DayEntry = new DayEntry
                                                 {
                                                     EarningCode = "Billable Hrs",
                                                     ContractLine = "1",
                                                     ContractNumber = "CON001234 OVERHEAD",
                                                     ActivityID = "ADMIN",
                                                     ProjectID = "0012345 Overhead"
                                                 },
                              };

            _interactor.PrepareForSubmit(request);

            _page.Verify(p => p.SelectCurrentWeek("11/24/2012"));
            _page.Verify(p => p.EnterHoursForDay("8", "Monday", request.DayEntry));
            _page.Verify(p => p.EnterHoursForDay("8", "Friday", request.DayEntry));
        }

        [TestMethod]
        public void notifies_view_about_progress()
        {
            var request = new SubmitRequest
            {
                DaysOfWeek = new List<string> { "Monday", "Tuesday", "Wednesday", "Friday" },
                DayEntry = new DayEntry ()
            };

            _interactor.PrepareForSubmit(request);

            _view.Verify(v => v.InitProgressBar(4));
            _view.Verify(v => v.AdvanceOneStep(), Times.Exactly(4));
        }

    }    
}
