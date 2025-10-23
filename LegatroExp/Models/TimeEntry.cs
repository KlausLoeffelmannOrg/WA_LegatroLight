namespace LegatroExp.Models;

/// <summary>
///  Represents an individual time tracking entry for a task.
/// </summary>
public class TimeEntry : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the time entry.
    /// </summary>
    public Guid IDTimeEntry { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the user who logged the time.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated project.
    /// </summary>
    public Guid IDProject { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated task.
    /// </summary>
    public Guid IDTodoTask { get; set; }

    /// <summary>
    ///  Gets or sets the description of work completed.
    /// </summary>
    public string? DescriptionDoneWork { get; set; }

    /// <summary>
    ///  Gets or sets the start time (UTC).
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    ///  Gets or sets the end time (UTC).
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    ///  Gets or sets the calculated duration (EndTime - StartTime).
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    ///  Gets or sets the time zone where the entry was recorded.
    /// </summary>
    public string TimeZone { get; set; } = string.Empty;

    // Navigation properties
    public User User { get; set; } = null!;
    public Project Project { get; set; } = null!;
    public TodoTask TodoTask { get; set; } = null!;
}
