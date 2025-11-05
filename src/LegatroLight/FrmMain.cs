using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WarpToolkit.WinForms.Extensions.UI;
using LegatroLight.Data.Context;
using LegatroLight.Data.Entities;
using LegatroLight.Services;
using LegatroLight.Dialogs;

namespace LegatroLight;

public partial class FrmMain : Form, IServiceProvider
{
    private static readonly string SettingsKey_FrmMainBounds = nameof(SettingsKey_FrmMainBounds);
    private static readonly string SettingsKey_LastDatabase = nameof(SettingsKey_LastDatabase);
    private static readonly string SettingsKey_RecentFiles = nameof(SettingsKey_RecentFiles);
    private const int MaxRecentFiles = 10;

    private string? _currentDatabasePath;
    private LegatroDbContext? _dbContext;
    private System.Windows.Forms.Timer? _clockTimer;

    public FrmMain()
    {
        InitializeComponent();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        Bounds = _userSettingsService.GetSetting(
            key: SettingsKey_FrmMainBounds,
            defaultValue: this.CenterToScreen(
                horizontalFillGrade: 70,
                verticalFillGrade: 70));

        InitializeClockTimer();
        LoadRecentFilesMenu();

        string? lastDb = _userSettingsService.GetSetting<string?>(SettingsKey_LastDatabase, null);
        if (!string.IsNullOrEmpty(lastDb) && File.Exists(lastDb))
        {
            OpenDatabase(lastDb);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        _clockTimer?.Stop();
        _clockTimer?.Dispose();

        CloseDatabase();

        _userSettingsService.SaveSetting(
            key: SettingsKey_FrmMainBounds,
            value: Bounds);

        _userSettingsService.Save();
    }

    private void InitializeClockTimer()
    {
        _clockTimer = new System.Windows.Forms.Timer
        {
            Interval = 1000
        };
        _clockTimer.Tick += ClockTimer_Tick;
        _clockTimer.Start();
    }

    private void ClockTimer_Tick(object? sender, EventArgs e)
    {
        _statusLabelDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private async void OpenDatabase(string filePath)
    {
        try
        {
            CloseDatabase();

            string connectionString = $"Data Source={filePath}";
            DbContextOptionsBuilder<LegatroDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlite(connectionString);

            _dbContext = new LegatroDbContext(optionsBuilder.Options);
            _dbContext.Database.EnsureCreated();

            _currentDatabasePath = filePath;
            _tsmFileClose.Enabled = true;

            UpdateDatabaseStatusLabel();
            AddToRecentFiles(filePath);

            _userSettingsService.SaveSetting(SettingsKey_LastDatabase, filePath);

            await CheckAndHandleFirstStartAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                this,
                $"Failed to open database:\n\n{ex.Message}",
                "Error Opening Database",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void CloseDatabase()
    {
        if (_dbContext is not null)
        {
            _dbContext.Dispose();
            _dbContext = null;
        }

        _currentDatabasePath = null;
        _tsmFileClose.Enabled = false;

        UpdateDatabaseStatusLabel();
    }

    private void UpdateDatabaseStatusLabel()
    {
        if (string.IsNullOrEmpty(_currentDatabasePath))
        {
            _statusLabelDatabase.Text = "Database: N/A";
        }
        else
        {
            _statusLabelDatabase.Text = $"Database: {_currentDatabasePath}";
        }
    }

    private void AddToRecentFiles(string filePath)
    {
        List<string> recentFiles = _userSettingsService.GetSetting<List<string>>(
            SettingsKey_RecentFiles, 
            new List<string>());

        recentFiles.Remove(filePath);
        recentFiles.Insert(0, filePath);

        if (recentFiles.Count > MaxRecentFiles)
        {
            recentFiles = recentFiles.Take(MaxRecentFiles).ToList();
        }

        _userSettingsService.SaveSetting(SettingsKey_RecentFiles, recentFiles);
        LoadRecentFilesMenu();
    }

    private void LoadRecentFilesMenu()
    {
        _tsmFileRecent.DropDownItems.Clear();

        List<string> recentFiles = _userSettingsService.GetSetting<List<string>>(
            SettingsKey_RecentFiles,
            new List<string>());

        if (recentFiles.Count == 0)
        {
            _tsmFileRecent.Enabled = false;
            return;
        }

        _tsmFileRecent.Enabled = true;

        foreach (string filePath in recentFiles.Where(File.Exists))
        {
            ToolStripMenuItem menuItem = new(filePath);
            menuItem.Click += (s, e) => OpenDatabase(filePath);
            _tsmFileRecent.DropDownItems.Add(menuItem);
        }

        if (_tsmFileRecent.DropDownItems.Count > 0)
        {
            _tsmFileRecent.DropDownItems.Add(new ToolStripSeparator());
            ToolStripMenuItem clearItem = new("Clear Recent Files");
            clearItem.Click += (s, e) =>
            {
                _userSettingsService.SaveSetting(SettingsKey_RecentFiles, new List<string>());
                LoadRecentFilesMenu();
            };
            _tsmFileRecent.DropDownItems.Add(clearItem);
        }
    }

    private void TsmFileNew_Click(object? sender, EventArgs e)
    {
        using SaveFileDialog dialog = new()
        {
            Filter = "LegatroLight Database (*.legatro)|*.legatro|All Files (*.*)|*.*",
            DefaultExt = "legatro",
            AddExtension = true,
            Title = "Create New Database"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            if (File.Exists(dialog.FileName))
            {
                File.Delete(dialog.FileName);
            }

            OpenDatabase(dialog.FileName);
        }
    }

    private void TsmFileOpen_Click(object? sender, EventArgs e)
    {
        using OpenFileDialog dialog = new()
        {
            Filter = "LegatroLight Database (*.legatro)|*.legatro|All Files (*.*)|*.*",
            DefaultExt = "legatro",
            Title = "Open Database"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
        {
            OpenDatabase(dialog.FileName);
        }
    }

    private void TsmFileClose_Click(object? sender, EventArgs e)
    {
        CloseDatabase();
    }

    private void TsmFileExit_Click(object? sender, EventArgs e)
    {
        Close();
    }

    /// <summary>
    ///  Checks if this is a first start (empty Users table) and handles user setup.
    /// </summary>
    private async Task CheckAndHandleFirstStartAsync()
    {
        if (_dbContext is null)
        {
            return;
        }

        bool hasUsers = await _dbContext.Users.AnyAsync();

        if (!hasUsers)
        {
            IWindowsAuthenticationService authService = _serviceProvider.GetRequiredService<IWindowsAuthenticationService>();
            IDatabaseSeedService seedService = _serviceProvider.GetRequiredService<IDatabaseSeedService>();

            WindowsAuthInfo authInfo = authService.GetCurrentUser();

            using FrmUserSetupAssistant dialog = new();
            dialog.PopulateFromWindowsAuth(authInfo);

            if (dialog.ShowDialog(this) == DialogResult.OK && dialog.User is not null)
            {
                User newUser = dialog.User;

                _dbContext.Users.Add(newUser);
                await _dbContext.SaveChangesAsync();

                await seedService.SeedSystemDataAsync(_dbContext, newUser.Id);

                MessageBox.Show(
                    this,
                    "User setup completed successfully. System data has been initialized.",
                    "Setup Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    this,
                    "User setup was cancelled. The application requires a user account to continue.",
                    "Setup Cancelled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
    }
}
