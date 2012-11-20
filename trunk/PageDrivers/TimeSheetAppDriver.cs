using System;
using System.Collections.Specialized;
using System.Linq;
using Boundaries;
using WatiN.Core;
using WatiN.Core.Exceptions;

namespace PageDrivers
{
    public class TimeSheetAppDriver : ILoginPage, ITimeCardPage, IDisposable
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

        public TimeCard GetTimeCard()
        {
            return PerformProtectedOperation(new TimeCard(), () =>
                    {
                        var p = new TimeCardPageDriver(_ie);

                        return new TimeCard
                                   {
                                       CurrentWeek = p.WeekEndingList.SelectedValue,
                                       WeekDays = p.WeekDateList.AllContents().Cast<string>().Where(s => !s.Contains("Select")).ToList(),
                                       EarningCodes = p.EarningCodes.AllContents().Cast<string>().ToList(),
                                       ContractLines = p.ContractLines.AllContents().Cast<string>().ToList(),
                                       ContractNumbers = p.ContractNumbers.AllContents().Cast<string>().ToList(),
                                       ActivityIDs = p.ActivityIDs.AllContents().Cast<string>().ToList(),
                                       ProjectIDs = p.ProjectIDs.AllContents().Cast<string>().ToList()
                                    };
                    });
        }

        public void EnterHoursForDay(string hours, string dayOfWeek, DayEntry entry)
        {
            var p = new TimeCardPageDriver(_ie);

            p.Hours.TypeText(hours);
            p.WeekDateList.Select(dayOfWeek);
            p.EarningCodes.Select(entry.EarningCode);
            p.ContractLines.Select(entry.ContractLine);
            p.ContractNumbers.Select(entry.ContractNumber);
            p.ActivityIDs.Select(entry.ActivityID);
            p.ProjectIDs.Select(entry.ProjectID);
            p.SaveDetailsButton.Click();
        }

        public void SelectCurrentWeek(string nextSaturday)
        {
            PerformProtectedOperation(() =>
            {
                var p = new TimeCardPageDriver(_ie);

                StringCollection allWeekends = p.WeekEndingList.AllContents();

                p.WeekEndingList.SelectByValue(
                    allWeekends.Contains(nextSaturday) ? nextSaturday : allWeekends[allWeekends.Count - 1]);
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