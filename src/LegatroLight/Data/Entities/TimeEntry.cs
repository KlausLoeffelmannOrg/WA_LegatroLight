namespace LegatroLight.Data.Entities;

/// <summary>
/// Records individual time tracking entries for tasks.
/// </summary>
public class TimeEntry : BaseEntity
{
    public Guid IDUser { get; set; }
    public Guid IDProject { get; set; }
    public Guid IDTodoTask { get; set; }
    public string? DescriptionDoneWork { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public required string TimeZone { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public Project? Project { get; set; }
    public TodoTask? TodoTask { get; set; }
}
