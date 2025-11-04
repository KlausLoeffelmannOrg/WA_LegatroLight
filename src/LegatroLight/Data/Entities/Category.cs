namespace LegatroLight.Data.Entities;

/// <summary>
/// Defines color categories for visual organization of projects and tasks.
/// </summary>
public class Category : BaseEntity
{
    public required string CategoryName { get; set; }
    public Guid IDUser { get; set; }
    public Guid? IDSymbol { get; set; }
    public required string BackColor { get; set; }
    public required string ForeColor { get; set; }
    public bool IsSystem { get; set; }

    // Navigation properties
    public User? User { get; set; }
    public SymbolConfiguration? Symbol { get; set; }
    public ICollection<Project> Projects { get; set; } = [];
}
