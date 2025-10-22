namespace LegatroExp.Models;

public class User : BaseEntity
{
    public required string IDUser
    {
        get => ID;
        set => ID = value;
    }

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

    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
