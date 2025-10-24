namespace LegatroExp.Data.Entities;

/// <summary>
/// Many-to-many relationship table for manual task-to-group assignments
/// </summary>
public class TasksGroupsRelation : BaseEntity
{
    /// <summary>
    /// Unique relation identifier
    /// </summary>
    public Guid IDTasksGroups { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Associated task
    /// </summary>
    public Guid IDTask { get; set; }

    /// <summary>
    /// Associated group
    /// </summary>
    public Guid IDGroup { get; set; }

    /// <summary>
    /// User who created the relation
    /// </summary>
    public Guid IDUser { get; set; }

    // Navigation properties
    public Task Task { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public User User { get; set; } = null!;
}
