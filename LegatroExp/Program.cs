using LegatroExp.Forms;
using LegatroExp.Services;

namespace LegatroExp;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetColorMode(SystemColorMode.System);
        Application.SetHighDpiMode(HighDpiMode.SystemAware);

        // Load settings
        SettingsService settingsService = new();
        
        // Check if this is first run or if database doesn't exist
        string? databasePath = settingsService.Settings.LastDatabasePath;
        bool isFirstRun = string.IsNullOrEmpty(databasePath) || !File.Exists(databasePath);

        if (isFirstRun)
        {
            // Show user setup assistant
            using UserSetupAssistant userSetup = new();
            if (userSetup.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Create default database path
            string appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "LegatroExp");
            Directory.CreateDirectory(appDataPath);
            
            databasePath = Path.Combine(appDataPath, "default.legatro");
            settingsService.Settings.LastDatabasePath = databasePath;
            settingsService.SaveSettings();
        }

        // Initialize database
        DatabaseService databaseService = new(databasePath!);
        System.Threading.Tasks.Task.Run(async () => await databaseService.InitializeAsync()).Wait();

        // Run main form
        Application.Run(new MainForm(databaseService, settingsService));

        // Cleanup
        if (settingsService.Settings.AutoBackupEnabled)
        {
            databaseService.CreateBackup();
        }
        databaseService.Close();
    }
}
