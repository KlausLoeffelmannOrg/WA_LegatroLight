using LegatroExp.Helpers;

namespace LegatroExp.Forms;

public partial class OptionsForm : Form
{
    private readonly AppSettings _settings;

    public OptionsForm(AppSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        _cmbSortOrder.SelectedItem = _settings.DefaultSortOrder;
        _chkAutoBackup.Checked = _settings.AutoBackup;
        _cmbLanguage.SelectedItem = _settings.Language;

        LoadInstalledFonts();
        SelectFont(_settings.BaseFont);
    }

    private void LoadInstalledFonts()
    {
        using System.Drawing.Text.InstalledFontCollection fontCollection = new();

        foreach (FontFamily family in fontCollection.Families)
        {
            _cmbBaseFont.Items.Add(family.Name);
        }
    }

    private void SelectFont(string fontName)
    {
        for (int i = 0; i < _cmbBaseFont.Items.Count; i++)
        {
            if (_cmbBaseFont.Items[i]?.ToString() == fontName)
            {
                _cmbBaseFont.SelectedIndex = i;
                return;
            }
        }

        _cmbBaseFont.Text = fontName;
    }

    private void SaveSettings()
    {
        _settings.DefaultSortOrder = _cmbSortOrder.SelectedItem?.ToString() ?? "DisplayName";
        _settings.AutoBackup = _chkAutoBackup.Checked;
        _settings.Language = _cmbLanguage.SelectedItem?.ToString() ?? "English";
        _settings.BaseFont = _cmbBaseFont.Text;
        _settings.Save();
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        SaveSettings();

        if (_settings.Language != _cmbLanguage.SelectedItem?.ToString())
        {
            MessageBox.Show(
                "The application needs to restart for language changes to take effect.",
                "Language Changed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
