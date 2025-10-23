namespace LegatroExp.Models;

/// <summary>
///  Represents a many-to-many relationship between tasks and groups for manual assignment.
/// </summary>
public class TasksGroupsRelation : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for this relation.
    /// </summary>
    public Guid IDTasksGroups { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated task.
    /// </summary>
    public Guid IDTodoTask { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the associated group.
    /// </summary>
    public Guid IDGroup { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the user who created the relation.
    /// </summary>
    public Guid IDUser { get; set; }

    // Navigation properties
    public TodoTask TodoTask { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public User User { get; set; } = null!;
}
