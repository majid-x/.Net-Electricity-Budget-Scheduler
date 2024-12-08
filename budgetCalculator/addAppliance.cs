using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace budgetCalculator
{
    public partial class addAppliance : Form
    {
        private string connectionString = @"Data Source=appliances.db;Version=3;";

        public addAppliance()
        {
            InitializeComponent();
        }

        private void ApplianceForm_Load(object sender, EventArgs e)
        {
            // Initialize the database and create table if not exists
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            // SQL query to create the Appliances table if it does not exist
            string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Appliances (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ApplianceName TEXT NOT NULL,
                    Wattage INTEGER NOT NULL
                )";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery(); // Create the table if it doesn't exist
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Get the appliance name and wattage from the user inputs
            string applianceName = txtApplianceName.Text;
            int wattage = (int)numericUpDownWattage.Value;

            // Validation: Ensure the appliance name and wattage are valid
            if (string.IsNullOrWhiteSpace(applianceName))
            {
                MessageBox.Show("Please enter a valid appliance name.");
                return;
            }

            if (wattage <= 0)
            {
                MessageBox.Show("Wattage must be a positive integer.");
                return;
            }

            // Save the appliance to the database
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Appliances (ApplianceName, Wattage) VALUES (@ApplianceName, @Wattage)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ApplianceName", applianceName);
                    cmd.Parameters.AddWithValue("@Wattage", wattage);

                    cmd.ExecuteNonQuery(); // Insert the appliance record
                }
            }

            MessageBox.Show($"Appliance '{applianceName}' added successfully with {wattage} watts.");

            // Clear the inputs after saving
            txtApplianceName.Clear();
            numericUpDownWattage.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }
    }
}
