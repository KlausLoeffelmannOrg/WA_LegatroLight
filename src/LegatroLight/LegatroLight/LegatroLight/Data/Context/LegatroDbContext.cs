using LegatroLight.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LegatroLight.Data.Context;

/// <summary>
/// Database context for LegatroLight application using SQLite with EF Core.
/// </summary>
public class LegatroDbContext : DbContext
{
    public LegatroDbContext(DbContextOptions<LegatroDbContext> options)
        : base(options)
    {
    }

    // DbSets for all entities
    public DbSet<User> Users => Set<User>();
    public DbSet<SymbolConfiguration> SymbolConfigurations => Set<SymbolConfiguration>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Project> Projects => Set<Project>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure value converters for SQLite
        var dateTimeConverter = new ValueConverter<DateTime, string>(
            v => v.ToString("o"), // ISO 8601 format
            v => DateTime.Parse(v));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, string?>(
            v => v.HasValue ? v.Value.ToString("o") : null,
            v => v != null ? DateTime.Parse(v) : null);

        var timeSpanConverter = new ValueConverter<TimeSpan, string>(
            v => v.ToString(),
            v => TimeSpan.Parse(v));

        var nullableTimeSpanConverter = new ValueConverter<TimeSpan?, string?>(
            v => v.HasValue ? v.Value.ToString() : null,
            v => v != null ? TimeSpan.Parse(v) : null);

        var guidConverter = new ValueConverter<Guid, string>(
            v => v.ToString(),
            v => Guid.Parse(v));

        var nullableGuidConverter = new ValueConverter<Guid?, string?>(
            v => v.HasValue ? v.Value.ToString() : null,
            v => v != null ? Guid.Parse(v) : null);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("IDUser").HasConversion(guidConverter);
            entity.Property(e => e.DateCreated).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateLastEdited).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateDeleted).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.SyncGuidChanged).HasConversion(guidConverter).IsRequired();
            
            entity.Property(e => e.UserDisplayId).IsRequired().HasMaxLength(255);
            entity.Property(e => e.UserAuthID).IsRequired().HasMaxLength(255);
            entity.Property(e => e.UserSid).IsRequired().HasMaxLength(255);
            entity.Property(e => e.DomainOrMachine).HasMaxLength(255);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.DisplayName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.MiddleName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);

            entity.HasIndex(e => e.UserAuthID).IsUnique().HasDatabaseName("IX_Users_UserAuthID");
            entity.HasIndex(e => e.UserSid).IsUnique().HasDatabaseName("IX_Users_UserSid");
            entity.HasIndex(e => e.UserDisplayId).IsUnique().HasDatabaseName("IX_Users_UserDisplayId");
            entity.HasIndex(e => e.DateDeleted).HasDatabaseName("IX_Users_DateDeleted");
            entity.HasIndex(e => e.DateCreated).HasDatabaseName("IX_Users_DateCreated");

            entity.HasQueryFilter(e => e.DateDeleted == null);
        });

        // Configure SymbolConfiguration entity
        modelBuilder.Entity<SymbolConfiguration>(entity =>
        {
            entity.ToTable("SymbolConfigurations");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("IDSymbol").HasConversion(guidConverter);
            entity.Property(e => e.DateCreated).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateLastEdited).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateDeleted).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.SyncGuidChanged).HasConversion(guidConverter).IsRequired();

            entity.Property(e => e.SymbolChar).IsRequired().HasMaxLength(10);
            entity.Property(e => e.SymbolName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.DefaultBackColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.DefaultForeColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.ContrastRatio).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();

            entity.HasIndex(e => e.SymbolName).IsUnique().HasDatabaseName("IX_SymbolConfiguration_SymbolName");
            entity.HasIndex(e => e.SymbolChar).IsUnique().HasDatabaseName("IX_SymbolConfiguration_SymbolChar");
            entity.HasIndex(e => e.IsSystem).HasDatabaseName("IX_SymbolConfiguration_IsSystem");
            entity.HasIndex(e => e.DateDeleted).HasDatabaseName("IX_SymbolConfiguration_DateDeleted");
            entity.HasIndex(e => e.DateCreated).HasDatabaseName("IX_SymbolConfiguration_DateCreated");

            entity.HasQueryFilter(e => e.DateDeleted == null);
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Categories");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("IDCategory").HasConversion(guidConverter);
            entity.Property(e => e.DateCreated).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateLastEdited).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateDeleted).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.SyncGuidChanged).HasConversion(guidConverter).IsRequired();

            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.IDUser).HasConversion(guidConverter).IsRequired();
            entity.Property(e => e.IDSymbol).HasConversion(nullableGuidConverter);
            entity.Property(e => e.BackColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.ForeColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.IsSystem).IsRequired();

            entity.HasOne(e => e.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Symbol)
                .WithMany(s => s.Categories)
                .HasForeignKey(e => e.IDSymbol)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.IDUser).HasDatabaseName("IX_Categories_IDUser");
            entity.HasIndex(e => e.CategoryName).HasDatabaseName("IX_Categories_CategoryName");
            entity.HasIndex(e => e.IDSymbol).HasDatabaseName("IX_Categories_IDSymbol");
            entity.HasIndex(e => e.DateDeleted).HasDatabaseName("IX_Categories_DateDeleted");
            entity.HasIndex(e => e.DateCreated).HasDatabaseName("IX_Categories_DateCreated");

            entity.HasQueryFilter(e => e.DateDeleted == null);
        });

        // Configure Group entity
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Groups");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("IDGroup").HasConversion(guidConverter);
            entity.Property(e => e.DateCreated).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateLastEdited).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateDeleted).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.SyncGuidChanged).HasConversion(guidConverter).IsRequired();

            entity.Property(e => e.GroupDisplayName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.GroupDescription).HasMaxLength(4000);
            entity.Property(e => e.CreatedByIDUser).HasConversion(guidConverter).IsRequired();
            entity.Property(e => e.IDSymbol).HasConversion(nullableGuidConverter);
            entity.Property(e => e.OrderNo);
            entity.Property(e => e.BackColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.ForeColor).IsRequired().HasMaxLength(10);
            entity.Property(e => e.IsHidden).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.AutoRangeSpan).HasConversion(nullableTimeSpanConverter);
            entity.Property(e => e.AutoRangeOffset);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.Groups)
                .HasForeignKey(e => e.CreatedByIDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Symbol)
                .WithMany(s => s.Groups)
                .HasForeignKey(e => e.IDSymbol)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.CreatedByIDUser).HasDatabaseName("IX_Groups_CreatedByIDUser");
            entity.HasIndex(e => e.GroupDisplayName).HasDatabaseName("IX_Groups_GroupDisplayName");
            entity.HasIndex(e => e.OrderNo).HasDatabaseName("IX_Groups_OrderNo");
            entity.HasIndex(e => e.IDSymbol).HasDatabaseName("IX_Groups_IDSymbol");
            entity.HasIndex(e => e.DateDeleted).HasDatabaseName("IX_Groups_DateDeleted");
            entity.HasIndex(e => e.DateCreated).HasDatabaseName("IX_Groups_DateCreated");

            entity.HasQueryFilter(e => e.DateDeleted == null);
        });

        // Configure Project entity
        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Projects");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("IDProject").HasConversion(guidConverter);
            entity.Property(e => e.DateCreated).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateLastEdited).HasConversion(dateTimeConverter).IsRequired();
            entity.Property(e => e.DateDeleted).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.SyncGuidChanged).HasConversion(guidConverter).IsRequired();

            entity.Property(e => e.IDUser).HasConversion(guidConverter).IsRequired();
            entity.Property(e => e.IDCategory).HasConversion(nullableGuidConverter);
            entity.Property(e => e.ProjectName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(4000);
            entity.Property(e => e.DueDate).HasConversion(nullableDateTimeConverter);
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.TimeBudget).HasConversion(nullableTimeSpanConverter);
            entity.Property(e => e.TimeSpent).HasConversion(timeSpanConverter).IsRequired().HasDefaultValue(TimeSpan.Zero);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(e => e.IDCategory)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasIndex(e => e.IDUser).HasDatabaseName("IX_Projects_IDUser");
            entity.HasIndex(e => e.IDCategory).HasDatabaseName("IX_Projects_IDCategory");
            entity.HasIndex(e => e.ProjectName).HasDatabaseName("IX_Projects_ProjectName");
            entity.HasIndex(e => e.DueDate).HasDatabaseName("IX_Projects_DueDate");
            entity.HasIndex(e => e.DateDeleted).HasDatabaseName("IX_Projects_DateDeleted");
            entity.HasIndex(e => e.DateCreated).HasDatabaseName("IX_Projects_DateCreated");

            entity.HasQueryFilter(e => e.DateDeleted == null);
        });
    }
}
