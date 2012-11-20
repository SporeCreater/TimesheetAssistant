using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Boundaries;
using Interactors;
using PageDrivers;
using Form = System.Windows.Forms.Form;

namespace TimeSheetAssistant
{
    public partial class MainForm : Form, ILoginView, ITimeCardView
    {
        private TimeSheetAppDriver _appDriver;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _appDriver = new TimeSheetAppDriver("https://timeandexpense.teksystems.com/webtime/"); //TODO: send to app.config and create config class
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var interactor = new LoginInteractor(this, _appDriver);

            var request = new LoginRequest
                              {
                                  UserName = tbUserName.Text,
                                  Password = tbPassword.Text
                              };

             interactor.Login(request);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _appDriver.Close();
        }

        private void btnEnterHours_Click(object sender, EventArgs e)
        {
            var interactor = new SubmitInteractor(this, _appDriver);

            var selectedDays = new List<string>(cbDayOfTheWeek.CheckedItems.Cast<object>().Select(item => item.ToString()));

            var request = new SubmitRequest
                              {
                                  Hours = tbHours.Text,
                                  CurrentWeek = cbWeeks.Text,
                                  DaysOfWeek = selectedDays,
                                  DayEntry = new DayEntry
                                                 {
                                                     EarningCode = cbEarningsCode.Text,
                                                     ContractLine = cbContractLine.Text,
                                                     ContractNumber = cbContractNo.Text,
                                                     ActivityID = cbActivityId.Text,
                                                     ProjectID = cbProjectId.Text
                                                 },
                              };

            interactor.PrepareForSubmit(request);
        }

        private void cbWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        public void SetCurrentWeek(LoginResponse response)
        {
            cbWeeks.DataSource = new List<string> { response.TimeCard.CurrentWeek };
            cbDayOfTheWeek.DataSource = response.TimeCard.WeekDays;

            cbEarningsCode.DataSource = response.TimeCard.EarningCodes;
            cbContractLine.DataSource = response.TimeCard.ContractLines;
            cbContractNo.DataSource = response.TimeCard.ContractNumbers;
            cbActivityId.DataSource = response.TimeCard.ActivityIDs;
            cbProjectId.DataSource = response.TimeCard.ProjectIDs;
        }

        public void InitProgressBar(int noOfSteps)
        {
            progressBar.Minimum = 1;
            progressBar.Maximum = noOfSteps;
            progressBar.Value = 1;
            progressBar.Step = 1;
        }

        public void AdvanceOneStep()
        {
            progressBar.PerformStep();
        }
    }
}
