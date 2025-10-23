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

    private void MenuFileQuit_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
