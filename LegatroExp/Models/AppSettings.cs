namespace LegatroExp.Models;

/// <summary>
/// Application settings stored in settings.json
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Last opened database file path
    /// </summary>
    public string? LastDatabasePath { get; set; }

    /// <summary>
    /// Main window position and size
    /// </summary>
    public WindowPosition? MainWindowPosition { get; set; }

    /// <summary>
    /// Main vertical splitter position (percentage)
    /// </summary>
    public double MainVerticalSplitterPosition { get; set; } = 0.3;

    /// <summary>
    /// Right horizontal splitter position (percentage)
    /// </summary>
    public double RightHorizontalSplitterPosition { get; set; } = 0.6;

    /// <summary>
    /// Default sort order for tasks
    /// </summary>
    public TaskSortOrder DefaultTaskSortOrder { get; set; } = TaskSortOrder.DisplayName;

    /// <summary>
    /// Auto-backup on shutdown
    /// </summary>
    public bool AutoBackupEnabled { get; set; } = true;

    /// <summary>
    /// Base font name for the application
    /// </summary>
    public string BaseFontName { get; set; } = "Segoe UI";

    /// <summary>
    /// Base font size in points
    /// </summary>
    public float BaseFontSize { get; set; } = 11f;

    /// <summary>
    /// UI language
    /// </summary>
    public string UiLanguage { get; set; } = "en-US";
}

/// <summary>
/// Window position and size information
/// </summary>
public class WindowPosition
{
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsMaximized { get; set; }
}

/// <summary>
/// Task sorting options
/// </summary>
public enum TaskSortOrder
{
    DisplayName,
    DueDate,
    PercentDone,
    EstimatedEffort,
    DateCreated
}
