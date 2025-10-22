using LegatroExp.Data;
using LegatroExp.Helpers;
using LegatroExp.Models;

namespace LegatroExp.Forms;

public partial class MainForm : Form
{
    private readonly LegatroDbContext _context;
    private readonly User _currentUser;
    private readonly AppSettings _settings;

    public MainForm(LegatroDbContext context, User currentUser, AppSettings settings)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        InitializeComponent();
        InitializeForm();
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

        if (_scMain.Panel1.Width > 0)
        {
            _scMain.SplitterDistance = (int)(_scMain.Width * _settings.MainWindow.VerticalSplitterPosition);
        }

        if (_scRight.Panel1.Height > 0)
        {
            _scRight.SplitterDistance = (int)(_scRight.Height * _settings.MainWindow.HorizontalSplitterPosition);
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
        // TODO: Implement data loading
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        if (e.Cancel)
        {
            return;
        }

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
}
