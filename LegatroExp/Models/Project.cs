namespace LegatroExp.Models;

public class Project : BaseEntity
{
    public required string IDProject
    {
        get => ID;
        set => ID = value;
    }

    public required string IDUser { get; set; }
    public string? IDCategory { get; set; }
    public required string ProjectName { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsSystem { get; set; }
    public TimeSpan? TimeBudget { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public User? User { get; set; }
    public Category? Category { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
