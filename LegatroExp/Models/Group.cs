namespace LegatroExp.Models;

public class Group : BaseEntity
{
    public required string IDGroup
    {
        get => ID;
        set => ID = value;
    }

    public required string GroupDisplayName { get; set; }
    public string? GroupDescription { get; set; }
    public required string CreatedByIDUser { get; set; }
    public string? GroupSymbol { get; set; }
    public int? OrderNo { get; set; }
    public required string BackColor { get; set; }
    public required string ForeColor { get; set; }
    public bool IsHidden { get; set; }
    public bool IsSystem { get; set; }
    public TimeSpan? AutoRangeSpan { get; set; }
    public int? AutoRangeOffset { get; set; }

    public User? CreatedByUser { get; set; }
    public ICollection<TasksGroupsRelation> TaskRelations { get; set; } = new List<TasksGroupsRelation>();
}
