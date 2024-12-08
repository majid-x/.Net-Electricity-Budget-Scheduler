using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class createNewRegion : Form
    {
        private const string DbFilePath = "Region.db";

        public createNewRegion()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Region (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    RegionName TEXT NOT NULL,
                    PeakStart TEXT NOT NULL,
                    PeakEnd TEXT NOT NULL,
                    OffPeakStart TEXT NOT NULL,
                    OffPeakEnd TEXT NOT NULL
                )";

            using (var connection = new SQLiteConnection($"Data Source={DbFilePath};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string regionName = txtRegionName.Text;
            string peakStart = dtpPeakStart.Value.ToString("HH:mm");
            string peakEnd = dtpPeakEnd.Value.ToString("HH:mm");
            string offPeakStart = DateTime.Parse(peakEnd).AddMinutes(1).ToString("HH:mm");
            string offPeakEnd = DateTime.Parse(peakStart).AddMinutes(-1).ToString("HH:mm");

            // Validation
            if (string.IsNullOrWhiteSpace(regionName))
            {
                MessageBox.Show("Please enter a valid region name.");
                return;
            }

            if (DateTime.Parse(peakStart) >= DateTime.Parse(peakEnd))
            {
                MessageBox.Show("Peak hours start time must be earlier than end time.");
                return;
            }

            SaveRegionToDatabase(regionName, peakStart, peakEnd, offPeakStart, offPeakEnd);

            MessageBox.Show($"Region '{regionName}' saved with:\n" +
                $"Peak Hours: {peakStart} - {peakEnd}\n" +
                $"Off-Peak Hours: {offPeakStart} - {offPeakEnd}");
        }

        private void SaveRegionToDatabase(string regionName, string peakStart, string peakEnd, string offPeakStart, string offPeakEnd)
        {
            string insertQuery = @"
                INSERT INTO Region (RegionName, PeakStart, PeakEnd, OffPeakStart, OffPeakEnd)
                VALUES (@RegionName, @PeakStart, @PeakEnd, @OffPeakStart, @OffPeakEnd)";

            using (var connection = new SQLiteConnection($"Data Source={DbFilePath};Version=3;"))
            {
                connection.Open();
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@RegionName", regionName);
                    command.Parameters.AddWithValue("@PeakStart", peakStart);
                    command.Parameters.AddWithValue("@PeakEnd", peakEnd);
                    command.Parameters.AddWithValue("@OffPeakStart", offPeakStart);
                    command.Parameters.AddWithValue("@OffPeakEnd", offPeakEnd);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateSelectedTimes()
        {
            lblPeakHours.Text = $"Peak Hours: {dtpPeakStart.Value:HH:mm} - {dtpPeakEnd.Value:HH:mm}";
            lblOffPeakHours.Text = $"Off-Peak Hours: {DateTime.Parse(dtpPeakEnd.Value.ToString("HH:mm")).AddMinutes(1):HH:mm} - {DateTime.Parse(dtpPeakStart.Value.ToString("HH:mm")).AddMinutes(-1):HH:mm}";
        }

        private void dtp_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedTimes();
        }

        private void createNewRegion_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }
    }
}
