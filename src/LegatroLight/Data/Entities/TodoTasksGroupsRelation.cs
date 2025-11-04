namespace LegatroLight.Data.Entities;

/// <summary>
/// Many-to-many relationship table for manual assignment of tasks to groups.
/// </summary>
public class TodoTasksGroupsRelation : BaseEntity
{
    public Guid IDTodoTask { get; set; }
    public Guid IDGroup { get; set; }
    public Guid IDUser { get; set; }

    // Navigation properties
    public TodoTask? TodoTask { get; set; }
    public Group? Group { get; set; }
    public User? User { get; set; }
}
