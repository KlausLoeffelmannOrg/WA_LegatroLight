using LegatroExp.Data;
using LegatroExp.Helpers;
using LegatroExp.Models;
using Microsoft.EntityFrameworkCore;

namespace LegatroExp.Forms;

public partial class MainForm : Form
{
    private readonly LegatroDbContext _context;
    private readonly User _currentUser;
    private readonly AppSettings _settings;
    private readonly System.Windows.Forms.Timer _clockTimer;
    private readonly DateTime _sessionStart;

    public MainForm(LegatroDbContext context, User currentUser, AppSettings settings)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        _sessionStart = DateTime.Now;

        InitializeComponent();
        InitializeForm();

        _clockTimer = new System.Windows.Forms.Timer
        {
            Interval = 1000
        };
        _clockTimer.Tick += ClockTimer_Tick;
        _clockTimer.Start();

        UpdateStatusBar();
    }

    private void InitializeForm()
    {
        Text = "Legatro Experimental - Time Tracking";
        Font = new Font(_settings.BaseFont, 11F);

        ApplyWindowPosition();
        LoadData();
    }

    private void ApplyWindowPosition()
    {
        Screen screen = Screen.FromPoint(Cursor.Position);
        Rectangle workingArea = screen.WorkingArea;

        if (_settings.MainWindow.Maximized)
        {
            WindowState = FormWindowState.Maximized;
        }
        else
        {
            int width = (int)(workingArea.Width * _settings.MainWindow.Width);
            int height = (int)(workingArea.Height * _settings.MainWindow.Height);
            int left = workingArea.Left + (int)(workingArea.Width * _settings.MainWindow.Left);
            int top = workingArea.Top + (int)(workingArea.Height * _settings.MainWindow.Top);

            StartPosition = FormStartPosition.Manual;
            Location = new Point(left, top);
            Size = new Size(width, height);
        }
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);

        if (_scMain.Panel1.Width > 0 && _settings.MainWindow.VerticalSplitterPosition > 0)
        {
            int distance = (int)(_scMain.Width * _settings.MainWindow.VerticalSplitterPosition);
            if (distance > _scMain.Panel1MinSize && distance < _scMain.Width - _scMain.Panel2MinSize)
            {
                _scMain.SplitterDistance = distance;
            }
        }

        if (_scRight.Panel1.Height > 0 && _settings.MainWindow.HorizontalSplitterPosition > 0)
        {
            int distance = (int)(_scRight.Height * _settings.MainWindow.HorizontalSplitterPosition);
            if (distance > _scRight.Panel1MinSize && distance < _scRight.Height - _scRight.Panel2MinSize)
            {
                _scRight.SplitterDistance = distance;
            }
        }
    }

    private void SaveWindowPosition()
    {
        _settings.MainWindow.Maximized = WindowState == FormWindowState.Maximized;

        if (WindowState == FormWindowState.Normal)
        {
            Screen screen = Screen.FromPoint(Location);
            Rectangle workingArea = screen.WorkingArea;

            _settings.MainWindow.Left = (double)(Location.X - workingArea.Left) / workingArea.Width;
            _settings.MainWindow.Top = (double)(Location.Y - workingArea.Top) / workingArea.Height;
            _settings.MainWindow.Width = (double)Width / workingArea.Width;
            _settings.MainWindow.Height = (double)Height / workingArea.Height;
        }

        if (_scMain.Width > 0)
        {
            _settings.MainWindow.VerticalSplitterPosition = (double)_scMain.SplitterDistance / _scMain.Width;
        }

        if (_scRight.Height > 0)
        {
            _settings.MainWindow.HorizontalSplitterPosition = (double)_scRight.SplitterDistance / _scRight.Height;
        }

        _settings.Save();
    }

    private void LoadData()
    {
        LoadGroupsAndTasks();
    }

    private void LoadGroupsAndTasks()
    {
        _tvwTasks.Nodes.Clear();

        TreeNode rootNode = new("Tasks")
        {
            Tag = null
        };
        _tvwTasks.Nodes.Add(rootNode);

        List<Group> groups = _context.Groups
            .Where(g => g.DateDeleted == null && !g.IsHidden)
            .OrderBy(g => g.OrderNo)
            .ThenBy(g => g.GroupDisplayName)
            .ToList();

        foreach (Group group in groups)
        {
            TreeNode groupNode = new(group.GroupDisplayName)
            {
                Tag = group
            };
            rootNode.Nodes.Add(groupNode);

            List<Models.Task> tasks = GetTasksForGroup(group);

            foreach (Models.Task task in tasks)
            {
                TreeNode taskNode = new(task.DisplayName)
                {
                    Tag = task
                };
                groupNode.Nodes.Add(taskNode);
            }
        }

        rootNode.Expand();
    }

    private List<Models.Task> GetTasksForGroup(Group group)
    {
        List<Models.Task> tasks = new();

        if (group.AutoRangeSpan.HasValue)
        {
            DateTime startDate = DateTime.UtcNow.Date.AddDays(group.AutoRangeOffset ?? 0);
            DateTime endDate = startDate.Add(group.AutoRangeSpan.Value);

            tasks = _context.Tasks
                .Where(t => t.DateDeleted == null && 
                           t.DateFinished == null &&
                           t.DateCreated >= startDate && 
                           t.DateCreated < endDate)
                .ToList();
        }
        else
        {
            List<string> taskIds = _context.TasksGroupsRelations
                .Where(r => r.IDGroup == group.IDGroup && r.DateDeleted == null)
                .Select(r => r.IDTask)
                .ToList();

            tasks = _context.Tasks
                .Where(t => taskIds.Contains(t.IDTask) && t.DateDeleted == null && t.DateFinished == null)
                .ToList();
        }

        return tasks;
    }

    private void ClockTimer_Tick(object? sender, EventArgs e)
    {
        UpdateStatusBar();
    }

    private void UpdateStatusBar()
    {
        _lblDatabase.Text = $"Database: {Path.GetFileName(_settings.DatabasePath ?? "None")}";
        _lblSessionStart.Text = $"Session Start: {_sessionStart:HH:mm:ss}";
        _lblCurrentDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        _lblCurrentTime.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        if (e.Cancel)
        {
            return;
        }

        _clockTimer?.Stop();
        SaveWindowPosition();

        if (_settings.AutoBackup)
        {
            CreateBackup();
        }
    }

    private void CreateBackup()
    {
        try
        {
            if (!string.IsNullOrEmpty(_settings.DatabasePath) && File.Exists(_settings.DatabasePath))
            {
                string backupPath = _settings.DatabasePath + ".bak";
                File.Copy(_settings.DatabasePath, backupPath, true);
            }
        }
        catch
        {
            // Silently fail if backup cannot be created
        }
    }

    private void MnuFileQuit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private void MnuFileNew_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("New Solution functionality not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuFileOpen_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Open Solution functionality not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuEditGroups_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Groups editor not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuEditProjects_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Projects editor not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuEditCategories_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Categories editor not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuEditTasks_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Tasks editor not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void MnuToolsOptions_Click(object? sender, EventArgs e)
    {
        MessageBox.Show("Options dialog not yet implemented.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
