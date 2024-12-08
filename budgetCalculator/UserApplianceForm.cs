using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class UserApplianceForm : Form
    {
        private string connectionString = @"Data Source=appliances.db;Version=3;";
        private string connectionString1 = @"Data Source=Region.db;Version=3;";

        public UserApplianceForm()
        {
            InitializeComponent();
        }

        private void UserApplianceForm_Load(object sender, EventArgs e)
        {
            LoadRegions();
            LoadAppliances();
        }

        private void LoadRegions()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString1))
            {
                connection.Open();
                string query = "SELECT RegionName FROM Region"; // Query to fetch all region names
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboRegion.Items.Add(reader["RegionName"].ToString());
                        }
                    }
                }
            }
        }

        private void LoadAppliances()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ApplianceName FROM Appliances"; // Query to fetch all appliance names
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Appliance.Items.Add(reader["ApplianceName"].ToString()); // Populate appliance ComboBox
                        }
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out string region, out string userId, out double budget))
            {
                return;
            }

            var appliancesData = GetAppliancesData();

            foreach (var applianceData in appliancesData)
            {
                if (applianceData.UsageHours > 24)
                {
                    MessageBox.Show($"Usage hours for appliance {applianceData.Appliance} cannot exceed 24 hours.");
                    return;
                }
            }

            if (appliancesData.Count == 0)
            {
                MessageBox.Show("Please select at least one appliance and provide its usage details.");
                return;
            }

            // Pass data to the Schedule form
            Schedule scheduleForm = new Schedule(appliancesData, budget, region, userId);
            scheduleForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reports reports = new Reports();
            reports.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out string region, out string userId, out double budget))
            {
                return;
            }

            var appliancesData = GetAppliancesData();
            double totalUsage = CalculateTotalUsage(appliancesData);

            if (totalUsage > 168) // Total hours in a week
            {
                MessageBox.Show("Total usage hours exceed the total hours in a week (168 hours).");
                return;
            }

            if (appliancesData.Count == 0)
            {
                MessageBox.Show("Please select at least one appliance and provide its usage details.");
                return;
            }

            Weekly form2 = new Weekly(appliancesData, budget, region, userId);
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out string region, out string userId, out double budget))
            {
                return;
            }

            var appliancesData = GetAppliancesData();
            double totalUsage = CalculateTotalUsage(appliancesData);

            if (totalUsage > 730) // Total hours in a month (approx.)
            {
                MessageBox.Show("Total usage hours exceed the total hours in a month (730 hours).");
                return;
            }

            if (appliancesData.Count == 0)
            {
                MessageBox.Show("Please select at least one appliance and provide its usage details.");
                return;
            }

            Monthly form3 = new Monthly(appliancesData, budget, region, userId);
            form3.Show();
        }

        private List<(string Appliance, int Units, double UsageHours)> GetAppliancesData()
        {
            var appliancesData = new List<(string Appliance, int Units, double UsageHours)>();

            foreach (DataGridViewRow row in dgvAppliances.Rows)
            {
                if (row.Cells["Appliance"].Value != null &&
                    row.Cells["UnitNumber"].Value != null &&
                    row.Cells["UsageHours"].Value != null)
                {
                    string appliance = row.Cells["Appliance"].Value.ToString();
                    int units = Convert.ToInt32(row.Cells["UnitNumber"].Value);
                    double usageHours = Convert.ToDouble(row.Cells["UsageHours"].Value);

                    appliancesData.Add((appliance, units, usageHours));
                }
            }

            return appliancesData;
        }

        private double CalculateTotalUsage(List<(string Appliance, int Units, double UsageHours)> appliancesData)
        {
            double totalUsage = 0;
            foreach (var appliance in appliancesData)
            {
                totalUsage += appliance.Units * appliance.UsageHours;
            }
            return totalUsage;
        }

        private bool ValidateInputs(out string region, out string userId, out double budget)
        {
            region = cboRegion.Text;
            userId = txtUserId.Text;
            budget = 0;

            if (string.IsNullOrWhiteSpace(region))
            {
                MessageBox.Show("Please select a valid region.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(userId) || userId.Length < 7 || !int.TryParse(userId, out _))
            {
                MessageBox.Show("Please enter a valid 7-digit Meter ID.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBudget.Text) || !double.TryParse(txtBudget.Text, out budget) || budget <= 0)
            {
                MessageBox.Show("Please enter a valid budget.");
                return false;
            }

            return true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addAppliance addappliance = new addAppliance();
            addappliance.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            UserApplianceForm  applianceForm = new UserApplianceForm();
            applianceForm.Show();
            this.Hide();
        }
    }
}
