namespace LegatroExp.Data.Entities;

/// <summary>
/// Defines color categories for visual organization of projects and tasks
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    /// Unique category identifier
    /// </summary>
    public Guid IDCategory { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Name of the category
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// User who created the category
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    /// Background color (hex format, e.g., #FFFFFF)
    /// </summary>
    public string BackColor { get; set; } = "#FFFFFF";

    /// <summary>
    /// Foreground/text color (hex format)
    /// </summary>
    public string ForeColor { get; set; } = "#000000";

    /// <summary>
    /// Determines if a category is a pre-defined system category which cannot be edited or deleted
    /// </summary>
    public bool IsSystem { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
