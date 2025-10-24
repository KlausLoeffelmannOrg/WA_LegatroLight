using Microsoft.EntityFrameworkCore;
using LegatroExp.Data.Entities;

namespace LegatroExp.Data.Context;

/// <summary>
/// Database context for LegatroExp application
/// </summary>
public class LegatroDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Entities.Task> Tasks { get; set; } = null!;
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;
    public DbSet<TasksGroupsRelation> TasksGroupsRelations { get; set; } = null!;

    public LegatroDbContext(DbContextOptions<LegatroDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Users
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IDUser);
            entity.HasIndex(e => e.UserAuthID).IsUnique();
            entity.HasIndex(e => e.UserSid).IsUnique();
            entity.HasIndex(e => e.UserDisplayId).IsUnique();
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
        });

        // Configure Categories
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IDCategory);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.CategoryName);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            
            entity.HasOne(e => e.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Groups
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.IDGroup);
            entity.HasIndex(e => e.CreatedByIDUser);
            entity.HasIndex(e => e.GroupDisplayName);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.Groups)
                .HasForeignKey(e => e.CreatedByIDUser)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TimeSpan as string
            entity.Property(e => e.AutoRangeSpan)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToString() : null,
                    v => string.IsNullOrEmpty(v) ? null : TimeSpan.Parse(v));
        });

        // Configure Projects
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IDProject);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDCategory);
            entity.HasIndex(e => e.ProjectName);
            entity.HasIndex(e => e.DueDate);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(e => e.IDCategory)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure TimeSpan as string
            entity.Property(e => e.TimeBudget)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToString() : null,
                    v => string.IsNullOrEmpty(v) ? null : TimeSpan.Parse(v));

            entity.Property(e => e.TimeSpent)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v))
                .HasDefaultValue(TimeSpan.Zero);
        });

        // Configure Tasks
        modelBuilder.Entity<Entities.Task>(entity =>
        {
            entity.HasKey(e => e.IDTask);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.DisplayName);
            entity.HasIndex(e => e.DueDate);
            entity.HasIndex(e => e.DateFinished);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.IDProject)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TimeSpan as string
            entity.Property(e => e.EstimatedEffort)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToString() : null,
                    v => string.IsNullOrEmpty(v) ? null : TimeSpan.Parse(v));

            entity.Property(e => e.TimeSpent)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v))
                .HasDefaultValue(TimeSpan.Zero);
        });

        // Configure TimeEntries
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.IDTimeEntry);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.IDTask);
            entity.HasIndex(e => e.StartTime);
            entity.HasIndex(e => e.EndTime);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.User)
                .WithMany(u => u.TimeEntries)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Project)
                .WithMany(p => p.TimeEntries)
                .HasForeignKey(e => e.IDProject)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.TimeEntries)
                .HasForeignKey(e => e.IDTask)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure TimeSpan as string
            entity.Property(e => e.Duration)
                .HasConversion(
                    v => v.ToString(),
                    v => TimeSpan.Parse(v));
        });

        // Configure TasksGroupsRelations
        modelBuilder.Entity<TasksGroupsRelation>(entity =>
        {
            entity.HasKey(e => e.IDTasksGroups);
            entity.HasIndex(e => e.IDTask);
            entity.HasIndex(e => e.IDGroup);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => new { e.IDTask, e.IDGroup }).IsUnique();
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.Task)
                .WithMany(t => t.TasksGroupsRelations)
                .HasForeignKey(e => e.IDTask)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Group)
                .WithMany(g => g.TasksGroupsRelations)
                .HasForeignKey(e => e.IDGroup)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.TasksGroupsRelations)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
