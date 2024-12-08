using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class Reports : Form
    {
        public Reports()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Form1 mainForm = new Form1();
            mainForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string userId = PromptForUserId();
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Please provide a valid Meter ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            string reportType = PromptForReportType();

            if (string.IsNullOrEmpty(reportType))
            {
                MessageBox.Show("Please select a valid report type.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (IsUserIdValid(userId, reportType))
            {
                DisplayReportForm(userId, reportType);
            }
            else
            {
                MessageBox.Show("User ID not found for the selected report type. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PromptForUserId()
        {
            string userInput = string.Empty;

            // Prompt for User ID using a dialog form
            using (var inputForm = new Form())
            {
                Label lbl = new Label() { Text = "Enter Meter ID:", Left = 10, Top = 10 };
                TextBox textBox = new TextBox() { Left = 10, Top = 40, Width = 250 };
                Button btnOk = new Button() { Text = "OK", Left = 100, Top = 80, Width = 75 };
                btnOk.Click += (sender, e) =>
                {
                    userInput = textBox.Text;
                    inputForm.Close();
                };

                inputForm.Controls.Add(lbl);
                inputForm.Controls.Add(textBox);
                inputForm.Controls.Add(btnOk);
                inputForm.ClientSize = new System.Drawing.Size(280, 130);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.ShowDialog();
            }

            return userInput;
        }

        private string PromptForReportType()
        {
            string selectedReportType = string.Empty;

            // Prompt for report type (Daily, Weekly, or Monthly)
            using (var inputForm = new Form())
            {
                Label lbl = new Label() { Text = "Select Report Type:", Left = 10, Top = 10 };
                Button btnDaily = new Button() { Text = "Daily", Left = 10, Top = 40, Width = 75 };
                Button btnWeekly = new Button() { Text = "Weekly", Left = 100, Top = 40, Width = 75 };
                Button btnMonthly = new Button() { Text = "Monthly", Left = 190, Top = 40, Width = 75 };

                btnDaily.Click += (sender, e) =>
                {
                    selectedReportType = "Daily";
                    inputForm.Close();
                };
                btnWeekly.Click += (sender, e) =>
                {
                    selectedReportType = "Weekly";
                    inputForm.Close();
                };
                btnMonthly.Click += (sender, e) =>
                {
                    selectedReportType = "Monthly"; 
                    inputForm.Close();
                };

                inputForm.Controls.Add(lbl);
                inputForm.Controls.Add(btnDaily);
                inputForm.Controls.Add(btnWeekly);
                inputForm.Controls.Add(btnMonthly);
                inputForm.ClientSize = new System.Drawing.Size(280, 130);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.ShowDialog();
            }

            return selectedReportType;
        }

        private bool IsUserIdValid(string userId, string reportType)
        {
            bool isValid = false;
            string query = string.Empty;

            try
            {
                string databasePath = "Data Source=appliances.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(databasePath))
                {
                    connection.Open();

                   
                    if (reportType == "Daily")
                    {
                        query = "SELECT COUNT(*) FROM Reports WHERE UserId = @UserId";
                    }
                    else if (reportType == "Weekly")
                    {
                        query = "SELECT COUNT(*) FROM WeekReports WHERE UserId = @UserId";
                    }
                    else if (reportType == "Monthly")
                    {
                        
                        query = "SELECT COUNT(*) FROM MonthRepot WHERE UserId = @UserId";
                    }

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        long count = (long)command.ExecuteScalar();
                        if (count > 0)
                        {
                            isValid = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking User ID: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return isValid;
        }

        private void DisplayReportForm(string userId, string reportType)
        {
            if (reportType == "Daily")
            {
               
                ReportDetailForm dailyReportForm = new ReportDetailForm(userId);
                dailyReportForm.Show();
            }
            else if (reportType == "Weekly")
            {
               
                WeeklyReportForm weeklyReportForm = new WeeklyReportForm(userId);
                weeklyReportForm.Show();
            }
            else if (reportType == "Monthly")
            {
               
                MonthlyReportForm weeklyReportForm = new MonthlyReportForm(userId);
                weeklyReportForm.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserApplianceForm applianceForm = new UserApplianceForm();
            applianceForm.Show();
            this.Hide();
        }

        private void Reports_Load(object sender, EventArgs e)
        {

        }
    }
}
