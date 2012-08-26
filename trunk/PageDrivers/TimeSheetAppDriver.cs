using System;
using Interactors;
using WatiN.Core;
using WatiN.Core.Exceptions;

namespace PageDrivers
{
    public class TimeSheetAppDriver : ILoginPage, IDisposable
    {
        private readonly IE _ie;

        public TimeSheetAppDriver(string appUrl)
        {
            _ie = new IE(appUrl);
            LastErrorMessage = string.Empty;
        }

        public void Close()
        {
            _ie.Close();
        }

        public bool Login(string userName, string password)
        {
            LastErrorMessage = string.Empty;

            try
            {
                var p = new LoginPageDriver(_ie);

                string pageVerificationResult = p.Verify();
                if (pageVerificationResult != string.Empty)
                {
                    LastErrorMessage = "Page doesn't contain expected controls: " + pageVerificationResult;
                    return false;
                }

                p.UserNameField.TypeText(userName);
                p.PwdField.TypeText(password);
                p.LoginButton.Click();

                if (_ie.Url.Contains(LoginPageDriver.NextPageName))
                {
                    return true;
                }
                else
                {
                    LastErrorMessage = "Login failed";
                    return false;
                }

            }
            catch (ElementNotFoundException ex)
            {
                LastErrorMessage = "Unexpected page content: " + ex.Message;
                return false;
            }
        }

        public string LastErrorMessage { get; private set; }

        public string CurrentWeek
        {
            get { throw new NotImplementedException(); }
        }

        public void SelectCurrentWeek(string nextSaturday)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _ie.Close();
            _ie.Dispose();
        }
    }
}