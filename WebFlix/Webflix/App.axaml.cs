using System;
using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
using Webflix.Models.Entities;
using Webflix.Repositories;
using Webflix.Repositories.Interfaces;
using Webflix.Resources;
using Webflix.Services;
using Webflix.Services.Interfaces;
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
        
        // Enregistrer le DbContext
        containerRegistry.RegisterSingleton<MyDbContext>();
        
        // Enregistrer les repositories
        containerRegistry.RegisterScoped<IFilmRepository, FilmRepository>();
        containerRegistry.RegisterScoped<ICopieFilmRepository, CopieFilmRepository>();
        containerRegistry.RegisterScoped<IPersonneRepository, PersonneRepository>();
        containerRegistry.RegisterScoped<IEmployeRepository, EmployeRepository>();
        containerRegistry.RegisterScoped<IClientRepository, ClientRepository>();
        containerRegistry.RegisterScoped<IRentalRepository, RentalRepository>();
        containerRegistry.RegisterScoped<IInformationRepository, InformationRepository>();
        containerRegistry.RegisterScoped<IMovieRatingRepository, MovieRatingRepository>();
        containerRegistry.RegisterScoped<IMovieCorrelationRepository, MovieCorrelationRepository>();
        containerRegistry.RegisterScoped<IClientMovieRepository, ClientMovieRepository>();
        
        // Services
        containerRegistry.RegisterScoped<IAuthenticationService, AuthenticationService>();
        containerRegistry.RegisterScoped<ICopieFilmService, CopieFilmService>();
        
        // Navigation
        containerRegistry.RegisterForNavigation<SearchView, SearchViewModel>();
        containerRegistry.RegisterForNavigation<MovieGridView, MovieGridViewModel>();
        containerRegistry.RegisterForNavigation<MovieView, MovieViewModel>();
        containerRegistry.RegisterForNavigation<PersonView, PersonViewModel>();
        AddHttpClientFactory(containerRegistry);
        AddDbContextFactory(containerRegistry);
    }

    private void AddHttpClientFactory(IContainerRegistry containerRegistry)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHttpClient();
        var provider = serviceCollection.BuildServiceProvider();

        containerRegistry.RegisterInstance(typeof(IHttpClientFactory),
            provider.GetRequiredService<IHttpClientFactory>());
    }
    
    private void AddDbContextFactory(IContainerRegistry containerRegistry)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContextFactory<MyDbContext>(options => options.UseOracle("User Id=EQUIPE201;Password=yy3IR1VP;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=bdlog660.ens.ad.etsmtl.ca)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLPDB.ens.ad.etsmtl.ca)))")
            .LogTo(message => Console.WriteLine($"EF Core: {message}"),  // Log SQL and parameters
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Debug
            )
            .EnableSensitiveDataLogging());
        var provider = serviceCollection.BuildServiceProvider();

        containerRegistry.RegisterInstance(typeof(IDbContextFactory<MyDbContext>),
            provider.GetRequiredService<IDbContextFactory<MyDbContext>>());
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