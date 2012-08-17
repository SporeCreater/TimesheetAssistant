using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper.WatiN;
using WatiN.Core;

namespace Spikes
{
    /// 
    /// These tests were written to learn how to use Watin to access the TimeSheet system web app.
    /// They are exploratory in nature, and they leave the browser open at the end so that additional
    /// manual verification can be done. 
    /// 
    /// They are not expected to be run as a suite, but individually. Some tests even require
    /// manual clean up to be done in the application in order to allow other tests to be run,
    /// or to run the tests again.
    /// 
    [Ignore]
    [TestClass]
    public class TimeSheetPageSpikes
    {
        // **** CHANGE THIS VALUES BEFORE RUNNING THE TESTS ***
        private const string USER_NAME = "fcuenca";
        private const string PASSWORD = "PASSWORD";  // Don't check-in your password!! :-)
        private const string CURRENT_WEEK_END = "8/18/2012";
        // ****************************************************

        private static IE _ie;
        private static WatinDriver _browser;

        [ClassInitialize]
        public static void SetupBrowser(TestContext testContext)
        {
            _ie = new IE { AutoClose = false };
            _browser = new WatinDriver(_ie, "https://timeandexpense.teksystems.com/webtime/");
        }

        [ClassCleanup]
        public static void CleanupBrowser()
        {
            _browser = null;
            _ie.Dispose();
            _ie = null;
        }

        [TestMethod]
        public void submit_timesheet_with_one_entry()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(CURRENT_WEEK_END);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate")).SelectByValue(CURRENT_WEEK_END);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode")).Select("Non billable- Regular Projects");
            _ie.TextField(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_txtHours")).TypeText("8");

            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf1_ddlValue")).SelectByValue("1");
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf2_ddlValue")).SelectByValue("CON000000003932");
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf3_ddlValue")).SelectByValue("ADMIN");
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf4_ddlValue")).SelectByValue("0000516167");

            _ie.Link(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_lbtnSaveDetail")).Click();
            _ie.Link(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_SubmitButton")).Click();
            _ie.Link(Find.ById("lBtnSubmit")).Click();
        }

        [TestMethod]
        public void unsubmit_timesheet()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(CURRENT_WEEK_END);
            _ie.Link(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_UnsubmitButton")).Click();            
        }

        [TestMethod]
        public void get_list_of_weeks()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            SelectList weekList = _ie.SelectList(Find.ById("WeekEnding1"));

            weekList.SelectByValue(CURRENT_WEEK_END);

            string currentWeek = weekList.SelectedItem;

            Assert.AreEqual(CURRENT_WEEK_END, currentWeek);            
        }

        [TestMethod]
        public void get_list_of_weekday_values()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(CURRENT_WEEK_END);

            SelectList dayList = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate"));

            Assert.AreEqual(CURRENT_WEEK_END, dayList.Options[7].Value);
        }

        [TestMethod]
        public void fill_up_this_weeks_timesheet()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(CURRENT_WEEK_END);

            var dayList = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate"));
            var days = dayList.AllContents.Cast<string>().Where(s => !s.Contains("Select")).ToList();

            foreach (string day in days)
            {
                EnterProjectBillableHours("8", day, "1", "CON000000003932", "ADMIN", "0000516167");                
            }
        }

        [TestMethod]
        public void find_option_by_text_value()
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(USER_NAME);
            _ie.TextField(Find.ById("txtPassword")).TypeText(PASSWORD);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(CURRENT_WEEK_END);

            var list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode"));
            var option = list.Option("Non billable- Regular Projects");

            Assert.AreEqual("ARX", option.Value);
        }


        private void EnterProjectBillableHours(string hours, string dayOfTheWeek, string contractLine, string contractNo, string activityId, string projectId)
        {
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate")).Select(dayOfTheWeek);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode")).Select("Non billable- Regular Projects");
            _ie.TextField(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_txtHours")).TypeText(hours);

            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf1_ddlValue")).SelectByValue(contractLine);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf2_ddlValue")).SelectByValue(contractNo);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf3_ddlValue")).SelectByValue(activityId);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf4_ddlValue")).SelectByValue(projectId);

            _ie.Link(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_lbtnSaveDetail")).Click();
        }
    }
}
