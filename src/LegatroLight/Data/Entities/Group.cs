namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines groups for organizing tasks with automatic date range assignment.
/// </summary>
public class Group : BaseEntity
{
    public required string GroupDisplayName { get; set; }
    public string? GroupDescription { get; set; }
    public Guid CreatedByIDUser { get; set; }
    public Guid? IDSymbol { get; set; }
    public int? OrderNo { get; set; }
    public required string BackColor { get; set; }
    public required string ForeColor { get; set; }
    public bool IsHidden { get; set; }
    public bool IsSystem { get; set; }
    public TimeSpan? AutoRangeSpan { get; set; }
    public int? AutoRangeOffset { get; set; }

    // Navigation properties
    public User? CreatedByUser { get; set; }
    public SymbolConfiguration? Symbol { get; set; }
    public ICollection<TodoTasksGroupsRelation> TaskRelations { get; set; } = [];
}
