using LegatroExp.Models;
using LegatroExp.Helpers;

namespace LegatroExp.Forms;

/// <summary>
///  User setup assistant form displayed on first run.
/// </summary>
public partial class UserSetupForm : Form
{
    private User _currentUser;

    public User CurrentUser => _currentUser;

    public UserSetupForm()
    {
        InitializeComponent();
        
        _currentUser = WindowsAuthHelper.GetCurrentWindowsUser();
        PopulateUserInformation();
    }

    private void PopulateUserInformation()
    {
        _txtUserName.Text = _currentUser.UserName;
        _txtDisplayName.Text = _currentUser.DisplayName;
        _txtFirstName.Text = _currentUser.FirstName ?? string.Empty;
        _txtMiddleName.Text = _currentUser.MiddleName ?? string.Empty;
        _txtLastName.Text = _currentUser.LastName ?? string.Empty;
        _txtEmail.Text = _currentUser.Email ?? string.Empty;
        _txtDomain.Text = _currentUser.DomainOrMachine ?? string.Empty;
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        _currentUser.DisplayName = _txtDisplayName.Text;
        _currentUser.FirstName = string.IsNullOrWhiteSpace(_txtFirstName.Text) ? null : _txtFirstName.Text;
        _currentUser.MiddleName = string.IsNullOrWhiteSpace(_txtMiddleName.Text) ? null : _txtMiddleName.Text;
        _currentUser.LastName = string.IsNullOrWhiteSpace(_txtLastName.Text) ? null : _txtLastName.Text;
        _currentUser.Email = string.IsNullOrWhiteSpace(_txtEmail.Text) ? null : _txtEmail.Text;
        _currentUser.UserDisplayId = _currentUser.DisplayName;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
