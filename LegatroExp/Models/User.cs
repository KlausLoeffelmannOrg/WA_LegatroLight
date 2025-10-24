namespace LegatroExp.Models;

/// <summary>
///  Represents a user authenticated via Windows credentials.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the user.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    ///  Gets or sets the user-friendly display ID.
    /// </summary>
    public string UserDisplayId { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the Windows authentication ID.
    /// </summary>
    public string UserAuthID { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the Windows Security Identifier (SID).
    /// </summary>
    public string UserSid { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the domain or machine name.
    /// </summary>
    public string? DomainOrMachine { get; set; }

    /// <summary>
    ///  Gets or sets the Windows username.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the full display name.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the first name.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///  Gets or sets the middle name.
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    ///  Gets or sets the last name.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///  Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
}
