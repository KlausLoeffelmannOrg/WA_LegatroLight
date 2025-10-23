using LegatroExp.Models;

namespace LegatroExp.Forms;

public partial class UserSetupForm : Form
{
    private readonly User _user;

    public User User => _user;

    public UserSetupForm(User user)
    {
        _user = user ?? throw new ArgumentNullException(nameof(user));
        InitializeComponent();
        LoadUserData();
    }

    private void LoadUserData()
    {
        _txtUserDisplayId.Text = _user.UserDisplayId;
        _txtUserAuthId.Text = _user.UserAuthID;
        _txtUserSid.Text = _user.UserSid;
        _txtDomain.Text = _user.DomainOrMachine ?? string.Empty;
        _txtUserName.Text = _user.UserName;
        _txtDisplayName.Text = _user.DisplayName;
        _txtFirstName.Text = _user.FirstName ?? string.Empty;
        _txtMiddleName.Text = _user.MiddleName ?? string.Empty;
        _txtLastName.Text = _user.LastName ?? string.Empty;
        _txtEmail.Text = _user.Email ?? string.Empty;
    }

    private void SaveUserData()
    {
        _user.UserDisplayId = _txtUserDisplayId.Text.Trim();
        _user.FirstName = string.IsNullOrWhiteSpace(_txtFirstName.Text) ? null : _txtFirstName.Text.Trim();
        _user.MiddleName = string.IsNullOrWhiteSpace(_txtMiddleName.Text) ? null : _txtMiddleName.Text.Trim();
        _user.LastName = string.IsNullOrWhiteSpace(_txtLastName.Text) ? null : _txtLastName.Text.Trim();
        _user.Email = string.IsNullOrWhiteSpace(_txtEmail.Text) ? null : _txtEmail.Text.Trim();
        _user.DisplayName = _txtDisplayName.Text.Trim();
    }

    private void BtnOK_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_txtUserDisplayId.Text))
        {
            MessageBox.Show("User Display ID cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtUserDisplayId.Focus();
            return;
        }

        if (string.IsNullOrWhiteSpace(_txtDisplayName.Text))
        {
            MessageBox.Show("Display Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            _txtDisplayName.Focus();
            return;
        }

        SaveUserData();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
