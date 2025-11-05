using LegatroLight.Data.Context;
using LegatroLight.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LegatroLight.Services;

/// <summary>
///  Provides database seeding functionality for system data.
/// </summary>
internal class DatabaseSeedService : IDatabaseSeedService
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
    public async Task SeedSystemDataAsync(LegatroDbContext context, Guid userId)
    {
        ArgumentNullException.ThrowIfNull(context);

        await SeedSymbolsAsync(context);
        await SeedCategoriesAsync(context, userId);
        await SeedGroupsAsync(context, userId);
        await SeedDefaultProjectAsync(context, userId);

        await context.SaveChangesAsync();
    }

    /// <summary>
    ///  Seeds system symbols from Segoe Fluent Icons.
    /// </summary>
    private async Task SeedSymbolsAsync(LegatroDbContext context)
    {
        SymbolDefinition[] symbols =
        [
            new("Calendar", "\uE787", "#2563EB", "#FFFFFF"),
            new("Star", "\uE734", "#EAB308", "#000000"),
            new("Folder", "\uE8B7", "#F59E0B", "#000000"),
            new("Checkmark", "\uE73E", "#10B981", "#FFFFFF"),
            new("Flag", "\uE7C1", "#EF4444", "#FFFFFF"),
            new("Heart", "\uEB51", "#EC4899", "#FFFFFF"),
            new("Lightning", "\uE945", "#8B5CF6", "#FFFFFF"),
            new("Clock", "\uE917", "#06B6D4", "#FFFFFF"),
            new("Pin", "\uE718", "#6366F1", "#FFFFFF"),
            new("Tag", "\uE8EC", "#14B8A6", "#FFFFFF")
        ];

        foreach (SymbolDefinition symbol in symbols)
        {
            bool exists = await context.SymbolConfigurations
                .AnyAsync(s => s.SymbolName == symbol.Name);

            if (!exists)
            {
                double contrastRatio = CalculateContrastRatio(
                    ColorTranslator.FromHtml(symbol.BackColor),
                    ColorTranslator.FromHtml(symbol.ForeColor));

                context.SymbolConfigurations.Add(new SymbolConfiguration
                {
                    Id = Guid.NewGuid(),
                    SymbolChar = symbol.Char,
                    SymbolName = symbol.Name,
                    DefaultBackColor = symbol.BackColor,
                    DefaultForeColor = symbol.ForeColor,
                    ContrastRatio = contrastRatio,
                    IsSystem = true,
                    DateCreated = DateTime.UtcNow,
                    DateLastEdited = DateTime.UtcNow,
                    SyncGuidChanged = Guid.NewGuid()
                });
            }
        }
    }

    /// <summary>
    ///  Seeds system color categories with WCAG-compliant contrast.
    /// </summary>
    private async Task SeedCategoriesAsync(LegatroDbContext context, Guid userId)
    {
        CategoryDefinition[] categories =
        [
            new("Red", "#DC2626"),
            new("Orange", "#EA580C"),
            new("Yellow", "#CA8A04"),
            new("Green", "#16A34A"),
            new("Blue", "#2563EB"),
            new("Purple", "#9333EA"),
            new("Pink", "#DB2777"),
            new("Brown", "#92400E"),
            new("Gray", "#4B5563"),
            new("Black", "#1F2937")
        ];

        foreach (CategoryDefinition category in categories)
        {
            bool exists = await context.Categories
                .AnyAsync(c => c.CategoryName == category.Name && c.IsSystem);

            if (!exists)
            {
                Color backColor = ColorTranslator.FromHtml(category.BackColor);
                Color foreColor = GetCompliantForeColor(backColor);

                context.Categories.Add(new Category
                {
                    Id = Guid.NewGuid(),
                    CategoryName = category.Name,
                    IDUser = userId,
                    IDSymbol = null,
                    BackColor = category.BackColor,
                    ForeColor = ColorTranslator.ToHtml(foreColor),
                    IsSystem = true,
                    DateCreated = DateTime.UtcNow,
                    DateLastEdited = DateTime.UtcNow,
                    SyncGuidChanged = Guid.NewGuid()
                });
            }
        }
    }

    /// <summary>
    ///  Seeds system groups with automatic date range assignment.
    /// </summary>
    private async Task SeedGroupsAsync(LegatroDbContext context, Guid userId)
    {
        GroupDefinition[] groups =
        [
            new("My Day", 1, TimeSpan.FromDays(1), 0, "#3B82F6", "#FFFFFF"),
            new("Sliding Week", 2, TimeSpan.FromDays(7), 0, "#10B981", "#FFFFFF"),
            new("My Month", 3, TimeSpan.FromDays(30), 0, "#F59E0B", "#000000"),
            new("Roaming Links", 4, null, null, "#8B5CF6", "#FFFFFF"),
            new("Roaming Notes", 5, null, null, "#EC4899", "#FFFFFF"),
            new("Done", 65534, null, null, "#6B7280", "#FFFFFF"),
            new("Clutter", 65535, null, null, "#9CA3AF", "#000000")
        ];

        foreach (GroupDefinition group in groups)
        {
            bool exists = await context.Groups
                .AnyAsync(g => g.GroupDisplayName == group.Name && g.IsSystem);

            if (!exists)
            {
                context.Groups.Add(new Group
                {
                    Id = Guid.NewGuid(),
                    GroupDisplayName = group.Name,
                    GroupDescription = null,
                    CreatedByIDUser = userId,
                    IDSymbol = null,
                    OrderNo = group.OrderNo,
                    BackColor = group.BackColor,
                    ForeColor = group.ForeColor,
                    IsHidden = false,
                    IsSystem = true,
                    AutoRangeSpan = group.AutoRangeSpan,
                    AutoRangeOffset = group.AutoRangeOffset,
                    DateCreated = DateTime.UtcNow,
                    DateLastEdited = DateTime.UtcNow,
                    SyncGuidChanged = Guid.NewGuid()
                });
            }
        }
    }

    /// <summary>
    ///  Seeds the default system project.
    /// </summary>
    private async Task SeedDefaultProjectAsync(LegatroDbContext context, Guid userId)
    {
        bool exists = await context.Projects
            .AnyAsync(p => p.ProjectName == "Default" && p.IsSystem);

        if (!exists)
        {
            context.Projects.Add(new Project
            {
                Id = Guid.NewGuid(),
                IDUser = userId,
                IDCategory = null,
                ProjectName = "Default",
                Description = "Default system project",
                DueDate = null,
                IsSystem = true,
                TimeBudget = null,
                TimeSpent = TimeSpan.Zero,
                DateCreated = DateTime.UtcNow,
                DateLastEdited = DateTime.UtcNow,
                SyncGuidChanged = Guid.NewGuid()
            });
        }
    }

    /// <summary>
    ///  Calculates the WCAG contrast ratio between two colors.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <returns>The contrast ratio.</returns>
    private static double CalculateContrastRatio(Color color1, Color color2)
    {
        double l1 = GetRelativeLuminance(color1);
        double l2 = GetRelativeLuminance(color2);

        double lighter = Math.Max(l1, l2);
        double darker = Math.Min(l1, l2);

        return (lighter + 0.05) / (darker + 0.05);
    }

    /// <summary>
    ///  Gets the relative luminance of a color according to WCAG formula.
    /// </summary>
    /// <param name="color">The color to calculate luminance for.</param>
    /// <returns>The relative luminance value between 0 and 1.</returns>
    private static double GetRelativeLuminance(Color color)
    {
        double r = GetLinearColorComponent(color.R / 255.0);
        double g = GetLinearColorComponent(color.G / 255.0);
        double b = GetLinearColorComponent(color.B / 255.0);

        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    /// <summary>
    ///  Converts a color component to its linear RGB value.
    /// </summary>
    private static double GetLinearColorComponent(double component)
    {
        if (component <= 0.03928)
        {
            return component / 12.92;
        }

        return Math.Pow((component + 0.055) / 1.055, 2.4);
    }

    /// <summary>
    ///  Gets a WCAG AA compliant foreground color (white or black) for the given background color.
    /// </summary>
    /// <param name="backColor">The background color.</param>
    /// <returns>Either white or black, whichever has better contrast.</returns>
    private static Color GetCompliantForeColor(Color backColor)
    {
        double whiteContrast = CalculateContrastRatio(backColor, Color.White);
        double blackContrast = CalculateContrastRatio(backColor, Color.Black);

        return whiteContrast > blackContrast ? Color.White : Color.Black;
    }

    /// <summary>
    ///  Represents a symbol definition for seeding.
    /// </summary>
    private record SymbolDefinition(string Name, string Char, string BackColor, string ForeColor);

    /// <summary>
    ///  Represents a category definition for seeding.
    /// </summary>
    private record CategoryDefinition(string Name, string BackColor);

    /// <summary>
    ///  Represents a group definition for seeding.
    /// </summary>
    private record GroupDefinition(
        string Name,
        int OrderNo,
        TimeSpan? AutoRangeSpan,
        int? AutoRangeOffset,
        string BackColor,
        string ForeColor);
}
