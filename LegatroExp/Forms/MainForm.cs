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

    public MainForm(DatabaseService databaseService, SettingsService settingsService)
    {
        _databaseService = databaseService;
        _settingsService = settingsService;

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

        // Update status bar
        string dbName = Path.GetFileNameWithoutExtension(_settingsService.Settings.LastDatabasePath ?? "Unknown");
        _toolStripStatusLabel.Text = $"Database: {dbName} | Session: {DateTime.Now:g}";
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

        _settingsService.SaveSettings();
    }
}
