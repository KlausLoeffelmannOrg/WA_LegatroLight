using LegatroExp.Models;
using LegatroExp.Services;
using System.ComponentModel;

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
        _tvwTasks.AfterSelect += TvwTasks_AfterSelect;
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

            // Load all non-deleted tasks
            List<Data.Entities.Task> allTasks = _databaseService.DbContext.Tasks
                .Where(t => t.DateDeleted == null)
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
                List<Data.Entities.Task> groupTasks = GetTasksForGroup(group, allTasks);
                
                foreach (Data.Entities.Task task in groupTasks)
                {
                    TreeNode taskNode = new(task.DisplayName)
                    {
                        Name = $"task_{task.IDTask}",
                        Tag = task
                    };
                    groupNode.Nodes.Add(taskNode);
                }
            }

            rootNode.Expand();
            
            // Expand first group if available
            if (rootNode.Nodes.Count > 0)
            {
                rootNode.Nodes[0].Expand();
            }
        }
        catch (Exception ex)
        {
            _statusLabelSpring.Text = $"Error: {ex.Message}";
            _statusLabelSpring.ForeColor = Color.Red;
        }
    }

    private List<Data.Entities.Task> GetTasksForGroup(Data.Entities.Group group, List<Data.Entities.Task> allTasks)
    {
        List<Data.Entities.Task> tasks = new();
        
        // Check if group has AutoRangeSpan for automatic task assignment
        if (group.AutoRangeSpan.HasValue)
        {
            DateTime now = DateTime.UtcNow;
            int offset = group.AutoRangeOffset ?? 0;
            DateTime startDate = now.Date.AddDays(offset);
            DateTime endDate = startDate.Add(group.AutoRangeSpan.Value);
            
            // Find tasks created within the date range
            tasks = allTasks
                .Where(t => t.DateCreated >= startDate && t.DateCreated < endDate)
                .ToList();
        }
        
        // Add manually assigned tasks from TasksGroupsRelations
        List<Guid> manualTaskIds = _databaseService.DbContext.TasksGroupsRelations
            .Where(tgr => tgr.IDGroup == group.IDGroup && tgr.DateDeleted == null)
            .Select(tgr => tgr.IDTask)
            .ToList();
            
        List<Data.Entities.Task> manualTasks = allTasks
            .Where(t => manualTaskIds.Contains(t.IDTask) && !tasks.Any(existing => existing.IDTask == t.IDTask))
            .ToList();
            
        tasks.AddRange(manualTasks);
        
        // Sort tasks based on user preference
        tasks = SortTasks(tasks);
        
        return tasks;
    }

    private List<Data.Entities.Task> SortTasks(List<Data.Entities.Task> tasks)
    {
        // Sort based on settings preference
        return _settingsService.Settings.DefaultTaskSortOrder switch
        {
            TaskSortOrder.DisplayName => tasks.OrderBy(t => t.DisplayName).ToList(),
            TaskSortOrder.DueDate => tasks.OrderBy(t => t.DueDate ?? DateTime.MaxValue).ToList(),
            TaskSortOrder.PercentDone => tasks.OrderByDescending(t => CalculatePercentDone(t)).ToList(),
            TaskSortOrder.EstimatedEffort => tasks.OrderByDescending(t => t.EstimatedEffort ?? TimeSpan.Zero).ToList(),
            TaskSortOrder.DateCreated => tasks.OrderByDescending(t => t.DateCreated).ToList(),
            _ => tasks.OrderBy(t => t.DisplayName).ToList()
        };
    }

    private double CalculatePercentDone(Data.Entities.Task task)
    {
        if (!task.EstimatedEffort.HasValue || task.EstimatedEffort.Value.TotalHours == 0)
        {
            return 0;
        }
        
        return (task.TimeSpent.TotalHours / task.EstimatedEffort.Value.TotalHours) * 100;
    }

    private void QuitToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Close();
    }

    private async void NewSolutionToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        try
        {
            using SaveFileDialog saveDialog = new()
            {
                Title = "Create New Legatro Solution",
                Filter = "Legatro Files (*.legatro)|*.legatro|All Files (*.*)|*.*",
                FilterIndex = 1,
                DefaultExt = "legatro",
                AddExtension = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (saveDialog.ShowDialog(this) == DialogResult.OK)
            {
                string newDbPath = saveDialog.FileName;
                
                // Check if file already exists
                if (File.Exists(newDbPath))
                {
                    DialogResult result = MessageBox.Show(
                        "A database file already exists at this location. Do you want to replace it?",
                        "File Exists",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                    
                    File.Delete(newDbPath);
                }

                // Create new database
                await _databaseService.CreateNewDatabaseAsync(newDbPath);
                
                // Update settings
                _settingsService.Settings.LastDatabasePath = newDbPath;
                _settingsService.SaveSettings();
                
                // Reload the UI
                _statusLabelDatabase.Text = $"Database: {Path.GetFileNameWithoutExtension(newDbPath)}";
                _statusLabelSpring.Text = "New database created successfully";
                _statusLabelSpring.ForeColor = Color.Green;
                
                LoadGroupsAndTasks();
                
                MessageBox.Show(
                    "New database created successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            _statusLabelSpring.Text = $"Error: {ex.Message}";
            _statusLabelSpring.ForeColor = Color.Red;
            
            MessageBox.Show(
                $"Failed to create new database: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private async void OpenSolutionToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        try
        {
            using OpenFileDialog openDialog = new()
            {
                Title = "Open Legatro Solution",
                Filter = "Legatro Files (*.legatro)|*.legatro|All Files (*.*)|*.*",
                FilterIndex = 1,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openDialog.ShowDialog(this) == DialogResult.OK)
            {
                string dbPath = openDialog.FileName;
                
                // Verify file exists
                if (!File.Exists(dbPath))
                {
                    MessageBox.Show(
                        "The selected database file does not exist.",
                        "File Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                // Open the database
                await _databaseService.OpenDatabaseAsync(dbPath);
                
                // Update settings
                _settingsService.Settings.LastDatabasePath = dbPath;
                _settingsService.SaveSettings();
                
                // Reload the UI
                _statusLabelDatabase.Text = $"Database: {Path.GetFileNameWithoutExtension(dbPath)}";
                _statusLabelSpring.Text = "Database opened successfully";
                _statusLabelSpring.ForeColor = Color.Green;
                
                LoadGroupsAndTasks();
                
                MessageBox.Show(
                    "Database opened successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        catch (Exception ex)
        {
            _statusLabelSpring.Text = $"Error: {ex.Message}";
            _statusLabelSpring.ForeColor = Color.Red;
            
            MessageBox.Show(
                $"Failed to open database: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void TvwTasks_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node is null || e.Node.Tag is null)
        {
            ClearTaskDetails();
            return;
        }

        if (e.Node.Tag is Data.Entities.Task task)
        {
            DisplayTaskDetails(task);
        }
        else if (e.Node.Tag is Data.Entities.Group group)
        {
            DisplayGroupInfo(group);
        }
    }

    private void ClearTaskDetails()
    {
        _lblSelectedGroup.Text = "No Selection";
        _grpTaskDetails.Controls.Clear();
        _grpTimeTracking.Controls.Clear();
    }

    private void DisplayGroupInfo(Data.Entities.Group group)
    {
        _lblSelectedGroup.Text = $"Group: {group.GroupDisplayName}";
        _grpTaskDetails.Controls.Clear();
        _grpTimeTracking.Controls.Clear();
        
        // Display group information
        Label lblInfo = new()
        {
            Text = $"Group Description: {group.GroupDescription ?? "No description"}\n" +
                   $"Auto Range: {(group.AutoRangeSpan.HasValue ? $"{group.AutoRangeSpan.Value.Days} days" : "Manual assignment only")}\n" +
                   $"Is System: {(group.IsSystem ? "Yes" : "No")}",
            AutoSize = true,
            Padding = new Padding(10),
            Dock = DockStyle.Top
        };
        _grpTaskDetails.Controls.Add(lblInfo);
    }

    private void DisplayTaskDetails(Data.Entities.Task task)
    {
        _lblSelectedGroup.Text = $"Task: {task.DisplayName}";
        _grpTaskDetails.Controls.Clear();
        _grpTimeTracking.Controls.Clear();

        // Create TableLayoutPanel for task details
        TableLayoutPanel tlpDetails = new()
        {
            ColumnCount = 2,
            RowCount = 7,
            Dock = DockStyle.Fill,
            AutoSize = true,
            Padding = new Padding(10)
        };

        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tlpDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        // Display Name
        AddDetailRow(tlpDetails, 0, "Display Name:", task.DisplayName);

        // Description
        AddDetailRow(tlpDetails, 1, "Description:", task.Description ?? "(No description)");

        // Date Created
        AddDetailRow(tlpDetails, 2, "Date Created:", task.DateCreated.ToLocalTime().ToString("g"));

        // Due Date
        AddDetailRow(tlpDetails, 3, "Due Date:", task.DueDate?.ToLocalTime().ToString("g") ?? "(Not set)");

        // Estimated Effort
        AddDetailRow(tlpDetails, 4, "Estimated Effort:", task.EstimatedEffort.HasValue ? FormatTimeSpan(task.EstimatedEffort.Value) : "(Not set)");

        // Time Spent
        AddDetailRow(tlpDetails, 5, "Time Spent:", FormatTimeSpan(task.TimeSpent));

        // Percent Done
        double percentDone = CalculatePercentDone(task);
        AddDetailRow(tlpDetails, 6, "Percent Done:", $"{percentDone:F1}%");

        _grpTaskDetails.Controls.Add(tlpDetails);

        // Display time entries
        DisplayTimeEntries(task);
    }

    private void AddDetailRow(TableLayoutPanel tlp, int row, string label, string value)
    {
        Label lblLabel = new()
        {
            Text = label,
            AutoSize = true,
            Font = new Font(Font, FontStyle.Bold),
            Anchor = AnchorStyles.Left,
            Margin = new Padding(3)
        };
        tlp.Controls.Add(lblLabel, 0, row);

        Label lblValue = new()
        {
            Text = value,
            AutoSize = true,
            Anchor = AnchorStyles.Left,
            Margin = new Padding(3)
        };
        tlp.Controls.Add(lblValue, 1, row);
    }

    private void DisplayTimeEntries(Data.Entities.Task task)
    {
        // Load time entries for this task
        List<Data.Entities.TimeEntry> timeEntries = _databaseService.DbContext.TimeEntries
            .Where(te => te.IDTask == task.IDTask && te.DateDeleted == null)
            .OrderByDescending(te => te.StartTime)
            .ToList();

        // Create DataGridView
        DataGridView dgvTimeEntries = new()
        {
            Dock = DockStyle.Fill,
            AutoGenerateColumns = false,
            AllowUserToAddRows = true,
            AllowUserToDeleteRows = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            Margin = new Padding(10)
        };

        // Add columns
        dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Description",
            HeaderText = "Description",
            DataPropertyName = "DescriptionDoneWork",
            FillWeight = 40
        });

        dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "StartTime",
            HeaderText = "Start Time",
            DataPropertyName = "StartTime",
            FillWeight = 20
        });

        dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "EndTime",
            HeaderText = "End Time",
            DataPropertyName = "EndTime",
            FillWeight = 20
        });

        dgvTimeEntries.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Duration",
            HeaderText = "Duration",
            DataPropertyName = "Duration",
            FillWeight = 20,
            ReadOnly = true
        });

        // Create a bindable list for the DataGridView
        BindingList<TimeEntryDisplay> bindingList = new();
        foreach (Data.Entities.TimeEntry entry in timeEntries)
        {
            bindingList.Add(new TimeEntryDisplay
            {
                IDTimeEntry = entry.IDTimeEntry,
                DescriptionDoneWork = entry.DescriptionDoneWork ?? string.Empty,
                StartTime = entry.StartTime.ToLocalTime().ToString("g"),
                EndTime = entry.EndTime.ToLocalTime().ToString("g"),
                Duration = FormatTimeSpan(entry.Duration)
            });
        }

        dgvTimeEntries.DataSource = bindingList;

        _grpTimeTracking.Controls.Add(dgvTimeEntries);
    }

    private string FormatTimeSpan(TimeSpan timeSpan)
    {
        if (timeSpan.TotalHours >= 1)
        {
            return $"{(int)timeSpan.TotalHours}h {timeSpan.Minutes}m";
        }
        else if (timeSpan.TotalMinutes >= 1)
        {
            return $"{timeSpan.Minutes}m {timeSpan.Seconds}s";
        }
        else
        {
            return $"{timeSpan.Seconds}s";
        }
    }

    // Helper class for displaying time entries in DataGridView
    private class TimeEntryDisplay
    {
        public Guid IDTimeEntry { get; set; }
        public string DescriptionDoneWork { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }
}
