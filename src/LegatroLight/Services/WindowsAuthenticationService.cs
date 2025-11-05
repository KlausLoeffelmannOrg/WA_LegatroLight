using System.Security.Principal;

namespace LegatroLight.Services;

/// <summary>
///  Provides Windows authentication information using WindowsIdentity.
/// </summary>
internal class WindowsAuthenticationService : IWindowsAuthenticationService
{
    /// <summary>
    ///  Gets the Windows authentication information for the current user.
    /// </summary>
    /// <returns>
    ///  A <see cref="WindowsAuthInfo"/> containing the current user's authentication details.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///  Thrown when unable to retrieve Windows identity information.
    /// </exception>
    public WindowsAuthInfo GetCurrentUser()
    {
        using WindowsIdentity identity = WindowsIdentity.GetCurrent();

        if (identity?.Name is null)
        {
            throw new InvalidOperationException("Unable to retrieve current Windows identity.");
        }

        string fullName = identity.Name;
        string[] parts = fullName.Split('\\');

        string domainOrMachine = parts.Length == 2 ? parts[0] : Environment.MachineName;
        string userName = parts.Length == 2 ? parts[1] : fullName;

        string displayName = userName;
        try
        {
            displayName = identity.User?.Translate(typeof(NTAccount))?.Value ?? userName;
            if (displayName.Contains('\\'))
            {
                displayName = displayName.Split('\\')[1];
            }
        }
        catch
        {
            displayName = userName;
        }

        return new WindowsAuthInfo
        {
            UserAuthID = fullName,
            UserSid = identity.User?.Value ?? string.Empty,
            DisplayName = displayName,
            UserName = userName,
            DomainOrMachine = domainOrMachine
        };
    }
}
