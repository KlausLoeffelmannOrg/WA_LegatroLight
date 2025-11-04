namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines color categories for visual organization of projects and tasks.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the category (e.g., "Red", "Marketing", "Personal").
    /// </summary>
    public required string CategoryName { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the category.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Gets or sets the optional symbol for the category.
    /// </summary>
    public Guid? IDSymbol { get; set; }

    /// <summary>
    /// Gets or sets the background color in hex format (e.g., "#FF5733").
    /// </summary>
    public required string BackColor { get; set; }

    /// <summary>
    /// Gets or sets the foreground/text color in hex format (e.g., "#FFFFFF").
    /// </summary>
    public required string ForeColor { get; set; }

    /// <summary>
    /// Gets or sets whether the category is system-defined (cannot be edited or deleted).
    /// </summary>
    public bool IsSystem { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public SymbolConfiguration? Symbol { get; set; }
    public ICollection<Project> Projects { get; set; } = [];
}
