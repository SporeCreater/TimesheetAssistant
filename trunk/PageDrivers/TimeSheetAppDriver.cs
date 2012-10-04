using System;
using System.Collections.Specialized;
using Boundaries;
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

        public bool Login(string userName, string password)
        {
            return PerformProtectedOperation(false, () =>
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
                
                LastErrorMessage = "Login failed";
                return false;
            });       
         }

        public string LastErrorMessage { get; private set; }

        public string CurrentWeek
        {
            get 
            {
                return PerformProtectedOperation("", () => 
                {
                    var p = new TimeCardPageDriver(_ie);

                    return p.WeekEndingList.SelectedValue;
                });
            }
        }

        public void SelectCurrentWeek(string nextSaturday)
        {
            PerformProtectedOperation(() =>
            {
                var p = new TimeCardPageDriver(_ie);

                StringCollection allWeekends = p.WeekEndingList.AllContents();

                if (allWeekends.Contains(nextSaturday))
                {
                    p.WeekEndingList.SelectByValue(nextSaturday);
                }
                else
                {
                    p.WeekEndingList.SelectByValue(allWeekends[allWeekends.Count - 1]);
                }               

            });
        }

        public StringCollection WeekEndings
        {
            get
            {
                return PerformProtectedOperation(new StringCollection(), () =>
                {
                    var p = new TimeCardPageDriver(_ie);

                    return p.WeekEndingList.AllContents();                                                                          
                });
            }
        }

        public void Close()
        {
            _ie.Close();
        }

        public void Dispose()
        {
            _ie.Close();
            _ie.Dispose();
        }

        private void PerformProtectedOperation(Action op)
        {
            PerformProtectedOperation("ignored", () =>
            {
                op();
                return "ignored";
            });
        }

        private T PerformProtectedOperation<T>(T errorReturnValue, Func<T> op)
        {
            LastErrorMessage = string.Empty;

            try
            {
                return op();
            }
            catch (ElementNotFoundException ex)
            {
                LastErrorMessage = "Unexpected page content: " + ex.Message;
                return errorReturnValue;
            }
        }
    }
}