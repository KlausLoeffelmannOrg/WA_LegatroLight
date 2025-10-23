using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using LegatroExp.Models;

namespace LegatroExp.Helpers;

public static class WindowsAuthHelper
{
    public static User GetCurrentWindowsUser()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        string sid = identity.User?.Value ?? string.Empty;
        string userName = identity.Name ?? string.Empty;
        string displayName = userName;
        string? domain = null;
        string? firstName = null;
        string? lastName = null;
        string? email = null;

        if (userName.Contains('\\'))
        {
            string[] parts = userName.Split('\\');
            domain = parts[0];
            userName = parts[1];
        }

        try
        {
            using PrincipalContext context = new(ContextType.Domain);
            UserPrincipal? userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);

            if (userPrincipal is not null)
            {
                displayName = userPrincipal.DisplayName ?? userName;
                firstName = userPrincipal.GivenName;
                lastName = userPrincipal.Surname;
                email = userPrincipal.EmailAddress;
            }
        }
        catch
        {
            // Not in domain or cannot access domain info
            try
            {
                using PrincipalContext context = new(ContextType.Machine);
                UserPrincipal? userPrincipal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userName);

                if (userPrincipal is not null)
                {
                    displayName = userPrincipal.DisplayName ?? userName;
                    firstName = userPrincipal.GivenName;
                    lastName = userPrincipal.Surname;
                    email = userPrincipal.EmailAddress;
                }
            }
            catch
            {
                // Cannot access local user info either
            }
        }

        string userAuthId = domain is not null ? $"{domain}\\{userName}" : userName;
        string userDisplayId = displayName != userName ? displayName : userAuthId;

        return new User
        {
            IDUser = Guid.NewGuid().ToString(),
            ID = Guid.NewGuid().ToString(),
            SyncGuidChanged = Guid.NewGuid().ToString(),
            UserDisplayId = userDisplayId,
            UserAuthID = userAuthId,
            UserSid = sid,
            DomainOrMachine = domain ?? Environment.MachineName,
            UserName = userName,
            DisplayName = displayName,
            FirstName = firstName,
            LastName = lastName,
            Email = email
        };
    }
}
