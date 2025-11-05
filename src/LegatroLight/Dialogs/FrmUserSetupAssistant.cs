using LegatroLight.Data.Entities;
using LegatroLight.Services;

namespace LegatroLight.Dialogs;

/// <summary>
///  User Setup Assistant dialog for first-time user configuration.
/// </summary>
public partial class FrmUserSetupAssistant : Form
{
    private User? _user;

    /// <summary>
    ///  Gets the configured user entity after successful dialog completion.
    /// </summary>
    public User? User => _user;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FrmUserSetupAssistant"/> class.
    /// </summary>
    public FrmUserSetupAssistant()
    {
        InitializeComponent();
    }

    /// <summary>
    ///  Populates the form with Windows authentication information.
    /// </summary>
    /// <param name="authInfo">The Windows authentication information.</param>
    public void PopulateFromWindowsAuth(WindowsAuthInfo authInfo)
    {
        ArgumentNullException.ThrowIfNull(authInfo);

        _txtUserAuthID.Text = authInfo.UserAuthID;
        _txtUserSid.Text = authInfo.UserSid;
        _txtDisplayName.Text = authInfo.DisplayName;
        _txtUserName.Text = authInfo.UserName;
        _txtDomainOrMachine.Text = authInfo.DomainOrMachine;
        _txtNickname.Text = authInfo.UserName;
    }

    /// <summary>
    ///  Handles the OK button click event.
    /// </summary>
    private void BtnOK_Click(object? sender, EventArgs e)
    {
        if (!ValidateInput())
        {
            return;
        }

        _user = new User
        {
            Id = Guid.NewGuid(),
            UserDisplayId = _txtNickname.Text.Trim(),
            UserAuthID = _txtUserAuthID.Text.Trim(),
            UserSid = _txtUserSid.Text.Trim(),
            DomainOrMachine = _txtDomainOrMachine.Text.Trim(),
            UserName = _txtUserName.Text.Trim(),
            DisplayName = _txtDisplayName.Text.Trim(),
            FirstName = string.IsNullOrWhiteSpace(_txtFirstName.Text) ? null : _txtFirstName.Text.Trim(),
            MiddleName = string.IsNullOrWhiteSpace(_txtMiddleName.Text) ? null : _txtMiddleName.Text.Trim(),
            LastName = string.IsNullOrWhiteSpace(_txtLastName.Text) ? null : _txtLastName.Text.Trim(),
            Email = string.IsNullOrWhiteSpace(_txtEmail.Text) ? null : _txtEmail.Text.Trim(),
            DateCreated = DateTime.UtcNow,
            DateLastEdited = DateTime.UtcNow,
            DateDeleted = null,
            SyncGuidChanged = Guid.NewGuid()
        };

        DialogResult = DialogResult.OK;
        Close();
    }

    /// <summary>
    ///  Handles the Cancel button click event.
    /// </summary>
    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    /// <summary>
    ///  Validates the input fields.
    /// </summary>
    /// <returns>True if validation passes; otherwise, false.</returns>
    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(_txtNickname.Text))
        {
            MessageBox.Show(
                this,
                "Nickname is required.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            _txtNickname.Focus();

            return false;
        }

        if (string.IsNullOrWhiteSpace(_txtDisplayName.Text))
        {
            MessageBox.Show(
                this,
                "Display Name is required.",
                "Validation Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

            _txtDisplayName.Focus();

            return false;
        }

        return true;
    }
}
