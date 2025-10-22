using LegatroExp.Data;
using LegatroExp.Models;

namespace LegatroExp.Helpers;

public static class DatabaseInitializer
{
    public static void InitializeDatabase(LegatroDbContext context, User currentUser)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        context.Users.Add(currentUser);

        InitializeSystemCategories(context, currentUser);
        InitializeSystemGroups(context, currentUser);
        InitializeDefaultProject(context, currentUser);

        context.SaveChanges();
    }

    private static void InitializeSystemCategories(LegatroDbContext context, User user)
    {
        string[] categoryColors = new[]
        {
            "#FF0000:Red",
            "#FFA500:Orange",
            "#FFFF00:Yellow",
            "#00FF00:Green",
            "#0000FF:Blue",
            "#4B0082:Indigo",
            "#EE82EE:Violet",
            "#FFC0CB:Pink",
            "#A52A2A:Brown",
            "#808080:Gray"
        };

        foreach (string colorDef in categoryColors)
        {
            string[] parts = colorDef.Split(':');
            string backColor = parts[0];
            string name = parts[1];

            string foreColor = GetContrastColor(backColor);

            Category category = new()
            {
                IDCategory = Guid.NewGuid().ToString(),
                ID = Guid.NewGuid().ToString(),
                SyncGuidChanged = Guid.NewGuid().ToString(),
                CategoryName = name,
                IDUser = user.IDUser,
                BackColor = backColor,
                ForeColor = foreColor,
                IsSystem = true
            };

            context.Categories.Add(category);
        }
    }

    private static void InitializeSystemGroups(LegatroDbContext context, User user)
    {
        List<(string name, int orderNo, TimeSpan? autoRangeSpan, int? autoRangeOffset)> groups = new()
        {
            ("My Day", 1, TimeSpan.FromDays(1), 0),
            ("Sliding Week", 2, TimeSpan.FromDays(7), 0),
            ("My Month", 3, TimeSpan.FromDays(30), 0),
            ("Roaming Links", 4, null, null),
            ("Roaming Notes", 5, null, null),
            ("Done", 65534, null, null),
            ("Clutter", 65535, null, null)
        };

        foreach ((string name, int orderNo, TimeSpan? autoRangeSpan, int? autoRangeOffset) in groups)
        {
            Group group = new()
            {
                IDGroup = Guid.NewGuid().ToString(),
                ID = Guid.NewGuid().ToString(),
                SyncGuidChanged = Guid.NewGuid().ToString(),
                GroupDisplayName = name,
                CreatedByIDUser = user.IDUser,
                OrderNo = orderNo,
                BackColor = "#F0F0F0",
                ForeColor = "#000000",
                IsHidden = false,
                IsSystem = true,
                AutoRangeSpan = autoRangeSpan,
                AutoRangeOffset = autoRangeOffset
            };

            context.Groups.Add(group);
        }
    }

    private static void InitializeDefaultProject(LegatroDbContext context, User user)
    {
        Project defaultProject = new()
        {
            IDProject = Guid.NewGuid().ToString(),
            ID = Guid.NewGuid().ToString(),
            SyncGuidChanged = Guid.NewGuid().ToString(),
            IDUser = user.IDUser,
            ProjectName = "Default",
            IsSystem = true,
            TimeSpent = TimeSpan.Zero
        };

        context.Projects.Add(defaultProject);
    }

    private static string GetContrastColor(string hexColor)
    {
        hexColor = hexColor.TrimStart('#');

        int r = Convert.ToInt32(hexColor.Substring(0, 2), 16);
        int g = Convert.ToInt32(hexColor.Substring(2, 2), 16);
        int b = Convert.ToInt32(hexColor.Substring(4, 2), 16);

        double luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;

        return luminance > 0.5 ? "#000000" : "#FFFFFF";
    }
}
