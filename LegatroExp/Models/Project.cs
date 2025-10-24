namespace LegatroExp.Models;

/// <summary>
///  Represents a project that contains tasks and tracks time budgets.
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the project.
    /// </summary>
    public Guid IDProject { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the user who owns the project.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated category.
    /// </summary>
    public Guid? IDCategory { get; set; }

    /// <summary>
    ///  Gets or sets the name of the project.
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the optional project description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///  Gets or sets the project due date (UTC).
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    ///  Gets or sets whether this is a system project that cannot be edited or deleted.
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    ///  Gets or sets the allocated time budget.
    /// </summary>
    public TimeSpan? TimeBudget { get; set; }

    /// <summary>
    ///  Gets or sets the total time spent (calculated from TimeEntries).
    /// </summary>
    public TimeSpan TimeSpent { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
