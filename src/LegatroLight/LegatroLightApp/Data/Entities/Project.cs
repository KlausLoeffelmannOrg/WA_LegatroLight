namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines projects that contain tasks and track time budgets.
/// </summary>
public class Project : BaseEntity
{
    public Guid IDUser { get; set; }
    public Guid? IDCategory { get; set; }
    public required string ProjectName { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsSystem { get; set; }
    public TimeSpan? TimeBudget { get; set; }
    public TimeSpan TimeSpent { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Category? Category { get; set; }
    public ICollection<TodoTask> TodoTasks { get; set; } = [];
    public ICollection<TimeEntry> TimeEntries { get; set; } = [];
}
