using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using MsBox.Avalonia;
using Prism.Regions;
using ReactiveUI;
using Webflix.Helpers;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;
using Webflix.Resources;
using Webflix.Services;
using Webflix.Services.Interfaces;
using Webflix.Views;

namespace Webflix.ViewModels;

public class MovieViewModel : ViewModelBase
{
    public static readonly string PERSON_PARAMETER = "person-parameter";
    public static readonly string IS_RECOMMENDATION_PARAM = "is-recommendation";
    
    private readonly IRegionManager _regionManager;
    private readonly ICopieFilmService _copieFilmService;
    private readonly IClientRepository _clientRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private List<Film> _movieRecommendations = [];

    private Film? _movie;
    
    #region ViewProperties
    private string _title = string.Empty;
    private string _releaseYear = string.Empty;
    private string _language = string.Empty;
    private string _movieLength = string.Empty;
    private string _directorName = string.Empty;
    private string _description = string.Empty;
    private double _rating;
    private ObservableCollection<string> _countries = new();
    private ObservableCollection<string> _genres = new();
    private ObservableCollection<string?> _screenwriters = new();
    private ObservableCollection<string> _actors = new();
    private ObservableCollection<string> _trailers = new();
    private ObservableCollection<string> _recommendations = new();
    
    private Bitmap? _moviePoster;

    public Bitmap? MoviePoster
    {
        get => _moviePoster;
        set => this.RaiseAndSetIfChanged(ref _moviePoster, value);
    }
    
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

    public double Rating
    {
        get => _rating;
        set
        {
            _rating = Math.Round(value, 2);
            this.RaisePropertyChanged();
        }
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

    public ObservableCollection<string?> Screenwriters
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

    public ObservableCollection<string> Recommendations
    {
        get => _recommendations;
        set => this.RaiseAndSetIfChanged(ref _recommendations, value);
    }
    #endregion

    private string _selectedActor;
    private string _selectedTrailer;
    private string _selectedRecommendation;

    private bool _rented = false;
    public string SelectedActor
    {
        get => _selectedActor;
        set => this.RaiseAndSetIfChanged(ref _selectedActor, value);
    }

    public string SelectedTrailer
    {
        get => _selectedTrailer;
        set => this.RaiseAndSetIfChanged(ref _selectedTrailer, value);
    }

    public string SelectedRecommendation
    {
        get => _selectedRecommendation;
        set => this.RaiseAndSetIfChanged(ref _selectedRecommendation, value);
    }
    
    public ReactiveCommand<Unit, Unit> RecommendationCommand { get; set; }
    public ReactiveCommand<Unit, Unit> ActorCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DirectorCommand { get; set; }
    public ReactiveCommand<Unit, Unit> TrailerCommand { get; set; }
    public ReactiveCommand<Unit, Unit> RentCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> ReturnCommand { get; set; }
    
    public MovieViewModel(IRegionManager regionManager, 
        ICopieFilmService copieFilmService, 
        IClientRepository clientRepository,
        IFilmRepository filmRepository,
        IHttpClientFactory httpClientFactory)
    {
        _regionManager = regionManager;
        _copieFilmService = copieFilmService;
        _clientRepository = clientRepository;
        _filmRepository = filmRepository;
        _httpClientFactory = httpClientFactory;
        
        ActorCommand = ReactiveCommand.Create(ActorCommandExecute);
        DirectorCommand = ReactiveCommand.Create(DirectorCommandExecute);
        TrailerCommand = ReactiveCommand.Create(NavigateToTrailerView);
        RentCommand = ReactiveCommand.CreateFromTask(RentCommandExecute);
        ReturnCommand = ReactiveCommand.CreateFromTask(ReturnCommandExecute);
        RecommendationCommand = ReactiveCommand.Create(RecommendationCommandExecute);
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);

        if (navigationContext.Parameters[MovieGridViewModel.MOVIE_PARAMETER] is Film movie)
        {
            _movie = movie;
            Rating = _filmRepository.GetMovieRating(_movie.FilmId) ?? 0;
        }
        
        if (navigationContext.Parameters[MovieGridViewModel.POSTER_PARAMETER] is Bitmap poster)
        {
            MoviePoster = poster;
        }
        else if (navigationContext.Parameters[IS_RECOMMENDATION_PARAM] is true)
        {
            _ = LoadMoviePosterAsync();
        }
        
        InitView();
        SetRecommendationsAsync().Await();
    }

    private void InitView()
    {
        if (_movie is null)
        {
            return;
        }

        Description = _movie.Resume;
        Title = _movie.Titre;
        ReleaseYear = _movie.AnneeSortie is null ? "Unknown" : $"{_movie.AnneeSortie}";
        Countries.Clear();
        Countries.AddRange(_movie.Pays.Select(x => x.Nom ?? ""));
        Language = _movie.LangueOriginale;
        MovieLength = _movie.DureeMinute is null ? "Unknown" : $"{_movie.DureeMinute}";
        Genres.Clear();
        Genres.AddRange(_movie.Genres);
        DirectorName = _movie.Realisateur.Nom;
        Screenwriters.Clear();
        Screenwriters.AddRange(_movie.Scenaristes.Select(x => x.Nom));
        Actors.Clear();
        Actors.AddRange(_movie.Acteurs.Select(x => x.Nom ?? ""));
        Trailers.Clear();
        Trailers.AddRange(_movie.BandesAnnonces.Select(x => x.Url ?? ""));
    }

    private async Task SetRecommendationsAsync()
    {
        if (_movie?.FilmId is null)
        {
            return;
        }

        var client = await _clientRepository.GetAuthenticatedClientAsync();
        var recommendations = _filmRepository.GetRecommendations(_movie.FilmId, client.ClientId);
        _movieRecommendations.Clear();
        _movieRecommendations.AddRange(recommendations ?? []);
        
        Recommendations.Clear();
        Recommendations.AddRange(_movieRecommendations.Where(x => x.Titre is not null).Select(x => x.Titre ?? ""));
    }

    private async Task RentCommandExecute()
    {
        // Logique pour rent le movie avec le LocationService
        var client = await _clientRepository.GetAuthenticatedClientAsync();
        var filmId = _movie.FilmId;
        var availableCopies = (await _copieFilmService.GetAvailableCopiesAsync(filmId)).ToList();

        if (!await _clientRepository.CanRentMoreFilmsAsync(client.ClientId)) {
            await ShowMessage("Maximum location reached");
        }
        else if (!availableCopies.Any()){
            await ShowMessage("All the copies for this movie are currently rented");
        }
        else
        {
            await _copieFilmService.RentMovieAsync(filmId, availableCopies, client.ClientId);
            _rented = true;
            await ShowMessage("The movie was rented succesfully");
        }
    }
    private async Task ReturnCommandExecute()
    {
        // Logique pour rent le movie avec le LocationService
        Client client = await _clientRepository.GetAuthenticatedClientAsync();
        int filmId = _movie.FilmId;
        IEnumerable<CopieFilm> availableCopies = await _copieFilmService.GetAvailableCopiesAsync(filmId);
        await _copieFilmService.ReturnMovie(filmId, client.ClientId);
        await ShowMessage("The movie was returned succesfully");
    }

    private async Task ShowMessage(string message)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Webflix", message);
        await box.ShowWindowAsync();
    }
    
    private void ActorCommandExecute()
    {
        var parameters = new NavigationParameters
        {
            { PERSON_PARAMETER, _movie?.Acteurs.FirstOrDefault(x => x.Nom == SelectedActor) }
        };
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(PersonView), parameters);
    }
    
    private void DirectorCommandExecute()
    {
        var parameters = new NavigationParameters
        {
            { PERSON_PARAMETER, _movie?.Realisateur }
        };
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(PersonView), parameters);
    }

    private void RecommendationCommandExecute()
    {
        var movie = _movieRecommendations.FirstOrDefault(x => x.Titre == SelectedRecommendation);
        
        var parameters = new NavigationParameters
        {
            { MovieGridViewModel.MOVIE_PARAMETER, movie },
            { IS_RECOMMENDATION_PARAM, true }
        };
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieView), parameters);
    }
    
    public async Task LoadMoviePosterAsync()
    {
        if (string.IsNullOrEmpty(_movie?.AfficheUrl))
        {
            return;
        }

        try
        {
            await using var imageStream = await LoadCoverBitmapAsync();
            MoviePoster = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
        catch (Exception _)
        {
            // ignored
        }
    }
    
    private async Task<Stream> LoadCoverBitmapAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var data = await client.GetByteArrayAsync(UrlHelper.EnsureHttps(_movie!.AfficheUrl!));
        return new MemoryStream(data);
    }

    private void NavigateToTrailerView()
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = UrlHelper.EnsureHttps(SelectedTrailer),
                UseShellExecute = true
            });
        }
        catch (Exception e)
        {
            //something went wrong
        }
    }

    public override bool IsNavigationTarget(NavigationContext navigationContext) => false;
}