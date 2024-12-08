using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class ReportDetailForm : Form
    {
        private string userId; 
        private DataTable appliancesTable;
        private DataTable reportsTable;
        private int currentReportIndex = 0;

        public ReportDetailForm(string userId)
        {
            this.userId = userId;
            InitializeComponent();
        }

        private void ReportDetailForm_Load(object sender, EventArgs e)
        {
            LoadReports();
            if (reportsTable.Rows.Count > 0)
            {
                LoadReportDetails(currentReportIndex);
            }
            else
            {
                MessageBox.Show($"No reports found for User ID: {userId}.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void LoadReports()
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=appliances.db;Version=3;"))
            {
                connection.Open();

                
                string query = "SELECT * FROM Reports WHERE UserId = @UserId";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@UserId", userId);

                reportsTable = new DataTable();
                adapter.Fill(reportsTable);
            }
        }

        private void LoadReportDetails(int reportIndex)
        {
            if (reportIndex < 0 || reportIndex >= reportsTable.Rows.Count)
                return;

            
            int reportId = Convert.ToInt32(reportsTable.Rows[reportIndex]["ReportId"]);
            string reportDate = reportsTable.Rows[reportIndex]["ReportDate"].ToString();
            double totalEnergy = Convert.ToDouble(reportsTable.Rows[reportIndex]["TotalEnergy"]);
            double totalCost = Convert.ToDouble(reportsTable.Rows[reportIndex]["TotalCost"]);
            double remainingBudget = Convert.ToDouble(reportsTable.Rows[reportIndex]["RemainingBudget"]);

            
            lblReportDate.Text = $"Report Date: {reportDate}";
            lblTotalEnergy.Text = $"Total Energy (kWh): {totalEnergy}";
            lblTotalCost.Text = $"Total Cost (RS): {totalCost}";
            lblRemainingBudget.Text = $"Remaining Budget (RS): {remainingBudget}";

            
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=appliances.db;Version=3;"))
            {
                connection.Open();
                string query = "SELECT ApplianceName AS 'Appliance', StartTime AS 'Start Time', EndTime AS 'End Time', Wattage, Energy, Cost, TimeType " +
                               "FROM ReportAppliances WHERE ReportId = @ReportId";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@ReportId", reportId);

                appliancesTable = new DataTable();
                adapter.Fill(appliancesTable);
            }

            
            dataGridView1.DataSource = appliancesTable;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentReportIndex < reportsTable.Rows.Count - 1)
            {
                currentReportIndex++;
                LoadReportDetails(currentReportIndex);
            }
            else
            {
                MessageBox.Show("No more reports available.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (currentReportIndex > 0)
            {
                currentReportIndex--;
                LoadReportDetails(currentReportIndex);
            }
            else
            {
                MessageBox.Show("This is the first report.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
