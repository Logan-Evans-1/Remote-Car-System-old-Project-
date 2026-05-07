using System;
using System.Drawing;
using System.Windows.Forms;

namespace RemoteCarSystem
{
    public partial class Form3_Notifications : Form
    {
        private bool switchState = false;
        private Label lblTitle;
        private Label lblSwitch;
        private Panel pnlSwitch;
        private Button btnExit;

        public Form3_Notifications()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblSwitch = new Label();
            this.pnlSwitch = new Panel();
            this.btnExit = new Button();

            this.SuspendLayout();

            // Form
            this.ClientSize = new Size(320, 250);
            this.Name = "Form3_Notifications";
            this.Text = "Notifications";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            this.lblTitle.Text = "Notifications";
            this.lblTitle.AutoSize = false;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTitle.Location = new Point(10, 20);
            this.lblTitle.Size = new Size(300, 30);
            this.lblTitle.Name = "lblTitle";

            // Switch
            int switchY = 80;
            int switchWidth = 280;
            int switchHeight = 35;
            int switchX = (320 - switchWidth) / 2;

            this.lblSwitch.Text = "Notifications";
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

            // Exit Button
            this.btnExit.Text = "Exit";
            this.btnExit.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.btnExit.Location = new Point(switchX, 180);
            this.btnExit.Size = new Size(switchWidth, 35);
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new EventHandler(btnExit_Click);

            // Add controls
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSwitch);
            this.Controls.Add(this.pnlSwitch);
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
                e.Graphics.DrawString("Disabled", font, Brushes.Black, leftRect, sf);
                e.Graphics.DrawString("Enabled", font, Brushes.Black, rightRect, sf);
            }
        }

        private void Switch_Click(object sender, EventArgs e)
        {
            switchState = !switchState;
            pnlSwitch.Invalidate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

