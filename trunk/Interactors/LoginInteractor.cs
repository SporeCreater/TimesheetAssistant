using System;
using System.Collections.Generic;
using Boundaries;

namespace Interactors
{
    public class LoginInteractor
    {
        private readonly ILoginView _view;
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

        public LoginInteractor(ILoginView view, ILoginPage page)
        {
            _view = view;
            _page = page;
        }

        public void Login(LoginRequest request)
        {
            LoginResponse response = ExecuteLogin(request);

            if (response.WasSuccessful)
            {
                _view.SetCurrentWeek(response);
            }
            else
            {
                _view.ShowErrorMessage(response.ErrorMessage);
            }            
        }

        private LoginResponse ExecuteLogin(LoginRequest request)
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
            return new LoginResponse
                       {
                           WasSuccessful = true, 
                           CurrentWeek = _page.CurrentWeek, 
                           WeekDays = _page.WeekDays,
                           EarningCodes =  _page.EarningCodes,
                           ContractLines = _page.ContractLines,
                           ContractNumbers = _page.ContractNumbers,
                           ActivityIDs = _page.ActivityIDs,
                           ProjectIDs = _page.ProjectIDs
                       };
        }
    }
}