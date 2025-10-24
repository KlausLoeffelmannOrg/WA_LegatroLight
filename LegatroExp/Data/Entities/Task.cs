namespace LegatroExp.Data.Entities;

/// <summary>
/// Defines individual tasks within projects
/// </summary>
public class Task : BaseEntity
{
    /// <summary>
    /// Unique task identifier
    /// </summary>
    public Guid IDTask { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Foreign ID for external system integration
    /// </summary>
    public Guid? IDForeignID { get; set; }

    /// <summary>
    /// Sync ID for external system integration
    /// </summary>
    public Guid? IDForeignSyncID { get; set; }

    /// <summary>
    /// User who owns the task
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Associated project
    /// </summary>
    public Guid IDProject { get; set; }

    /// <summary>
    /// Task display name
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Optional task description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Task due date (UTC, nullable)
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Estimated time to complete
    /// </summary>
    public TimeSpan? EstimatedEffort { get; set; }

    /// <summary>
    /// UTC timestamp when task was completed
    /// </summary>
    public DateTime? DateFinished { get; set; }

    /// <summary>
    /// Total time spent (calculated from TimeEntries)
    /// </summary>
    public TimeSpan TimeSpent { get; set; } = TimeSpan.Zero;

    // Navigation properties
    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
}
