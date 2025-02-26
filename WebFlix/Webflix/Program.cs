// using Avalonia;
// using Avalonia.ReactiveUI;
// using System;
//
// namespace Webflix;
//
// sealed class Program
// {
//     // Initialization code. Don't use any Avalonia, third-party APIs or any
//     // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
//     // yet and stuff might break.
//     [STAThread]
//     public static void Main(string[] args) => BuildAvaloniaApp()
//         .StartWithClassicDesktopLifetime(args);
//
//     // Avalonia configuration, don't remove; also used by visual designer.
//     public static AppBuilder BuildAvaloniaApp()
//         => AppBuilder.Configure<App>()
//             .UsePlatformDetect()
//             .WithInterFont()
//             .LogToTrace()
//             .UseReactiveUI();
// }

using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using (var context = new MyDbContext())
        {
            try
            {
                var cartes = context.CartesCreditBackup.ToList();

                if (!cartes.Any())
                {
                    Console.WriteLine("No records found in CARTECREDIT_BACKUP.");
                }
                else
                {
                    foreach (var carte in cartes)
                    {
                        Console.WriteLine($"ID: {carte.Id}, Numéro: {carte.Numero}, Expiration: {carte.DateExpiration.ToShortDateString()}, CVV: {carte.CVV}, Type: {carte.TypeCarte}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to Oracle: " + ex.Message);
            }
        }
    }
}