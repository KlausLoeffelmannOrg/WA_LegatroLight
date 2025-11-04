namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines individual tasks within projects. Tasks are the atomic unit of work.
/// </summary>
public class TodoTask : BaseEntity
{
    public Guid IDUser { get; set; }
    public Guid IDProject { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TimeSpan? EstimatedEffort { get; set; }
    public TimeSpan TimeSpent { get; set; }
    public int PercentDone { get; set; }
    public DateTime? DateFinished { get; set; }
    public int Priority { get; set; } = 3;

    // Navigation properties
    public User? User { get; set; }
    public Project? Project { get; set; }
    public ICollection<TodoTasksGroupsRelation> GroupRelations { get; set; } = [];
    public ICollection<TimeEntry> TimeEntries { get; set; } = [];
}
