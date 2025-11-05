namespace LegatroLight.Services;

/// <summary>
///  Provides Windows authentication information for the current user.
/// </summary>
public interface IWindowsAuthenticationService
{
    /// <summary>
    ///  Gets the Windows authentication information for the current user.
    /// </summary>
    /// <returns>
    ///  A <see cref="WindowsAuthInfo"/> containing the current user's authentication details.
    /// </returns>
    WindowsAuthInfo GetCurrentUser();
}

/// <summary>
///  Contains Windows authentication information for a user.
/// </summary>
public record WindowsAuthInfo
{
    /// <summary>
    ///  Gets the user authentication identifier in the format DOMAIN\username or MACHINE\username.
    /// </summary>
    public required string UserAuthID { get; init; }

    /// <summary>
    ///  Gets the Windows Security Identifier (SID) for the user.
    /// </summary>
    public required string UserSid { get; init; }

    /// <summary>
    ///  Gets the display name of the user (full name from Active Directory or local account).
    /// </summary>
    public required string DisplayName { get; init; }

    /// <summary>
    ///  Gets the username without the domain or machine prefix.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    ///  Gets the domain name or machine name for the user.
    /// </summary>
    public required string DomainOrMachine { get; init; }
}
