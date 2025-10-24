namespace LegatroExp.Data.Entities;

/// <summary>
/// Base class for all entities with common fields
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// UTC timestamp when record was created
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// UTC timestamp when record was last edited
    /// </summary>
    public DateTime DateLastEdited { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// UTC timestamp when record was soft-deleted (null if not deleted)
    /// </summary>
    public DateTime? DateDeleted { get; set; }

    /// <summary>
    /// Unique identifier for sync scenarios; updated when record changes
    /// </summary>
    public Guid SyncGuidChanged { get; set; } = Guid.NewGuid();
}
