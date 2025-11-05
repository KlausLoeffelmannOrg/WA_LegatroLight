using LegatroLight.Data.Context;

namespace LegatroLight.Services;

/// <summary>
///  Provides database seeding functionality for system data.
/// </summary>
public interface IDatabaseSeedService
{
    /// <summary>
    ///  Seeds the database with system data including default project, groups, categories, and symbols.
    /// </summary>
    /// <param name="context">
    ///  The database context to seed.
    /// </param>
    /// <param name="userId">
    ///  The user ID to associate with the system data.
    /// </param>
    /// <returns>
    ///  A task representing the asynchronous operation.
    /// </returns>
    Task SeedSystemDataAsync(LegatroDbContext context, Guid userId);
}
