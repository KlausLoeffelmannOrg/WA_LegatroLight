namespace LegatroExp.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

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
        _menuStrip = new MenuStrip();
        _menuFile = new ToolStripMenuItem();
        _menuFileNew = new ToolStripMenuItem();
        _menuFileOpen = new ToolStripMenuItem();
        _menuFileSep1 = new ToolStripSeparator();
        _menuFileQuit = new ToolStripMenuItem();
        _menuEdit = new ToolStripMenuItem();
        _menuEditGroups = new ToolStripMenuItem();
        _menuEditProjects = new ToolStripMenuItem();
        _menuEditCategories = new ToolStripMenuItem();
        _menuTools = new ToolStripMenuItem();
        _menuToolsOptions = new ToolStripMenuItem();
        _statusStrip = new StatusStrip();
        _lblDatabase = new ToolStripStatusLabel();
        _lblStatus = new ToolStripStatusLabel();
        _lblDate = new ToolStripStatusLabel();
        _lblTime = new ToolStripStatusLabel();
        _splitMain = new SplitContainer();
        _tvwTasks = new TreeView();
        _splitRight = new SplitContainer();
        _panelTaskDetails = new Panel();
        _lblGroupTitle = new Label();
        _panelTimeTracking = new Panel();
        _lblTimeTracking = new Label();
        
        _menuStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitMain).BeginInit();
        _splitMain.Panel1.SuspendLayout();
        _splitMain.Panel2.SuspendLayout();
        _splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitRight).BeginInit();
        _splitRight.Panel1.SuspendLayout();
        _splitRight.Panel2.SuspendLayout();
        _splitRight.SuspendLayout();
        _panelTaskDetails.SuspendLayout();
        _panelTimeTracking.SuspendLayout();
        SuspendLayout();
        
        _menuStrip.Items.AddRange(new ToolStripItem[] {
            _menuFile,
            _menuEdit,
            _menuTools
        });
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Size = new Size(1200, 24);
        
        _menuFile.DropDownItems.AddRange(new ToolStripItem[] {
            _menuFileNew,
            _menuFileOpen,
            _menuFileSep1,
            _menuFileQuit
        });
        _menuFile.Name = "_menuFile";
        _menuFile.Size = new Size(37, 20);
        _menuFile.Text = "&File";
        
        _menuFileNew.Name = "_menuFileNew";
        _menuFileNew.Size = new Size(180, 22);
        _menuFileNew.Text = "&New Solution...";
        
        _menuFileOpen.Name = "_menuFileOpen";
        _menuFileOpen.Size = new Size(180, 22);
        _menuFileOpen.Text = "&Open Solution...";
        
        _menuFileSep1.Name = "_menuFileSep1";
        _menuFileSep1.Size = new Size(177, 6);
        
        _menuFileQuit.Name = "_menuFileQuit";
        _menuFileQuit.Size = new Size(180, 22);
        _menuFileQuit.Text = "&Quit";
        _menuFileQuit.Click += MenuFileQuit_Click;
        
        _menuEdit.DropDownItems.AddRange(new ToolStripItem[] {
            _menuEditGroups,
            _menuEditProjects,
            _menuEditCategories
        });
        _menuEdit.Name = "_menuEdit";
        _menuEdit.Size = new Size(39, 20);
        _menuEdit.Text = "&Edit";
        
        _menuEditGroups.Name = "_menuEditGroups";
        _menuEditGroups.Size = new Size(180, 22);
        _menuEditGroups.Text = "&Groups...";
        
        _menuEditProjects.Name = "_menuEditProjects";
        _menuEditProjects.Size = new Size(180, 22);
        _menuEditProjects.Text = "&Projects...";
        
        _menuEditCategories.Name = "_menuEditCategories";
        _menuEditCategories.Size = new Size(180, 22);
        _menuEditCategories.Text = "&Categories...";
        
        _menuTools.DropDownItems.AddRange(new ToolStripItem[] {
            _menuToolsOptions
        });
        _menuTools.Name = "_menuTools";
        _menuTools.Size = new Size(46, 20);
        _menuTools.Text = "&Tools";
        
        _menuToolsOptions.Name = "_menuToolsOptions";
        _menuToolsOptions.Size = new Size(180, 22);
        _menuToolsOptions.Text = "&Options...";
        
        _statusStrip.Items.AddRange(new ToolStripItem[] {
            _lblDatabase,
            _lblStatus,
            _lblDate,
            _lblTime
        });
        _statusStrip.Location = new Point(0, 776);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Size = new Size(1200, 24);
        
        _lblDatabase.Name = "_lblDatabase";
        _lblDatabase.Size = new Size(100, 19);
        _lblDatabase.Text = "Database: ";
        
        _lblStatus.Name = "_lblStatus";
        _lblStatus.Size = new Size(100, 19);
        _lblStatus.Spring = true;
        _lblStatus.TextAlign = ContentAlignment.MiddleLeft;
        
        _lblDate.Name = "_lblDate";
        _lblDate.Size = new Size(100, 19);
        _lblDate.Text = "2025-01-01";
        
        _lblTime.Name = "_lblTime";
        _lblTime.Size = new Size(60, 19);
        _lblTime.Text = "00:00:00";
        
        _splitMain.Dock = DockStyle.Fill;
        _splitMain.Location = new Point(0, 24);
        _splitMain.Name = "_splitMain";
        _splitMain.Orientation = Orientation.Vertical;
        _splitMain.Size = new Size(1200, 752);
        _splitMain.SplitterDistance = 300;
        _splitMain.SplitterWidth = 6;
        
        _tvwTasks.Dock = DockStyle.Fill;
        _tvwTasks.Location = new Point(3, 3);
        _tvwTasks.Name = "_tvwTasks";
        _tvwTasks.Size = new Size(294, 746);
        
        _splitMain.Panel1.Controls.Add(_tvwTasks);
        _splitMain.Panel1.Padding = new Padding(3);
        
        _splitRight.Dock = DockStyle.Fill;
        _splitRight.Location = new Point(3, 3);
        _splitRight.Name = "_splitRight";
        _splitRight.Orientation = Orientation.Horizontal;
        _splitRight.Size = new Size(888, 746);
        _splitRight.SplitterDistance = 450;
        _splitRight.SplitterWidth = 6;
        
        _panelTaskDetails.Dock = DockStyle.Fill;
        _panelTaskDetails.Location = new Point(0, 0);
        _panelTaskDetails.Name = "_panelTaskDetails";
        _panelTaskDetails.Size = new Size(888, 450);
        
        _lblGroupTitle.AutoSize = true;
        _lblGroupTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblGroupTitle.Location = new Point(10, 10);
        _lblGroupTitle.Name = "_lblGroupTitle";
        _lblGroupTitle.Size = new Size(150, 25);
        _lblGroupTitle.Text = "Task Details";
        
        _panelTaskDetails.Controls.Add(_lblGroupTitle);
        
        _panelTimeTracking.Dock = DockStyle.Fill;
        _panelTimeTracking.Location = new Point(0, 0);
        _panelTimeTracking.Name = "_panelTimeTracking";
        _panelTimeTracking.Size = new Size(888, 290);
        
        _lblTimeTracking.AutoSize = true;
        _lblTimeTracking.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        _lblTimeTracking.Location = new Point(10, 10);
        _lblTimeTracking.Name = "_lblTimeTracking";
        _lblTimeTracking.Size = new Size(120, 21);
        _lblTimeTracking.Text = "Time Tracking";
        
        _panelTimeTracking.Controls.Add(_lblTimeTracking);
        
        _splitRight.Panel1.Controls.Add(_panelTaskDetails);
        _splitRight.Panel2.Controls.Add(_panelTimeTracking);
        
        _splitMain.Panel2.Controls.Add(_splitRight);
        _splitMain.Panel2.Padding = new Padding(3);
        
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 800);
        Controls.Add(_splitMain);
        Controls.Add(_statusStrip);
        Controls.Add(_menuStrip);
        Font = new Font("Segoe UI", 11F);
        MainMenuStrip = _menuStrip;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Legatro - Task Management";
        
        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        _splitMain.Panel1.ResumeLayout(false);
        _splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitMain).EndInit();
        _splitMain.ResumeLayout(false);
        _splitRight.Panel1.ResumeLayout(false);
        _splitRight.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitRight).EndInit();
        _splitRight.ResumeLayout(false);
        _panelTaskDetails.ResumeLayout(false);
        _panelTaskDetails.PerformLayout();
        _panelTimeTracking.ResumeLayout(false);
        _panelTimeTracking.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _menuFile;
    private ToolStripMenuItem _menuFileNew;
    private ToolStripMenuItem _menuFileOpen;
    private ToolStripSeparator _menuFileSep1;
    private ToolStripMenuItem _menuFileQuit;
    private ToolStripMenuItem _menuEdit;
    private ToolStripMenuItem _menuEditGroups;
    private ToolStripMenuItem _menuEditProjects;
    private ToolStripMenuItem _menuEditCategories;
    private ToolStripMenuItem _menuTools;
    private ToolStripMenuItem _menuToolsOptions;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _lblDatabase;
    private ToolStripStatusLabel _lblStatus;
    private ToolStripStatusLabel _lblDate;
    private ToolStripStatusLabel _lblTime;
    private SplitContainer _splitMain;
    private TreeView _tvwTasks;
    private SplitContainer _splitRight;
    private Panel _panelTaskDetails;
    private Label _lblGroupTitle;
    private Panel _panelTimeTracking;
    private Label _lblTimeTracking;
}
