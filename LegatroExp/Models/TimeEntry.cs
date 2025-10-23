namespace LegatroExp.Models;

public class TimeEntry : BaseEntity
{
    public required string IDTimeEntry
    {
        get => ID;
        set => ID = value;
    }

    public required string IDUser { get; set; }
    public required string IDProject { get; set; }
    public required string IDTask { get; set; }
    public string? DescriptionDoneWork { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public required string TimeZone { get; set; }

    public User? User { get; set; }
    public Project? Project { get; set; }
    public Task? Task { get; set; }
}
