namespace LegatroExp.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _menuStrip = new MenuStrip();
        _mnuFile = new ToolStripMenuItem();
        _mnuFileNew = new ToolStripMenuItem();
        _mnuFileOpen = new ToolStripMenuItem();
        _mnuFileSep1 = new ToolStripSeparator();
        _mnuFileQuit = new ToolStripMenuItem();
        _mnuEdit = new ToolStripMenuItem();
        _mnuEditGroups = new ToolStripMenuItem();
        _mnuEditProjects = new ToolStripMenuItem();
        _mnuEditCategories = new ToolStripMenuItem();
        _mnuEditTasks = new ToolStripMenuItem();
        _mnuTools = new ToolStripMenuItem();
        _mnuToolsOptions = new ToolStripMenuItem();
        _statusStrip = new StatusStrip();
        _lblDatabase = new ToolStripStatusLabel();
        _lblSessionStart = new ToolStripStatusLabel();
        _lblStatus = new ToolStripStatusLabel();
        _lblCurrentDate = new ToolStripStatusLabel();
        _lblCurrentTime = new ToolStripStatusLabel();
        _scMain = new SplitContainer();
        _tvwTasks = new TreeView();
        _scRight = new SplitContainer();
        _pnlTaskDetails = new Panel();
        _lblPlaceholder = new Label();
        _pnlTimeTracking = new Panel();
        _lblTimePlaceholder = new Label();

        _menuStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_scMain).BeginInit();
        _scMain.Panel1.SuspendLayout();
        _scMain.Panel2.SuspendLayout();
        _scMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_scRight).BeginInit();
        _scRight.Panel1.SuspendLayout();
        _scRight.Panel2.SuspendLayout();
        _scRight.SuspendLayout();
        _pnlTaskDetails.SuspendLayout();
        _pnlTimeTracking.SuspendLayout();
        SuspendLayout();

        _menuStrip.Items.AddRange(new ToolStripItem[] { _mnuFile, _mnuEdit, _mnuTools });
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Size = new Size(1200, 28);
        _menuStrip.TabIndex = 0;

        _mnuFile.DropDownItems.AddRange(new ToolStripItem[] { _mnuFileNew, _mnuFileOpen, _mnuFileSep1, _mnuFileQuit });
        _mnuFile.Name = "_mnuFile";
        _mnuFile.Size = new Size(46, 24);
        _mnuFile.Text = "&File";

        _mnuFileNew.Name = "_mnuFileNew";
        _mnuFileNew.Size = new Size(180, 26);
        _mnuFileNew.Text = "&New Solution";

        _mnuFileOpen.Name = "_mnuFileOpen";
        _mnuFileOpen.Size = new Size(180, 26);
        _mnuFileOpen.Text = "&Open Solution";

        _mnuFileSep1.Name = "_mnuFileSep1";
        _mnuFileSep1.Size = new Size(177, 6);

        _mnuFileQuit.Name = "_mnuFileQuit";
        _mnuFileQuit.Size = new Size(180, 26);
        _mnuFileQuit.Text = "&Quit";

        _mnuEdit.DropDownItems.AddRange(new ToolStripItem[] { _mnuEditGroups, _mnuEditProjects, _mnuEditCategories, _mnuEditTasks });
        _mnuEdit.Name = "_mnuEdit";
        _mnuEdit.Size = new Size(49, 24);
        _mnuEdit.Text = "&Edit";

        _mnuEditGroups.Name = "_mnuEditGroups";
        _mnuEditGroups.Size = new Size(180, 26);
        _mnuEditGroups.Text = "&Groups...";

        _mnuEditProjects.Name = "_mnuEditProjects";
        _mnuEditProjects.Size = new Size(180, 26);
        _mnuEditProjects.Text = "&Projects...";

        _mnuEditCategories.Name = "_mnuEditCategories";
        _mnuEditCategories.Size = new Size(180, 26);
        _mnuEditCategories.Text = "&Categories...";

        _mnuEditTasks.Name = "_mnuEditTasks";
        _mnuEditTasks.Size = new Size(180, 26);
        _mnuEditTasks.Text = "&Tasks...";

        _mnuTools.DropDownItems.AddRange(new ToolStripItem[] { _mnuToolsOptions });
        _mnuTools.Name = "_mnuTools";
        _mnuTools.Size = new Size(58, 24);
        _mnuTools.Text = "&Tools";

        _mnuToolsOptions.Name = "_mnuToolsOptions";
        _mnuToolsOptions.Size = new Size(180, 26);
        _mnuToolsOptions.Text = "&Options...";

        _statusStrip.Items.AddRange(new ToolStripItem[] { _lblDatabase, _lblSessionStart, _lblStatus, _lblCurrentDate, _lblCurrentTime });
        _statusStrip.Location = new Point(0, 736);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Size = new Size(1200, 24);
        _statusStrip.TabIndex = 2;

        _lblDatabase.Name = "_lblDatabase";
        _lblDatabase.Size = new Size(70, 19);
        _lblDatabase.Text = "Database: ";

        _lblSessionStart.Name = "_lblSessionStart";
        _lblSessionStart.Size = new Size(103, 19);
        _lblSessionStart.Text = "Session Start: ";

        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(910, 19);
        _lblStatus.Spring = true;
        _lblStatus.Text = "Ready";
        _lblStatus.TextAlign = ContentAlignment.MiddleLeft;

        _lblCurrentDate.Name = "_lblCurrentDate";
        _lblCurrentDate.Size = new Size(70, 19);
        _lblCurrentDate.Text = "Date";

        _lblCurrentTime.Name = "_lblCurrentTime";
        _lblCurrentTime.Size = new Size(70, 19);
        _lblCurrentTime.Text = "Time";

        _scMain.Dock = DockStyle.Fill;
        _scMain.Location = new Point(0, 28);
        _scMain.Name = "_scMain";
        _scMain.Orientation = Orientation.Vertical;

        _scMain.Panel1.Controls.Add(_tvwTasks);

        _scMain.Panel2.Controls.Add(_scRight);
        _scMain.Size = new Size(1200, 708);
        _scMain.SplitterDistance = 300;
        _scMain.TabIndex = 1;

        _tvwTasks.Dock = DockStyle.Fill;
        _tvwTasks.Location = new Point(0, 0);
        _tvwTasks.Name = "_tvwTasks";
        _tvwTasks.Size = new Size(300, 708);
        _tvwTasks.TabIndex = 0;

        _scRight.Dock = DockStyle.Fill;
        _scRight.Location = new Point(0, 0);
        _scRight.Name = "_scRight";
        _scRight.Orientation = Orientation.Horizontal;

        _scRight.Panel1.Controls.Add(_pnlTaskDetails);

        _scRight.Panel2.Controls.Add(_pnlTimeTracking);
        _scRight.Size = new Size(896, 708);
        _scRight.SplitterDistance = 424;
        _scRight.TabIndex = 0;

        _pnlTaskDetails.Dock = DockStyle.Fill;
        _pnlTaskDetails.Location = new Point(0, 0);
        _pnlTaskDetails.Name = "_pnlTaskDetails";
        _pnlTaskDetails.Size = new Size(896, 424);
        _pnlTaskDetails.TabIndex = 0;

        _lblPlaceholder.Dock = DockStyle.Fill;
        _lblPlaceholder.Location = new Point(0, 0);
        _lblPlaceholder.Name = "_lblPlaceholder";
        _lblPlaceholder.Size = new Size(896, 424);
        _lblPlaceholder.TabIndex = 0;
        _lblPlaceholder.Text = "Task Details Panel (To Be Implemented)";
        _lblPlaceholder.TextAlign = ContentAlignment.MiddleCenter;

        _pnlTaskDetails.Controls.Add(_lblPlaceholder);

        _pnlTimeTracking.Dock = DockStyle.Fill;
        _pnlTimeTracking.Location = new Point(0, 0);
        _pnlTimeTracking.Name = "_pnlTimeTracking";
        _pnlTimeTracking.Size = new Size(896, 280);
        _pnlTimeTracking.TabIndex = 0;

        _lblTimePlaceholder.Dock = DockStyle.Fill;
        _lblTimePlaceholder.Location = new Point(0, 0);
        _lblTimePlaceholder.Name = "_lblTimePlaceholder";
        _lblTimePlaceholder.Size = new Size(896, 280);
        _lblTimePlaceholder.TabIndex = 0;
        _lblTimePlaceholder.Text = "Time Tracking Panel (To Be Implemented)";
        _lblTimePlaceholder.TextAlign = ContentAlignment.MiddleCenter;

        _pnlTimeTracking.Controls.Add(_lblTimePlaceholder);

        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(1200, 760);
        Controls.Add(_scMain);
        Controls.Add(_menuStrip);
        Controls.Add(_statusStrip);
        MainMenuStrip = _menuStrip;
        MinimumSize = new Size(800, 600);
        Name = "MainForm";
        Text = "Legatro Experimental";

        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        _scMain.Panel1.ResumeLayout(false);
        _scMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_scMain).EndInit();
        _scMain.ResumeLayout(false);
        _scRight.Panel1.ResumeLayout(false);
        _scRight.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_scRight).EndInit();
        _scRight.ResumeLayout(false);
        _pnlTaskDetails.ResumeLayout(false);
        _pnlTimeTracking.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _mnuFile;
    private ToolStripMenuItem _mnuFileNew;
    private ToolStripMenuItem _mnuFileOpen;
    private ToolStripSeparator _mnuFileSep1;
    private ToolStripMenuItem _mnuFileQuit;
    private ToolStripMenuItem _mnuEdit;
    private ToolStripMenuItem _mnuEditGroups;
    private ToolStripMenuItem _mnuEditProjects;
    private ToolStripMenuItem _mnuEditCategories;
    private ToolStripMenuItem _mnuEditTasks;
    private ToolStripMenuItem _mnuTools;
    private ToolStripMenuItem _mnuToolsOptions;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _lblDatabase;
    private ToolStripStatusLabel _lblSessionStart;
    private ToolStripStatusLabel _lblStatus;
    private ToolStripStatusLabel _lblCurrentDate;
    private ToolStripStatusLabel _lblCurrentTime;
    private SplitContainer _scMain;
    private TreeView _tvwTasks;
    private SplitContainer _scRight;
    private Panel _pnlTaskDetails;
    private Label _lblPlaceholder;
    private Panel _pnlTimeTracking;
    private Label _lblTimePlaceholder;
}
