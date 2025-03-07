using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class MovieViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    #region ViewProperties
    private string _title = string.Empty;
    private string _releaseYear = string.Empty;
    private string _language = string.Empty;
    private string _movieLength = string.Empty;
    private string _directorName = string.Empty;
    private string _description = string.Empty;
    private ObservableCollection<string> _countries = new();
    private ObservableCollection<string> _genres = new();
    private ObservableCollection<string> _screenwriters = new();
    private ObservableCollection<string> _actors = new();
    private ObservableCollection<string> _trailers = new();
    
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public string ReleaseYear
    {
        get => _releaseYear;
        set => this.RaiseAndSetIfChanged(ref _releaseYear, value);
    }

    public string Language
    {
        get => _language;
        set => this.RaiseAndSetIfChanged(ref _language, value);
    }

    public string MovieLength
    {
        get => _movieLength;
        set => this.RaiseAndSetIfChanged(ref _movieLength, value);
    }

    public string DirectorName
    {
        get => _directorName;
        set => this.RaiseAndSetIfChanged(ref _directorName, value);
    }

    public string Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public ObservableCollection<string> Countries
    {
        get => _countries;
        set => this.RaiseAndSetIfChanged(ref _countries, value);
    }

    public ObservableCollection<string> Genres
    {
        get => _genres;
        set => this.RaiseAndSetIfChanged(ref _genres, value);
    }

    public ObservableCollection<string> Screenwriters
    {
        get => _screenwriters;
        set => this.RaiseAndSetIfChanged(ref _screenwriters, value);
    }

    public ObservableCollection<string> Actors
    {
        get => _actors;
        set => this.RaiseAndSetIfChanged(ref _actors, value);
    }

    public ObservableCollection<string> Trailers
    {
        get => _trailers;
        set => this.RaiseAndSetIfChanged(ref _trailers, value);
    }
    #endregion

    private string _selectedActor;
    private string _selectedScreenwriter;
    private string _selectedTrailer;
    
    public string SelectedActor
    {
        get => _selectedActor;
        set => this.RaiseAndSetIfChanged(ref _selectedActor, value);
    }

    public string SelectedScreenwriter
    {
        get => _selectedScreenwriter;
        set => this.RaiseAndSetIfChanged(ref _selectedScreenwriter, value);
    }

    public string SelectedTrailer
    {
        get => _selectedTrailer;
        set => this.RaiseAndSetIfChanged(ref _selectedTrailer, value);
    }
    
    public ReactiveCommand<Unit, Unit> ActorCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ScreenwriterCommand { get; set; }
    public ReactiveCommand<Unit, Unit> TrailerCommand { get; set; }
    
    public MovieViewModel(IRegionManager regionManager)
    {
        Title = "Spider-Man";
        ReleaseYear = "2012";
        Language = "English";
        MovieLength = "120";
        DirectorName = "Director";
        Description = "This is a description";
        Countries.Add("Canada");
        Countries.Add("Canada");
        Countries.Add("Canada");
        Genres.Add("Horror");
        Genres.Add("Horror");
        Genres.Add("Horror");
        Screenwriters.Add("Screenwriter");
        Screenwriters.Add("Screenwriter");
        Screenwriters.Add("Screenwriter");
        Actors.Add("Tobey");
        Actors.Add("Tobey");
        Actors.Add("Tobey");
        Trailers.Add("http://www.google.com");
        Trailers.Add("https://www.youtube.com");
        Trailers.Add("http://www.microsoft.com");

        _regionManager = regionManager;
        
        ActorCommand = ReactiveCommand.Create(ActorCommandExecute);
        ScreenwriterCommand = ReactiveCommand.Create(ScreenwriterCommandExecute);
        TrailerCommand = ReactiveCommand.Create(NavigateToTrailerView);
    }

    private void ActorCommandExecute()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(PersonView));
    }
    
    private void ScreenwriterCommandExecute()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(PersonView));
    }

    private void NavigateToTrailerView()
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = EnsureHttps(SelectedTrailer),
                UseShellExecute = true
            });
        }
        catch (Exception e)
        {
            //something went wrong
        }
    }
    
    private string EnsureHttps(string url)
    {
        if (url.StartsWith("http://"))
        {
            return "https://" + url.Substring(7); // Replace "http://" with "https://"
        }
        return url; // Already secure or malformed URL
    }
}