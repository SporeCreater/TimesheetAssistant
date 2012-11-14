using System.Collections.Generic;

namespace Boundaries
{
    public interface ILoginPage
    {       
        string LastErrorMessage { get; }
        string CurrentWeek { get; }
        List<string> WeekDays { get; }
        List<string> EarningCodes { get; }
        List<string> ContractLines { get; }
        List<string> ActivityIDs { get; }
        List<string> ProjectIDs { get; }
        List<string> ContractNumbers { get; }

        bool Login(string userName, string password);
        void SelectCurrentWeek(string nextSaturday);
    }
}