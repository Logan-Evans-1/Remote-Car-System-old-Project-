using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteCarSystem
{
    public partial class Form3_Logout : Form
    {
        private Label lblMessage;
        private Button btnCancel;
        private Button btnLogout;

        public Form3_Logout()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblMessage = new Label();
            this.btnCancel = new Button();
            this.btnLogout = new Button();

            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(300, 150);
            this.Name = "Form3_Logout";
            this.Text = "Logout";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Message
            this.lblMessage.Text = "Are you sure you want to logout?";
            this.lblMessage.AutoSize = false;
            this.lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            this.lblMessage.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            this.lblMessage.Location = new Point(10, 30);
            this.lblMessage.Size = new Size(280, 40);
            this.lblMessage.Name = "lblMessage";

            // Cancel Button
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnCancel.Location = new Point(20, 90);
            this.btnCancel.Size = new Size(120, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            // Logout Button
            this.btnLogout.Text = "Logout";
            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnLogout.Location = new Point(160, 90);
            this.btnLogout.Size = new Size(120, 35);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.DialogResult = DialogResult.OK;
            this.btnLogout.Click += new EventHandler(btnLogout_Click);

            // Add controls
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnLogout);

            this.ResumeLayout(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

