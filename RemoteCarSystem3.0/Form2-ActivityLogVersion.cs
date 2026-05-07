using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;//added

namespace RemoteCarSystem
{
    public partial class Form2 : Form
    {
        private readonly string _userName;
        private readonly string _vehicles;

        private Label lblPageTitle;
        private Button btnLeft;
        private Button btnRight;
        private Label lblTime;
        private Label dotStatus;
        private Label dotLocation;
        private Label dotActivity;
        private Label dotSettings;
        private Timer timeTimer;
        private PictureBox picMap;
        private Panel pnlVehicleBox;
        private PictureBox picVehicle;
        private ListBox lstActivityLog;//added by le
        
        // Toggle switches for Status page
        private Label lblSwitchPower;
        private Panel pnlSwitchPower;
        private bool switchPowerState = false;
        
        private Label lblSwitchDoors;
        private Panel pnlSwitchDoors;
        private bool switchDoorsState = false;
        
        private Label lblSwitchWindows;
        private Panel pnlSwitchWindows;
        private bool switchWindowsState = false;
        
        private Label lblSwitchAlarm;
        private Panel pnlSwitchAlarm;
        private bool switchAlarmState = false;

        private enum Page
        {
            Status = 0,
            Location = 1,
            ActivityLog = 2,
            Settings = 3
        }

        private Page _currentPage = Page.Status;

        public Form2(string userName, string vehicles)
        {
            _userName = userName;
            _vehicles = vehicles;

            InitializeComponent();
            StartClock();
            UpdatePage(Page.Status);
        }

        private void InitializeComponent()
        {
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.dotStatus = new System.Windows.Forms.Label();
            this.dotLocation = new System.Windows.Forms.Label();
            this.dotActivity = new System.Windows.Forms.Label();
            this.dotSettings = new System.Windows.Forms.Label();
            this.timeTimer = new System.Windows.Forms.Timer();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.pnlVehicleBox = new System.Windows.Forms.Panel();
            this.picVehicle = new System.Windows.Forms.PictureBox();
            
            // Toggle switches
            this.lblSwitchPower = new System.Windows.Forms.Label();
            this.pnlSwitchPower = new System.Windows.Forms.Panel();
            this.lblSwitchDoors = new System.Windows.Forms.Label();
            this.pnlSwitchDoors = new System.Windows.Forms.Panel();
            this.lblSwitchWindows = new System.Windows.Forms.Label();
            this.pnlSwitchWindows = new System.Windows.Forms.Panel();
            this.lblSwitchAlarm = new System.Windows.Forms.Label();
            this.pnlSwitchAlarm = new System.Windows.Forms.Panel();

            this.SuspendLayout();

            // Form2 - phone-like dimensions (portrait)
            this.ClientSize = new System.Drawing.Size(360, 640);
            this.Name = "Form2";
            this.Text = "Remote Car System";
            this.StartPosition = FormStartPosition.CenterScreen;

            // lblPageTitle (centered at top)
            this.lblPageTitle.AutoSize = false;
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(55, 20);
            this.lblPageTitle.Size = new System.Drawing.Size(250, 40);
            this.lblPageTitle.Name = "lblPageTitle";

            // lblTime (top-right current time)
            this.lblTime.AutoSize = false;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.lblTime.Location = new System.Drawing.Point(260, 22);
            this.lblTime.Size = new System.Drawing.Size(90, 20);
            this.lblTime.Name = "lblTime";

            // btnLeft (back arrow)
            this.btnLeft.Text = "<";
            this.btnLeft.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnLeft.Location = new System.Drawing.Point(10, 280);
            this.btnLeft.Size = new System.Drawing.Size(60, 60);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);

            // btnRight (forward arrow)
            this.btnRight.Text = ">";
            this.btnRight.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnRight.Location = new System.Drawing.Point(290, 280);
            this.btnRight.Size = new System.Drawing.Size(60, 60);
            this.btnRight.Name = "btnRight";
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);

            // Pagination dots at bottom center
            int dotsY = 580;

            this.dotStatus.AutoSize = true;
            this.dotStatus.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.dotStatus.Location = new System.Drawing.Point(135, dotsY);
            this.dotStatus.Name = "dotStatus";

            this.dotLocation.AutoSize = true;
            this.dotLocation.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.dotLocation.Location = new System.Drawing.Point(160, dotsY);
            this.dotLocation.Name = "dotLocation";

            this.dotActivity.AutoSize = true;
            this.dotActivity.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.dotActivity.Location = new System.Drawing.Point(185, dotsY);
            this.dotActivity.Name = "dotActivity";

            this.dotSettings.AutoSize = true;
            this.dotSettings.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular);
            this.dotSettings.Location = new System.Drawing.Point(210, dotsY);
            this.dotSettings.Name = "dotSettings";

            // Timer for time label
            this.timeTimer.Interval = 1000;
            this.timeTimer.Tick += new System.EventHandler(this.timeTimer_Tick);

            // picMap (for Location page - static map image)
            this.picMap.Location = new System.Drawing.Point(10, 70);
            this.picMap.Size = new System.Drawing.Size(340, 500);
            this.picMap.SizeMode = PictureBoxSizeMode.Zoom;
            this.picMap.Visible = false;
            this.picMap.Name = "picMap";
            LoadMapImage();

            // pnlVehicleBox (for Status page - black border box with 2px thick border)
            this.pnlVehicleBox.BorderStyle = BorderStyle.None;
            this.pnlVehicleBox.BackColor = System.Drawing.Color.White;
            this.pnlVehicleBox.Visible = false;
            this.pnlVehicleBox.Name = "pnlVehicleBox";
            this.pnlVehicleBox.Paint += new PaintEventHandler(pnlVehicleBox_Paint);
            this.pnlVehicleBox.Controls.Add(this.picVehicle);

            // picVehicle (vehicle image inside the box - scaled to fit)
            this.picVehicle.SizeMode = PictureBoxSizeMode.Zoom;
            this.picVehicle.Location = new System.Drawing.Point(2, 2);
            this.picVehicle.Name = "picVehicle";
            LoadVehicleImage();

            // Initialize toggle switches
            InitializeSwitches();

            // Add controls
            this.Controls.Add(this.lblPageTitle);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.dotStatus);
            this.Controls.Add(this.dotLocation);
            this.Controls.Add(this.dotActivity);
            this.Controls.Add(this.dotSettings);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.pnlVehicleBox);
            
            // Add switch controls
            this.Controls.Add(this.lblSwitchPower);
            this.Controls.Add(this.pnlSwitchPower);
            this.Controls.Add(this.lblSwitchDoors);
            this.Controls.Add(this.pnlSwitchDoors);
            this.Controls.Add(this.lblSwitchWindows);
            this.Controls.Add(this.pnlSwitchWindows);
            this.Controls.Add(this.lblSwitchAlarm);
            this.Controls.Add(this.pnlSwitchAlarm);
            
            //Activity log  //added
            this.lstActivityLog = new System.Windows.Forms.ListBox();
            this.lstActivityLog.Location = new System.Drawing.Point(70, 70);
            this.lstActivityLog.Size = new System.Drawing.Size(220, 480);
            this.lstActivityLog.Font = new System.Drawing.Font("Segoue UI", 9F);
            this.lstActivityLog.Visible = false;
            this.Controls.Add(this.lstActivityLog);

            this.ResumeLayout(false);
        }

        private void StartClock()
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
            timeTimer.Start();
        }

        private void UpdatePage(Page page)
        {
            _currentPage = page;

            switch (page)
            {
                case Page.Status:
                    lblPageTitle.Text = "Status";
                    btnLeft.Visible = false;
                    btnRight.Visible = true;
                    picMap.Visible = false;
                    pnlVehicleBox.Visible = true;
                    PositionVehicleBox();
                    ShowSwitches(true);
                    lstActivityLog.Visible = false;//add per case
                    break;
                case Page.Location:
                    lblPageTitle.Text = "Location";
                    btnLeft.Visible = true;
                    btnRight.Visible = true;
                    picMap.Visible = true;
                    pnlVehicleBox.Visible = false;
                    ShowSwitches(false);
                    lstActivityLog.Visible = false;
                    break;
                case Page.ActivityLog:
                    lblPageTitle.Text = "Activity Log";
                    btnLeft.Visible = true;
                    btnRight.Visible = true;
                    picMap.Visible = false;
                    pnlVehicleBox.Visible = false;
                    ShowSwitches(false);
                    lstActivityLog.Visible = true;
                    break;
                case Page.Settings:
                    lblPageTitle.Text = "Settings";
                    btnLeft.Visible = true;
                    btnRight.Visible = false;
                    picMap.Visible = false;
                    pnlVehicleBox.Visible = false;
                    ShowSwitches(false);
                    lstActivityLog.Visible = false;
                    break;
            }

            // Update pagination dots
            string active = "●";
            string inactive = "○";

            dotStatus.Text = page == Page.Status ? active : inactive;
            dotLocation.Text = page == Page.Location ? active : inactive;
            dotActivity.Text = page == Page.ActivityLog ? active : inactive;
            dotSettings.Text = page == Page.Settings ? active : inactive;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            // Navigation rules for left arrow
            switch (_currentPage)
            {
                case Page.Location:
                    UpdatePage(Page.Status);
                    break;
                case Page.ActivityLog:
                    UpdatePage(Page.Location);
                    break;
                case Page.Settings:
                    UpdatePage(Page.ActivityLog);
                    break;
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            // Navigation rules for right arrow
            switch (_currentPage)
            {
                case Page.Status:
                    UpdatePage(Page.Location);
                    break;
                case Page.Location:
                    UpdatePage(Page.ActivityLog);
                    break;
                case Page.ActivityLog:
                    UpdatePage(Page.Settings);
                    break;
            }
        }

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm");
        }

        private void LoadMapImage()
        {
            // Load GoogleMapTA.png from the project folder
            // EXE is in ...\RemoteCarSystem\RemoteCarSystem\RemoteCarSystem\bin\Debug
            // Image is in ...\RemoteCarSystem\RemoteCarSystem\GoogleMapTA.png
            try
            {
                string imagePath = Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "GoogleMapTA.png"));
                
                if (File.Exists(imagePath))
                {
                    picMap.Image = System.Drawing.Image.FromFile(imagePath);
                }
            }
            catch
            {
                // If image load fails, PictureBox will just be blank
            }
        }

        private void LoadVehicleImage()
        {
            // Load 1986-chevrolet-k-10-pickup_1.jpg from the project folder
            try
            {
                string imagePath = Path.GetFullPath(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "1986-chevrolet-k-10-pickup_1.jpg"));
                
                if (File.Exists(imagePath))
                {
                    picVehicle.Image = System.Drawing.Image.FromFile(imagePath);
                }
            }
            catch
            {
                // If image load fails, PictureBox will just be blank
            }
        }

        private void PositionVehicleBox()
        {
            // Position the vehicle box on the top half of the page
            // Box size matches window dimensions (scaled to fit nicely in top half)
            // Form is 360x640, so make box about 320x240 to fit in top half
            int boxWidth = 320;
            int boxHeight = 240;
            
            // Center the box horizontally on the form (360 wide)
            int boxX = (360 - boxWidth) / 2;
            
            // Position on top half of page (below title, which ends around y=60)
            int boxY = 70;
            
            pnlVehicleBox.Location = new System.Drawing.Point(boxX, boxY);
            pnlVehicleBox.Size = new System.Drawing.Size(boxWidth, boxHeight);
            
            // Size the picture box to fit inside with 2px border on all sides
            picVehicle.Location = new System.Drawing.Point(2, 2);
            picVehicle.Size = new System.Drawing.Size(boxWidth - 4, boxHeight - 4);
        }

        private void pnlVehicleBox_Paint(object sender, PaintEventArgs e)
        {
            // Draw a 2px thick black border around the panel
            Panel panel = sender as Panel;
            if (panel != null)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    Rectangle rect = new Rectangle(1, 1, panel.Width - 2, panel.Height - 2);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            }
        }

        private void InitializeSwitches()
        {
            // Vehicle box ends at y=310 (70 + 240), dots start at y=580
            // Space 4 switches evenly: start at y=330, each switch takes ~50px (label + switch + spacing)
            int startY = 330;
            int switchWidth = 280;
            int switchHeight = 35;
            int switchX = (360 - switchWidth) / 2;
            int spacing = 50;

            // Power Switch
            this.lblSwitchPower.Text = "Power";
            this.lblSwitchPower.AutoSize = true;
            this.lblSwitchPower.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitchPower.Location = new Point(switchX, startY);
            this.lblSwitchPower.Name = "lblSwitchPower";

            this.pnlSwitchPower.Location = new Point(switchX, startY + 18);
            this.pnlSwitchPower.Size = new Size(switchWidth, switchHeight);
            this.pnlSwitchPower.BackColor = Color.LightGray;
            this.pnlSwitchPower.BorderStyle = BorderStyle.FixedSingle;
            this.pnlSwitchPower.Cursor = Cursors.Hand;
            this.pnlSwitchPower.Paint += new PaintEventHandler(Switch_Paint);
            this.pnlSwitchPower.Click += new EventHandler(SwitchPower_Click);
            this.pnlSwitchPower.Name = "pnlSwitchPower";

            // Car Doors Switch
            this.lblSwitchDoors.Text = "Car Doors";
            this.lblSwitchDoors.AutoSize = true;
            this.lblSwitchDoors.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitchDoors.Location = new Point(switchX, startY + spacing);
            this.lblSwitchDoors.Name = "lblSwitchDoors";

            this.pnlSwitchDoors.Location = new Point(switchX, startY + spacing + 18);
            this.pnlSwitchDoors.Size = new Size(switchWidth, switchHeight);
            this.pnlSwitchDoors.BackColor = Color.LightGray;
            this.pnlSwitchDoors.BorderStyle = BorderStyle.FixedSingle;
            this.pnlSwitchDoors.Cursor = Cursors.Hand;
            this.pnlSwitchDoors.Paint += new PaintEventHandler(Switch_Paint);
            this.pnlSwitchDoors.Click += new EventHandler(SwitchDoors_Click);
            this.pnlSwitchDoors.Name = "pnlSwitchDoors";

            // Windows Switch
            this.lblSwitchWindows.Text = "Windows";
            this.lblSwitchWindows.AutoSize = true;
            this.lblSwitchWindows.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitchWindows.Location = new Point(switchX, startY + spacing * 2);
            this.lblSwitchWindows.Name = "lblSwitchWindows";

            this.pnlSwitchWindows.Location = new Point(switchX, startY + spacing * 2 + 18);
            this.pnlSwitchWindows.Size = new Size(switchWidth, switchHeight);
            this.pnlSwitchWindows.BackColor = Color.LightGray;
            this.pnlSwitchWindows.BorderStyle = BorderStyle.FixedSingle;
            this.pnlSwitchWindows.Cursor = Cursors.Hand;
            this.pnlSwitchWindows.Paint += new PaintEventHandler(Switch_Paint);
            this.pnlSwitchWindows.Click += new EventHandler(SwitchWindows_Click);
            this.pnlSwitchWindows.Name = "pnlSwitchWindows";

            // Alarm Switch
            this.lblSwitchAlarm.Text = "Alarm";
            this.lblSwitchAlarm.AutoSize = true;
            this.lblSwitchAlarm.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitchAlarm.Location = new Point(switchX, startY + spacing * 3);
            this.lblSwitchAlarm.Name = "lblSwitchAlarm";

            this.pnlSwitchAlarm.Location = new Point(switchX, startY + spacing * 3 + 18);
            this.pnlSwitchAlarm.Size = new Size(switchWidth, switchHeight);
            this.pnlSwitchAlarm.BackColor = Color.LightGray;
            this.pnlSwitchAlarm.BorderStyle = BorderStyle.FixedSingle;
            this.pnlSwitchAlarm.Cursor = Cursors.Hand;
            this.pnlSwitchAlarm.Paint += new PaintEventHandler(Switch_Paint);
            this.pnlSwitchAlarm.Click += new EventHandler(SwitchAlarm_Click);
            this.pnlSwitchAlarm.Name = "pnlSwitchAlarm";

            // Initially hide switches
            ShowSwitches(false);
        }

        private void ShowSwitches(bool show)
        {
            lblSwitchPower.Visible = show;
            pnlSwitchPower.Visible = show;
            lblSwitchDoors.Visible = show;
            pnlSwitchDoors.Visible = show;
            lblSwitchWindows.Visible = show;
            pnlSwitchWindows.Visible = show;
            lblSwitchAlarm.Visible = show;
            pnlSwitchAlarm.Visible = show;
        }

        private void Switch_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            bool isOn = false;
            string leftText = "";
            string rightText = "";

            // Determine state and labels based on which switch
            if (panel == pnlSwitchPower)
            {
                isOn = switchPowerState;
                leftText = "OFF";
                rightText = "ON";
            }
            else if (panel == pnlSwitchDoors)
            {
                isOn = switchDoorsState;
                leftText = "Unlocked";
                rightText = "Locked";
            }
            else if (panel == pnlSwitchWindows)
            {
                isOn = switchWindowsState;
                leftText = "Down";
                rightText = "Up";
            }
            else if (panel == pnlSwitchAlarm)
            {
                isOn = switchAlarmState;
                leftText = "OFF";
                rightText = "ON";
            }

            // Draw the switch background
            int halfWidth = panel.Width / 2;
            Rectangle leftRect = new Rectangle(0, 0, halfWidth, panel.Height);
            Rectangle rightRect = new Rectangle(halfWidth, 0, halfWidth, panel.Height);

            // Highlight left side if off, right side if on
            if (isOn)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen), rightRect);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), leftRect);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGreen), leftRect);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), rightRect);
            }

            // Draw text
            using (Font font = new Font("Segoe UI", 8F, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // Left text
                e.Graphics.DrawString(leftText, font, Brushes.Black, leftRect, sf);
                // Right text
                e.Graphics.DrawString(rightText, font, Brushes.Black, rightRect, sf);
            }
        }
        private List<string> _activityLog = new List<string>();//added
        private void AddActivity(string message)//added method
        {
            string logEntry = $"{DateTime.Now:HH:mm:ss} - {message}";
            _activityLog.Add(logEntry);
            if (lstActivityLog != null)
            {
                lstActivityLog.Items.Insert(0, logEntry);
            }
        }
        private void SwitchPower_Click(object sender, EventArgs e)
        {
            switchPowerState = !switchPowerState;
            pnlSwitchPower.Invalidate();
            AddActivity($"Power turned {(switchPowerState ? "On" : "Off")}");//added one per click
        }

        private void SwitchDoors_Click(object sender, EventArgs e)
        {
            switchDoorsState = !switchDoorsState;
            pnlSwitchDoors.Invalidate();
            AddActivity($"Doors {(switchDoorsState ? "Locked" : "Unlocked")}");
        }

        private void SwitchWindows_Click(object sender, EventArgs e)
        {
            switchWindowsState = !switchWindowsState;
            pnlSwitchWindows.Invalidate();
            AddActivity($"Windows {(switchWindowsState ? "Up" : "Down")}");
        }

        private void SwitchAlarm_Click(object sender, EventArgs e)
        {
            switchAlarmState = !switchAlarmState;
            pnlSwitchAlarm.Invalidate();
            AddActivity($"Alarm is {(switchAlarmState ? "On" : "Off")}");
        }
    }
}
