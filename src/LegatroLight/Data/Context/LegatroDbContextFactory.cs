using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LegatroLight.Data.Context;

/// <summary>
/// Design-time factory for creating LegatroDbContext during EF Core migrations.
/// This is needed because the application uses WinForms-specific startup code.
/// </summary>
public class LegatroDbContextFactory : IDesignTimeDbContextFactory<LegatroDbContext>
{
    public LegatroDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LegatroDbContext>();
        
        // Use a temporary connection string for migrations
        // The actual connection string will be loaded from appsettings.json at runtime
        optionsBuilder.UseSqlite("Data Source=LegatroLight.legatro");

        return new LegatroDbContext(optionsBuilder.Options);
    }
}
