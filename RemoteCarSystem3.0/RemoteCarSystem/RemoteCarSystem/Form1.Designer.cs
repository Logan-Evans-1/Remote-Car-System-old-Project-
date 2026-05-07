using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;
namespace RemoteCarSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.RadioButton rdoUser;
        private System.Windows.Forms.RadioButton rdoAdmin;

        private System.Windows.Forms.Label lblRole;

        private void InitializeComponent()
        {
            ///Login and Exit buttons
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            ///Username
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();

            ///Password
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();

            ///Role selection
            this.lblRole = new System.Windows.Forms.Label();
            this.rdoUser = new System.Windows.Forms.RadioButton();
            this.rdoAdmin = new System.Windows.Forms.RadioButton();
            
            this.SuspendLayout();

            ///Login
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Location = new System.Drawing.Point(40, 150);
            this.btnLogin.Size = new System.Drawing.Size(80, 25);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            ///Exit
            this.btnExit.Name = "btnExit";
            this.btnExit.Location = new System.Drawing.Point(140, 150);
            this.btnExit.Size = new System.Drawing.Size(80, 25);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            ///lbl Username
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(20, 20);
            this.lblUsername.Size = new System.Drawing.Size(70, 15);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";

            ///txt Username
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.Location = new System.Drawing.Point(110, 17);
            this.txtUsername.TabIndex = 1;

            ///lbl Password
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(20, 55);
            this.lblPassword.Size = new System.Drawing.Size(61, 15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";

            ///txt Password
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.Location = new System.Drawing.Point(110, 52);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.PasswordChar = '*';

            ///lbl Role
            this.lblRole.Name = "lblRole";
            this.lblRole.AutoSize = true;
            this.lblRole.Location = new System.Drawing.Point(20, 95);
            this.lblRole.Size = new System.Drawing.Size(32, 15);
            this.lblRole.TabIndex = 6;
            this.lblRole.Text = "Role";

            ///rdoUser
            this.rdoUser.Name = "rdoUser";
            this.rdoUser.AutoSize = true;
            this.rdoUser.Location = new System.Drawing.Point(110, 93);
            this.rdoUser.Size = new System.Drawing.Size(47, 17);
            this.rdoUser.TabIndex = 7;
            this.rdoUser.TabStop = true;
            this.rdoUser.Checked = true;
            this.rdoUser.Text = "User";
            this.rdoUser.UseVisualStyleBackColor = true;

            ///rdoAdmin
            this.rdoAdmin.Name = "rdoAdmin";
            this.rdoAdmin.AutoSize = true;
            this.rdoAdmin.Location = new System.Drawing.Point(180, 93);
            this.rdoAdmin.Size = new System.Drawing.Size(54, 17);
            this.rdoAdmin.TabIndex = 8;
            this.rdoAdmin.Text = "Admin";
            this.rdoAdmin.UseVisualStyleBackColor = true;

            ///Form1
            this.Name = "Form1";
            this.Text = "Remote Car System - Login";
            this.ClientSize = new System.Drawing.Size(320, 210);

            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnExit);

            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);

            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);

            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.rdoUser);
            this.Controls.Add(this.rdoAdmin);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
