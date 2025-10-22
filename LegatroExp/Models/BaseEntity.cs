namespace LegatroExp.Models;

public abstract class BaseEntity
{
    public required string ID { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateLastEdited { get; set; }
    public DateTime? DateDeleted { get; set; }
    public required string SyncGuidChanged { get; set; }

    protected BaseEntity()
    {
        DateCreated = DateTime.UtcNow;
        DateLastEdited = DateTime.UtcNow;
        SyncGuidChanged = Guid.NewGuid().ToString();
    }
}
