using System;
using System.Collections.Generic;

namespace Boundaries
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public class LoginResponse
    {

        public LoginResponse()
        {
            ErrorMessage = string.Empty;
        }

        public bool WasSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string CurrentWeek { get; set; }
        public List<string> WeekDays { get; set; }
        public List<string> EarningCodes { get; set; }
        public List<string> ContractLines { get; set; }
        public List<string> ContractNumbers { get; set; }
        public List<string> ActivityIDs { get; set; }
        public List<string> ProjectIDs { get; set; }
    }

    public interface ILoginView
    {
        void ShowErrorMessage(string errorMessage);
        void SetCurrentWeek(LoginResponse response);
    }
}
