using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace BudgetCalculator
{
    public partial class DeleteApp : Form
    {
        private string connectionString = "Data Source=Appliances.db;Version=3;";

        public DeleteApp()
        {
            InitializeComponent();
            LoadAppliances();
        }

        // Method to load appliances from the database and display them in the DataGridView
        private void LoadAppliances()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Appliances", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvAppliances.DataSource = table;
            }
        }

        // Method to handle selection change in DataGridView and populate the appliance name and wattage
        private void dgvAppliances_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppliances.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvAppliances.SelectedRows[0];
                lblSelectedAppliance.Text = $"Selected Appliance: {row.Cells["ApplianceName"].Value}";
                lblWattage.Text = $"Wattage: {row.Cells["Wattage"].Value}";
            }
        }

        // Button click event to delete the selected appliance
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppliances.SelectedRows.Count == 0)
            {
                MessageBox.Show("No appliance selected.");
                return;
            }

            DataGridViewRow row = dgvAppliances.SelectedRows[0];
            int applianceId = Convert.ToInt32(row.Cells["Id"].Value);
            string applianceName = row.Cells["ApplianceName"].Value.ToString();

            DialogResult dialogResult = MessageBox.Show($"Are you sure you want to delete the appliance '{applianceName}'?", "Delete Appliance", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Delete appliance from the database
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Appliances WHERE Id = @Id";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Id", applianceId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Appliance '{applianceName}' deleted successfully.");
                LoadAppliances(); // Reload the appliances list to reflect the changes
            }
        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
