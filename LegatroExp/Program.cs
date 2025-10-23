using LegatroExp.Forms;

namespace LegatroExp;

/// <summary>
///  The main entry point for the application.
/// </summary>
internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
        
        Application.Run(new MainForm());
    }
}
