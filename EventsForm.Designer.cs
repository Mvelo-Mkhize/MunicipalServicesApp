using System.Drawing;
using System.Windows.Forms;

namespace MunicipalServicesApp
{
    partial class EventsForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private GroupBox grpFilter;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Label lblKeyword;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnClear;
        private ListView lvEvents;
        private GroupBox grpRecommend;
        private ListBox lbRecommendations;
        private GroupBox grpAnnouncements;
        private ListBox lbAnnouncements;
        private GroupBox grpPriority;
        private ListBox lbPriority;
        private Label lblSearchHistory;
        private Button btnBackToMain;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.grpFilter = new GroupBox();
            this.btnClear = new Button();
            this.btnSearch = new Button();
            this.txtKeyword = new TextBox();
            this.lblKeyword = new Label();
            this.dtpTo = new DateTimePicker();
            this.lblTo = new Label();
            this.dtpFrom = new DateTimePicker();
            this.lblFrom = new Label();
            this.cmbCategory = new ComboBox();
            this.lblCategory = new Label();
            this.lvEvents = new ListView();
            this.grpRecommend = new GroupBox();
            this.lbRecommendations = new ListBox();
            this.grpAnnouncements = new GroupBox();
            this.lbAnnouncements = new ListBox();
            this.grpPriority = new GroupBox();
            this.lbPriority = new ListBox();
            this.lblSearchHistory = new Label();
            this.btnBackToMain = new Button();

            this.grpFilter.SuspendLayout();
            this.grpRecommend.SuspendLayout();
            this.grpAnnouncements.SuspendLayout();
            this.grpPriority.SuspendLayout();
            this.SuspendLayout();

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(420, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Local Events and Announcements";

            this.grpFilter.Controls.Add(this.btnClear);
            this.grpFilter.Controls.Add(this.btnSearch);
            this.grpFilter.Controls.Add(this.txtKeyword);
            this.grpFilter.Controls.Add(this.lblKeyword);
            this.grpFilter.Controls.Add(this.dtpTo);
            this.grpFilter.Controls.Add(this.lblTo);
            this.grpFilter.Controls.Add(this.dtpFrom);
            this.grpFilter.Controls.Add(this.lblFrom);
            this.grpFilter.Controls.Add(this.cmbCategory);
            this.grpFilter.Controls.Add(this.lblCategory);
            this.grpFilter.Location = new Point(12, 45);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new Size(920, 100);
            this.grpFilter.TabIndex = 1;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Search and Filter";

            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new Point(15, 30);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new Size(52, 13);
            this.lblCategory.Text = "Category:";

            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new Point(73, 27);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new Size(150, 21);
            this.cmbCategory.TabIndex = 1;

            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new Point(240, 30);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new Size(33, 13);
            this.lblFrom.Text = "From:";

            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Location = new Point(279, 27);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new Size(120, 20);
            this.dtpFrom.TabIndex = 2;

            this.lblTo.AutoSize = true;
            this.lblTo.Location = new Point(415, 30);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new Size(23, 13);
            this.lblTo.Text = "To:";

            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Location = new Point(444, 27);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new Size(120, 20);
            this.dtpTo.TabIndex = 3;

            this.lblKeyword.AutoSize = true;
            this.lblKeyword.Location = new Point(580, 30);
            this.lblKeyword.Name = "lblKeyword";
            this.lblKeyword.Size = new Size(56, 13);
            this.lblKeyword.Text = "Keyword:";

            this.txtKeyword.Location = new Point(642, 27);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new Size(160, 20);
            this.txtKeyword.TabIndex = 4;
            this.txtKeyword.KeyDown += new KeyEventHandler(this.txtKeyword_KeyDown);

            this.btnSearch.Location = new Point(18, 62);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(120, 28);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search Events";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            this.btnClear.Location = new Point(150, 62);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new Size(120, 28);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear Filters";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);

            this.lvEvents.Columns.Add("Title", 170);
            this.lvEvents.Columns.Add("Category", 95);
            this.lvEvents.Columns.Add("Date and Time", 120);
            this.lvEvents.Columns.Add("Location", 110);
            this.lvEvents.Columns.Add("Priority", 65);
            this.lvEvents.Columns.Add("Type", 85);
            this.lvEvents.FullRowSelect = true;
            this.lvEvents.GridLines = true;
            this.lvEvents.Location = new Point(12, 155);
            this.lvEvents.MultiSelect = false;
            this.lvEvents.Name = "lvEvents";
            this.lvEvents.Size = new Size(650, 320);
            this.lvEvents.TabIndex = 2;
            this.lvEvents.UseCompatibleStateImageBehavior = false;
            this.lvEvents.View = View.Details;
            this.lvEvents.MouseDoubleClick += new MouseEventHandler(this.lvEvents_MouseDoubleClick);

            this.grpRecommend.Controls.Add(this.lbRecommendations);
            this.grpRecommend.Location = new Point(668, 155);
            this.grpRecommend.Name = "grpRecommend";
            this.grpRecommend.Size = new Size(264, 320);
            this.grpRecommend.TabIndex = 3;
            this.grpRecommend.TabStop = false;
            this.grpRecommend.Text = "Recommended For You";

            this.lbRecommendations.IntegralHeight = false;
            this.lbRecommendations.ItemHeight = 13;
            this.lbRecommendations.Location = new Point(12, 22);
            this.lbRecommendations.Name = "lbRecommendations";
            this.lbRecommendations.Size = new Size(240, 286);
            this.lbRecommendations.TabIndex = 0;
            this.lbRecommendations.MouseDoubleClick += new MouseEventHandler(this.lbRecommendations_MouseDoubleClick);

            this.grpAnnouncements.Controls.Add(this.lbAnnouncements);
            this.grpAnnouncements.Location = new Point(12, 485);
            this.grpAnnouncements.Name = "grpAnnouncements";
            this.grpAnnouncements.Size = new Size(440, 160);
            this.grpAnnouncements.TabIndex = 4;
            this.grpAnnouncements.TabStop = false;
            this.grpAnnouncements.Text = "Latest Announcements";

            this.lbAnnouncements.IntegralHeight = false;
            this.lbAnnouncements.ItemHeight = 13;
            this.lbAnnouncements.Location = new Point(12, 22);
            this.lbAnnouncements.Name = "lbAnnouncements";
            this.lbAnnouncements.Size = new Size(416, 125);
            this.lbAnnouncements.TabIndex = 0;

            this.grpPriority.Controls.Add(this.lbPriority);
            this.grpPriority.Location = new Point(460, 485);
            this.grpPriority.Name = "grpPriority";
            this.grpPriority.Size = new Size(472, 160);
            this.grpPriority.TabIndex = 5;
            this.grpPriority.TabStop = false;
            this.grpPriority.Text = "Top Priority Upcoming";

            this.lbPriority.IntegralHeight = false;
            this.lbPriority.ItemHeight = 13;
            this.lbPriority.Location = new Point(12, 22);
            this.lbPriority.Name = "lbPriority";
            this.lbPriority.Size = new Size(448, 125);
            this.lbPriority.TabIndex = 0;

            this.lblSearchHistory.AutoEllipsis = true;
            this.lblSearchHistory.AutoSize = false;
            this.lblSearchHistory.Location = new Point(12, 652);
            this.lblSearchHistory.Name = "lblSearchHistory";
            this.lblSearchHistory.Size = new Size(780, 20);
            this.lblSearchHistory.TabIndex = 6;
            this.lblSearchHistory.Text = "Recent searches: none yet";

            this.btnBackToMain.Location = new Point(810, 648);
            this.btnBackToMain.Name = "btnBackToMain";
            this.btnBackToMain.Size = new Size(122, 30);
            this.btnBackToMain.TabIndex = 7;
            this.btnBackToMain.Text = "Back to Main Menu";
            this.btnBackToMain.UseVisualStyleBackColor = true;
            this.btnBackToMain.Click += new System.EventHandler(this.btnBackToMain_Click);

            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(950, 690);
            this.Controls.Add(this.btnBackToMain);
            this.Controls.Add(this.lblSearchHistory);
            this.Controls.Add(this.grpPriority);
            this.Controls.Add(this.grpAnnouncements);
            this.Controls.Add(this.grpRecommend);
            this.Controls.Add(this.lvEvents);
            this.Controls.Add(this.grpFilter);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EventsForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Local Events and Announcements";

            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.grpRecommend.ResumeLayout(false);
            this.grpAnnouncements.ResumeLayout(false);
            this.grpPriority.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}