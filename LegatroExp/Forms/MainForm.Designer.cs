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
        _fileToolStripMenuItem = new ToolStripMenuItem();
        _newSolutionToolStripMenuItem = new ToolStripMenuItem();
        _openSolutionToolStripMenuItem = new ToolStripMenuItem();
        _quitToolStripMenuItem = new ToolStripMenuItem();
        _editToolStripMenuItem = new ToolStripMenuItem();
        _groupsToolStripMenuItem = new ToolStripMenuItem();
        _projectsToolStripMenuItem = new ToolStripMenuItem();
        _categoriesToolStripMenuItem = new ToolStripMenuItem();
        _tasksToolStripMenuItem = new ToolStripMenuItem();
        _toolsToolStripMenuItem = new ToolStripMenuItem();
        _optionsToolStripMenuItem = new ToolStripMenuItem();
        _statusStrip = new StatusStrip();
        _statusLabelDatabase = new ToolStripStatusLabel();
        _statusLabelSession = new ToolStripStatusLabel();
        _statusLabelSpring = new ToolStripStatusLabel();
        _statusLabelDate = new ToolStripStatusLabel();
        _statusLabelTime = new ToolStripStatusLabel();
        _splitContainer = new SplitContainer();
        _tvwTasks = new TreeView();
        _splitContainerRight = new SplitContainer();
        _panelTaskDetails = new Panel();
        _grpNewTask = new GroupBox();
        _tlpNewTask = new TableLayoutPanel();
        _txtNewTask = new TextBox();
        _btnAddTask = new Button();
        _lblSelectedGroup = new Label();
        _grpTaskDetails = new GroupBox();
        _panelTimeTracking = new Panel();
        _grpTimeTracking = new GroupBox();
        _timer = new System.Windows.Forms.Timer();

        SuspendLayout();
        _menuStrip.SuspendLayout();
        _statusStrip.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitContainer).BeginInit();
        _splitContainer.Panel1.SuspendLayout();
        _splitContainer.Panel2.SuspendLayout();
        _splitContainer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)_splitContainerRight).BeginInit();
        _splitContainerRight.Panel1.SuspendLayout();
        _splitContainerRight.Panel2.SuspendLayout();
        _splitContainerRight.SuspendLayout();
        _panelTaskDetails.SuspendLayout();
        _grpNewTask.SuspendLayout();
        _panelTimeTracking.SuspendLayout();

        // _menuStrip
        _menuStrip.ImageScalingSize = new Size(20, 20);
        _menuStrip.Items.AddRange(new ToolStripItem[] {
            _fileToolStripMenuItem,
            _editToolStripMenuItem,
            _toolsToolStripMenuItem});
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Padding = new Padding(8, 3, 0, 3);
        _menuStrip.Size = new Size(1200, 32);
        _menuStrip.TabIndex = 0;

        // _fileToolStripMenuItem
        _fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            _newSolutionToolStripMenuItem,
            _openSolutionToolStripMenuItem,
            _quitToolStripMenuItem});
        _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
        _fileToolStripMenuItem.Size = new Size(54, 26);
        _fileToolStripMenuItem.Text = "&File";

        // _newSolutionToolStripMenuItem
        _newSolutionToolStripMenuItem.Name = "_newSolutionToolStripMenuItem";
        _newSolutionToolStripMenuItem.Size = new Size(250, 34);
        _newSolutionToolStripMenuItem.Text = "&New Solution...";

        // _openSolutionToolStripMenuItem
        _openSolutionToolStripMenuItem.Name = "_openSolutionToolStripMenuItem";
        _openSolutionToolStripMenuItem.Size = new Size(250, 34);
        _openSolutionToolStripMenuItem.Text = "&Open Solution...";

        // _quitToolStripMenuItem
        _quitToolStripMenuItem.Name = "_quitToolStripMenuItem";
        _quitToolStripMenuItem.Size = new Size(250, 34);
        _quitToolStripMenuItem.Text = "&Quit";
        _quitToolStripMenuItem.Click += QuitToolStripMenuItem_Click;

        // Wire up File menu handlers
        _newSolutionToolStripMenuItem.Click += NewSolutionToolStripMenuItem_Click;
        _openSolutionToolStripMenuItem.Click += OpenSolutionToolStripMenuItem_Click;

        // _editToolStripMenuItem
        _editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            _groupsToolStripMenuItem,
            _projectsToolStripMenuItem,
            _categoriesToolStripMenuItem,
            _tasksToolStripMenuItem});
        _editToolStripMenuItem.Name = "_editToolStripMenuItem";
        _editToolStripMenuItem.Size = new Size(54, 26);
        _editToolStripMenuItem.Text = "&Edit";

        // _groupsToolStripMenuItem
        _groupsToolStripMenuItem.Name = "_groupsToolStripMenuItem";
        _groupsToolStripMenuItem.Size = new Size(250, 34);
        _groupsToolStripMenuItem.Text = "&Groups...";

        // _projectsToolStripMenuItem
        _projectsToolStripMenuItem.Name = "_projectsToolStripMenuItem";
        _projectsToolStripMenuItem.Size = new Size(250, 34);
        _projectsToolStripMenuItem.Text = "&Projects...";

        // _categoriesToolStripMenuItem
        _categoriesToolStripMenuItem.Name = "_categoriesToolStripMenuItem";
        _categoriesToolStripMenuItem.Size = new Size(250, 34);
        _categoriesToolStripMenuItem.Text = "&Categories...";

        // _tasksToolStripMenuItem
        _tasksToolStripMenuItem.Name = "_tasksToolStripMenuItem";
        _tasksToolStripMenuItem.Size = new Size(250, 34);
        _tasksToolStripMenuItem.Text = "&Tasks...";

        // _toolsToolStripMenuItem
        _toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            _optionsToolStripMenuItem});
        _toolsToolStripMenuItem.Name = "_toolsToolStripMenuItem";
        _toolsToolStripMenuItem.Size = new Size(65, 26);
        _toolsToolStripMenuItem.Text = "&Tools";

        // _optionsToolStripMenuItem
        _optionsToolStripMenuItem.Name = "_optionsToolStripMenuItem";
        _optionsToolStripMenuItem.Size = new Size(250, 34);
        _optionsToolStripMenuItem.Text = "&Options...";

        // _statusStrip
        _statusStrip.ImageScalingSize = new Size(20, 20);
        _statusStrip.Items.AddRange(new ToolStripItem[] {
            _statusLabelDatabase,
            _statusLabelSession,
            _statusLabelSpring,
            _statusLabelDate,
            _statusLabelTime});
        _statusStrip.Location = new Point(0, 628);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Padding = new Padding(1, 0, 18, 0);
        _statusStrip.Size = new Size(1200, 32);
        _statusStrip.TabIndex = 1;

        // _statusLabelDatabase
        _statusLabelDatabase.Name = "_statusLabelDatabase";
        _statusLabelDatabase.Size = new Size(120, 25);
        _statusLabelDatabase.Text = "Database: N/A";

        // _statusLabelSession
        _statusLabelSession.Name = "_statusLabelSession";
        _statusLabelSession.Size = new Size(100, 25);
        _statusLabelSession.Text = "Session: N/A";

        // _statusLabelSpring
        _statusLabelSpring.Name = "_statusLabelSpring";
        _statusLabelSpring.Size = new Size(59, 25);
        _statusLabelSpring.Spring = true;
        _statusLabelSpring.Text = "Ready";

        // _statusLabelDate
        _statusLabelDate.Name = "_statusLabelDate";
        _statusLabelDate.Size = new Size(90, 25);
        _statusLabelDate.Text = "Date";

        // _statusLabelTime
        _statusLabelTime.Name = "_statusLabelTime";
        _statusLabelTime.Size = new Size(90, 25);
        _statusLabelTime.Text = "Time";

        // _splitContainer
        _splitContainer.Dock = DockStyle.Fill;
        _splitContainer.Location = new Point(0, 32);
        _splitContainer.Name = "_splitContainer";
        _splitContainer.Panel1.Controls.Add(_tvwTasks);
        _splitContainer.Panel1.Padding = new Padding(3);
        _splitContainer.Panel2.Controls.Add(_splitContainerRight);
        _splitContainer.Panel2.Padding = new Padding(3);
        _splitContainer.Size = new Size(1200, 596);
        _splitContainer.SplitterDistance = 360;
        _splitContainer.TabIndex = 2;

        // _tvwTasks
        _tvwTasks.Dock = DockStyle.Fill;
        _tvwTasks.Location = new Point(3, 3);
        _tvwTasks.Name = "_tvwTasks";
        _tvwTasks.Size = new Size(354, 590);
        _tvwTasks.TabIndex = 0;

        // _splitContainerRight
        _splitContainerRight.Dock = DockStyle.Fill;
        _splitContainerRight.Location = new Point(3, 3);
        _splitContainerRight.Name = "_splitContainerRight";
        _splitContainerRight.Orientation = Orientation.Horizontal;
        _splitContainerRight.Panel1.Controls.Add(_panelTaskDetails);
        _splitContainerRight.Panel2.Controls.Add(_panelTimeTracking);
        _splitContainerRight.Size = new Size(830, 590);
        _splitContainerRight.SplitterDistance = 354;
        _splitContainerRight.TabIndex = 0;

        // _panelTaskDetails
        _panelTaskDetails.AutoScroll = true;
        _panelTaskDetails.Controls.Add(_grpTaskDetails);
        _panelTaskDetails.Controls.Add(_grpNewTask);
        _panelTaskDetails.Controls.Add(_lblSelectedGroup);
        _panelTaskDetails.Dock = DockStyle.Fill;
        _panelTaskDetails.Location = new Point(0, 0);
        _panelTaskDetails.Name = "_panelTaskDetails";
        _panelTaskDetails.Size = new Size(830, 354);
        _panelTaskDetails.TabIndex = 0;

        // _lblSelectedGroup
        _lblSelectedGroup.AutoSize = true;
        _lblSelectedGroup.Dock = DockStyle.Top;
        _lblSelectedGroup.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblSelectedGroup.Location = new Point(0, 0);
        _lblSelectedGroup.Name = "_lblSelectedGroup";
        _lblSelectedGroup.Padding = new Padding(10, 10, 10, 5);
        _lblSelectedGroup.Size = new Size(196, 52);
        _lblSelectedGroup.TabIndex = 0;
        _lblSelectedGroup.Text = "No Group Selected";

        // _grpNewTask
        _grpNewTask.AutoSize = true;
        _grpNewTask.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _grpNewTask.Controls.Add(_tlpNewTask);
        _grpNewTask.Dock = DockStyle.Top;
        _grpNewTask.Location = new Point(0, 52);
        _grpNewTask.Name = "_grpNewTask";
        _grpNewTask.Padding = new Padding(10);
        _grpNewTask.Size = new Size(830, 100);
        _grpNewTask.TabIndex = 1;
        _grpNewTask.TabStop = false;
        _grpNewTask.Text = "New Task";

        // _tlpNewTask
        _tlpNewTask.AutoSize = true;
        _tlpNewTask.ColumnCount = 2;
        _tlpNewTask.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpNewTask.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpNewTask.Controls.Add(_txtNewTask, 0, 0);
        _tlpNewTask.Controls.Add(_btnAddTask, 1, 0);
        _tlpNewTask.Dock = DockStyle.Fill;
        _tlpNewTask.Location = new Point(10, 33);
        _tlpNewTask.Name = "_tlpNewTask";
        _tlpNewTask.RowCount = 1;
        _tlpNewTask.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpNewTask.Size = new Size(810, 57);
        _tlpNewTask.TabIndex = 0;

        // _txtNewTask
        _txtNewTask.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _txtNewTask.Location = new Point(3, 3);
        _txtNewTask.Multiline = true;
        _txtNewTask.Name = "_txtNewTask";
        _txtNewTask.PlaceholderText = "Enter task name...";
        _txtNewTask.Size = new Size(684, 51);
        _txtNewTask.TabIndex = 0;
        _txtNewTask.TextChanged += TxtNewTask_TextChanged;

        // _btnAddTask
        _btnAddTask.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        _btnAddTask.Enabled = false;
        _btnAddTask.Location = new Point(693, 3);
        _btnAddTask.Name = "_btnAddTask";
        _btnAddTask.Size = new Size(114, 51);
        _btnAddTask.TabIndex = 1;
        _btnAddTask.Text = "Add";
        _btnAddTask.UseVisualStyleBackColor = true;
        _btnAddTask.Click += BtnAddTask_Click;

        // _grpTaskDetails
        _grpTaskDetails.AutoSize = true;
        _grpTaskDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _grpTaskDetails.Dock = DockStyle.Top;
        _grpTaskDetails.Location = new Point(0, 152);
        _grpTaskDetails.Name = "_grpTaskDetails";
        _grpTaskDetails.Padding = new Padding(10);
        _grpTaskDetails.Size = new Size(830, 36);
        _grpTaskDetails.TabIndex = 2;
        _grpTaskDetails.TabStop = false;
        _grpTaskDetails.Text = "Task Details";

        // _panelTimeTracking
        _panelTimeTracking.Controls.Add(_grpTimeTracking);
        _panelTimeTracking.Dock = DockStyle.Fill;
        _panelTimeTracking.Location = new Point(0, 0);
        _panelTimeTracking.Name = "_panelTimeTracking";
        _panelTimeTracking.Size = new Size(830, 232);
        _panelTimeTracking.TabIndex = 0;

        // _grpTimeTracking
        _grpTimeTracking.Dock = DockStyle.Fill;
        _grpTimeTracking.Location = new Point(0, 0);
        _grpTimeTracking.Name = "_grpTimeTracking";
        _grpTimeTracking.Padding = new Padding(10);
        _grpTimeTracking.Size = new Size(830, 232);
        _grpTimeTracking.TabIndex = 0;
        _grpTimeTracking.TabStop = false;
        _grpTimeTracking.Text = "Time Tracking";

        // _timer
        _timer.Enabled = true;
        _timer.Interval = 1000;
        _timer.Tick += Timer_Tick;

        // MainForm
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 660);
        Controls.Add(_splitContainer);
        Controls.Add(_statusStrip);
        Controls.Add(_menuStrip);
        MainMenuStrip = _menuStrip;
        Name = "MainForm";
        Text = "LegatroExp - Time Tracking";

        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        _splitContainer.Panel1.ResumeLayout(false);
        _splitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitContainer).EndInit();
        _splitContainer.ResumeLayout(false);
        _splitContainerRight.Panel1.ResumeLayout(false);
        _splitContainerRight.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)_splitContainerRight).EndInit();
        _splitContainerRight.ResumeLayout(false);
        _grpNewTask.ResumeLayout(false);
        _grpNewTask.PerformLayout();
        _panelTaskDetails.ResumeLayout(false);
        _panelTaskDetails.PerformLayout();
        _panelTimeTracking.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _fileToolStripMenuItem;
    private ToolStripMenuItem _newSolutionToolStripMenuItem;
    private ToolStripMenuItem _openSolutionToolStripMenuItem;
    private ToolStripMenuItem _quitToolStripMenuItem;
    private ToolStripMenuItem _editToolStripMenuItem;
    private ToolStripMenuItem _groupsToolStripMenuItem;
    private ToolStripMenuItem _projectsToolStripMenuItem;
    private ToolStripMenuItem _categoriesToolStripMenuItem;
    private ToolStripMenuItem _tasksToolStripMenuItem;
    private ToolStripMenuItem _toolsToolStripMenuItem;
    private ToolStripMenuItem _optionsToolStripMenuItem;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusLabelDatabase;
    private ToolStripStatusLabel _statusLabelSession;
    private ToolStripStatusLabel _statusLabelSpring;
    private ToolStripStatusLabel _statusLabelDate;
    private ToolStripStatusLabel _statusLabelTime;
    private SplitContainer _splitContainer;
    private TreeView _tvwTasks;
    private SplitContainer _splitContainerRight;
    private Panel _panelTaskDetails;
    private GroupBox _grpNewTask;
    private TableLayoutPanel _tlpNewTask;
    private TextBox _txtNewTask;
    private Button _btnAddTask;
    private Label _lblSelectedGroup;
    private GroupBox _grpTaskDetails;
    private Panel _panelTimeTracking;
    private GroupBox _grpTimeTracking;
    private System.Windows.Forms.Timer _timer;
}
