namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines available symbols for visual representation. Single-character icons from Segoe Fluent Icons font.
/// </summary>
public class SymbolConfiguration : BaseEntity
{
    public required string SymbolChar { get; set; }
    public required string SymbolName { get; set; }
    public required string DefaultBackColor { get; set; }
    public required string DefaultForeColor { get; set; }
    public double ContrastRatio { get; set; }
    public bool IsSystem { get; set; }

    // Navigation properties
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Group> Groups { get; set; } = [];
}
