using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class WeeklyReportForm : Form
    {
        private string userId;
        private int currentReportIndex = 0;
        private DataTable weekReportData;

        public WeeklyReportForm(string userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void WeeklyReportForm_Load(object sender, EventArgs e)
        {
            LoadReports();
            DisplayReport(currentReportIndex);
        }

        private void LoadReports()
        {
            string databasePath = "Data Source=appliances.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                // Get the reports for the given user
                string query = "SELECT * FROM WeekReports WHERE UserId = @UserId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        weekReportData = new DataTable();
                        adapter.Fill(weekReportData);
                    }
                }
            }
        }

        private void DisplayReport(int reportIndex)
        {
            if (weekReportData == null || weekReportData.Rows.Count == 0)
            {
                MessageBox.Show("No reports found for this user.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the current report based on the current index
            DataRow currentReport = weekReportData.Rows[reportIndex];

            // Show report data in the grid
            dataGridView1.DataSource = GetAppliancesForReport(Convert.ToInt32(currentReport["ReportId"]));

            // Display the report details (Region, Date, etc.)
            labelReportRegion.Text = currentReport["Region"].ToString();
            labelReportDate.Text = currentReport["ReportDate"].ToString();
            labelTotalEnergy.Text = currentReport["TotalEnergy"].ToString();
            labelTotalCost.Text = currentReport["TotalCost"].ToString();
            labelRemainingBudget.Text = currentReport["RemainingBudget"].ToString();
        }

        private DataTable GetAppliancesForReport(int reportId)
        {
            DataTable appliancesData = new DataTable();

            string databasePath = "Data Source=appliances.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(databasePath))
            {
                connection.Open();

                // Get the appliance details for the given report ID
                string query = "SELECT * FROM WeekReportAppliances WHERE ReportId = @ReportId";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReportId", reportId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(appliancesData);
                    }
                }
            }

            return appliancesData;
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (currentReportIndex < weekReportData.Rows.Count - 1)
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
