using System;

namespace Interactors
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
    }

    public class LoginInteractor
    {
        private readonly ILoginPage _page;
        private IClock _clock = new SystemClock();

        public IClock Clock
        {
            get
            {
                return _clock;
            }
            set
            {
                _clock = value;
            }
        }

        public LoginInteractor(ILoginPage page)
        {
            _page = page;
        }

        public LoginResponse Login(LoginRequest request)
        {
            if (_page.Login(request.UserName, request.Password))
            {
                return BuildResponseForSuccessfulLogin();
            }

            return BuildResponseForFailedLogin();
        }

        private LoginResponse BuildResponseForFailedLogin()
        {
            return new LoginResponse { WasSuccessful = false, ErrorMessage = _page.LastErrorMessage };
        }

        private LoginResponse BuildResponseForSuccessfulLogin()
        {
            DateTime today = Clock.Now();
            string nextSaturday = today.CalculateNextSaturday().ToShortDateString();
            _page.SelectCurrentWeek(nextSaturday);
            return new LoginResponse {WasSuccessful = true, CurrentWeek = _page.CurrentWeek};
        }
    }
}