namespace LegatroExp.Data.Entities;

/// <summary>
/// Defines groups for organizing tasks
/// </summary>
public class Group : BaseEntity
{
    /// <summary>
    /// Unique group identifier
    /// </summary>
    public Guid IDGroup { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Display name of the group
    /// </summary>
    public string GroupDisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Optional description
    /// </summary>
    public string? GroupDescription { get; set; }

    /// <summary>
    /// User who created the group
    /// </summary>
    public Guid CreatedByIDUser { get; set; }

    /// <summary>
    /// Single character from "Segoe Fluent Icons" font
    /// </summary>
    public string? GroupSymbol { get; set; }

    /// <summary>
    /// Defines the order in which groups are shown in the TreeView
    /// </summary>
    public int? OrderNo { get; set; }

    /// <summary>
    /// Background color (hex format)
    /// </summary>
    public string BackColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// Foreground/text color (hex format)
    /// </summary>
    public string ForeColor { get; set; } = "#000000";

    /// <summary>
    /// Determines if a group is shown in the TreeView
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Determines if a group is a pre-defined system group which cannot be edited or deleted
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Time span for automatic task assignment (e.g., "7.00:00:00" for 7 days)
    /// </summary>
    public TimeSpan? AutoRangeSpan { get; set; }

    /// <summary>
    /// Day offset to shift the AutoRangeSpan into the future
    /// </summary>
    public int? AutoRangeOffset { get; set; }

    // Navigation properties
    public User CreatedByUser { get; set; } = null!;
    public ICollection<TasksGroupsRelation> TasksGroupsRelations { get; set; } = new List<TasksGroupsRelation>();
}
