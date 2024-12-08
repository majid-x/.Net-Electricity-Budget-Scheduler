using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection.Emit;

namespace budgetCalculator
{
    public partial class Schedule : Form
    {
        private readonly double userBudget;
        private readonly List<(string Appliance, int Units, double UsageHours)> appliancesData;
        private readonly List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType)> schedule;
        private readonly string region;
        private readonly string userId;
        private DateTime peakStartTime, peakEndTime, offPeakStartTime, offPeakEndTime;

        private string connectionString = @"Data Source=Region.db;Version=3;";
        private string applianceConnectionString = @"Data Source=appliances.db;Version=3;";

        public Schedule(List<(string Appliance, int Units, double UsageHours)> appliances, double budget, string region, string userId)
        {
            InitializeComponent();
            this.userBudget = budget;
            this.appliancesData = appliances;
            this.schedule = new List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType)>();
            this.region = region;
            this.userId = userId;

            lblReportDate.Text = $"Report Date: {DateTime.Now:dd MMM yyyy HH:mm}";

            
            CreateReportsTable();

            
            LoadRegionTimes();

            CheckPeakHours();
            GenerateSchedule();
        }
        private void CheckPeakHours()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PeakStart, PeakEnd FROM Region WHERE RegionName = @region";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@region", region);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Parse peak start and end times from the database
                            TimeSpan peakStart = DateTime.Parse(reader["PeakStart"].ToString()).TimeOfDay;
                            TimeSpan peakEnd = DateTime.Parse(reader["PeakEnd"].ToString()).TimeOfDay;

                            // Get the current time
                            TimeSpan currentTime = DateTime.Now.TimeOfDay;

                            // Check if the current time falls within peak hours
                            if (currentTime >= peakStart && currentTime < peakEnd)
                            {
                                label1.Visible = true;
                                label1.Text = "Currently within peak hours!";
                            }
                            else
                            {
                                label1.Visible = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No peak hour data found for region: {region}",
                                "Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }
        private void CreateReportsTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();
                string createReportsTableQuery = @"
            CREATE TABLE IF NOT EXISTS Reports (
                ReportId INTEGER PRIMARY KEY AUTOINCREMENT,
                UserId TEXT,
                Region TEXT,
                ReportDate TEXT,
                TotalEnergy REAL,
                TotalCost REAL,
                RemainingBudget REAL
            )";

                using (SQLiteCommand command = new SQLiteCommand(createReportsTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createReportAppliancesTableQuery = @"
            CREATE TABLE IF NOT EXISTS ReportAppliances (
                ApplianceName TEXT,
                StartTime TEXT,
                EndTime TEXT,
                Wattage INTEGER,
                Energy REAL,
                Cost REAL,
                TimeType TEXT,
                ReportId INTEGER,
                FOREIGN KEY (ReportId) REFERENCES Reports(ReportId)
            )";

                using (SQLiteCommand command = new SQLiteCommand(createReportAppliancesTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        private void LoadRegionTimes()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PeakStart, PeakEnd, OffPeakStart, OffPeakEnd FROM Region WHERE RegionName = @region";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@region", region);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            peakStartTime = DateTime.Parse(reader["PeakStart"].ToString());
                            peakEndTime = DateTime.Parse(reader["PeakEnd"].ToString());
                            offPeakStartTime = DateTime.Parse(reader["OffPeakStart"].ToString());
                            offPeakEndTime = DateTime.Parse(reader["OffPeakEnd"].ToString());
                        }
                    }
                }
            }
        }

        private void GenerateSchedule()
        {
            var applianceWattage = GetApplianceWattage();

            var random = new Random();
            double totalEnergy = 0;
            double totalCost = 0;
            var sortedAppliances = appliancesData
                .OrderBy(appliance => applianceWattage.ContainsKey(appliance.Appliance) ? applianceWattage[appliance.Appliance] : int.MaxValue)
                .ToList();
            foreach (var appliance in sortedAppliances)
            {
                string applianceName = appliance.Appliance;
                int units = appliance.Units;
                double usageHours = appliance.UsageHours;

                if (applianceWattage.ContainsKey(applianceName))
                {
                    int wattage = applianceWattage[applianceName];
                    double energy = (wattage * usageHours * units) / 1000;


                    double costPerUnit;
                    if (totalEnergy + energy <= 200)
                    {
                        costPerUnit = 63;
                    }
                    else
                    {

                        double remainingUnder200 = Math.Max(200 - totalEnergy, 0);
                        double over200 = energy - remainingUnder200;


                        costPerUnit = ((remainingUnder200 * 63) + (over200 * 94.5)) / energy;
                    }
                    totalEnergy += energy;
                    totalCost += costPerUnit;

                    int startHour = (wattage <= 500) ? peakStartTime.Hour : peakEndTime.Hour;

                    int maxHour = 24 - (int)Math.Ceiling(usageHours);
                    int randomHour = (startHour < maxHour) ? random.Next(startHour, maxHour) : startHour;
                    int randomMinute = random.Next(0, 60);
                    DateTime startDateTime = new DateTime(2024, 1, 1, randomHour, randomMinute, 0);
                    DateTime endDateTime = startDateTime.AddHours(usageHours);

                    string timeType = (startDateTime.Hour >= peakStartTime.Hour && startDateTime.Hour < peakEndTime.Hour) ? "Peak" : "Off-Peak";

                    schedule.Add((applianceName, units, startDateTime.ToString("hh:mm tt"), endDateTime.ToString("hh:mm tt"), wattage, energy, costPerUnit, timeType));
                }
            }

            DisplaySchedule(totalEnergy, totalCost);
        }

        private Dictionary<string, int> GetApplianceWattage()
        {
            var applianceWattage = new Dictionary<string, int>();
            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();
                string query = "SELECT ApplianceName, Wattage FROM Appliances";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            applianceWattage.Add(reader["ApplianceName"].ToString(), Convert.ToInt32(reader["Wattage"]));
                        }
                    }
                }
            }
            return applianceWattage;
        }

        private void DisplaySchedule(double totalEnergy, double totalCost)
        {
            dgvSchedule.Rows.Clear();

            foreach (var item in schedule)
            {
                dgvSchedule.Rows.Add(
                    $"{item.Appliance} (Unit {item.UnitNumber})",
                    item.StartTime,
                    item.EndTime,
                    item.Wattage,
                    item.Energy.ToString("F2"),
                    item.Cost.ToString("F2"),
                    item.TimeType
                );
            }

            lblTotalEnergy.Text = $"Total Energy (kWh): {totalEnergy:F2}";
            lblTotalCost.Text = $"Total Cost (RS): {totalCost:F2}";
            lblRemainingBudget.Text = $"Remaining Budget (RS): {(userBudget - totalCost):F2}";
        }

        private void CheckPeakHourWarning(DateTime peakStartTime, DateTime peakEndTime)
        {
            DateTime currentTime = DateTime.Now;
            TimeSpan currentTimeSpan = currentTime.TimeOfDay;

            TimeSpan peakStart = peakStartTime.TimeOfDay;
            TimeSpan peakEnd = peakEndTime.TimeOfDay;

            if (currentTimeSpan >= peakStart && currentTimeSpan < peakEnd)
            {
                MessageBox.Show("Warning: You are currently within peak hours. Energy usage might be higher!",
                                "Peak Hour Alert",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveReport();
            MessageBox.Show("Schedule has been submitted successfully!", "Submission Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveReport()
        {
            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();

                // Step 1: Insert the main report into the Reports table
                string insertReportQuery = @"
            INSERT INTO Reports (UserId, Region, ReportDate, TotalEnergy, TotalCost, RemainingBudget) 
            VALUES (@userId, @region, @reportDate, @totalEnergy, @totalCost, @remainingBudget)";

                using (SQLiteCommand command = new SQLiteCommand(insertReportQuery, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@region", region);
                    command.Parameters.AddWithValue("@reportDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@totalEnergy", schedule.Sum(item => item.Energy));
                    command.Parameters.AddWithValue("@totalCost", schedule.Sum(item => item.Cost));
                    command.Parameters.AddWithValue("@remainingBudget", userBudget - schedule.Sum(item => item.Cost));

                    command.ExecuteNonQuery();
                }

                // Step 2: Get the ReportId of the inserted report
                long reportId;
                using (SQLiteCommand command = new SQLiteCommand("SELECT last_insert_rowid()", connection))
                {
                    reportId = (long)command.ExecuteScalar();
                }

                // Step 3: Insert each appliance's data into the ReportAppliances table
                string insertApplianceQuery = @"
            INSERT INTO ReportAppliances (ApplianceName, StartTime, EndTime, Wattage, Energy, Cost, TimeType, ReportId)
            VALUES (@applianceName, @startTime, @endTime, @wattage, @energy, @cost, @timeType, @reportId)";

                foreach (var item in schedule)
                {
                    using (SQLiteCommand command = new SQLiteCommand(insertApplianceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@applianceName", item.Appliance);
                        command.Parameters.AddWithValue("@startTime", item.StartTime);
                        command.Parameters.AddWithValue("@endTime", item.EndTime);
                        command.Parameters.AddWithValue("@wattage", item.Wattage);
                        command.Parameters.AddWithValue("@energy", item.Energy);
                        command.Parameters.AddWithValue("@cost", item.Cost);
                        command.Parameters.AddWithValue("@timeType", item.TimeType);
                        command.Parameters.AddWithValue("@reportId", reportId);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }


        private void btnDownloadPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.Title = "Save Report as PDF";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    GeneratePdf(filePath);
                    MessageBox.Show("Report successfully saved as PDF.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GeneratePdf(string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    var pdfDoc = new Document();
                    var writer = PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();
                    pdfDoc.Add(new Paragraph($"Report Date: {DateTime.Now:dd MMM yyyy HH:mm}\n\n"));

                    var table = new PdfPTable(7);
                    table.AddCell("Appliance");
                    table.AddCell("Start Time");
                    table.AddCell("End Time");
                    table.AddCell("Wattage (W)");
                    table.AddCell("Energy (kWh)");
                    table.AddCell("Cost (RS)");
                    table.AddCell("Time Type");

                    foreach (var item in schedule)
                    {
                        table.AddCell(item.Appliance);
                        table.AddCell(item.StartTime);
                        table.AddCell(item.EndTime);
                        table.AddCell(item.Wattage.ToString());
                        table.AddCell(item.Energy.ToString("F2"));
                        table.AddCell(item.Cost.ToString("F2"));
                        table.AddCell(item.TimeType);
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save the report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
