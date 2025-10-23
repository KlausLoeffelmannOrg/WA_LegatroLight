namespace LegatroExp.Models;

/// <summary>
///  Represents a group for organizing tasks.
/// </summary>
public class Group : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the group.
    /// </summary>
    public Guid IDGroup { get; set; }

    /// <summary>
    ///  Gets or sets the display name of the group.
    /// </summary>
    public string GroupDisplayName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the optional description.
    /// </summary>
    public string? GroupDescription { get; set; }

    /// <summary>
    ///  Gets or sets the ID of the user who created the group.
    /// </summary>
    public Guid CreatedByIDUser { get; set; }

    /// <summary>
    ///  Gets or sets a single character from "Segoe Fluent Icons" font.
    /// </summary>
    public string? GroupSymbol { get; set; }

    /// <summary>
    ///  Gets or sets the order number for displaying groups in the TreeView.
    /// </summary>
    public int? OrderNo { get; set; }

    /// <summary>
    ///  Gets or sets the background color in hex format.
    /// </summary>
    public string BackColor { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the foreground/text color in hex format.
    /// </summary>
    public string ForeColor { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets whether the group is hidden in the TreeView.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    ///  Gets or sets whether this is a system group that cannot be edited or deleted.
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    ///  Gets or sets the time span for automatic task assignment.
    /// </summary>
    public TimeSpan? AutoRangeSpan { get; set; }

    /// <summary>
    ///  Gets or sets the day offset to shift the AutoRangeSpan into the future.
    /// </summary>
    public int? AutoRangeOffset { get; set; }

    // Navigation properties
    public User CreatedByUser { get; set; } = null!;
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
}
