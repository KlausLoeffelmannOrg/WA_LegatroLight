namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines groups for organizing tasks with automatic date range assignment and manual task linking.
/// </summary>
public class Group : BaseEntity
{
    /// <summary>
    /// Gets or sets the display name shown in TreeView and dialogs.
    /// </summary>
    public required string GroupDisplayName { get; set; }

    /// <summary>
    /// Gets or sets the optional longer description of the group's purpose.
    /// </summary>
    public string? GroupDescription { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the group.
    /// </summary>
    public Guid CreatedByIDUser { get; set; }

    /// <summary>
    /// Gets or sets the optional symbol displayed next to group name.
    /// </summary>
    public Guid? IDSymbol { get; set; }

    /// <summary>
    /// Gets or sets the display order in TreeView (lower numbers appear first).
    /// </summary>
    public int? OrderNo { get; set; }

    /// <summary>
    /// Gets or sets the background color in hex format.
    /// </summary>
    public required string BackColor { get; set; }

    /// <summary>
    /// Gets or sets the foreground/text color in hex format.
    /// </summary>
    public required string ForeColor { get; set; }

    /// <summary>
    /// Gets or sets whether the group is hidden from TreeView.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Gets or sets whether the group is system-defined (cannot be edited or deleted).
    /// </summary>
    public bool IsSystem { get; set; }

    /// <summary>
    /// Gets or sets the duration for automatic task assignment (e.g., "7.00:00:00" for 7 days).
    /// </summary>
    public TimeSpan? AutoRangeSpan { get; set; }

    /// <summary>
    /// Gets or sets the number of days to offset the AutoRangeSpan (positive = future, negative = past).
    /// </summary>
    public int? AutoRangeOffset { get; set; }

    // Navigation properties
    public User? CreatedByUser { get; set; }
    public SymbolConfiguration? Symbol { get; set; }
}
