using System.Text.Json;

namespace LegatroExp.Helpers;

public class AppSettings
{
    public string? DatabasePath { get; set; }
    public WindowPosition MainWindow { get; set; } = new();
    public string DefaultSortOrder { get; set; } = "DisplayName";
    public bool AutoBackup { get; set; } = true;
    public string BaseFont { get; set; } = "Segoe UI";
    public string Language { get; set; } = "English";

    public class WindowPosition
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; } = 0.8;
        public double Height { get; set; } = 0.8;
        public bool Maximized { get; set; }
        public double VerticalSplitterPosition { get; set; } = 0.3;
        public double HorizontalSplitterPosition { get; set; } = 0.6;
    }

    private static readonly string SettingsPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "LegatroExp",
        "settings.json");

    public static AppSettings Load()
    {
        try
        {
            if (File.Exists(SettingsPath))
            {
                string json = File.ReadAllText(SettingsPath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
        }
        catch
        {
            // If loading fails, return default settings
        }

        return new AppSettings();
    }

    public void Save()
    {
        try
        {
            string directory = Path.GetDirectoryName(SettingsPath)!;
            Directory.CreateDirectory(directory);

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SettingsPath, json);
        }
        catch
        {
            // Silently fail if we cannot save settings
        }
    }
}
