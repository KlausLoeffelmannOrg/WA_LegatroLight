namespace LegatroExp.Data.Entities;

/// <summary>
/// Stores user information retrieved from Windows authentication
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Unique user identifier
    /// </summary>
    public Guid IDUser { get; set; } = Guid.NewGuid();

    /// <summary>
    /// User-friendly display ID
    /// </summary>
    public string UserDisplayId { get; set; } = string.Empty;

    /// <summary>
    /// Windows authentication ID
    /// </summary>
    public string UserAuthID { get; set; } = string.Empty;

    /// <summary>
    /// Windows Security Identifier (SID)
    /// </summary>
    public string UserSid { get; set; } = string.Empty;

    /// <summary>
    /// Domain or machine name
    /// </summary>
    public string? DomainOrMachine { get; set; }

    /// <summary>
    /// Windows username
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Full display name
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// First name
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Middle name
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Last name
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email address
    /// </summary>
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
}
