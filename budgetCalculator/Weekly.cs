using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class Weekly : Form
    {
        private readonly double userBudget;
        private readonly List<(string Appliance, int Units, double UsageHours)> appliancesData;
        private readonly List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType, string DayOfWeek)> schedule;
        private readonly string region;
        private readonly string userId;
        private DateTime peakStartTime, peakEndTime, offPeakStartTime, offPeakEndTime;

        private string connectionString = @"Data Source=Region.db;Version=3;";
        private string applianceConnectionString = @"Data Source=appliances.db;Version=3;";
        private double totalEnergy = 0;
        private double totalCost = 0;

        public Weekly(List<(string Appliance, int Units, double UsageHours)> appliances, double budget, string region, string userId)
        {
            InitializeComponent();
            this.userBudget = budget;
            this.appliancesData = appliances;
            this.schedule = new List<(string Appliance, int UnitNumber, string StartTime, string EndTime, int Wattage, double Energy, double Cost, string TimeType, string DayOfWeek)>();
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
                    CREATE TABLE IF NOT EXISTS WeekReports (
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
                    CREATE TABLE IF NOT EXISTS WeekReportAppliances (
                        ApplianceName TEXT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Wattage INTEGER,
                        Energy REAL,
                        Cost REAL,
                        TimeType TEXT,
                        DayOfWeek TEXT,
                        ReportId INTEGER,
                        FOREIGN KEY (ReportId) REFERENCES WeekReports(ReportId)
                    )";

                using (SQLiteCommand command = new SQLiteCommand(createReportAppliancesTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                string createDailyUsageTableQuery = @"
                    CREATE TABLE IF NOT EXISTS ApplianceDailyUsage (
                        ApplianceName TEXT,
                        DayOfWeek TEXT,
                        DailyUsageHours REAL,
                        ReportId INTEGER,
                        FOREIGN KEY (ReportId) REFERENCES WeekReports(ReportId)
                    )";

                using (SQLiteCommand command = new SQLiteCommand(createDailyUsageTableQuery, connection))
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

            schedule.Clear();

            // Sort appliances by wattage for prioritization during peak hours
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

                    double cost = energy * costPerUnit;

                    totalEnergy += energy;
                    totalCost += cost;

                    double dailyUsage = usageHours / 9;
                    Dictionary<string, double> dailyUsageMap = new Dictionary<string, double>();

                    foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                    {
                        double usageForDay = dailyUsage;

                        if (day == DayOfWeek.Saturday || day == DayOfWeek.Sunday)
                        {
                            usageForDay *= 2;
                        }

                        dailyUsageMap[day.ToString()] = usageForDay;
                    }

                    foreach (var dayOfWeek in dailyUsageMap.Keys)
                    {
                        double dailyHours = dailyUsageMap[dayOfWeek];

                        // Prioritize peak hours for low wattage appliances
                        int startHour = (wattage <= 500) ? peakStartTime.Hour : offPeakStartTime.Hour;

                        int maxHour = 24 - (int)Math.Ceiling(dailyHours);
                        int randomHour = (startHour < maxHour) ? random.Next(startHour, maxHour) : startHour;
                        int randomMinute = random.Next(0, 60);
                        DateTime startDateTime = new DateTime(2024, 1, 1, randomHour, randomMinute, 0);
                        DateTime endDateTime = startDateTime.AddHours(dailyHours);

                        string timeType = (startDateTime.Hour >= peakStartTime.Hour && startDateTime.Hour < peakEndTime.Hour) ? "Peak" : "Off-Peak";

                        schedule.Add((applianceName, units, startDateTime.ToString("hh:mm tt"), endDateTime.ToString("hh:mm tt"), wattage, energy, cost, timeType, dayOfWeek));
                    }
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
                    item.TimeType,
                    item.DayOfWeek
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

        private void btnDownload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("PDF Report Generation is under development.");
        }

        private void Weekly_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dgvSchedule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            GenerateSchedule();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveScheduleToDatabase();
        }

        private void SaveScheduleToDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(applianceConnectionString))
            {
                connection.Open();

                string insertReportQuery = @"
                    INSERT INTO WeekReports (UserId, Region, ReportDate, TotalEnergy, TotalCost, RemainingBudget)
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

                // Retrieve the ReportId
                string getLastReportIdQuery = "SELECT last_insert_rowid()";
                int reportId;
                using (SQLiteCommand command = new SQLiteCommand(getLastReportIdQuery, connection))
                {
                    reportId = Convert.ToInt32(command.ExecuteScalar());
                }

                // Save the appliances data for the schedule
                foreach (var item in schedule)
                {
                    string insertApplianceQuery = @"
                        INSERT INTO WeekReportAppliances (ApplianceName, StartTime, EndTime, Wattage, Energy, Cost, TimeType, DayOfWeek, ReportId)
                        VALUES (@ApplianceName, @StartTime, @EndTime, @Wattage, @Energy, @Cost, @TimeType, @DayOfWeek, @ReportId)";
                    using (SQLiteCommand command = new SQLiteCommand(insertApplianceQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ApplianceName", item.Appliance);
                        command.Parameters.AddWithValue("@StartTime", item.StartTime);
                        command.Parameters.AddWithValue("@EndTime", item.EndTime);
                        command.Parameters.AddWithValue("@Wattage", item.Wattage);
                        command.Parameters.AddWithValue("@Energy", item.Energy);
                        command.Parameters.AddWithValue("@Cost", item.Cost);
                        command.Parameters.AddWithValue("@TimeType", item.TimeType);
                        command.Parameters.AddWithValue("@DayOfWeek", item.DayOfWeek);
                        command.Parameters.AddWithValue("@ReportId", reportId);
                        command.ExecuteNonQuery();
                    }
                }

                // Save daily usage data
                foreach (var item in appliancesData)
                {
                    string insertDailyUsageQuery = @"
                        INSERT INTO ApplianceDailyUsage (ApplianceName, DayOfWeek, DailyUsageHours, ReportId)
                        VALUES (@ApplianceName, @DayOfWeek, @DailyUsageHours, @ReportId)";
                    using (SQLiteCommand command = new SQLiteCommand(insertDailyUsageQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ApplianceName", item.Appliance);
                        command.Parameters.AddWithValue("@DayOfWeek", DateTime.Now.DayOfWeek.ToString());
                        command.Parameters.AddWithValue("@DailyUsageHours", item.UsageHours);
                        command.Parameters.AddWithValue("@ReportId", reportId);
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Schedule saved successfully.");
            }
        }
    }
}
