using Boundaries;

namespace Interactors
{
    public interface ITimeCardView
    {
        void InitProgressBar(int noOfSteps);
        void AdvanceOneStep();
    }

    public class SubmitInteractor
    {
        private readonly ITimeCardView _view;
        private readonly ITimeCardPage _page;

        public SubmitInteractor(ITimeCardView view, ITimeCardPage page)
        {
            _view = view;
            _page = page;
        }

        public void PrepareForSubmit(SubmitRequest request)
        {
            _page.SelectCurrentWeek(request.CurrentWeek);

            _view.InitProgressBar(request.DaysOfWeek.Count);

            foreach (var dayOfweek in request.DaysOfWeek)
            {
                _page.EnterHoursForDay(request.Hours, dayOfweek, request.DayEntry);  
                _view.AdvanceOneStep();
            }
        }
    }
}