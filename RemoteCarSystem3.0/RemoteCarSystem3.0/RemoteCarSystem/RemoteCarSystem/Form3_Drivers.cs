using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RemoteCarSystem
{
    public partial class Form3_Drivers : Form
    {
        private readonly string _driverDataPath;
        private bool switchState = false; // false = Add, true = Remove
        private Label lblTitle;
        private Label lblPrompt;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lblSwitch;
        private Panel pnlSwitch;
        private Button btnAction;
        private Button btnExit;

        public Form3_Drivers(string driverDataPath)
        {
            _driverDataPath = driverDataPath;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblPrompt = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.lblSwitch = new Label();
            this.pnlSwitch = new Panel();
            this.btnAction = new Button();
            this.btnExit = new Button();

            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(320, 350);
            this.Name = "Form3_Drivers";
            this.Text = "Drivers";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            this.lblTitle.Text = "Drivers";
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 20);
            this.lblTitle.Size = new Size(300, 30);
            this.lblTitle.Name = "lblTitle";

            // Prompt
            this.lblPrompt.Text = "Enter username and password of the driver you wish to add/remove:";
            this.lblPrompt.AutoSize = false;
            this.lblPrompt.TextAlign = ContentAlignment.MiddleLeft;
            this.lblPrompt.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblPrompt.Location = new Point(20, 60);
            this.lblPrompt.Size = new Size(280, 40);
            this.lblPrompt.Name = "lblPrompt";

            // Username TextBox
            this.txtUsername.Location = new Point(20, 110);
            this.txtUsername.Size = new Size(280, 25);
            this.txtUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.txtUsername.Name = "txtUsername";

            // Password TextBox
            this.txtPassword.Location = new Point(20, 145);
            this.txtPassword.Size = new Size(280, 25);
            this.txtPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.Name = "txtPassword";

            // Switch
            int switchY = 180;
            int switchWidth = 280;
            int switchHeight = 35;
            int switchX = 20;

            this.lblSwitch.Text = "Action";
            this.lblSwitch.AutoSize = true;
            this.lblSwitch.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitch.Location = new Point(switchX, switchY);
            this.lblSwitch.Name = "lblSwitch";

            this.pnlSwitch.Location = new Point(switchX, switchY + 18);
            this.pnlSwitch.Size = new Size(switchWidth, switchHeight);
            this.pnlSwitch.BackColor = Color.LightGray;
            this.pnlSwitch.BorderStyle = BorderStyle.FixedSingle;
            this.pnlSwitch.Cursor = Cursors.Hand;
            this.pnlSwitch.Paint += new PaintEventHandler(Switch_Paint);
            this.pnlSwitch.Click += new EventHandler(Switch_Click);
            this.pnlSwitch.Name = "pnlSwitch";

            // Action Button
            this.btnAction.Text = "Add";
            this.btnAction.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnAction.Location = new Point(switchX, 250);
            this.btnAction.Size = new Size(switchWidth, 35);
            this.btnAction.Name = "btnAction";
            this.btnAction.Click += new EventHandler(btnAction_Click);

            // Exit Button
            this.btnExit.Text = "Exit";
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnExit.Location = new Point(switchX, 295);
            this.btnExit.Size = new Size(switchWidth, 35);
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new EventHandler(btnExit_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPrompt);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblSwitch);
            this.Controls.Add(this.pnlSwitch);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnExit);

            this.ResumeLayout(false);
        }

        private void Switch_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            int halfWidth = panel.Width / 2;
            Rectangle leftRect = new Rectangle(0, 0, halfWidth, panel.Height);
            Rectangle rightRect = new Rectangle(halfWidth, 0, halfWidth, panel.Height);

            if (switchState)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen), rightRect);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), leftRect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen), leftRect);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rightRect);
            }

            using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString("Add", font, Brushes.Black, leftRect, sf);
                e.Graphics.DrawString("Remove", font, Brushes.Black, rightRect, sf);
            }
        }

        private void Switch_Click(object sender, EventArgs e)
        {
            switchState = !switchState;
            pnlSwitch.Invalidate();
            btnAction.Text = switchState ? "Remove" : "Add";
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Please enter a username.");
                return;
            }

            try
            {
                if (!File.Exists(_driverDataPath))
                {
                    MessageBox.Show("Driver data file not found.");
                    return;
                }

                if (switchState) // Remove
                {
                    RemoveDriver(username);
                }
                else // Add
                {
                    if (string.IsNullOrWhiteSpace(password))
                    {
                        MessageBox.Show("Please enter a password.");
                        return;
                    }
                    AddDriver(username, password);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void AddDriver(string username, string password)
        {
            var lines = new System.Collections.Generic.List<string>();
            
            if (File.Exists(_driverDataPath))
            {
                lines.AddRange(File.ReadAllLines(_driverDataPath));
            }

            // Check if driver already exists
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split(':');
                if (parts.Length >= 1)
                {
                    string existingDriver = parts[0].Trim();
                    if (existingDriver.Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("Driver already exists.");
                        return;
                    }
                }
            }

            // Add new driver with default vehicle
            lines.Add($"{username} : {password} : 1986 Chevy");
            File.WriteAllLines(_driverDataPath, lines);
            MessageBox.Show("Driver added successfully.");
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void RemoveDriver(string username)
        {
            var lines = File.ReadAllLines(_driverDataPath);
            var newLines = new System.Collections.Generic.List<string>();
            bool found = false;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    newLines.Add(line);
                    continue;
                }

                var parts = line.Split(':');
                if (parts.Length < 1) continue;

                string driver = parts[0].Trim();
                if (!driver.Equals(username, StringComparison.OrdinalIgnoreCase))
                {
                    newLines.Add(line);
                }
                else
                {
                    found = true;
                }
            }

            if (!found)
            {
                MessageBox.Show("Driver not found.");
                return;
            }

            File.WriteAllLines(_driverDataPath, newLines);
            MessageBox.Show("Driver removed successfully.");
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

