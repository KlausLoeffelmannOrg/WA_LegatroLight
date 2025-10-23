using Microsoft.EntityFrameworkCore;
using LegatroExp.Data;
using LegatroExp.Forms;
using LegatroExp.Helpers;
using LegatroExp.Models;

namespace LegatroExp;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetDefaultFont(new Font("Segoe UI", 11F));

        AppSettings settings = AppSettings.Load();

        if (string.IsNullOrEmpty(settings.DatabasePath))
        {
            string defaultPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "LegatroExp",
                "default.legatro");

            Directory.CreateDirectory(Path.GetDirectoryName(defaultPath)!);
            settings.DatabasePath = defaultPath;
            settings.Save();
        }

        User currentUser = WindowsAuthHelper.GetCurrentWindowsUser();

        DbContextOptionsBuilder<LegatroDbContext> optionsBuilder = new();
        optionsBuilder.UseSqlite($"Data Source={settings.DatabasePath}");

        using LegatroDbContext context = new(optionsBuilder.Options);

        bool isFirstRun = !File.Exists(settings.DatabasePath);

        if (isFirstRun)
        {
            using UserSetupForm setupForm = new(currentUser);

            if (setupForm.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            DatabaseInitializer.InitializeDatabase(context, currentUser);
        }
        else
        {
            context.Database.EnsureCreated();

            User? existingUser = context.Users.FirstOrDefault(u => u.UserSid == currentUser.UserSid);

            if (existingUser is null)
            {
                using UserSetupForm setupForm = new(currentUser);

                if (setupForm.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                context.Users.Add(currentUser);
                context.SaveChanges();
            }
            else
            {
                currentUser = existingUser;
            }
        }

        Application.Run(new MainForm(context, currentUser, settings));
    }
}
