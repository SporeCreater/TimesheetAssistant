using System;
using System.Linq;
using MvcContrib.TestHelper.WatiN;
using WatiN.Core;
using Form = System.Windows.Forms.Form;

namespace TimeSheetAssistant
{
    /// 
    /// This spike app was built to determine if Watin would work with a WinForms app
    /// and to prototype the basic functionality of the app.
    /// 
    public partial class MainForm : Form
    {
        private static IE _ie;
        private static WatinDriver _browser;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _ie = new IE { AutoClose = false };
            _browser = new WatinDriver(_ie, "https://timeandexpense.teksystems.com/webtime/");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _browser.Navigate("/");
            _ie.TextField(Find.ById("txtUserName")).TypeText(tbUserName.Text);
            _ie.TextField(Find.ById("txtPassword")).TypeText(tbPassword.Text);
            _ie.Button(Find.ById("lBtnLogin")).Click();

            SelectList list = _ie.SelectList(Find.ById("WeekEnding1"));
            cbWeeks.DataSource = list.AllContents;
            cbWeeks.SelectedItem = list.SelectedItem;
        }

        private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            _browser = null;
            _ie.Dispose();
            _ie = null;
        }


        private void btnEnterHours_Click(object sender, EventArgs e)
        {
            string hours = tbHours.Text;

            foreach (var checkedItem in cbDayOfTheWeek.CheckedItems)
            {
                EnterProjectBillableHours(hours, checkedItem.ToString(), cbContractLine.Text, cbContractNo.Text,
                                          cbActivityId.Text, cbProjectId.Text, cbEarningsCode.Text);
            }
        }

        private void EnterProjectBillableHours(string hours, string dayOfTheWeek, string contractLine, string contractNo, string activityId, string projectId, string earningsCode)
        {
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate")).Select(dayOfTheWeek);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode")).Select(earningsCode);
            _ie.TextField(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_txtHours")).TypeText(hours);

            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf1_ddlValue")).Select(contractLine);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf2_ddlValue")).Select(contractNo);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf3_ddlValue")).Select(activityId);
            _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf4_ddlValue")).Select(projectId);

            _ie.Link(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_lbtnSaveDetail")).Click();
        }

        private void cbWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ie.SelectList(Find.ById("WeekEnding1")).SelectByValue(cbWeeks.SelectedItem.ToString());

            var span = _ie.Spans.FirstOrDefault(s => s.Id == "repHourlyTimeCards_ctl00_uclHourlyTime_lblStatus");
            if (span == null) return;

            var list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlEarnCode"));
            cbEarningsCode.DataSource = list.AllContents;

            list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf1_ddlValue"));
            cbContractLine.DataSource = list.AllContents;

            list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf2_ddlValue"));
            cbContractNo.DataSource = list.AllContents;

            list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf3_ddlValue"));
            cbActivityId.DataSource = list.AllContents;

            list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_udfControl_udf4_ddlValue"));
            cbProjectId.DataSource = list.AllContents;

            list = _ie.SelectList(Find.ById("repHourlyTimeCards_ctl00_uclHourlyTime_ddlDate"));
            cbDayOfTheWeek.DataSource = list.AllContents.Cast<string>().Where(s => !s.Contains("Select")).ToList();

        }

    }
}
