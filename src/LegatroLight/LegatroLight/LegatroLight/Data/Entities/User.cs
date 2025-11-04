namespace LegatroLight.Data.Entities;

/// <summary>
/// Represents a user authenticated via Windows authentication.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Gets or sets the user-friendly display ID (e.g., "klaus", "john.smith").
    /// </summary>
    public required string UserDisplayId { get; set; }

    /// <summary>
    /// Gets or sets the Windows authentication ID (e.g., "DOMAIN\username").
    /// </summary>
    public required string UserAuthID { get; set; }

    /// <summary>
    /// Gets or sets the Windows Security Identifier (SID).
    /// </summary>
    public required string UserSid { get; set; }

    /// <summary>
    /// Gets or sets the domain name or machine name for the user.
    /// </summary>
    public string? DomainOrMachine { get; set; }

    /// <summary>
    /// Gets or sets the Windows username without domain.
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Gets or sets the full display name (e.g., "Klaus Loeffelmann").
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's middle name or initial.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Group> Groups { get; set; } = [];
    public ICollection<Project> Projects { get; set; } = [];
}
