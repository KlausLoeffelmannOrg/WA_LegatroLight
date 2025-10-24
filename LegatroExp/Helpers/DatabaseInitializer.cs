using LegatroExp.Data;
using LegatroExp.Models;

namespace LegatroExp.Helpers;

/// <summary>
///  Initializes the database with system data on first run.
/// </summary>
public static class DatabaseInitializer
{
    /// <summary>
    ///  Seeds the database with system data (groups, categories, default project).
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="currentUser">The current user.</param>
    public static void SeedSystemData(LegatroDbContext context, User currentUser)
    {
        DateTime now = DateTime.UtcNow;
        Guid syncGuid = Guid.NewGuid();

        // Create system groups
        List<Group> systemGroups = new List<Group>
        {
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "My Day",
                GroupDescription = "Tasks created today",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "📅",
                OrderNo = 1,
                BackColor = "#E3F2FD",
                ForeColor = "#0D47A1",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(1),
                AutoRangeOffset = 0,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "Sliding Week",
                GroupDescription = "Tasks from the last 7 days",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "📊",
                OrderNo = 2,
                BackColor = "#F3E5F5",
                ForeColor = "#4A148C",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(7),
                AutoRangeOffset = 0,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "My Month",
                GroupDescription = "Tasks from the last 30 days",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "📆",
                OrderNo = 3,
                BackColor = "#E8F5E9",
                ForeColor = "#1B5E20",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(30),
                AutoRangeOffset = 0,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "Roaming Links",
                GroupDescription = "Manual group for reference links",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "🔗",
                OrderNo = 4,
                BackColor = "#FFF3E0",
                ForeColor = "#E65100",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = null,
                AutoRangeOffset = null,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "Roaming Notes",
                GroupDescription = "Manual group for notes",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "📝",
                OrderNo = 5,
                BackColor = "#FFF8E1",
                ForeColor = "#F57F17",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = null,
                AutoRangeOffset = null,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "Done",
                GroupDescription = "Completed tasks",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "✓",
                OrderNo = 65534,
                BackColor = "#C8E6C9",
                ForeColor = "#2E7D32",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = null,
                AutoRangeOffset = null,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            },
            new Group
            {
                IDGroup = Guid.NewGuid(),
                GroupDisplayName = "Clutter",
                GroupDescription = "Miscellaneous items",
                CreatedByIDUser = currentUser.IDUser,
                GroupSymbol = "🗂️",
                OrderNo = 65535,
                BackColor = "#EEEEEE",
                ForeColor = "#424242",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = null,
                AutoRangeOffset = null,
                DateCreated = now,
                DateLastEdited = now,
                SyncGuidChanged = Guid.NewGuid()
            }
        };

        context.Groups.AddRange(systemGroups);

        // Create system categories
        List<(string Name, string BackColor, string ForeColor)> categoryColors = new List<(string, string, string)>
        {
            ("Red", "#FFCDD2", "#B71C1C"),
            ("Orange", "#FFE0B2", "#E65100"),
            ("Yellow", "#FFF9C4", "#F57F17"),
            ("Green", "#C8E6C9", "#2E7D32"),
            ("Blue", "#BBDEFB", "#0D47A1"),
            ("Indigo", "#C5CAE9", "#1A237E"),
            ("Violet", "#E1BEE7", "#4A148C"),
            ("Pink", "#F8BBD0", "#880E4F"),
            ("Brown", "#D7CCC8", "#3E2723"),
            ("Gray", "#E0E0E0", "#424242")
        };

        List<Category> systemCategories = categoryColors.Select(c => new Category
        {
            IDCategory = Guid.NewGuid(),
            CategoryName = c.Name,
            IDUser = currentUser.IDUser,
            BackColor = c.BackColor,
            ForeColor = c.ForeColor,
            IsSystem = true,
            DateCreated = now,
            DateLastEdited = now,
            SyncGuidChanged = Guid.NewGuid()
        }).ToList();

        context.Categories.AddRange(systemCategories);

        // Create default project
        Project defaultProject = new Project
        {
            IDProject = Guid.NewGuid(),
            IDUser = currentUser.IDUser,
            ProjectName = "Default",
            Description = "Default project for general tasks",
            IsSystem = true,
            TimeSpent = TimeSpan.Zero,
            DateCreated = now,
            DateLastEdited = now,
            SyncGuidChanged = Guid.NewGuid()
        };

        context.Projects.Add(defaultProject);

        context.SaveChanges();
    }
}
