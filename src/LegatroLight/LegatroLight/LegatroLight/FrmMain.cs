using WarpToolkit.WinForms.Extensions.UI;

namespace LegatroLight;

public partial class FrmMain : Form, IServiceProvider
{
    private static readonly string SettingsKey_FrmMainBounds
        = nameof(SettingsKey_FrmMainBounds);

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
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        _userSettingsService.SaveSetting(
            key: SettingsKey_FrmMainBounds,
            value: Bounds);

        _userSettingsService.Save();
    }
}
