namespace LegatroExp.Models;

/// <summary>
///  Represents a color category for visual organization of projects and tasks.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    ///  Gets or sets the unique identifier for the category.
    /// </summary>
    public Guid IDCategory { get; set; }

    /// <summary>
    ///  Gets or sets the name of the category.
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the ID of the user who created the category.
    /// </summary>
    public Guid IDUser { get; set; }

    /// <summary>
    ///  Gets or sets the background color in hex format (e.g., #FFFFFF).
    /// </summary>
    public string BackColor { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets the foreground/text color in hex format.
    /// </summary>
    public string ForeColor { get; set; } = string.Empty;

    /// <summary>
    ///  Gets or sets whether this is a system category that cannot be edited or deleted.
    /// </summary>
    public bool IsSystem { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
