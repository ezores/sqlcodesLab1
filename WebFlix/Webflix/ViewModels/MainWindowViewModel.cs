using System;
using System.Linq;
using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;
using Webflix.Services;
using Webflix.Models;

namespace Webflix.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;

    private string _errorMessage = string.Empty;

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    private bool _isErrorMessageVisible;

    public bool IsErrorMessageVisible
    {
        get => _isErrorMessageVisible;
        set => this.RaiseAndSetIfChanged(ref _isErrorMessageVisible, value);
    }

    public string ApplicationTile => "WEBFLIX";
    public string SignIn => "Sign In";
    public string Email => "Email";
    public string Password => "Password";

    public string UserNameTextBox { get; set; } = string.Empty;

    public string PasswordTextBox { get; set; } = string.Empty;

    public ReactiveCommand<Unit, Unit> SignInCommand { get; set; }

    public MainWindowViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;

        SignInCommand = ReactiveCommand.Create(SignInCommandExecute);
    }

    private bool _isLogoutVisible;

    public bool IsLogoutVisible
    {
        get => _isLogoutVisible;
        set => this.RaiseAndSetIfChanged(ref _isLogoutVisible, value);
    }

    // private void SignInCommandExecute()
    // {
    //     //if client exists: navigate to search view
    //     // else show error message.
    //     
    //     _regionManager.RequestNavigate(Regions.MainRegion, nameof(SearchView));
    // }
//     private void SignInCommandExecute()
//     {
//         // 1) Begin
//         Console.WriteLine("[DEBUG] SignInCommandExecute() started.");
//
//         // Since you removed hashing, we use the user's typed password as is:
//         string plainPassword = PasswordTextBox?.Trim() ?? "";
//         string email = UserNameTextBox?.Trim() ?? "";
//
//         Console.WriteLine($"[DEBUG] Attempting login with email: \"{email}\" and password: \"{plainPassword}\"");
//
//         bool foundAny = false;
//         try
//         {
//             // 2) Attempt to connect & query
//             using (var db = new MyDbContext())
//             {
//                 Console.WriteLine("[DEBUG] Database context created. Checking 'Employes'...");
//
//                 // 3) Check if there's an employee record
//                 var employee = db.Employes
//                     .FirstOrDefault(e => e.Courriel == email && e.MotDePasse == plainPassword);
//
//                 Console.WriteLine($"[DEBUG] Employee found? {(employee != null ? "YES" : "NO")}");
//
//                 if (employee != null)
//                 {
//                     foundAny = true;
//                 }
//                 else
//                 {
//                     // 4) If not employee, check client
//                     Console.WriteLine("[DEBUG] Checking 'Clients'...");
//
//                     var client = db.Clients
//                         .FirstOrDefault(c => c.Courriel == email && c.MotDePasse == plainPassword);
//
//                     Console.WriteLine($"[DEBUG] Client found? {(client != null ? "YES" : "NO")}");
//
//                     if (client != null)
//                     {
//                         foundAny = true;
//                     }
//                 }
//
//                 Console.WriteLine($"[DEBUG] foundAny = {foundAny}");
//             }
//         }
//         catch (Exception ex)
//         {
//             // 5) Any exception means we couldn't query properly
//             Console.WriteLine("[DEBUG] EXCEPTION thrown: " + ex.Message);
//             foundAny = false;
//         }
//
//         // 6) If we found a match => success
//         if (foundAny)
//         {
//             Console.WriteLine("[DEBUG] Credentials validated. Navigating to SearchView...");
//             ErrorMessage = string.Empty;
//             IsErrorMessageVisible = false;
//             _regionManager.RequestNavigate(Regions.MainRegion, nameof(SearchView));
//         }
//         else
//         {
//             // 7) Otherwise => invalid
//             Console.WriteLine("[DEBUG] Invalid credentials. Setting error message.");
//             ErrorMessage = "Invalid credentials. Please try again.";
//             IsErrorMessageVisible = true;
//         }
//
//         Console.WriteLine("[DEBUG] SignInCommandExecute() finished.");
//     }
// }

    private void SignInCommandExecute()
    {
        Console.WriteLine("[DEBUG] SignInCommandExecute() started.");

        string plainPassword = PasswordTextBox?.Trim() ?? "";
        string email = UserNameTextBox?.Trim() ?? "";

        Console.WriteLine($"[DEBUG] Attempting login with email: \"{email}\" and password: \"{plainPassword}\"");

        bool foundAny = false;
        try
        {
            using (var db = new MyDbContext())
            {
                // Remove / comment out the employee check if your EMPLOYE table is empty:
                // var employee = db.Employes
                //     .FirstOrDefault(e => e.Courriel == email && e.MotDePasse == plainPassword);
                // if (employee != null)
                // {
                //     foundAny = true;
                // }
                // else
                // {
                //     ...
                // }

                // Just do a direct client check:
                Console.WriteLine("[DEBUG] Checking 'Clients'...");
                // var client = db.Clients
                //     .FirstOrDefault(c => c.Courriel == email && c.MotDePasse == plainPassword);
                var client = db.Clients
                    .Where(c => c.Courriel == email && c.MotDePasse == plainPassword)
                    .Select(c => new
                    {
                        c.ClientId,
                        c.Courriel,
                        c.MotDePasse,
                        CodeAbonnementTrimmed = c.CodeAbonnement.Trim() // Ensuring we remove spaces
                    })
                    .FirstOrDefault();

                Console.WriteLine($"[DEBUG] Client found? {(client != null ? "YES" : "NO")}");

                if (client != null)
                {
                    foundAny = true;
                }

                Console.WriteLine($"[DEBUG] foundAny = {foundAny}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[DEBUG] EXCEPTION thrown: " + ex.Message);
            foundAny = false;
        }

        if (foundAny)
        {
            Console.WriteLine("[DEBUG] Credentials validated. Navigating to SearchView...");
            ErrorMessage = string.Empty;
            IsErrorMessageVisible = false;
            _regionManager.RequestNavigate(Regions.MainRegion, nameof(SearchView));
        }
        else
        {
            Console.WriteLine("[DEBUG] Invalid credentials. Setting error message.");
            ErrorMessage = "Invalid credentials. Please try again.";
            IsErrorMessageVisible = true;
        }

        Console.WriteLine("[DEBUG] SignInCommandExecute() finished.");
    }
}
