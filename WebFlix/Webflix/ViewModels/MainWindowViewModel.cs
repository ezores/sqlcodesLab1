using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;

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

    private void SignInCommandExecute()
    {
        //if client exists: navigate to search view
        // else show error message.
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(SearchView));
    }
}