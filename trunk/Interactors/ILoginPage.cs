using System;

namespace Interactors
{
    public interface ILoginPage
    {
        bool Login(string userName, string password);
        string LastErrorMessage { get; }
        string CurrentWeek { get; }
        void SelectCurrentWeek(string nextSaturday);
    }
}