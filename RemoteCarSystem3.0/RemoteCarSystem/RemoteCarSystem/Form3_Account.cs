using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace RemoteCarSystem
{
    public partial class Form3_Account : Form
    {
        private readonly string _currentUserName;
        private readonly string _driverDataPath;
        private Label lblTitle;
        private Label lblChangeUsername;
        private RadioButton rdoChangeUsername;
        private Label lblChangePassword;
        private RadioButton rdoChangePassword;
        private TextBox txtNewUsername;
        private TextBox txtNewPassword;
        private Button btnSave;
        private Button btnExit;

        public Form3_Account(string currentUserName, string driverDataPath)
        {
            _currentUserName = currentUserName;
            _driverDataPath = driverDataPath;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblChangeUsername = new Label();
            this.rdoChangeUsername = new RadioButton();
            this.lblChangePassword = new Label();
            this.rdoChangePassword = new RadioButton();
            this.txtNewUsername = new TextBox();
            this.txtNewPassword = new TextBox();
            this.btnSave = new Button();
            this.btnExit = new Button();

            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(320, 400);
            this.Name = "Form3_Account";
            this.Text = "Account Settings";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            this.lblTitle.Text = "Account Settings";
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 20);
            this.lblTitle.Size = new Size(300, 30);
            this.lblTitle.Name = "lblTitle";

            // Username Radio Button
            int startY = 70;
            int leftMargin = 20;
            int itemWidth = 280;

            this.lblChangeUsername.Text = "Change Username";
            this.lblChangeUsername.AutoSize = true;
            this.lblChangeUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblChangeUsername.Location = new Point(leftMargin, startY);
            this.lblChangeUsername.Name = "lblChangeUsername";

            this.rdoChangeUsername.AutoSize = true;
            this.rdoChangeUsername.Location = new Point(leftMargin + 120, startY);
            this.rdoChangeUsername.Size = new Size(17, 17);
            this.rdoChangeUsername.TabStop = true;
            this.rdoChangeUsername.Text = "";
            this.rdoChangeUsername.UseVisualStyleBackColor = true;
            this.rdoChangeUsername.CheckedChanged += new EventHandler(rdoChangeUsername_CheckedChanged);
            this.rdoChangeUsername.Name = "rdoChangeUsername";

            // New Username TextBox
            this.txtNewUsername.Location = new Point(leftMargin, startY + 30);
            this.txtNewUsername.Size = new Size(itemWidth, 25);
            this.txtNewUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.txtNewUsername.Visible = false;
            this.txtNewUsername.Name = "txtNewUsername";

            // Password Radio Button
            startY = 130;
            this.lblChangePassword.Text = "Change Password";
            this.lblChangePassword.AutoSize = true;
            this.lblChangePassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblChangePassword.Location = new Point(leftMargin, startY);
            this.lblChangePassword.Name = "lblChangePassword";

            this.rdoChangePassword.AutoSize = true;
            this.rdoChangePassword.Location = new Point(leftMargin + 120, startY);
            this.rdoChangePassword.Size = new Size(17, 17);
            this.rdoChangePassword.TabStop = true;
            this.rdoChangePassword.Text = "";
            this.rdoChangePassword.UseVisualStyleBackColor = true;
            this.rdoChangePassword.CheckedChanged += new EventHandler(rdoChangePassword_CheckedChanged);
            this.rdoChangePassword.Name = "rdoChangePassword";

            // New Password TextBox
            this.txtNewPassword.Location = new Point(leftMargin, startY + 30);
            this.txtNewPassword.Size = new Size(itemWidth, 25);
            this.txtNewPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.Visible = false;
            this.txtNewPassword.Name = "txtNewPassword";

            // Save Button
            this.btnSave.Text = "Save";
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnSave.Location = new Point(leftMargin, 240);
            this.btnSave.Size = new Size(itemWidth, 35);
            this.btnSave.Name = "btnSave";
            this.btnSave.Click += new EventHandler(btnSave_Click);

            // Exit Button
            this.btnExit.Text = "Exit";
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnExit.Location = new Point(leftMargin, 290);
            this.btnExit.Size = new Size(itemWidth, 35);
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new EventHandler(btnExit_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblChangeUsername);
            this.Controls.Add(this.rdoChangeUsername);
            this.Controls.Add(this.lblChangePassword);
            this.Controls.Add(this.rdoChangePassword);
            this.Controls.Add(this.txtNewUsername);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);

            this.ResumeLayout(false);
        }

        private void rdoChangeUsername_CheckedChanged(object sender, EventArgs e)
        {
            txtNewUsername.Visible = rdoChangeUsername.Checked;
            if (rdoChangeUsername.Checked)
            {
                rdoChangePassword.Checked = false;
                txtNewPassword.Visible = false;
            }
        }

        private void rdoChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            txtNewPassword.Visible = rdoChangePassword.Checked;
            if (rdoChangePassword.Checked)
            {
                rdoChangeUsername.Checked = false;
                txtNewUsername.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(_driverDataPath))
                {
                    MessageBox.Show("Driver data file not found.");
                    return;
                }

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
                    if (parts.Length < 2) continue;

                    string driver = parts[0].Trim();
                    string password = parts[1].Trim();
                    string vehicles = parts.Length > 2 ? parts[2].Trim() : "";

                    if (driver.Equals(_currentUserName, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        // Update username if radio button is checked
                        if (rdoChangeUsername.Checked && !string.IsNullOrWhiteSpace(txtNewUsername.Text))
                        {
                            driver = txtNewUsername.Text.Trim();
                        }
                        // Update password if radio button is checked
                        if (rdoChangePassword.Checked && !string.IsNullOrWhiteSpace(txtNewPassword.Text))
                        {
                            password = txtNewPassword.Text.Trim();
                        }
                        newLines.Add($"{driver} : {password} : {vehicles}");
                    }
                    else
                    {
                        newLines.Add(line);
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Current user not found in database.");
                    return;
                }

                File.WriteAllLines(_driverDataPath, newLines);
                MessageBox.Show("Account updated successfully.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account: {ex.Message}");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

