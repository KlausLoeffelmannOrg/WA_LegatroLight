namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines projects that contain tasks and track time budgets.
/// </summary>
public class Project : BaseEntity
{
    /// <summary>
    /// Gets or sets the ID of the user who owns the project.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Gets or sets the optional category for visual organization.
    /// </summary>
    public Guid? IDCategory { get; set; }

    /// <summary>
    /// Gets or sets the name of the project (e.g., "WinForms Designer Refactor").
    /// </summary>
    public required string ProjectName { get; set; }

    /// <summary>
    /// Gets or sets the detailed project description or notes.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the project deadline (UTC). NULL indicates no deadline.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Gets or sets whether the project is system-defined (e.g., "Default").
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Gets or sets the allocated time budget (e.g., "40:00:00" for 40 hours).
    /// </summary>
    public TimeSpan? TimeBudget { get; set; }

    /// <summary>
    /// Gets or sets the total time logged on project (calculated from TimeEntries).
    /// </summary>
    public TimeSpan TimeSpent { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Category? Category { get; set; }
}
