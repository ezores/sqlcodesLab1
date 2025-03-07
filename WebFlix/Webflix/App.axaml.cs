using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Webflix.Resources;
using Webflix.ViewModels;
using Webflix.Views;

namespace Webflix;

public partial class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }
    
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<MainWindow>();
        
        containerRegistry.RegisterForNavigation<SearchView, SearchViewModel>();
        containerRegistry.RegisterForNavigation<MovieGridView, MovieGridViewModel>();
        containerRegistry.RegisterForNavigation<MovieView, MovieViewModel>();
        containerRegistry.RegisterForNavigation<PersonView, PersonViewModel>();
    }
    
    protected override AvaloniaObject CreateShell() => Container.Resolve<MainWindow>();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        // Register initial Views to Region.
        var regionManager = Container.Resolve<IRegionManager>();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Exit += OnExit;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
    }
}