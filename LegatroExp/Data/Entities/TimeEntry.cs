namespace LegatroExp.Data.Entities;

/// <summary>
/// Records individual time tracking entries for tasks
/// </summary>
public class TimeEntry : BaseEntity
{
    /// <summary>
    /// Unique time entry identifier
    /// </summary>
    public Guid IDTimeEntry { get; set; } = Guid.NewGuid();

    /// <summary>
    /// User who logged the time
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Associated project
    /// </summary>
    public Guid IDProject { get; set; }

    /// <summary>
    /// Associated task
    /// </summary>
    public Guid IDTask { get; set; }

    /// <summary>
    /// Description of work completed
    /// </summary>
    public string? DescriptionDoneWork { get; set; }

    /// <summary>
    /// UTC start time
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// UTC end time
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// Calculated duration (EndTime - StartTime)
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Time zone where entry was recorded (e.g., "Pacific Standard Time")
    /// </summary>
    public string TimeZone { get; set; } = string.Empty;

    // Navigation properties
    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
    public Task Task { get; set; } = null!;
}
