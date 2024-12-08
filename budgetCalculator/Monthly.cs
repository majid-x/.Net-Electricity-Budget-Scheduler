using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace budgetCalculator
{
    public partial class Monthly : Form
    {
        private readonly double userBudget;
        private readonly List<(string Appliance, int Units, double UsageHours)> appliancesData;
        private readonly List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType, string DayOfWeek)> schedule;
        private readonly string region;
        private readonly string userId;
        private DateTime peakStartTime, peakEndTime;

        private string connectionString = @"Data Source=Region.db;Version=3;";
        private string applianceConnectionString = @"Data Source=appliances.db;Version=3;";

        // Universal variables
        private double totalEnergy = 0;
        private double totalCost = 0;

        public Monthly(List<(string Appliance, int Units, double UsageHours)> appliances, double budget, string region, string userId)
        {
            InitializeComponent();
            this.userBudget = budget;
            this.appliancesData = appliances;
            this.schedule = new List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType, string DayOfWeek)>();
            this.region = region;
            this.userId = userId;

            lblReportDate.Text = $"Report Date: {DateTime.Now:dd MMM yyyy HH:mm}";

            CreateReportsTable();  // Ensure table is created on form load
            LoadRegionTimes();
            CheckPeakHours();
            GenerateSchedule();
        }
       


        private void CreateReportsTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();
                string createReportsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS MonthRepot (
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
            }
        }

        private void LoadRegionTimes()
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
                            peakStartTime = DateTime.Parse(reader["PeakStart"].ToString());
                            peakEndTime = DateTime.Parse(reader["PeakEnd"].ToString());
                        }
                    }
                }
            }
        }

        private void GenerateSchedule()
        {
            var applianceWattage = GetApplianceWattage();
            var random = new Random();

            totalEnergy = 0;
            totalCost = 0;

            schedule.Clear();
            var sortedAppliances = appliancesData
                .OrderBy(appliance => applianceWattage.ContainsKey(appliance.Appliance) ? applianceWattage[appliance.Appliance] : int.MaxValue)
                .ToList();
            foreach (var appliance in sortedAppliances)
            {
                string applianceName = appliance.Appliance;
                int units = appliance.Units;
                double usageHours = appliance.UsageHours / 30.0;

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
                    totalEnergy += energy * 30; // Multiply by 30 days
                    totalCost += costPerUnit * 30;     // Multiply by 30 days

                    for (int day = 1; day <= 30; day++)
                    {
                        int startHour = (wattage <= 500) ? peakStartTime.Hour : peakEndTime.Hour;

                        int maxHour = 24 - (int)Math.Ceiling(usageHours);
                        int randomHour = (startHour < maxHour) ? random.Next(startHour, maxHour) : startHour;
                        int randomMinute = random.Next(0, 60);
                        DateTime startDateTime = new DateTime(2024, 1, 1, randomHour, randomMinute, 0);
                        DateTime endDateTime = startDateTime.AddHours(usageHours);
                        string timeType = (startDateTime.Hour >= peakStartTime.Hour && startDateTime.Hour < peakEndTime.Hour) ? "Peak" : "Off-Peak";

                        schedule.Add((applianceName, units, startDateTime.ToString("hh:mm tt"), endDateTime.ToString("hh:mm tt"), wattage, energy, costPerUnit, timeType, $"Day {day}"));
                    }
                }
            }

            DisplaySchedule();
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

        private void DisplaySchedule()
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
                    item.TimeType,
                    item.DayOfWeek
                );
            }

            lblTotalEnergy.Text = $"Total Energy (kWh): {totalEnergy:F2}";
            lblTotalCost.Text = $"Total Cost (RS): {totalCost:F2}";
            lblRemainingBudget.Text = $"Remaining Budget (RS): {(userBudget - totalCost):F2}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveScheduleToDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            this.Hide();
        }

        private void SaveScheduleToDatabase()
        {
            int reportId = 0;

            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();

                string insertReportQuery = @"
                    INSERT INTO MonthRepot (UserId, Region, ReportDate, TotalEnergy, TotalCost, RemainingBudget)
                    VALUES (@UserId, @Region, @ReportDate, @TotalEnergy, @TotalCost, @RemainingBudget)";
                using (SQLiteCommand command = new SQLiteCommand(insertReportQuery, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@Region", region);
                    command.Parameters.AddWithValue("@ReportDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@TotalEnergy", totalEnergy);
                    command.Parameters.AddWithValue("@TotalCost", totalCost);
                    command.Parameters.AddWithValue("@RemainingBudget", userBudget - totalCost);
                    command.ExecuteNonQuery();
                }

                string getLastReportIdQuery = "SELECT LAST_INSERT_ROWID()";
                using (SQLiteCommand command = new SQLiteCommand(getLastReportIdQuery, connection))
                {
                    reportId = Convert.ToInt32(command.ExecuteScalar());
                }

                MessageBox.Show($"Report saved successfully with ReportId: {reportId}. Total Energy: {totalEnergy:F2} kWh, Total Cost: {totalCost:F2}, Remaining Budget: {userBudget - totalCost:F2}.");
            }
        }
        private void btnDownloadPDF_Click()
        {
            // Check if the current time is within peak hours
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= peakStartTime.Hour && currentTime.Hour < peakEndTime.Hour)
            {
                MessageBox.Show("Warning: Current time is within the peak hour. Please be aware of the potential higher charges during this time.",
                    "Peak Hour Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Let the user select the folder and save the report
            SaveReportAsPDF();
        }

        private void SaveReportAsPDF()
        {
            // Open a folder browser dialog for selecting the save location
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Select the folder to save the PDF report";

                // If the user selects a folder
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = folderBrowser.SelectedPath;

                    // Generate the file name
                    string fileName = $"Report_{userId}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                    string filePath = System.IO.Path.Combine(selectedPath, fileName);

                    // Create and save the PDF
                    var document = new iTextSharp.text.Document();
                    FileStream fs = null;

                    try
                    {
                        // Create the file stream
                        fs = new System.IO.FileStream(filePath, System.IO.FileMode.Create);
                        var pdfWriter = iTextSharp.text.pdf.PdfWriter.GetInstance(document, fs);

                        // Open the document for writing
                        document.Open();

                        // Add title and details
                        document.Add(new iTextSharp.text.Paragraph($"Monthly Report for User: {userId}"));
                        document.Add(new iTextSharp.text.Paragraph($"Region: {region}"));
                        document.Add(new iTextSharp.text.Paragraph($"Report Date: {DateTime.Now:dd MMM yyyy HH:mm}"));
                        document.Add(new iTextSharp.text.Paragraph($"Total Energy: {totalEnergy:F2} kWh"));
                        document.Add(new iTextSharp.text.Paragraph($"Total Cost: {totalCost:F2} Rs"));
                        document.Add(new iTextSharp.text.Paragraph($"Remaining Budget: {(userBudget - totalCost):F2} Rs"));
                        document.Add(new iTextSharp.text.Paragraph("\n"));

                        // Add schedule details
                        document.Add(new iTextSharp.text.Paragraph("Schedule:"));
                        foreach (var item in schedule)
                        {
                            document.Add(new iTextSharp.text.Paragraph(
                                $"Appliance: {item.Appliance} | Unit: {item.UnitNumber} | Start: {item.StartTime} | End: {item.EndTime} | " +
                                $"Wattage: {item.Wattage} W | Energy: {item.Energy:F2} kWh | Cost: {item.Cost:F2} Rs | Time Type: {item.TimeType} | Day: {item.DayOfWeek}"));
                        }

                        MessageBox.Show($"PDF Report has been saved at {filePath}", "Report Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        // Ensure resources are properly closed
                        if (document.IsOpen())
                        {
                            document.Close();
                        }

                        if (fs != null)
                        {
                            fs.Close();
                            fs.Dispose();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Operation canceled. No folder selected.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Monthly_Load(object sender, EventArgs e)
        {

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
        private void button2_Click(object sender, EventArgs e)
        {
            btnDownloadPDF_Click();
        }

        

    }
}
