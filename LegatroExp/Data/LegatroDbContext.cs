using Microsoft.EntityFrameworkCore;
using LegatroExp.Models;

namespace LegatroExp.Data;

/// <summary>
///  Database context for the Legatro application.
/// </summary>
public class LegatroDbContext : DbContext
{
    /// <summary>
    ///  Gets or sets the Users table.
    /// </summary>
    public DbSet<User> Users { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the Categories table.
    /// </summary>
    public DbSet<Category> Categories { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the Groups table.
    /// </summary>
    public DbSet<Group> Groups { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the Projects table.
    /// </summary>
    public DbSet<Project> Projects { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the Tasks table.
    /// </summary>
    public DbSet<TodoTask> TodoTasks { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the TimeEntries table.
    /// </summary>
    public DbSet<TimeEntry> TimeEntries { get; set; } = null!;

    /// <summary>
    ///  Gets or sets the TasksGroupsRelations table.
    /// </summary>
    public DbSet<TasksGroupsRelation> TasksGroupsRelations { get; set; } = null!;

    public LegatroDbContext(DbContextOptions<LegatroDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IDUser);
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.UserDisplayId).IsRequired();
            entity.Property(e => e.UserAuthID).IsRequired();
            entity.Property(e => e.UserSid).IsRequired();
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.DisplayName).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.UserAuthID).IsUnique();
            entity.HasIndex(e => e.UserSid).IsUnique();
            entity.HasIndex(e => e.UserDisplayId).IsUnique();
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
        });

        // Configure Category entity
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IDCategory);
            entity.Property(e => e.IDCategory).IsRequired();
            entity.Property(e => e.CategoryName).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.BackColor).IsRequired();
            entity.Property(e => e.ForeColor).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.CategoryName);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Group entity
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.IDGroup);
            entity.Property(e => e.IDGroup).IsRequired();
            entity.Property(e => e.GroupDisplayName).IsRequired();
            entity.Property(e => e.CreatedByIDUser).IsRequired();
            entity.Property(e => e.BackColor).IsRequired();
            entity.Property(e => e.ForeColor).IsRequired();
            entity.Property(e => e.IsHidden).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.CreatedByIDUser);
            entity.HasIndex(e => e.GroupDisplayName);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.Groups)
                .HasForeignKey(e => e.CreatedByIDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Project entity
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IDProject);
            entity.Property(e => e.IDProject).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.ProjectName).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.TimeSpent).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

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
        });

        // Configure TodoTask entity
        modelBuilder.Entity<TodoTask>(entity =>
        {
            entity.HasKey(e => e.IDTodoTask);
            entity.Property(e => e.IDTodoTask).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.IDProject).IsRequired();
            entity.Property(e => e.DisplayName).IsRequired();
            entity.Property(e => e.TimeSpent).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.DisplayName);
            entity.HasIndex(e => e.DueDate);
            entity.HasIndex(e => e.DateFinished);
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.User)
                .WithMany(u => u.TodoTasks)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Project)
                .WithMany(p => p.TodoTasks)
                .HasForeignKey(e => e.IDProject)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TimeEntry entity
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.IDTimeEntry);
            entity.Property(e => e.IDTimeEntry).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.IDProject).IsRequired();
            entity.Property(e => e.IDTodoTask).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.Duration).IsRequired();
            entity.Property(e => e.TimeZone).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.IDTodoTask);
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

            entity.HasOne(e => e.TodoTask)
                .WithMany(t => t.TimeEntries)
                .HasForeignKey(e => e.IDTodoTask)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure TasksGroupsRelation entity
        modelBuilder.Entity<TasksGroupsRelation>(entity =>
        {
            entity.HasKey(e => e.IDTasksGroups);
            entity.Property(e => e.IDTasksGroups).IsRequired();
            entity.Property(e => e.IDTodoTask).IsRequired();
            entity.Property(e => e.IDGroup).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();

            entity.HasIndex(e => e.IDTodoTask);
            entity.HasIndex(e => e.IDGroup);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => new { e.IDTodoTask, e.IDGroup }).IsUnique();
            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);

            entity.HasOne(e => e.TodoTask)
                .WithMany(t => t.TasksGroupsRelations)
                .HasForeignKey(e => e.IDTodoTask)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Group)
                .WithMany(g => g.TasksGroupsRelations)
                .HasForeignKey(e => e.IDGroup)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.User)
                .WithMany(u => u.TasksGroupsRelations)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
