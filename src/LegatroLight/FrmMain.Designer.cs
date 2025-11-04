using Microsoft.Extensions.DependencyInjection;
using WarpToolkit.ComponentModel;

namespace LegatroLight;

public partial class FrmMain : Form, IServiceProvider
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private readonly IUserSettingsService _userSettingsService = null!;
    private readonly IServiceProvider _serviceProvider = null!;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FrmMain"/> class with dependency injection support.
    /// </summary>
    /// <param name="serviceProvider">
    ///  The service provider that contains all registered services for dependency injection.
    ///  This parameter is used to resolve dependencies and configure the form with the required services.
    /// </param>
    /// <exception cref="ArgumentNullException">
    ///  Thrown when <paramref name="serviceProvider"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="NullReferenceException">
    ///  Thrown when the required <see cref="IUserSettingsService"/> is not registered in the service provider.
    /// </exception>
    /// <remarks>
    ///  This constructor overload is specifically designed to be used when the Form is instantiated 
    ///  through Dependency Injection (DI) using the <c>WinFormsApplication</c> class and the 
    ///  <c>WinFormsApplicationBuilder</c>. This approach provides the same infrastructure pattern 
    ///  as ASP.NET Core applications, enabling familiar service registration, configuration, 
    ///  and dependency injection patterns in WinForms applications.
    ///  <para>
    ///   When using this constructor, the Form acts as a ServiceProvider-aware component, 
    ///   allowing it to resolve and utilize services that have been registered in the 
    ///   application's service container. This enables loose coupling, testability, 
    ///   and modern application architecture patterns in WinForms development.
    ///  </para>
    ///  <para>
    ///   The constructor automatically assigns the service provider to the form using the 
    ///   <c>AssignServiceProvider</c> extension method and resolves the required 
    ///   <see cref="IUserSettingsService"/> from the container.
    ///  </para>
    /// </remarks>
    public FrmMain(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
        _serviceProvider = serviceProvider;

        _userSettingsService = serviceProvider.GetRequiredService<IUserSettingsService>();

        if (_userSettingsService is null)
        {
            throw new NullReferenceException($"The service '{nameof(IUserSettingsService)}' is not registered.");
        }

        InitializeComponent();
    }

    object IServiceProvider.GetService(Type serviceType)
    {
        ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

        if (_serviceProvider is null)
        {
            throw new InvalidOperationException("Service provider is not initialized.");
        }

        return _serviceProvider.GetService(serviceType)
            ?? throw new InvalidOperationException($"Service of type '{serviceType.Name}' is not registered.");
    }

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        _mainMenuStrip = new MenuStrip();
        _tsmFile = new ToolStripMenuItem();
        _tsmFileNew = new ToolStripMenuItem();
        _tsmFileOpen = new ToolStripMenuItem();
        _tsmFileClose = new ToolStripMenuItem();
        _tsmFileSeparator1 = new ToolStripSeparator();
        _tsmFileRecent = new ToolStripMenuItem();
        _tsmFileSeparator2 = new ToolStripSeparator();
        _tsmFileExit = new ToolStripMenuItem();
        _tsmEdit = new ToolStripMenuItem();
        _tsmView = new ToolStripMenuItem();
        _tsmTools = new ToolStripMenuItem();
        _mainStatusStrip = new StatusStrip();
        _statusLabelDatabase = new ToolStripStatusLabel();
        _statusSeparator1 = new ToolStripStatusLabel();
        _statusLabelToday = new ToolStripStatusLabel();
        _statusSeparator2 = new ToolStripStatusLabel();
        _statusLabelWeek = new ToolStripStatusLabel();
        _statusSeparator3 = new ToolStripStatusLabel();
        _statusLabelSession = new ToolStripStatusLabel();
        _statusSeparator4 = new ToolStripStatusLabel();
        _statusLabelDateTime = new ToolStripStatusLabel();
        _mainMenuStrip.SuspendLayout();
        _mainStatusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _mainMenuStrip
        // 
        _mainMenuStrip.ImageScalingSize = new Size(20, 20);
        _mainMenuStrip.Items.AddRange(new ToolStripItem[] { _tsmFile, _tsmEdit, _tsmView, _tsmTools });
        _mainMenuStrip.Location = new Point(0, 0);
        _mainMenuStrip.Name = "_mainMenuStrip";
        _mainMenuStrip.Size = new Size(1006, 28);
        _mainMenuStrip.TabIndex = 0;
        // 
        // _tsmFile
        // 
        _tsmFile.DropDownItems.AddRange(new ToolStripItem[] { _tsmFileNew, _tsmFileOpen, _tsmFileClose, _tsmFileSeparator1, _tsmFileRecent, _tsmFileSeparator2, _tsmFileExit });
        _tsmFile.Name = "_tsmFile";
        _tsmFile.Size = new Size(46, 24);
        _tsmFile.Text = "&File";
        // 
        // _tsmFileNew
        // 
        _tsmFileNew.Name = "_tsmFileNew";
        _tsmFileNew.ShortcutKeys = Keys.Control | Keys.N;
        _tsmFileNew.Size = new Size(223, 26);
        _tsmFileNew.Text = "&New Solution...";
        _tsmFileNew.Click += TsmFileNew_Click;
        // 
        // _tsmFileOpen
        // 
        _tsmFileOpen.Name = "_tsmFileOpen";
        _tsmFileOpen.ShortcutKeys = Keys.Control | Keys.O;
        _tsmFileOpen.Size = new Size(223, 26);
        _tsmFileOpen.Text = "&Open Solution...";
        _tsmFileOpen.Click += TsmFileOpen_Click;
        // 
        // _tsmFileClose
        // 
        _tsmFileClose.Enabled = false;
        _tsmFileClose.Name = "_tsmFileClose";
        _tsmFileClose.Size = new Size(223, 26);
        _tsmFileClose.Text = "&Close Solution";
        _tsmFileClose.Click += TsmFileClose_Click;
        // 
        // _tsmFileSeparator1
        // 
        _tsmFileSeparator1.Name = "_tsmFileSeparator1";
        _tsmFileSeparator1.Size = new Size(220, 6);
        // 
        // _tsmFileRecent
        // 
        _tsmFileRecent.Enabled = false;
        _tsmFileRecent.Name = "_tsmFileRecent";
        _tsmFileRecent.Size = new Size(223, 26);
        _tsmFileRecent.Text = "&Recent Files";
        // 
        // _tsmFileSeparator2
        // 
        _tsmFileSeparator2.Name = "_tsmFileSeparator2";
        _tsmFileSeparator2.Size = new Size(220, 6);
        // 
        // _tsmFileExit
        // 
        _tsmFileExit.Name = "_tsmFileExit";
        _tsmFileExit.ShortcutKeys = Keys.Alt | Keys.F4;
        _tsmFileExit.Size = new Size(223, 26);
        _tsmFileExit.Text = "E&xit";
        _tsmFileExit.Click += TsmFileExit_Click;
        // 
        // _tsmEdit
        // 
        _tsmEdit.Name = "_tsmEdit";
        _tsmEdit.Size = new Size(49, 24);
        _tsmEdit.Text = "&Edit";
        // 
        // _tsmView
        // 
        _tsmView.Name = "_tsmView";
        _tsmView.Size = new Size(55, 24);
        _tsmView.Text = "&View";
        // 
        // _tsmTools
        // 
        _tsmTools.Name = "_tsmTools";
        _tsmTools.Size = new Size(58, 24);
        _tsmTools.Text = "&Tools";
        // 
        // _mainStatusStrip
        // 
        _mainStatusStrip.ImageScalingSize = new Size(20, 20);
        _mainStatusStrip.Items.AddRange(new ToolStripItem[] { _statusLabelDatabase, _statusSeparator1, _statusLabelToday, _statusSeparator2, _statusLabelWeek, _statusSeparator3, _statusLabelSession, _statusSeparator4, _statusLabelDateTime });
        _mainStatusStrip.Location = new Point(0, 696);
        _mainStatusStrip.Name = "_mainStatusStrip";
        _mainStatusStrip.Size = new Size(1006, 25);
        _mainStatusStrip.TabIndex = 1;
        // 
        // _statusLabelDatabase
        // 
        _statusLabelDatabase.Name = "_statusLabelDatabase";
        _statusLabelDatabase.Size = new Size(101, 20);
        _statusLabelDatabase.Text = "Database: N/A";
        // 
        // _statusSeparator1
        // 
        _statusSeparator1.Name = "_statusSeparator1";
        _statusSeparator1.Size = new Size(13, 20);
        _statusSeparator1.Text = "|";
        // 
        // _statusLabelToday
        // 
        _statusLabelToday.Name = "_statusLabelToday";
        _statusLabelToday.Size = new Size(96, 20);
        _statusLabelToday.Text = "Today: 0:00:00";
        // 
        // _statusSeparator2
        // 
        _statusSeparator2.Name = "_statusSeparator2";
        _statusSeparator2.Size = new Size(13, 20);
        _statusSeparator2.Text = "|";
        // 
        // _statusLabelWeek
        // 
        _statusLabelWeek.Name = "_statusLabelWeek";
        _statusLabelWeek.Size = new Size(95, 20);
        _statusLabelWeek.Text = "Week: 0:00:00";
        // 
        // _statusSeparator3
        // 
        _statusSeparator3.Name = "_statusSeparator3";
        _statusSeparator3.Size = new Size(13, 20);
        _statusSeparator3.Text = "|";
        // 
        // _statusLabelSession
        // 
        _statusLabelSession.Name = "_statusLabelSession";
        _statusLabelSession.Size = new Size(107, 20);
        _statusLabelSession.Text = "Session: 0:00:00";
        // 
        // _statusSeparator4
        // 
        _statusSeparator4.Name = "_statusSeparator4";
        _statusSeparator4.Size = new Size(13, 20);
        _statusSeparator4.Text = "|";
        // 
        // _statusLabelDateTime
        // 
        _statusLabelDateTime.Name = "_statusLabelDateTime";
        _statusLabelDateTime.Size = new Size(130, 20);
        _statusLabelDateTime.Text = "2025-10-22 14:32:15";
        // 
        // FrmMain
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1006, 721);
        Controls.Add(_mainStatusStrip);
        Controls.Add(_mainMenuStrip);
        MainMenuStrip = _mainMenuStrip;
        Name = "FrmMain";
        Text = "LegatroLight";
        _mainMenuStrip.ResumeLayout(false);
        _mainMenuStrip.PerformLayout();
        _mainStatusStrip.ResumeLayout(false);
        _mainStatusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private MenuStrip _mainMenuStrip;
    private ToolStripMenuItem _tsmFile;
    private ToolStripMenuItem _tsmFileNew;
    private ToolStripMenuItem _tsmFileOpen;
    private ToolStripMenuItem _tsmFileClose;
    private ToolStripSeparator _tsmFileSeparator1;
    private ToolStripMenuItem _tsmFileRecent;
    private ToolStripSeparator _tsmFileSeparator2;
    private ToolStripMenuItem _tsmFileExit;
    private ToolStripMenuItem _tsmEdit;
    private ToolStripMenuItem _tsmView;
    private ToolStripMenuItem _tsmTools;
    private StatusStrip _mainStatusStrip;
    private ToolStripStatusLabel _statusLabelDatabase;
    private ToolStripStatusLabel _statusSeparator1;
    private ToolStripStatusLabel _statusLabelToday;
    private ToolStripStatusLabel _statusSeparator2;
    private ToolStripStatusLabel _statusLabelWeek;
    private ToolStripStatusLabel _statusSeparator3;
    private ToolStripStatusLabel _statusLabelSession;
    private ToolStripStatusLabel _statusSeparator4;
    private ToolStripStatusLabel _statusLabelDateTime;
}
