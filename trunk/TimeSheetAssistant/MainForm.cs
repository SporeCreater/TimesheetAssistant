using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Interactors;
using PageDrivers;
using Form = System.Windows.Forms.Form;

namespace TimeSheetAssistant
{
    public partial class MainForm : Form
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
            var interactor = new LoginInteractor(_appDriver);

            var request = new LoginRequest
                              {
                                  UserName = tbUserName.Text,
                                  Password = tbPassword.Text
                              };

            var response = interactor.Login(request);

            if (response.WasSuccessful)
            {
                cbWeeks.DataSource = new List<string>() {response.CurrentWeek};
            }
            else
            {
                MessageBox.Show(response.ErrorMessage);
            }

        }

        private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            _appDriver.Close();
        }


        private void btnEnterHours_Click(object sender, EventArgs e)
        {
        }

        private void cbWeeks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
