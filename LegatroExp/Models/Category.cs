namespace LegatroExp.Models;

public class Category : BaseEntity
{
    public required string IDCategory
    {
        get => ID;
        set => ID = value;
    }

    public required string CategoryName { get; set; }
    public required string IDUser { get; set; }
    public required string BackColor { get; set; }
    public required string ForeColor { get; set; }
    public bool IsSystem { get; set; }

    public User? User { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
