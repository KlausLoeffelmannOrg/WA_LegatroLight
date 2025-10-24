namespace LegatroExp.Models;

/// <summary>
///  Represents an individual task within a project.
/// </summary>
public class TodoTask : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the task.
    /// </summary>
    public Guid IDTodoTask { get; set; }

    /// <summary>
    ///  Gets or sets the foreign ID for external system integration.
    /// </summary>
    public Guid? IDForeignID { get; set; }

    /// <summary>
    ///  Gets or sets the sync ID for external system integration.
    /// </summary>
    public Guid? IDForeignSyncID { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the user who owns the task.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated project.
    /// </summary>
    public Guid IDProject { get; set; }

    /// <summary>
    ///  Gets or sets the task display name.
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the optional task description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///  Gets or sets the task due date (UTC).
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    ///  Gets or sets the estimated time to complete.
    /// </summary>
    public TimeSpan? EstimatedEffort { get; set; }

    /// <summary>
    ///  Gets or sets the date and time when the task was completed (UTC).
    /// </summary>
    public DateTime? DateFinished { get; set; }

    /// <summary>
    ///  Gets or sets the total time spent (calculated from TimeEntries).
    /// </summary>
    public TimeSpan TimeSpent { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
