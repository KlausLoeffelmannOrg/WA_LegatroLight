namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines available symbols for visual representation of Groups and Categories.
/// Symbols are single-character icons from the Segoe Fluent Icons font.
/// </summary>
public class SymbolConfiguration : BaseEntity
{
    /// <summary>
    /// Gets or sets the Unicode character from Segoe Fluent Icons font.
    /// </summary>
    public required string SymbolChar { get; set; }

    /// <summary>
    /// Gets or sets the human-readable name (e.g., "Calendar", "Star", "Folder").
    /// </summary>
    public required string SymbolName { get; set; }

    /// <summary>
    /// Gets or sets the default background color in hex format (e.g., "#FFFFFF").
    /// </summary>
    public required string DefaultBackColor { get; set; }

    /// <summary>
    /// Gets or sets the default foreground/text color in hex format (e.g., "#000000").
    /// </summary>
    public required string DefaultForeColor { get; set; }

    /// <summary>
    /// Gets or sets the calculated WCAG contrast ratio between foreground and background.
    /// </summary>
    public double ContrastRatio { get; set; }

    /// <summary>
    /// Gets or sets whether the symbol is system-defined (cannot be edited or deleted).
    /// </summary>
    public bool IsSystem { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Group> Groups { get; set; } = [];
}
