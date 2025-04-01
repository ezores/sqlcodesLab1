using System.Reactive;
using Prism.Regions;
using ReactiveUI;

namespace Webflix.ViewModels;

public class ViewModelBase : ReactiveObject, INavigationAware
{
    protected IRegionNavigationService? _navigationService;
    
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; set; }

    protected ViewModelBase()
    {
        GoBackCommand = ReactiveCommand.Create(GoBack);
    }
    
    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
        _navigationService = navigationContext.NavigationService;
    }

    public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    private void GoBack()
    {
        if (_navigationService?.Journal.CanGoBack is true)
        {
            _navigationService.Journal.GoBack();
        }
    }
}