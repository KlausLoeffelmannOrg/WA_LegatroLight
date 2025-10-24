using LegatroExp.Models;
using LegatroExp.Services;

namespace LegatroExp.Forms;

/// <summary>
/// Main application form
/// </summary>
public partial class MainForm : Form
{
    private readonly DatabaseService _databaseService;
    private readonly SettingsService _settingsService;
    private readonly DateTime _sessionStart;

    public MainForm(DatabaseService databaseService, SettingsService settingsService)
    {
        _databaseService = databaseService;
        _settingsService = settingsService;
        _sessionStart = DateTime.Now;

        InitializeComponent();
        
        Font = new Font(_settingsService.Settings.BaseFontName, _settingsService.Settings.BaseFontSize);
        
        Load += MainForm_Load;
        FormClosing += MainForm_FormClosing;
    }

    private void MainForm_Load(object? sender, EventArgs e)
    {
        // Restore window position
        if (_settingsService.Settings.MainWindowPosition is not null)
        {
            WindowPosition pos = _settingsService.Settings.MainWindowPosition;
            
            if (pos.IsMaximized)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                StartPosition = FormStartPosition.Manual;
                Left = pos.Left;
                Top = pos.Top;
                Width = pos.Width;
                Height = pos.Height;
            }
        }

        // Restore splitter positions
        _splitContainer.SplitterDistance = (int)(Width * _settingsService.Settings.MainVerticalSplitterPosition);
        _splitContainerRight.SplitterDistance = (int)(_splitContainerRight.Height * _settingsService.Settings.RightHorizontalSplitterPosition);

        // Update status bar
        string dbName = Path.GetFileNameWithoutExtension(_settingsService.Settings.LastDatabasePath ?? "Unknown");
        _statusLabelDatabase.Text = $"Database: {dbName}";
        _statusLabelSession.Text = $"Session: {_sessionStart:g}";
        
        UpdateStatusBarTime();
        
        // Load groups and tasks
        LoadGroupsAndTasks();
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        // Save window position
        _settingsService.Settings.MainWindowPosition = new WindowPosition
        {
            Left = Left,
            Top = Top,
            Width = Width,
            Height = Height,
            IsMaximized = WindowState == FormWindowState.Maximized
        };

        // Save splitter positions as percentages
        if (Width > 0)
        {
            _settingsService.Settings.MainVerticalSplitterPosition = (double)_splitContainer.SplitterDistance / Width;
        }
        
        if (_splitContainerRight.Height > 0)
        {
            _settingsService.Settings.RightHorizontalSplitterPosition = (double)_splitContainerRight.SplitterDistance / _splitContainerRight.Height;
        }

        _settingsService.SaveSettings();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        UpdateStatusBarTime();
    }

    private void UpdateStatusBarTime()
    {
        _statusLabelDate.Text = DateTime.Now.ToString("d");
        _statusLabelTime.Text = DateTime.Now.ToString("HH:mm:ss");
    }

    private void LoadGroupsAndTasks()
    {
        try
        {
            _tvwTasks.Nodes.Clear();
            
            TreeNode rootNode = new("Task Groups")
            {
                Name = "root"
            };
            _tvwTasks.Nodes.Add(rootNode);

            // Load groups from database
            List<Data.Entities.Group> groups = _databaseService.DbContext.Groups
                .Where(g => g.DateDeleted == null && !g.IsHidden)
                .OrderBy(g => g.OrderNo)
                .ToList();

            foreach (Data.Entities.Group group in groups)
            {
                TreeNode groupNode = new(group.GroupDisplayName)
                {
                    Name = $"group_{group.IDGroup}",
                    Tag = group
                };
                rootNode.Nodes.Add(groupNode);

                // Load tasks for this group
                // TODO: Implement task loading based on AutoRangeSpan and manual assignments
            }

            rootNode.Expand();
        }
        catch (Exception ex)
        {
            _statusLabelSpring.Text = $"Error: {ex.Message}";
            _statusLabelSpring.ForeColor = Color.Red;
        }
    }

    private void QuitToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
