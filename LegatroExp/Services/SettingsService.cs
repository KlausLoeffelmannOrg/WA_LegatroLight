using System.Text.Json;
using LegatroExp.Models;

namespace LegatroExp.Services;

/// <summary>
/// Service for managing application settings
/// </summary>
public class SettingsService
{
    private static readonly string SettingsDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "LegatroExp");

    private static readonly string SettingsFilePath = Path.Combine(SettingsDirectory, "settings.json");

    private AppSettings _settings;

    public SettingsService()
    {
        _settings = LoadSettings();
    }

    /// <summary>
    /// Gets the current settings
    /// </summary>
    public AppSettings Settings => _settings;

    /// <summary>
    /// Loads settings from file or creates default settings
    /// </summary>
    private AppSettings LoadSettings()
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
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error loading settings: {ex.Message}\nUsing default settings.",
                "Settings Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        return new AppSettings();
    }

    /// <summary>
    /// Saves settings to file
    /// </summary>
    public void SaveSettings()
    {
        try
        {
            Directory.CreateDirectory(SettingsDirectory);

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(_settings, options);
            File.WriteAllText(SettingsFilePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error saving settings: {ex.Message}",
                "Settings Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
