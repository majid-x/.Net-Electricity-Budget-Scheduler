using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class EditRegion : Form
    {
        private string connectionString = "Data Source=Region.db;Version=3;";

        public EditRegion()
        {
            InitializeComponent();
            LoadRegions();
        }

        private void LoadRegions()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Region", connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvRegions.DataSource = table;
            }
        }

        private void dgvRegions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRegions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvRegions.SelectedRows[0];

                lblSelectedRegion.Text = $"Selected Region: {row.Cells["RegionName"].Value}";

                dtpPeakStart.Value = DateTime.Parse(row.Cells["PeakStart"].Value.ToString());
                dtpPeakEnd.Value = DateTime.Parse(row.Cells["PeakEnd"].Value.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvRegions.SelectedRows.Count == 0)
            {
                MessageBox.Show("No region selected.");
                return;
            }

            DataGridViewRow row = dgvRegions.SelectedRows[0];
            string regionName = row.Cells["RegionName"].Value.ToString();
            string peakStart = dtpPeakStart.Value.ToString("HH:mm");
            string peakEnd = dtpPeakEnd.Value.ToString("HH:mm");

            // Validation
            if (DateTime.Parse(peakStart) >= DateTime.Parse(peakEnd))
            {
                MessageBox.Show("Peak start time must be before peak end time.");
                return;
            }

            // Calculate off-peak hours
            string offPeakStart = DateTime.Parse(peakEnd).ToString("HH:mm");
            string offPeakEnd = DateTime.Parse(peakStart).ToString("HH:mm");

            // Update the region's times in the database
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"
                    UPDATE Region
                    SET PeakStart = @PeakStart, PeakEnd = @PeakEnd, OffPeakStart = @OffPeakStart, OffPeakEnd = @OffPeakEnd
                    WHERE RegionName = @RegionName";

                SQLiteCommand cmd = new SQLiteCommand(query, connection);
                cmd.Parameters.AddWithValue("@PeakStart", peakStart);
                cmd.Parameters.AddWithValue("@PeakEnd", peakEnd);
                cmd.Parameters.AddWithValue("@OffPeakStart", offPeakStart);
                cmd.Parameters.AddWithValue("@OffPeakEnd", offPeakEnd);
                cmd.Parameters.AddWithValue("@RegionName", regionName);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Region updated successfully.");
            LoadRegions(); // Reload the regions list to reflect changes
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRegions.SelectedRows.Count == 0)
            {
                MessageBox.Show("No region selected to delete.");
                return;
            }

            DataGridViewRow row = dgvRegions.SelectedRows[0];
            string regionName = row.Cells["RegionName"].Value.ToString();

            var confirmResult = MessageBox.Show(
                $"Are you sure you want to delete the region '{regionName}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Region WHERE RegionName = @RegionName";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@RegionName", regionName);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Region deleted successfully.");
                LoadRegions(); // Reload the regions list
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditRegion_Load(object sender, EventArgs e)
        {
            // Any additional initialization code
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }
    }
}
