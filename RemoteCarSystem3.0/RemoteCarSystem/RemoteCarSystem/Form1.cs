using System;
using System.IO;
using System.Windows.Forms;

namespace RemoteCarSystem
{
    public partial class Form1 : Form
    {
        // Resolve full path to driverdata.txt.
        // EXE is in ...\RemoteCarSystem\RemoteCarSystem\RemoteCarSystem\bin\Debug
        // File is in ...\RemoteCarSystem\RemoteCarSystem\driverdata.txt
        private readonly string database = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "driverdata.txt"));

        public string LoggedInUser { get; private set; }
        public string LoggedInVehicles { get; private set; }
        public string LoggedInRole { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Please enter a username and password.");
                return;
            }

            if (!rdoUser.Checked && !rdoAdmin.Checked)
            {
                MessageBox.Show("Please select a role (User or Admin).");
                return;
            }

            // Admin login (no extra UI yet)
            if (rdoAdmin.Checked)
            {
                if (user.Equals("admin", StringComparison.OrdinalIgnoreCase) && pass == "admin")
                {
                    LoggedInUser = "admin";
                    LoggedInVehicles = string.Empty;
                    LoggedInRole = "Admin";
                    MessageBox.Show("Admin logged in.");
                }
                else
                {
                    MessageBox.Show("Invalid admin username or password.");
                }
                return;
            }

            // User login - validate against driverdata.txt
            if (!File.Exists(database))
            {
                MessageBox.Show("User database not found at:\n" + database);
                return;
            }

            var lines = File.ReadAllLines(database);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(':');
                if (parts.Length < 2) continue;

                string driver = parts[0].Trim();
                string password = parts[1].Trim();
                string vehicles = parts.Length > 2 ? parts[2].Trim() : "";

                if (driver.Equals(user, StringComparison.OrdinalIgnoreCase) && password == pass)
                {
                    LoggedInUser = driver;
                    LoggedInVehicles = vehicles;
                    LoggedInRole = "User";

                    MessageBox.Show($"{driver} logged in.");

                    // Open Form2 and pass the logged-in user's information
                    var form2 = new Form2(LoggedInUser, LoggedInVehicles);
                    form2.FormClosed += (s, args) => this.Close();
                    this.Hide();
                    form2.Show();
                    return;
                }
            }

            MessageBox.Show("Invalid username or password.");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


