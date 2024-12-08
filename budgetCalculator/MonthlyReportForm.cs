using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class MonthlyReportForm : Form
    {
        private string userId;
        private int currentReportIndex = 0;
        private DataTable monthReportData;

        public MonthlyReportForm(string userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void MonthlyReportForm_Load(object sender, EventArgs e)
        {
            LoadReports();
            DisplayReport(currentReportIndex);
        }

        private void LoadReports()
        {
            string databasePath = "Data Source=appliances.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                try
                {
                    connection.Open();

                    // Get the reports for the given user
                    string query = "SELECT ReportId, TotalEnergy, TotalCost, RemainingBudget, ReportDate FROM MonthRepot WHERE UserId = @UserId";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                        {
                            monthReportData = new DataTable();
                            adapter.Fill(monthReportData);
                        }
                    }

                    // Check if data was loaded successfully
                    if (monthReportData.Rows.Count == 0)
                    {
                        MessageBox.Show("No monthly reports found for this user.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading reports: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DisplayReport(int reportIndex)
        {
            if (monthReportData == null || monthReportData.Rows.Count == 0)
            {
                MessageBox.Show("No reports available to display.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get the current report based on the current index
            DataRow currentReport = monthReportData.Rows[reportIndex];

            // Display the report details (Total Energy, Total Cost, Remaining Budget)
            labelReportDate.Text = $"Report Date: {currentReport["ReportDate"]}";
            labelTotalEnergy.Text = $"Total Energy: {currentReport["TotalEnergy"]} kWh";
            labelTotalCost.Text = $"Total Cost: {currentReport["TotalCost"]} RS";
            labelRemainingBudget.Text = $"Remaining Budget: {currentReport["RemainingBudget"]} RS";

            // Enable or disable navigation buttons based on the index
            buttonPrevious.Enabled = reportIndex > 0;
            buttonNext.Enabled = reportIndex < monthReportData.Rows.Count - 1;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentReportIndex < monthReportData.Rows.Count - 1)
            {
                currentReportIndex++;
                DisplayReport(currentReportIndex);
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            if (currentReportIndex > 0)
            {
                currentReportIndex--;
                DisplayReport(currentReportIndex);
            }
        }
    }
}
