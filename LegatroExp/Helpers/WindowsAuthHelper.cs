using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using LegatroExp.Models;

namespace LegatroExp.Helpers;

/// <summary>
///  Helper class for Windows authentication and user information retrieval.
/// </summary>
public static class WindowsAuthHelper
{
    /// <summary>
    ///  Gets the current Windows user information.
    /// </summary>
    /// <returns>A User object populated with Windows authentication data.</returns>
    public static User GetCurrentWindowsUser()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        User user = new User
        {
            IDUser = Guid.NewGuid(),
            UserSid = identity.User?.Value ?? string.Empty,
            UserAuthID = identity.Name ?? string.Empty,
            UserName = Environment.UserName,
            DateCreated = DateTime.UtcNow,
            DateLastEdited = DateTime.UtcNow,
            SyncGuidChanged = Guid.NewGuid()
        };

        // Try to get more detailed information from Active Directory
        try
        {
            using PrincipalContext context = new PrincipalContext(ContextType.Domain);
            using UserPrincipal principal = UserPrincipal.FindByIdentity(context, identity.Name);
            
            if (principal is not null)
            {
                user.DisplayName = principal.DisplayName ?? user.UserName;
                user.FirstName = principal.GivenName;
                user.MiddleName = principal.MiddleName;
                user.LastName = principal.Surname;
                user.Email = principal.EmailAddress;
                
                string[] nameParts = identity.Name?.Split('\\') ?? Array.Empty<string>();
                user.DomainOrMachine = nameParts.Length > 1 ? nameParts[0] : Environment.MachineName;
            }
        }
        catch
        {
            // Not in a domain, use local machine context
            try
            {
                using PrincipalContext context = new PrincipalContext(ContextType.Machine);
                using UserPrincipal principal = UserPrincipal.FindByIdentity(context, identity.Name);
                
                if (principal is not null)
                {
                    user.DisplayName = principal.DisplayName ?? user.UserName;
                    user.FirstName = principal.GivenName;
                    user.MiddleName = principal.MiddleName;
                    user.LastName = principal.Surname;
                    user.Email = principal.EmailAddress;
                }
                
                user.DomainOrMachine = Environment.MachineName;
            }
            catch
            {
                // Fallback to basic information
                user.DisplayName = user.UserName;
                user.DomainOrMachine = Environment.MachineName;
            }
        }

        // Generate UserDisplayId
        user.UserDisplayId = !string.IsNullOrEmpty(user.DisplayName) ? user.DisplayName : user.UserName;

        return user;
    }
}
