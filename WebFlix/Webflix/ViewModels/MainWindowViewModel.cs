using System;
using System.Linq;
using System.Reactive;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;
using Webflix.Services;
using Webflix.Models;
using Webflix.Repositories;
using Webflix.Repositories.Interfaces;

namespace Webflix.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    private readonly IAuthenticationService _authService;
    
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
    
    private readonly IClientRepository _clientRepository;
    
    public MainWindowViewModel(IRegionManager regionManager, IAuthenticationService authService)
    {
        _regionManager = regionManager;
        _authService = authService;
        //_clientRepository = clientRepository;
        SignInCommand = ReactiveCommand.Create(SignInCommandExecute);
    }

    private bool _isLogoutVisible;

    public bool IsLogoutVisible
    {
        get => _isLogoutVisible;
        set => this.RaiseAndSetIfChanged(ref _isLogoutVisible, value);
    }

    private async void SignInCommandExecute()
    {
        var (isAuthenticated, errorMessage) = await _authService.AuthenticateAsync(UserNameTextBox, PasswordTextBox);

        if (isAuthenticated)
        {
            errorMessage = string.Empty;
            IsErrorMessageVisible = false;
            _regionManager.RequestNavigate(Regions.MainRegion, nameof(SearchView));
        }
        else
        {
            this.ErrorMessage = errorMessage;
            IsErrorMessageVisible = true;
        }
    }
}
