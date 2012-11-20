namespace Boundaries
{
    public interface ITimeCardPage
    {
        void EnterHoursForDay(string hours, string dayOfWeek, DayEntry entry);
        void SelectCurrentWeek(string currentWeek);
    }
}