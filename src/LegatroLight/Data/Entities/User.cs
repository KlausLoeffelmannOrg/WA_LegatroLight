namespace LegatroLight.Data.Entities;

/// <summary>
/// Represents a user authenticated via Windows authentication.
/// </summary>
public class User : BaseEntity
{
    public required string UserDisplayId { get; set; }
    public required string UserAuthID { get; set; }
    public required string UserSid { get; set; }
    public string? DomainOrMachine { get; set; }
    public required string UserName { get; set; }
    public required string DisplayName { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Group> Groups { get; set; } = [];
    public ICollection<Project> Projects { get; set; } = [];
    public ICollection<TodoTask> TodoTasks { get; set; } = [];
    public ICollection<TimeEntry> TimeEntries { get; set; } = [];
}
