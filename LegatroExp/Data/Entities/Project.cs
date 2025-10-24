namespace LegatroExp.Data.Entities;

/// <summary>
/// Defines projects that contain tasks and track time budgets
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Unique project identifier
    /// </summary>
    public Guid IDProject { get; set; } = Guid.NewGuid();

    /// <summary>
    /// User who owns the project
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Associated category (nullable)
    /// </summary>
    public Guid? IDCategory { get; set; }

    /// <summary>
    /// Name of the project
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Optional project description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Project due date (UTC, nullable)
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Determines if a project is a pre-defined system project which cannot be edited or deleted
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Allocated time budget (e.g., "40:00:00" for 40 hours)
    /// </summary>
    public TimeSpan? TimeBudget { get; set; }

    /// <summary>
    /// Total time spent (calculated from TimeEntries)
    /// </summary>
    public TimeSpan TimeSpent { get; set; } = TimeSpan.Zero;

    // Navigation properties
    public User User { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
