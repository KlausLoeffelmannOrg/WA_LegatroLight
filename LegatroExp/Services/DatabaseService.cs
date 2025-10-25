using System.Security.Principal;
using Microsoft.EntityFrameworkCore;
using LegatroExp.Data.Context;
using LegatroExp.Data.Entities;

namespace LegatroExp.Services;

/// <summary>
/// Service for database initialization and management
/// </summary>
public class DatabaseService
{
    private readonly string _databasePath;
    private LegatroDbContext? _dbContext;

    public DatabaseService(string databasePath)
    {
        _databasePath = databasePath;
    }

    /// <summary>
    /// Gets the database context
    /// </summary>
    public LegatroDbContext DbContext
    {
        get
        {
            if (_dbContext is null)
            {
                throw new InvalidOperationException("Database not initialized. Call InitializeAsync first.");
            }
            
            return _dbContext;
        }
    }

    /// <summary>
    /// Initializes the database
    /// </summary>
    public async System.Threading.Tasks.Task<bool> InitializeAsync()
    {
        try
        {
            DbContextOptionsBuilder<LegatroDbContext> optionsBuilder = new();
            optionsBuilder.UseSqlite($"Data Source={_databasePath}");

            _dbContext = new LegatroDbContext(optionsBuilder.Options);

            bool isNewDatabase = !File.Exists(_databasePath);

            await _dbContext.Database.EnsureCreatedAsync();

            if (isNewDatabase)
            {
                await SeedInitialDataAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error initializing database: {ex.Message}",
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            
            return false;
        }
    }

    /// <summary>
    /// Seeds initial data for a new database
    /// </summary>
    private async System.Threading.Tasks.Task SeedInitialDataAsync()
    {
        if (_dbContext is null) return;

        // Get current Windows user
        User currentUser = CreateCurrentWindowsUser();
        _dbContext.Users.Add(currentUser);
        await _dbContext.SaveChangesAsync();

        // Create system groups
        List<Group> systemGroups = CreateSystemGroups(currentUser.IDUser);
        _dbContext.Groups.AddRange(systemGroups);

        // Create system categories
        List<Category> systemCategories = CreateSystemCategories(currentUser.IDUser);
        _dbContext.Categories.AddRange(systemCategories);

        // Create default project
        Project defaultProject = new()
        {
            ProjectName = "Default",
            Description = "Default system project",
            IDUser = currentUser.IDUser,
            IsSystem = true
        };
        _dbContext.Projects.Add(defaultProject);

        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Creates a User entity from current Windows user
    /// </summary>
    private User CreateCurrentWindowsUser()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();
        string userName = Environment.UserName;
        string domainName = Environment.UserDomainName;
        string displayName = identity.Name ?? userName;

        // Parse display name to extract first/last name if possible
        string[] nameParts = displayName.Split(new[] { '\\', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string firstName = nameParts.Length > 0 ? nameParts[^1] : userName;
        string lastName = nameParts.Length > 1 ? nameParts[^2] : string.Empty;

        return new User
        {
            UserDisplayId = userName,
            UserAuthID = identity.Name ?? userName,
            UserSid = identity.User?.Value ?? Guid.NewGuid().ToString(),
            DomainOrMachine = domainName,
            UserName = userName,
            DisplayName = displayName,
            FirstName = firstName,
            LastName = lastName
        };
    }

    /// <summary>
    /// Creates system groups
    /// </summary>
    private List<Group> CreateSystemGroups(Guid userId)
    {
        return new List<Group>
        {
            new()
            {
                GroupDisplayName = "My Day",
                OrderNo = 1,
                CreatedByIDUser = userId,
                BackColor = "#E3F2FD",
                ForeColor = "#000000",
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(1),
                AutoRangeOffset = 0
            },
            new()
            {
                GroupDisplayName = "Sliding Week",
                OrderNo = 2,
                CreatedByIDUser = userId,
                BackColor = "#F3E5F5",
                ForeColor = "#000000",
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(7),
                AutoRangeOffset = 0
            },
            new()
            {
                GroupDisplayName = "My Month",
                OrderNo = 3,
                CreatedByIDUser = userId,
                BackColor = "#FFF3E0",
                ForeColor = "#000000",
                IsSystem = true,
                AutoRangeSpan = TimeSpan.FromDays(30),
                AutoRangeOffset = 0
            },
            new()
            {
                GroupDisplayName = "Roaming Links",
                OrderNo = 4,
                CreatedByIDUser = userId,
                BackColor = "#E8F5E9",
                ForeColor = "#000000",
                IsSystem = true
            },
            new()
            {
                GroupDisplayName = "Roaming Notes",
                OrderNo = 5,
                CreatedByIDUser = userId,
                BackColor = "#FFF9C4",
                ForeColor = "#000000",
                IsSystem = true
            },
            new()
            {
                GroupDisplayName = "Done",
                OrderNo = 65534,
                CreatedByIDUser = userId,
                BackColor = "#C8E6C9",
                ForeColor = "#000000",
                IsSystem = true
            },
            new()
            {
                GroupDisplayName = "Clutter",
                OrderNo = 65535,
                CreatedByIDUser = userId,
                BackColor = "#EEEEEE",
                ForeColor = "#000000",
                IsSystem = true
            }
        };
    }

    /// <summary>
    /// Creates system color categories
    /// </summary>
    private List<Category> CreateSystemCategories(Guid userId)
    {
        List<(string name, string back, string fore)> colors = new()
        {
            ("Red", "#FFCDD2", "#000000"),
            ("Pink", "#F8BBD0", "#000000"),
            ("Purple", "#E1BEE7", "#000000"),
            ("Blue", "#BBDEFB", "#000000"),
            ("Cyan", "#B2EBF2", "#000000"),
            ("Teal", "#B2DFDB", "#000000"),
            ("Green", "#C8E6C9", "#000000"),
            ("Yellow", "#FFF9C4", "#000000"),
            ("Orange", "#FFE0B2", "#000000"),
            ("Brown", "#D7CCC8", "#000000")
        };

        return colors.Select(c => new Category
        {
            CategoryName = c.name,
            IDUser = userId,
            BackColor = c.back,
            ForeColor = c.fore,
            IsSystem = true
        }).ToList();
    }

    /// <summary>
    /// Creates a backup of the database
    /// </summary>
    public void CreateBackup()
    {
        try
        {
            if (File.Exists(_databasePath))
            {
                string backupPath = $"{_databasePath}.bak";
                File.Copy(_databasePath, backupPath, overwrite: true);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error creating backup: {ex.Message}",
                "Backup Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Closes the database connection
    /// </summary>
    public void Close()
    {
        _dbContext?.Dispose();
        _dbContext = null;
    }
}
