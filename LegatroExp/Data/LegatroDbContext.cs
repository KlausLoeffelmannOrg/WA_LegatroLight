using Microsoft.EntityFrameworkCore;
using LegatroExp.Models;

namespace LegatroExp.Data;

public class LegatroDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<TasksGroupsRelation> TasksGroupsRelations { get; set; }
    public DbSet<TimeEntry> TimeEntries { get; set; }

    public LegatroDbContext(DbContextOptions<LegatroDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Users configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IDUser);
            entity.Property(e => e.IDUser).HasColumnName("IDUser");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.UserDisplayId).IsRequired();
            entity.Property(e => e.UserAuthID).IsRequired();
            entity.Property(e => e.UserSid).IsRequired();
            entity.Property(e => e.UserName).IsRequired();
            entity.Property(e => e.DisplayName).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.UserAuthID).IsUnique();
            entity.HasIndex(e => e.UserSid).IsUnique();
            entity.HasIndex(e => e.UserDisplayId).IsUnique();
        });

        // Categories configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IDCategory);
            entity.Property(e => e.IDCategory).HasColumnName("IDCategory");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.CategoryName).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.BackColor).IsRequired();
            entity.Property(e => e.ForeColor).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.CategoryName);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Groups configuration
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.IDGroup);
            entity.Property(e => e.IDGroup).HasColumnName("IDGroup");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.GroupDisplayName).IsRequired();
            entity.Property(e => e.CreatedByIDUser).IsRequired();
            entity.Property(e => e.BackColor).IsRequired();
            entity.Property(e => e.ForeColor).IsRequired();
            entity.Property(e => e.IsHidden).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.CreatedByIDUser);
            entity.HasIndex(e => e.GroupDisplayName);

            entity.HasOne(e => e.CreatedByUser)
                .WithMany(u => u.Groups)
                .HasForeignKey(e => e.CreatedByIDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Projects configuration
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.IDProject);
            entity.Property(e => e.IDProject).HasColumnName("IDProject");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.ProjectName).IsRequired();
            entity.Property(e => e.IsSystem).IsRequired();
            entity.Property(e => e.TimeSpent).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDCategory);
            entity.HasIndex(e => e.ProjectName);
            entity.HasIndex(e => e.DueDate);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Projects)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Projects)
                .HasForeignKey(e => e.IDCategory)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Tasks configuration
        modelBuilder.Entity<Models.Task>(entity =>
        {
            entity.HasKey(e => e.IDTask);
            entity.Property(e => e.IDTask).HasColumnName("IDTask");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.IDProject).IsRequired();
            entity.Property(e => e.DisplayName).IsRequired();
            entity.Property(e => e.TimeSpent).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.DisplayName);
            entity.HasIndex(e => e.DueDate);
            entity.HasIndex(e => e.DateFinished);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.IDProject)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // TasksGroupsRelations configuration
        modelBuilder.Entity<TasksGroupsRelation>(entity =>
        {
            entity.HasKey(e => e.IDTasksGroups);
            entity.Property(e => e.IDTasksGroups).HasColumnName("IDTasksGroups");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.IDTask).IsRequired();
            entity.Property(e => e.IDGroup).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.IDTask);
            entity.HasIndex(e => e.IDGroup);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => new { e.IDTask, e.IDGroup }).IsUnique();

            entity.HasOne(e => e.Task)
                .WithMany(t => t.GroupRelations)
                .HasForeignKey(e => e.IDTask)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Group)
                .WithMany(g => g.TaskRelations)
                .HasForeignKey(e => e.IDGroup)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.IDUser)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // TimeEntries configuration
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasKey(e => e.IDTimeEntry);
            entity.Property(e => e.IDTimeEntry).HasColumnName("IDTimeEntry");
            entity.Property(e => e.DateCreated).IsRequired();
            entity.Property(e => e.DateLastEdited).IsRequired();
            entity.Property(e => e.SyncGuidChanged).IsRequired();
            entity.Property(e => e.IDUser).IsRequired();
            entity.Property(e => e.IDProject).IsRequired();
            entity.Property(e => e.IDTask).IsRequired();
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.EndTime).IsRequired();
            entity.Property(e => e.Duration).IsRequired();
            entity.Property(e => e.TimeZone).IsRequired();

            entity.HasIndex(e => e.DateCreated);
            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(e => e.IDUser);
            entity.HasIndex(e => e.IDProject);
            entity.HasIndex(e => e.IDTask);
            entity.HasIndex(e => e.StartTime);
            entity.HasIndex(e => e.EndTime);

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
        });
    }
}
