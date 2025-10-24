using System.Text.Json;

namespace LegatroExp.Helpers;

/// <summary>
///  Application settings manager.
/// </summary>
public class AppSettings
{
    /// <summary>
    ///  Gets or sets the database file path.
    /// </summary>
    public string? DatabasePath { get; set; }

    /// <summary>
    ///  Gets or sets the main window left position as percentage (0-100).
    /// </summary>
    public double WindowLeftPercent { get; set; }

    /// <summary>
    ///  Gets or sets the main window top position as percentage (0-100).
    /// </summary>
    public double WindowTopPercent { get; set; }

    /// <summary>
    ///  Gets or sets the main window width as percentage (0-100).
    /// </summary>
    public double WindowWidthPercent { get; set; } = 80;

    /// <summary>
    ///  Gets or sets the main window height as percentage (0-100).
    /// </summary>
    public double WindowHeightPercent { get; set; } = 80;

    /// <summary>
    ///  Gets or sets whether the window is maximized.
    /// </summary>
    public bool IsMaximized { get; set; }

    /// <summary>
    ///  Gets or sets the main vertical splitter position as percentage (0-100).
    /// </summary>
    public double MainSplitterPercent { get; set; } = 30;

    /// <summary>
    ///  Gets or sets the right panel horizontal splitter position as percentage (0-100).
    /// </summary>
    public double RightSplitterPercent { get; set; } = 60;

    /// <summary>
    ///  Gets or sets the default sort order for tasks.
    /// </summary>
    public TaskSortOrder DefaultSortOrder { get; set; } = TaskSortOrder.DisplayName;

    /// <summary>
    ///  Gets or sets whether auto-backup is enabled.
    /// </summary>
    public bool AutoBackupEnabled { get; set; } = true;

    /// <summary>
    ///  Gets or sets the base font name.
    /// </summary>
    public string BaseFontName { get; set; } = "Segoe UI";

    /// <summary>
    ///  Gets or sets the base font size.
    /// </summary>
    public float BaseFontSize { get; set; } = 11f;

    /// <summary>
    ///  Gets or sets the UI language.
    /// </summary>
    public string Language { get; set; } = "English";

    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "LegatroExp");

    private static readonly string SettingsFilePath = Path.Combine(SettingsDirectory, "settings.json");

    /// <summary>
    ///  Loads settings from the configuration file.
    /// </summary>
    /// <returns>The loaded settings or a new instance if the file doesn't exist.</returns>
    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsFilePath))
            {
                string json = File.ReadAllText(SettingsFilePath);
                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json);
                
                return settings ?? new AppSettings();
            }
        }
        catch
        {
            // If there's any error loading settings, return defaults
        }

        return new AppSettings();
    }

    /// <summary>
    ///  Saves the current settings to the configuration file.
    /// </summary>
    public void Save()
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);
            
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsFilePath, json);
        }
        catch
        {
            // Silently fail if settings can't be saved
        }
    }
}

/// <summary>
///  Enumeration of task sort orders.
/// </summary>
public enum TaskSortOrder
{
    DisplayName,
    DueDate,
    PercentDone,
    EstimatedEffort,
    DateCreated
}
