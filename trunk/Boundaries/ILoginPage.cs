namespace Boundaries
{
    public interface ILoginPage
    {       
        string LastErrorMessage { get; }

        TimeCard GetTimeCard();

        bool Login(string userName, string password);
        void SelectCurrentWeek(string nextSaturday);
    }
}