using Microsoft.EntityFrameworkCore;
using LegatroExp.Data;
using LegatroExp.Models;
using LegatroExp.Helpers;

namespace LegatroExp.Forms;

/// <summary>
///  Main application form.
/// </summary>
public partial class MainForm : Form
{
    private LegatroDbContext? _dbContext;
    private User? _currentUser;
    private AppSettings _settings;
    private string? _databasePath;
    private System.Windows.Forms.Timer? _clockTimer;

    public MainForm()
    {
        InitializeComponent();
        
        _settings = AppSettings.Load();
        
        Load += MainForm_Load;
        FormClosing += MainForm_FormClosing;
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        try
        {
            await InitializeDatabaseAsync();
            RestoreWindowPosition();
            StartClock();
            UpdateStatusBar();
            await LoadGroupsAndTasksAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error initializing application: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
        }
    }

    private async Task InitializeDatabaseAsync()
    {
        _databasePath = _settings.DatabasePath;

        if (string.IsNullOrEmpty(_databasePath) || !File.Exists(_databasePath))
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string legatroPath = Path.Combine(documentsPath, "LegatroExp");
            Directory.CreateDirectory(legatroPath);
            _databasePath = Path.Combine(legatroPath, "default.legatro");
            _settings.DatabasePath = _databasePath;
        }

        bool isNewDatabase = !File.Exists(_databasePath);

        DbContextOptionsBuilder<LegatroDbContext> optionsBuilder = new DbContextOptionsBuilder<LegatroDbContext>();
        optionsBuilder.UseSqlite($"Data Source={_databasePath}");
        _dbContext = new LegatroDbContext(optionsBuilder.Options);

        await _dbContext.Database.EnsureCreatedAsync();

        if (isNewDatabase)
        {
            using UserSetupForm setupForm = new UserSetupForm();
            
            if (setupForm.ShowDialog(this) == DialogResult.OK)
            {
                _currentUser = setupForm.CurrentUser;
                _dbContext.Users.Add(_currentUser);
                await _dbContext.SaveChangesAsync();

                DatabaseInitializer.SeedSystemData(_dbContext, _currentUser);
            }
            else
            {
                Close();
                return;
            }
        }
        else
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync();
            
            if (user is null)
            {
                using UserSetupForm setupForm = new UserSetupForm();
                
                if (setupForm.ShowDialog(this) == DialogResult.OK)
                {
                    _currentUser = setupForm.CurrentUser;
                    _dbContext.Users.Add(_currentUser);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    Close();
                    return;
                }
            }
            else
            {
                _currentUser = user;
            }
        }
    }

    private void RestoreWindowPosition()
    {
        if (_settings.IsMaximized)
        {
            WindowState = FormWindowState.Maximized;
        }
        else
        {
            Rectangle workingArea = Screen.PrimaryScreen?.WorkingArea ?? Screen.PrimaryScreen!.Bounds;
            
            int width = (int)(workingArea.Width * _settings.WindowWidthPercent / 100);
            int height = (int)(workingArea.Height * _settings.WindowHeightPercent / 100);
            int left = (int)(workingArea.Left + workingArea.Width * _settings.WindowLeftPercent / 100);
            int top = (int)(workingArea.Top + workingArea.Height * _settings.WindowTopPercent / 100);

            if (width > 0 && height > 0)
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point(left, top);
                Size = new Size(width, height);
            }
        }

        if (_settings.MainSplitterPercent > 0)
        {
            int distance = (int)(_splitMain.Width * _settings.MainSplitterPercent / 100);
            _splitMain.SplitterDistance = Math.Max(100, Math.Min(distance, _splitMain.Width - 100));
        }

        if (_settings.RightSplitterPercent > 0)
        {
            int distance = (int)(_splitRight.Height * _settings.RightSplitterPercent / 100);
            _splitRight.SplitterDistance = Math.Max(100, Math.Min(distance, _splitRight.Height - 100));
        }
    }

    private void SaveWindowPosition()
    {
        if (WindowState == FormWindowState.Maximized)
        {
            _settings.IsMaximized = true;
        }
        else
        {
            _settings.IsMaximized = false;
            
            Rectangle workingArea = Screen.PrimaryScreen?.WorkingArea ?? Screen.PrimaryScreen!.Bounds;
            
            _settings.WindowWidthPercent = (double)Width / workingArea.Width * 100;
            _settings.WindowHeightPercent = (double)Height / workingArea.Height * 100;
            _settings.WindowLeftPercent = (double)(Left - workingArea.Left) / workingArea.Width * 100;
            _settings.WindowTopPercent = (double)(Top - workingArea.Top) / workingArea.Height * 100;
        }

        _settings.MainSplitterPercent = (double)_splitMain.SplitterDistance / _splitMain.Width * 100;
        _settings.RightSplitterPercent = (double)_splitRight.SplitterDistance / _splitRight.Height * 100;

        _settings.Save();
    }

    private void StartClock()
    {
        _clockTimer = new System.Windows.Forms.Timer();
        _clockTimer.Interval = 1000;
        _clockTimer.Tick += ClockTimer_Tick;
        _clockTimer.Start();
    }

    private void ClockTimer_Tick(object? sender, EventArgs e)
    {
        UpdateStatusBar();
    }

    private void UpdateStatusBar()
    {
        _lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        _lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        
        if (!string.IsNullOrEmpty(_databasePath))
        {
            _lblDatabase.Text = $"Database: {Path.GetFileName(_databasePath)}";
        }
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        try
        {
            SaveWindowPosition();

            if (_settings.AutoBackupEnabled && !string.IsNullOrEmpty(_databasePath) && File.Exists(_databasePath))
            {
                string backupPath = _databasePath + ".bak";
                File.Copy(_databasePath, backupPath, true);
            }

            _clockTimer?.Stop();
            _clockTimer?.Dispose();
            _dbContext?.Dispose();
        }
        catch
        {
            // Silently handle cleanup errors
        }
    }

    private async Task LoadGroupsAndTasksAsync()
    {
        if (_dbContext is null || _currentUser is null)
            return;

        _tvwTasks.BeginUpdate();
        _tvwTasks.Nodes.Clear();

        TreeNode rootNode = new TreeNode("All Groups");
        _tvwTasks.Nodes.Add(rootNode);

        List<Group> groups = await _dbContext.Groups
            .Where(g => g.DateDeleted == null && !g.IsHidden)
            .OrderBy(g => g.OrderNo)
            .ToListAsync();

        foreach (Group group in groups)
        {
            string nodeText = group.GroupSymbol is not null 
                ? $"{group.GroupSymbol} {group.GroupDisplayName}" 
                : group.GroupDisplayName;
            
            TreeNode groupNode = new TreeNode(nodeText);
            groupNode.Tag = group;
            rootNode.Nodes.Add(groupNode);

            List<TodoTask> tasks = await GetTasksForGroupAsync(group);

            foreach (TodoTask task in tasks)
            {
                TreeNode taskNode = new TreeNode(task.DisplayName);
                taskNode.Tag = task;
                groupNode.Nodes.Add(taskNode);
            }

            if (tasks.Count == 0)
            {
                TreeNode emptyNode = new TreeNode("(No tasks)");
                emptyNode.ForeColor = Color.Gray;
                groupNode.Nodes.Add(emptyNode);
            }
        }

        rootNode.Expand();
        _tvwTasks.EndUpdate();
    }

    private async Task<List<TodoTask>> GetTasksForGroupAsync(Group group)
    {
        if (_dbContext is null || _currentUser is null)
            return new List<TodoTask>();

        List<TodoTask> tasks = new List<TodoTask>();

        if (group.AutoRangeSpan.HasValue)
        {
            DateTime startDate = DateTime.UtcNow.Date.AddDays(-(group.AutoRangeSpan.Value.Days - 1));
            
            if (group.AutoRangeOffset.HasValue)
            {
                startDate = startDate.AddDays(group.AutoRangeOffset.Value);
            }

            tasks = await _dbContext.TodoTasks
                .Where(t => t.DateDeleted == null 
                    && t.IDUser == _currentUser.IDUser
                    && t.DateCreated >= startDate
                    && t.DateFinished == null)
                .OrderBy(t => t.DisplayName)
                .ToListAsync();
        }
        else
        {
            List<Guid> taskIds = await _dbContext.TasksGroupsRelations
                .Where(r => r.IDGroup == group.IDGroup && r.DateDeleted == null)
                .Select(r => r.IDTodoTask)
                .ToListAsync();

            if (taskIds.Count > 0)
            {
                tasks = await _dbContext.TodoTasks
                    .Where(t => taskIds.Contains(t.IDTodoTask) 
                        && t.DateDeleted == null
                        && t.DateFinished == null)
                    .OrderBy(t => t.DisplayName)
                    .ToListAsync();
            }
        }

        return tasks;
    }

    private void TvwTasks_AfterSelect(object? sender, TreeViewEventArgs e)
    {
        if (e.Node?.Tag is TodoTask task)
        {
            DisplayTaskDetails(task);
        }
        else if (e.Node?.Tag is Group group)
        {
            DisplayGroupInfo(group);
        }
        else
        {
            ClearTaskDetails();
        }
    }

    private async void DisplayTaskDetails(TodoTask task)
    {
        _lblGroupTitle.Text = $"Task: {task.DisplayName}";
        _lblStatus.Text = $"Selected task: {task.DisplayName}";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Display Name: {task.DisplayName}");
        sb.AppendLine($"Description: {task.Description ?? "(none)"}");
        sb.AppendLine($"Created: {task.DateCreated.ToLocalTime():yyyy-MM-dd HH:mm}");
        sb.AppendLine($"Due Date: {(task.DueDate.HasValue ? task.DueDate.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm") : "(not set)")}");
        sb.AppendLine($"Estimated Effort: {(task.EstimatedEffort.HasValue ? task.EstimatedEffort.Value.ToString(@"hh\:mm\:ss") : "(not set)")}");
        sb.AppendLine($"Time Spent: {task.TimeSpent.ToString(@"hh\:mm\:ss")}");
        
        if (task.EstimatedEffort.HasValue && task.EstimatedEffort.Value.TotalMinutes > 0)
        {
            double percentDone = (task.TimeSpent.TotalMinutes / task.EstimatedEffort.Value.TotalMinutes) * 100;
            sb.AppendLine($"Percent Done: {percentDone:F1}%");
        }
        
        sb.AppendLine($"Finished: {(task.DateFinished.HasValue ? task.DateFinished.Value.ToLocalTime().ToString("yyyy-MM-dd HH:mm") : "(not finished)")}");

        _txtTaskInfo.Text = sb.ToString();

        await LoadTimeEntriesAsync(task.IDTodoTask);
    }

    private async Task LoadTimeEntriesAsync(Guid taskId)
    {
        if (_dbContext is null)
            return;

        try
        {
            List<TimeEntry> timeEntries = await _dbContext.TimeEntries
                .Where(e => e.IDTodoTask == taskId && e.DateDeleted == null)
                .OrderByDescending(e => e.StartTime)
                .ToListAsync();

            _dgvTimeEntries.AutoGenerateColumns = false;
            _dgvTimeEntries.Columns.Clear();

            DataGridViewTextBoxColumn descColumn = new DataGridViewTextBoxColumn
            {
                Name = "Description",
                HeaderText = "Description",
                DataPropertyName = "DescriptionDoneWork",
                FillWeight = 40
            };

            DataGridViewTextBoxColumn startColumn = new DataGridViewTextBoxColumn
            {
                Name = "StartTime",
                HeaderText = "Start Time",
                DataPropertyName = "StartTime",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            };

            DataGridViewTextBoxColumn endColumn = new DataGridViewTextBoxColumn
            {
                Name = "EndTime",
                HeaderText = "End Time",
                DataPropertyName = "EndTime",
                FillWeight = 20,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "yyyy-MM-dd HH:mm" }
            };

            DataGridViewTextBoxColumn durationColumn = new DataGridViewTextBoxColumn
            {
                Name = "Duration",
                HeaderText = "Duration",
                DataPropertyName = "Duration",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Format = @"hh\:mm\:ss" }
            };

            _dgvTimeEntries.Columns.AddRange(new DataGridViewColumn[] 
            { 
                descColumn, 
                startColumn, 
                endColumn, 
                durationColumn 
            });

            List<TimeEntryDisplay> displayEntries = timeEntries.Select(e => new TimeEntryDisplay
            {
                DescriptionDoneWork = e.DescriptionDoneWork ?? string.Empty,
                StartTime = e.StartTime.ToLocalTime(),
                EndTime = e.EndTime.ToLocalTime(),
                Duration = e.Duration
            }).ToList();

            _dgvTimeEntries.DataSource = displayEntries;
        }
        catch (Exception ex)
        {
            _lblStatus.Text = $"Error loading time entries: {ex.Message}";
            _lblStatus.ForeColor = Color.Red;
        }
    }

    private class TimeEntryDisplay
    {
        public string DescriptionDoneWork { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }

    private void DisplayGroupInfo(Group group)
    {
        _lblGroupTitle.Text = $"Group: {group.GroupDisplayName}";
        _lblStatus.Text = $"Selected group: {group.GroupDisplayName}";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Display Name: {group.GroupDisplayName}");
        sb.AppendLine($"Description: {group.GroupDescription ?? "(none)"}");
        sb.AppendLine($"Symbol: {group.GroupSymbol ?? "(none)"}");
        sb.AppendLine($"Order: {group.OrderNo?.ToString() ?? "(not set)"}");
        sb.AppendLine($"Hidden: {(group.IsHidden ? "Yes" : "No")}");
        sb.AppendLine($"System Group: {(group.IsSystem ? "Yes" : "No")}");
        
        if (group.AutoRangeSpan.HasValue)
        {
            sb.AppendLine($"Auto Range: {group.AutoRangeSpan.Value.Days} days");
            
            if (group.AutoRangeOffset.HasValue)
            {
                sb.AppendLine($"Auto Range Offset: {group.AutoRangeOffset.Value} days");
            }
        }

        _txtTaskInfo.Text = sb.ToString();
        _dgvTimeEntries.DataSource = null;
    }

    private void ClearTaskDetails()
    {
        _lblGroupTitle.Text = "Task Details";
        _lblStatus.Text = "Ready";
        _txtTaskInfo.Text = "Select a task or group to view details.";
        _dgvTimeEntries.DataSource = null;
    }

    private void TxtNewTask_TextChanged(object? sender, EventArgs e)
    {
        _btnAddTask.Enabled = !string.IsNullOrWhiteSpace(_txtNewTask.Text);
    }

    private async void BtnAddTask_Click(object? sender, EventArgs e)
    {
        if (_dbContext is null || _currentUser is null)
            return;

        string taskName = _txtNewTask.Text.Trim();
        
        if (string.IsNullOrWhiteSpace(taskName))
            return;

        try
        {
            Project? defaultProject = await _dbContext.Projects
                .Where(p => p.IDUser == _currentUser.IDUser && p.IsSystem)
                .FirstOrDefaultAsync();

            if (defaultProject is null)
            {
                _lblStatus.Text = "Error: No default project found";
                return;
            }

            TodoTask newTask = new TodoTask
            {
                IDTodoTask = Guid.NewGuid(),
                IDUser = _currentUser.IDUser,
                IDProject = defaultProject.IDProject,
                DisplayName = taskName,
                TimeSpent = TimeSpan.Zero,
                DateCreated = DateTime.UtcNow,
                DateLastEdited = DateTime.UtcNow,
                SyncGuidChanged = Guid.NewGuid()
            };

            _dbContext.TodoTasks.Add(newTask);
            await _dbContext.SaveChangesAsync();

            TreeNode? selectedNode = _tvwTasks.SelectedNode;
            Group? selectedGroup = null;

            if (selectedNode?.Tag is Group group)
            {
                selectedGroup = group;
            }
            else if (selectedNode?.Tag is TodoTask && selectedNode.Parent?.Tag is Group parentGroup)
            {
                selectedGroup = parentGroup;
            }

            if (selectedGroup is not null && !selectedGroup.AutoRangeSpan.HasValue)
            {
                TasksGroupsRelation relation = new TasksGroupsRelation
                {
                    IDTasksGroups = Guid.NewGuid(),
                    IDTodoTask = newTask.IDTodoTask,
                    IDGroup = selectedGroup.IDGroup,
                    IDUser = _currentUser.IDUser,
                    DateCreated = DateTime.UtcNow,
                    DateLastEdited = DateTime.UtcNow,
                    SyncGuidChanged = Guid.NewGuid()
                };

                _dbContext.TasksGroupsRelations.Add(relation);
                await _dbContext.SaveChangesAsync();
            }

            _txtNewTask.Clear();
            _btnAddTask.Enabled = false;
            
            await LoadGroupsAndTasksAsync();
            
            _lblStatus.Text = $"Task '{taskName}' created successfully";
        }
        catch (Exception ex)
        {
            _lblStatus.Text = $"Error creating task: {ex.Message}";
            _lblStatus.ForeColor = Color.Red;
        }
    }

    private void MenuFileQuit_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
