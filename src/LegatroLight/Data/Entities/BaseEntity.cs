namespace LegatroLight.Data.Entities;

/// <summary>
/// Base class for all entities with common fields for sync scenarios and soft deletion.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier. Stored as TEXT (GUID) in database.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the UTC timestamp when the record was created.
    /// </summary>
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC timestamp when the record was last modified.
    /// </summary>
    public DateTime DateLastEdited { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the UTC timestamp when the record was soft-deleted. NULL indicates active record.
    /// </summary>
    public DateTime? DateDeleted { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier updated on every change for sync conflict resolution.
    /// </summary>
    public Guid SyncGuidChanged { get; set; } = Guid.NewGuid();
}
