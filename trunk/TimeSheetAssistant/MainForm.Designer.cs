namespace TimeSheetAssistant
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbWeeks = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbContractLine = new System.Windows.Forms.ComboBox();
            this.cbContractNo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbActivityId = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbProjectId = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.btnEnterHours = new System.Windows.Forms.Button();
            this.cbEarningsCode = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbDayOfTheWeek = new System.Windows.Forms.CheckedListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Week";
            // 
            // cbWeeks
            // 
            this.cbWeeks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWeeks.FormattingEnabled = true;
            this.cbWeeks.Location = new System.Drawing.Point(124, 121);
            this.cbWeeks.Name = "cbWeeks";
            this.cbWeeks.Size = new System.Drawing.Size(153, 24);
            this.cbWeeks.TabIndex = 2;
            this.cbWeeks.SelectedIndexChanged += new System.EventHandler(this.cbWeeks_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "User Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Password";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(92, 20);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(153, 22);
            this.tbUserName.TabIndex = 0;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(92, 48);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(153, 22);
            this.tbPassword.TabIndex = 1;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(251, 26);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 36);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbUserName);
            this.panel1.Location = new System.Drawing.Point(15, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 91);
            this.panel1.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 577);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Daily Hours";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Contract Line";
            // 
            // cbContractLine
            // 
            this.cbContractLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContractLine.FormattingEnabled = true;
            this.cbContractLine.Location = new System.Drawing.Point(124, 220);
            this.cbContractLine.Name = "cbContractLine";
            this.cbContractLine.Size = new System.Drawing.Size(242, 24);
            this.cbContractLine.TabIndex = 6;
            // 
            // cbContractNo
            // 
            this.cbContractNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbContractNo.FormattingEnabled = true;
            this.cbContractNo.Location = new System.Drawing.Point(124, 250);
            this.cbContractNo.Name = "cbContractNo";
            this.cbContractNo.Size = new System.Drawing.Size(242, 24);
            this.cbContractNo.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Contract No.";
            // 
            // cbActivityId
            // 
            this.cbActivityId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbActivityId.FormattingEnabled = true;
            this.cbActivityId.Location = new System.Drawing.Point(124, 280);
            this.cbActivityId.Name = "cbActivityId";
            this.cbActivityId.Size = new System.Drawing.Size(242, 24);
            this.cbActivityId.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 283);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Activity ID";
            // 
            // cbProjectId
            // 
            this.cbProjectId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProjectId.FormattingEnabled = true;
            this.cbProjectId.Location = new System.Drawing.Point(124, 310);
            this.cbProjectId.Name = "cbProjectId";
            this.cbProjectId.Size = new System.Drawing.Size(242, 24);
            this.cbProjectId.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 313);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 17);
            this.label8.TabIndex = 11;
            this.label8.Text = "Project ID";
            // 
            // tbHours
            // 
            this.tbHours.Location = new System.Drawing.Point(124, 577);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(65, 22);
            this.tbHours.TabIndex = 14;
            // 
            // btnEnterHours
            // 
            this.btnEnterHours.Location = new System.Drawing.Point(242, 568);
            this.btnEnterHours.Name = "btnEnterHours";
            this.btnEnterHours.Size = new System.Drawing.Size(124, 31);
            this.btnEnterHours.TabIndex = 15;
            this.btnEnterHours.Text = "Enter Hours";
            this.btnEnterHours.UseVisualStyleBackColor = true;
            this.btnEnterHours.Click += new System.EventHandler(this.btnEnterHours_Click);
            // 
            // cbEarningsCode
            // 
            this.cbEarningsCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEarningsCode.FormattingEnabled = true;
            this.cbEarningsCode.Location = new System.Drawing.Point(124, 190);
            this.cbEarningsCode.Name = "cbEarningsCode";
            this.cbEarningsCode.Size = new System.Drawing.Size(242, 24);
            this.cbEarningsCode.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Earnings Code";
            // 
            // cbDayOfTheWeek
            // 
            this.cbDayOfTheWeek.FormattingEnabled = true;
            this.cbDayOfTheWeek.Location = new System.Drawing.Point(15, 353);
            this.cbDayOfTheWeek.Name = "cbDayOfTheWeek";
            this.cbDayOfTheWeek.Size = new System.Drawing.Size(351, 191);
            this.cbDayOfTheWeek.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 606);
            this.Controls.Add(this.cbDayOfTheWeek);
            this.Controls.Add(this.cbEarningsCode);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnEnterHours);
            this.Controls.Add(this.tbHours);
            this.Controls.Add(this.cbProjectId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbActivityId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbContractNo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbContractLine);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbWeeks);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "TimeSheet Assistant";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbWeeks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbContractLine;
        private System.Windows.Forms.ComboBox cbContractNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbActivityId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbProjectId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Button btnEnterHours;
        private System.Windows.Forms.ComboBox cbEarningsCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckedListBox cbDayOfTheWeek;
    }
}

