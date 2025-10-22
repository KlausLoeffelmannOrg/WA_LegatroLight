namespace LegatroExp.Models;

public class TasksGroupsRelation : BaseEntity
{
    public required string IDTasksGroups
    {
        get => ID;
        set => ID = value;
    }

    public required string IDTask { get; set; }
    public required string IDGroup { get; set; }
    public required string IDUser { get; set; }

    public Task? Task { get; set; }
    public Group? Group { get; set; }
    public User? User { get; set; }
}
