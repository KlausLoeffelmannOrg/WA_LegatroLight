using System.Security.Principal;
using LegatroExp.Data.Entities;

namespace LegatroExp.Forms;

/// <summary>
/// First-run user setup dialog
/// </summary>
public partial class UserSetupAssistant : Form
{
    public User? CurrentUser { get; private set; }

    public UserSetupAssistant()
    {
        InitializeComponent();
        LoadWindowsUserInfo();
    }

    /// <summary>
    /// Loads current Windows user information
    /// </summary>
    private void LoadWindowsUserInfo()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        string userName = Environment.UserName;
        string domainName = Environment.UserDomainName;
        string displayName = identity.Name ?? userName;

        _txtUserName.Text = displayName;
        
        // Pre-populate nickname with username
        _txtNickname.Text = userName;
    }

    /// <summary>
    /// Handles OK button click
    /// </summary>
    private void BtnOK_Click(object? sender, EventArgs e)
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        string userName = Environment.UserName;
        string domainName = Environment.UserDomainName;
        string displayName = identity.Name ?? userName;

        // Parse display name to extract first/last name if possible
        string[] nameParts = displayName.Split(new[] { '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string firstName = nameParts.Length > 0 ? nameParts[^1] : userName;
        string lastName = nameParts.Length > 1 ? nameParts[^2] : string.Empty;

        // Use nickname as first name if provided
        if (!string.IsNullOrWhiteSpace(_txtNickname.Text))
        {
            firstName = _txtNickname.Text.Trim();
        }

        CurrentUser = new User
        {
            UserDisplayId = _txtNickname.Text?.Trim() ?? userName,
            UserAuthID = identity.Name ?? userName,
            UserSid = identity.User?.Value ?? Guid.NewGuid().ToString(),
            DomainOrMachine = domainName,
            UserName = userName,
            DisplayName = displayName,
            FirstName = firstName,
            LastName = lastName,
            Email = _txtEmail.Text?.Trim()
        };
    }
}
