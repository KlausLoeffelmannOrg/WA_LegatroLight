namespace LegatroExp.Models;

/// <summary>
///  Base entity class with common fields for all entities.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///  Gets or sets the date and time when the entity was created (UTC).
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    ///  Gets or sets the date and time when the entity was last edited (UTC).
    /// </summary>
    public DateTime DateLastEdited { get; set; }

    /// <summary>
    ///  Gets or sets the date and time when the entity was deleted (UTC).
    ///  Null if not deleted (soft delete).
    /// </summary>
    public DateTime? DateDeleted { get; set; }

    /// <summary>
    ///  Gets or sets the sync GUID that changes whenever the entity is modified.
    ///  Used for synchronization scenarios.
    /// </summary>
    public Guid SyncGuidChanged { get; set; }
}
