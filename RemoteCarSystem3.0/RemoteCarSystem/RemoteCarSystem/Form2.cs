using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
        private ListBox lstActivityLog;
        private List<string> _activityLog = new List<string>();
        
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
        
        // Status information labels
        private Label lblInTemp;
        private Label lblInTempValue;
        private Label lblOutTemp;
        private Label lblOutTempValue;
        private Label lblFuel;
        private Label lblFuelValue;
        private Label lblOil;
        private Label lblOilValue;
        
        // Settings page menu items
        private Label lblSettingsAccount;
        private Label lblSettingsNotifications;
        private Label lblSettingsDrivers;
        private Label lblSettingsVehicles;
        private Label lblSettingsAppearance;
        private Label lblSettingsMisc;
        private Label lblSettingsLogout;
        private Button btnSettingsAccount;
        private Button btnSettingsNotifications;
        private Button btnSettingsDrivers;
        private Button btnSettingsVehicles;
        private Button btnSettingsAppearance;
        private Button btnSettingsMisc;
        private Button btnSettingsLogout;
        private Panel pnlSeparator1;
        private Panel pnlSeparator2;
        private Panel pnlSeparator3;
        private Panel pnlSeparator4;
        private Panel pnlSeparator5;
        private Panel pnlSeparator6;

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
            this.lstActivityLog = new System.Windows.Forms.ListBox();
            
            // Toggle switches
            this.lblSwitchPower = new System.Windows.Forms.Label();
            this.pnlSwitchPower = new System.Windows.Forms.Panel();
            this.lblSwitchDoors = new System.Windows.Forms.Label();
            this.pnlSwitchDoors = new System.Windows.Forms.Panel();
            this.lblSwitchWindows = new System.Windows.Forms.Label();
            this.pnlSwitchWindows = new System.Windows.Forms.Panel();
            this.lblSwitchAlarm = new System.Windows.Forms.Label();
            this.pnlSwitchAlarm = new System.Windows.Forms.Panel();
            
            // Status information labels
            this.lblInTemp = new System.Windows.Forms.Label();
            this.lblInTempValue = new System.Windows.Forms.Label();
            this.lblOutTemp = new System.Windows.Forms.Label();
            this.lblOutTempValue = new System.Windows.Forms.Label();
            this.lblFuel = new System.Windows.Forms.Label();
            this.lblFuelValue = new System.Windows.Forms.Label();
            this.lblOil = new System.Windows.Forms.Label();
            this.lblOilValue = new System.Windows.Forms.Label();
            
            // Settings menu controls
            this.lblSettingsAccount = new System.Windows.Forms.Label();
            this.lblSettingsNotifications = new System.Windows.Forms.Label();
            this.lblSettingsDrivers = new System.Windows.Forms.Label();
            this.lblSettingsVehicles = new System.Windows.Forms.Label();
            this.lblSettingsAppearance = new System.Windows.Forms.Label();
            this.lblSettingsMisc = new System.Windows.Forms.Label();
            this.lblSettingsLogout = new System.Windows.Forms.Label();
            this.btnSettingsAccount = new System.Windows.Forms.Button();
            this.btnSettingsNotifications = new System.Windows.Forms.Button();
            this.btnSettingsDrivers = new System.Windows.Forms.Button();
            this.btnSettingsVehicles = new System.Windows.Forms.Button();
            this.btnSettingsAppearance = new System.Windows.Forms.Button();
            this.btnSettingsMisc = new System.Windows.Forms.Button();
            this.btnSettingsLogout = new System.Windows.Forms.Button();
            this.pnlSeparator1 = new System.Windows.Forms.Panel();
            this.pnlSeparator2 = new System.Windows.Forms.Panel();
            this.pnlSeparator3 = new System.Windows.Forms.Panel();
            this.pnlSeparator4 = new System.Windows.Forms.Panel();
            this.pnlSeparator5 = new System.Windows.Forms.Panel();
            this.pnlSeparator6 = new System.Windows.Forms.Panel();

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
            this.btnLeft.Location = new System.Drawing.Point(10, 330);
            this.btnLeft.Size = new System.Drawing.Size(60, 60);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);

            // btnRight (forward arrow)
            this.btnRight.Text = ">";
            this.btnRight.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.btnRight.Location = new System.Drawing.Point(290, 330);
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

            // Initialize status information labels
            InitializeStatusLabels();
            
            // Initialize toggle switches
            InitializeSwitches();
            
            // Initialize settings menu
            InitializeSettingsMenu();

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
            
            // Add status information labels
            this.Controls.Add(this.lblInTemp);
            this.Controls.Add(this.lblInTempValue);
            this.Controls.Add(this.lblOutTemp);
            this.Controls.Add(this.lblOutTempValue);
            this.Controls.Add(this.lblFuel);
            this.Controls.Add(this.lblFuelValue);
            this.Controls.Add(this.lblOil);
            this.Controls.Add(this.lblOilValue);
            
            // Add settings menu controls
            this.Controls.Add(this.lblSettingsAccount);
            this.Controls.Add(this.lblSettingsNotifications);
            this.Controls.Add(this.lblSettingsDrivers);
            this.Controls.Add(this.lblSettingsVehicles);
            this.Controls.Add(this.lblSettingsAppearance);
            this.Controls.Add(this.lblSettingsMisc);
            this.Controls.Add(this.lblSettingsLogout);
            this.Controls.Add(this.btnSettingsAccount);
            this.Controls.Add(this.btnSettingsNotifications);
            this.Controls.Add(this.btnSettingsDrivers);
            this.Controls.Add(this.btnSettingsVehicles);
            this.Controls.Add(this.btnSettingsAppearance);
            this.Controls.Add(this.btnSettingsMisc);
            this.Controls.Add(this.btnSettingsLogout);
            this.Controls.Add(this.pnlSeparator1);
            this.Controls.Add(this.pnlSeparator2);
            this.Controls.Add(this.pnlSeparator3);
            this.Controls.Add(this.pnlSeparator4);
            this.Controls.Add(this.pnlSeparator5);
            this.Controls.Add(this.pnlSeparator6);
            
            // Activity log
            this.lstActivityLog.Location = new System.Drawing.Point(70, 70);
            this.lstActivityLog.Size = new System.Drawing.Size(220, 480);
            this.lstActivityLog.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lstActivityLog.Visible = false;
            this.lstActivityLog.Name = "lstActivityLog";
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
                    ShowStatusLabels(true);
                    ShowSwitches(true);
                    ShowSettingsMenu(false);
                    lstActivityLog.Visible = false;
                    break;
                case Page.Location:
                    lblPageTitle.Text = "Location";
                    btnLeft.Visible = true;
                    btnRight.Visible = true;
                    picMap.Visible = true;
                    pnlVehicleBox.Visible = false;
                    ShowStatusLabels(false);
                    ShowSwitches(false);
                    ShowSettingsMenu(false);
                    lstActivityLog.Visible = false;
                    break;
                case Page.ActivityLog:
                    lblPageTitle.Text = "Activity Log";
                    btnLeft.Visible = true;
                    btnRight.Visible = true;
                    picMap.Visible = false;
                    pnlVehicleBox.Visible = false;
                    ShowStatusLabels(false);
                    ShowSwitches(false);
                    ShowSettingsMenu(false);
                    lstActivityLog.Visible = true;
                    break;
                case Page.Settings:
                    lblPageTitle.Text = "Settings";
                    btnLeft.Visible = true;
                    btnRight.Visible = false;
                    picMap.Visible = false;
                    pnlVehicleBox.Visible = false;
                    ShowStatusLabels(false);
                    ShowSwitches(false);
                    ShowSettingsMenu(true);
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
        
        private string GetDriverDataPath()
        {
            // Resolve full path to driverdata.txt (same pattern as Form1)
            // EXE is in ...\RemoteCarSystem\RemoteCarSystem\RemoteCarSystem\bin\Debug
            // File is in ...\RemoteCarSystem\RemoteCarSystem\driverdata.txt
            return Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "driverdata.txt"));
        }

        private void PositionVehicleBox()
        {
            // Get actual image dimensions to size the box accordingly
            int boxWidth = 320;
            int boxHeight = 240;
            
            if (picVehicle.Image != null)
            {
                // Get the actual image dimensions
                int imageWidth = picVehicle.Image.Width;
                int imageHeight = picVehicle.Image.Height;
                
                // Calculate aspect ratio
                double aspectRatio = (double)imageWidth / imageHeight;
                
                // Maximum dimensions that fit nicely on the form
                // Form is 360 wide, leave some margin, so max width ~340
                // Position below title (y=70), leave space for status labels below, so max height ~200
                int maxWidth = 340;
                int maxHeight = 200;
                
                // Calculate box dimensions maintaining aspect ratio
                if (aspectRatio > 1)
                {
                    // Landscape image
                    boxWidth = Math.Min(maxWidth, (int)(maxHeight * aspectRatio));
                    boxHeight = (int)(boxWidth / aspectRatio);
                }
                else
                {
                    // Portrait or square image
                    boxHeight = Math.Min(maxHeight, (int)(maxWidth / aspectRatio));
                    boxWidth = (int)(boxHeight * aspectRatio);
                }
            }
            
            // Center the box horizontally on the form (360 wide)
            int boxX = (360 - boxWidth) / 2;
            
            // Position on top half of page (below title, which ends around y=60)
            int boxY = 70;
            
            pnlVehicleBox.Location = new System.Drawing.Point(boxX, boxY);
            pnlVehicleBox.Size = new System.Drawing.Size(boxWidth, boxHeight);
            
            // Size the picture box to fit inside with 2px border on all sides
            picVehicle.Location = new System.Drawing.Point(2, 2);
            picVehicle.Size = new System.Drawing.Size(boxWidth - 4, boxHeight - 4);
            
            // Position status labels below the vehicle box
            PositionStatusLabels();
        }
        
        private void PositionStatusLabels()
        {
            // Position status labels below the vehicle box with some spacing
            int spacingFromBox = 10;
            int statusStartY = pnlVehicleBox.Location.Y + pnlVehicleBox.Height + spacingFromBox;
            int statusSpacing = 35; // Horizontal spacing between items (reduced to fit on screen)
            int statusItemWidth = 60; // Width for each status item (reduced to fit on screen)
            int statusLabelHeight = 20;
            int statusValueHeight = 25;
            
            // Calculate starting X position to center the 4 items
            int totalWidth = statusItemWidth * 4 + statusSpacing * 3;
            int startX = (360 - totalWidth) / 2;
            
            // Update positions
            this.lblInTemp.Location = new Point(startX, statusStartY);
            this.lblInTempValue.Location = new Point(startX, statusStartY + statusLabelHeight);
            
            this.lblOutTemp.Location = new Point(startX + statusItemWidth + statusSpacing, statusStartY);
            this.lblOutTempValue.Location = new Point(startX + statusItemWidth + statusSpacing, statusStartY + statusLabelHeight);
            
            this.lblFuel.Location = new Point(startX + (statusItemWidth + statusSpacing) * 2, statusStartY);
            this.lblFuelValue.Location = new Point(startX + (statusItemWidth + statusSpacing) * 2, statusStartY + statusLabelHeight);
            
            this.lblOil.Location = new Point(startX + (statusItemWidth + statusSpacing) * 3, statusStartY);
            this.lblOilValue.Location = new Point(startX + (statusItemWidth + statusSpacing) * 3, statusStartY + statusLabelHeight);
            
            // Position switches below status labels
            PositionSwitches();
        }
        
        private void PositionSwitches()
        {
            // Calculate where status labels end
            int statusEndY = lblInTempValue.Location.Y + lblInTempValue.Height;
            int spacingFromStatus = 15;
            int startY = statusEndY + spacingFromStatus;
            int switchWidth = 280;
            int switchHeight = 35;
            int switchX = (360 - switchWidth) / 2;
            int spacing = 50;
            
            // Update switch positions
            this.lblSwitchPower.Location = new Point(switchX, startY);
            this.pnlSwitchPower.Location = new Point(switchX, startY + 18);
            
            this.lblSwitchDoors.Location = new Point(switchX, startY + spacing);
            this.pnlSwitchDoors.Location = new Point(switchX, startY + spacing + 18);
            
            this.lblSwitchWindows.Location = new Point(switchX, startY + spacing * 2);
            this.pnlSwitchWindows.Location = new Point(switchX, startY + spacing * 2 + 18);
            
            this.lblSwitchAlarm.Location = new Point(switchX, startY + spacing * 3);
            this.pnlSwitchAlarm.Location = new Point(switchX, startY + spacing * 3 + 18);
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

        private void InitializeStatusLabels()
        {
            // Position status labels below the vehicle box
            // Vehicle box ends around y=270 (70 + 200 max height), status labels start at y=280
            int statusStartY = 280;
            int statusSpacing = 35; // Horizontal spacing between items (reduced to fit on screen)
            int statusItemWidth = 60; // Width for each status item (reduced to fit on screen)
            int statusLabelHeight = 20;
            int statusValueHeight = 25;
            
            // Calculate starting X position to center the 4 items
            int totalWidth = statusItemWidth * 4 + statusSpacing * 3;
            int startX = (360 - totalWidth) / 2;
            
            // In Temp
            this.lblInTemp.Text = "In Temp";
            this.lblInTemp.AutoSize = false;
            this.lblInTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInTemp.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            this.lblInTemp.Location = new Point(startX, statusStartY);
            this.lblInTemp.Size = new Size(statusItemWidth, statusLabelHeight);
            this.lblInTemp.Name = "lblInTemp";
            
            this.lblInTempValue.Text = "75°";
            this.lblInTempValue.AutoSize = false;
            this.lblInTempValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblInTempValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblInTempValue.Location = new Point(startX, statusStartY + statusLabelHeight);
            this.lblInTempValue.Size = new Size(statusItemWidth, statusValueHeight);
            this.lblInTempValue.Name = "lblInTempValue";
            
            // Out Temp
            this.lblOutTemp.Text = "Out Temp";
            this.lblOutTemp.AutoSize = false;
            this.lblOutTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOutTemp.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            this.lblOutTemp.Location = new Point(startX + statusItemWidth + statusSpacing, statusStartY);
            this.lblOutTemp.Size = new Size(statusItemWidth, statusLabelHeight);
            this.lblOutTemp.Name = "lblOutTemp";
            
            this.lblOutTempValue.Text = "56°";
            this.lblOutTempValue.AutoSize = false;
            this.lblOutTempValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOutTempValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblOutTempValue.Location = new Point(startX + statusItemWidth + statusSpacing, statusStartY + statusLabelHeight);
            this.lblOutTempValue.Size = new Size(statusItemWidth, statusValueHeight);
            this.lblOutTempValue.Name = "lblOutTempValue";
            
            // Fuel
            this.lblFuel.Text = "Fuel";
            this.lblFuel.AutoSize = false;
            this.lblFuel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFuel.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            this.lblFuel.Location = new Point(startX + (statusItemWidth + statusSpacing) * 2, statusStartY);
            this.lblFuel.Size = new Size(statusItemWidth, statusLabelHeight);
            this.lblFuel.Name = "lblFuel";
            
            this.lblFuelValue.Text = "88%";
            this.lblFuelValue.AutoSize = false;
            this.lblFuelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFuelValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblFuelValue.Location = new Point(startX + (statusItemWidth + statusSpacing) * 2, statusStartY + statusLabelHeight);
            this.lblFuelValue.Size = new Size(statusItemWidth, statusValueHeight);
            this.lblFuelValue.Name = "lblFuelValue";
            
            // Oil
            this.lblOil.Text = "Oil";
            this.lblOil.AutoSize = false;
            this.lblOil.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOil.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            this.lblOil.Location = new Point(startX + (statusItemWidth + statusSpacing) * 3, statusStartY);
            this.lblOil.Size = new Size(statusItemWidth, statusLabelHeight);
            this.lblOil.Name = "lblOil";
            
            this.lblOilValue.Text = "64%";
            this.lblOilValue.AutoSize = false;
            this.lblOilValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOilValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblOilValue.Location = new Point(startX + (statusItemWidth + statusSpacing) * 3, statusStartY + statusLabelHeight);
            this.lblOilValue.Size = new Size(statusItemWidth, statusValueHeight);
            this.lblOilValue.Name = "lblOilValue";
            
            // Initially hide status labels
            ShowStatusLabels(false);
        }
        
        private void ShowStatusLabels(bool show)
        {
            lblInTemp.Visible = show;
            lblInTempValue.Visible = show;
            lblOutTemp.Visible = show;
            lblOutTempValue.Visible = show;
            lblFuel.Visible = show;
            lblFuelValue.Visible = show;
            lblOil.Visible = show;
            lblOilValue.Visible = show;
        }
        
        private void InitializeSettingsMenu()
        {
            // Settings menu starts below title (around y=70)
            int startY = 70;
            int itemHeight = 50;
            int leftMargin = 80; // Left margin to avoid arrow overlap
            int arrowButtonSize = 30;
            int arrowRightMargin = 10; // Margin from right edge
            int separatorHeight = 1;
            int fontSize = 11;
            
            // Account
            this.lblSettingsAccount.Text = "Account";
            this.lblSettingsAccount.AutoSize = false;
            this.lblSettingsAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsAccount.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsAccount.Location = new Point(leftMargin, startY);
            this.lblSettingsAccount.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsAccount.Name = "lblSettingsAccount";
            
            this.btnSettingsAccount.Text = ">";
            this.btnSettingsAccount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsAccount.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsAccount.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsAccount.Name = "btnSettingsAccount";
            this.btnSettingsAccount.FlatStyle = FlatStyle.Flat;
            this.btnSettingsAccount.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator1.BackColor = Color.LightGray;
            this.pnlSeparator1.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator1.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator1.Name = "pnlSeparator1";
            
            // Notifications
            startY += itemHeight + 5;
            this.lblSettingsNotifications.Text = "Notifications";
            this.lblSettingsNotifications.AutoSize = false;
            this.lblSettingsNotifications.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsNotifications.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsNotifications.Location = new Point(leftMargin, startY);
            this.lblSettingsNotifications.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsNotifications.Name = "lblSettingsNotifications";
            
            this.btnSettingsNotifications.Text = ">";
            this.btnSettingsNotifications.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsNotifications.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsNotifications.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsNotifications.Name = "btnSettingsNotifications";
            this.btnSettingsNotifications.FlatStyle = FlatStyle.Flat;
            this.btnSettingsNotifications.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator2.BackColor = Color.LightGray;
            this.pnlSeparator2.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator2.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator2.Name = "pnlSeparator2";
            
            // Drivers
            startY += itemHeight + 5;
            this.lblSettingsDrivers.Text = "Drivers";
            this.lblSettingsDrivers.AutoSize = false;
            this.lblSettingsDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsDrivers.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsDrivers.Location = new Point(leftMargin, startY);
            this.lblSettingsDrivers.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsDrivers.Name = "lblSettingsDrivers";
            
            this.btnSettingsDrivers.Text = ">";
            this.btnSettingsDrivers.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsDrivers.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsDrivers.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsDrivers.Name = "btnSettingsDrivers";
            this.btnSettingsDrivers.FlatStyle = FlatStyle.Flat;
            this.btnSettingsDrivers.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator3.BackColor = Color.LightGray;
            this.pnlSeparator3.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator3.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator3.Name = "pnlSeparator3";
            
            // Vehicles
            startY += itemHeight + 5;
            this.lblSettingsVehicles.Text = "Vehicles";
            this.lblSettingsVehicles.AutoSize = false;
            this.lblSettingsVehicles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsVehicles.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsVehicles.Location = new Point(leftMargin, startY);
            this.lblSettingsVehicles.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsVehicles.Name = "lblSettingsVehicles";
            
            this.btnSettingsVehicles.Text = ">";
            this.btnSettingsVehicles.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsVehicles.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsVehicles.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsVehicles.Name = "btnSettingsVehicles";
            this.btnSettingsVehicles.FlatStyle = FlatStyle.Flat;
            this.btnSettingsVehicles.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator4.BackColor = Color.LightGray;
            this.pnlSeparator4.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator4.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator4.Name = "pnlSeparator4";
            
            // Appearance
            startY += itemHeight + 5;
            this.lblSettingsAppearance.Text = "Appearance";
            this.lblSettingsAppearance.AutoSize = false;
            this.lblSettingsAppearance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsAppearance.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsAppearance.Location = new Point(leftMargin, startY);
            this.lblSettingsAppearance.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsAppearance.Name = "lblSettingsAppearance";
            
            this.btnSettingsAppearance.Text = ">";
            this.btnSettingsAppearance.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsAppearance.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsAppearance.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsAppearance.Name = "btnSettingsAppearance";
            this.btnSettingsAppearance.FlatStyle = FlatStyle.Flat;
            this.btnSettingsAppearance.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator5.BackColor = Color.LightGray;
            this.pnlSeparator5.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator5.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator5.Name = "pnlSeparator5";
            
            // Misc
            startY += itemHeight + 5;
            this.lblSettingsMisc.Text = "Misc";
            this.lblSettingsMisc.AutoSize = false;
            this.lblSettingsMisc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsMisc.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsMisc.Location = new Point(leftMargin, startY);
            this.lblSettingsMisc.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsMisc.Name = "lblSettingsMisc";
            
            this.btnSettingsMisc.Text = ">";
            this.btnSettingsMisc.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsMisc.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsMisc.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsMisc.Name = "btnSettingsMisc";
            this.btnSettingsMisc.FlatStyle = FlatStyle.Flat;
            this.btnSettingsMisc.FlatAppearance.BorderSize = 0;
            
            this.pnlSeparator6.BackColor = Color.LightGray;
            this.pnlSeparator6.Location = new Point(leftMargin, startY + itemHeight);
            this.pnlSeparator6.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, separatorHeight);
            this.pnlSeparator6.Name = "pnlSeparator6";
            
            // Logout
            startY += itemHeight + 5;
            this.lblSettingsLogout.Text = "Logout";
            this.lblSettingsLogout.AutoSize = false;
            this.lblSettingsLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSettingsLogout.Font = new Font("Segoe UI", fontSize, FontStyle.Regular);
            this.lblSettingsLogout.Location = new Point(leftMargin, startY);
            this.lblSettingsLogout.Size = new Size(360 - leftMargin - arrowButtonSize - arrowRightMargin - 10, itemHeight);
            this.lblSettingsLogout.Name = "lblSettingsLogout";
            
            this.btnSettingsLogout.Text = ">";
            this.btnSettingsLogout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.btnSettingsLogout.Location = new Point(360 - arrowButtonSize - arrowRightMargin, startY + (itemHeight - arrowButtonSize) / 2);
            this.btnSettingsLogout.Size = new Size(arrowButtonSize, arrowButtonSize);
            this.btnSettingsLogout.Name = "btnSettingsLogout";
            this.btnSettingsLogout.FlatStyle = FlatStyle.Flat;
            this.btnSettingsLogout.FlatAppearance.BorderSize = 0;
            
            // Wire up click handlers
            this.btnSettingsAccount.Click += new EventHandler(btnSettingsAccount_Click);
            this.btnSettingsNotifications.Click += new EventHandler(btnSettingsNotifications_Click);
            this.btnSettingsDrivers.Click += new EventHandler(btnSettingsDrivers_Click);
            this.btnSettingsLogout.Click += new EventHandler(btnSettingsLogout_Click);
            
            // Initially hide settings menu
            ShowSettingsMenu(false);
        }
        
        private void ShowSettingsMenu(bool show)
        {
            lblSettingsAccount.Visible = show;
            lblSettingsNotifications.Visible = show;
            lblSettingsDrivers.Visible = show;
            lblSettingsVehicles.Visible = show;
            lblSettingsAppearance.Visible = show;
            lblSettingsMisc.Visible = show;
            lblSettingsLogout.Visible = show;
            btnSettingsAccount.Visible = show;
            btnSettingsNotifications.Visible = show;
            btnSettingsDrivers.Visible = show;
            btnSettingsVehicles.Visible = show;
            btnSettingsAppearance.Visible = show;
            btnSettingsMisc.Visible = show;
            btnSettingsLogout.Visible = show;
            pnlSeparator1.Visible = show;
            pnlSeparator2.Visible = show;
            pnlSeparator3.Visible = show;
            pnlSeparator4.Visible = show;
            pnlSeparator5.Visible = show;
            pnlSeparator6.Visible = show;
        }

        private void InitializeSwitches()
        {
            // Initialize switch properties (positions will be set by PositionSwitches)
            int switchWidth = 280;
            int switchHeight = 35;

            // Power Switch
            this.lblSwitchPower.Text = "Power";
            this.lblSwitchPower.AutoSize = true;
            this.lblSwitchPower.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.lblSwitchPower.Name = "lblSwitchPower";

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
            this.lblSwitchDoors.Name = "lblSwitchDoors";

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
            this.lblSwitchWindows.Name = "lblSwitchWindows";

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
            this.lblSwitchAlarm.Name = "lblSwitchAlarm";

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

        private void AddActivity(string message)
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
            AddActivity($"Power turned {(switchPowerState ? "On" : "Off")}");
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
        
        private void btnSettingsAccount_Click(object sender, EventArgs e)
        {
            var accountForm = new Form3_Account(_userName, GetDriverDataPath());
            accountForm.ShowDialog();
        }
        
        private void btnSettingsNotifications_Click(object sender, EventArgs e)
        {
            var notificationsForm = new Form3_Notifications();
            notificationsForm.ShowDialog();
        }
        
        private void btnSettingsDrivers_Click(object sender, EventArgs e)
        {
            var driversForm = new Form3_Drivers(GetDriverDataPath());
            driversForm.ShowDialog();
        }
        
        private void btnSettingsLogout_Click(object sender, EventArgs e)
        {
            var logoutForm = new Form3_Logout();
            if (logoutForm.ShowDialog() == DialogResult.OK)
            {
                // User confirmed logout - reopen Form1 and close Form2
                var form1 = new Form1();
                form1.Show();
                this.Close();
            }
        }
    }
}
