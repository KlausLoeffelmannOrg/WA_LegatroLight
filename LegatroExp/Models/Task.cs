namespace LegatroExp.Models;

public class Task : BaseEntity
{
    public required string IDTask
    {
        get => ID;
        set => ID = value;
    }

    public string? IDForeignID { get; set; }
    public string? IDForeignSyncID { get; set; }
    public required string IDUser { get; set; }
    public required string IDProject { get; set; }
    public required string DisplayName { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public TimeSpan? EstimatedEffort { get; set; }
    public DateTime? DateFinished { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public User? User { get; set; }
    public Project? Project { get; set; }
    public ICollection<TasksGroupsRelation> GroupRelations { get; set; } = new List<TasksGroupsRelation>();
    public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
}
