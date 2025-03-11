using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using MsBox.Avalonia;
using NP.Utilities;
using Prism.Events;
using Prism.Regions;
using ReactiveUI;
using Webflix.Events;
using Webflix.Models;
using Webflix.Repositories.Interfaces;
using Webflix.Resources;
using Webflix.Views;
using Webflix.Services;

namespace Webflix.ViewModels;

public class SearchViewModel : ViewModelBase
{
    public static readonly string FILMS_PARAMETER = "film-parameter";
    
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IInformationRepository _informationRepository;
    private readonly FilmService _filmService;
    
    public string SearchString => "Search for a movie";
    public string TitleWatermark => "Title";
    public string ActorWatermark => "Actor";
    public string DirectorWatermark => "Director";
    public string GenrePlaceholder => "Genre";
    
    public string CountryPlaceholder => "Country";
    public string LanguagePlaceholder => "Language";
    public string ClearButtonString => "Clear";
    public string SearchButtonString => "Search";

    private bool _isSearchButtonEnabled = true;

    public bool IsSearchButtonEnabled
    {
        get => _isSearchButtonEnabled;
        set => this.RaiseAndSetIfChanged(ref _isSearchButtonEnabled, value);
    }

    private string _title = string.Empty;

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private string _actor = string.Empty;

    public string Actor
    {
        get => _actor;
        set => this.RaiseAndSetIfChanged(ref _actor, value);
    }

    private string _director = string.Empty;

    public string Director
    {
        get => _director;
        set => this.RaiseAndSetIfChanged(ref _director, value);
    }

    private DateTimeOffset? _minDate;

    public DateTimeOffset? MinDate
    {
        get => _minDate;
        set
        {
            _minDate = value;
            MinYear = MinDate?.Year;
        }
    }

    private DateTimeOffset? _maxDate;

    public DateTimeOffset? MaxDate
    {
        get => _maxDate;
        set
        {
            _maxDate = value;
            MaxYear = MaxDate?.Year;
        }
    }
    
    public DateTimeOffset MaxYearDatePicker => DateTimeOffset.Now;

    public string Country { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;

    private ObservableCollection<string> _countries = new();

    public ObservableCollection<string> Countries
    {
        get => _countries;
        set => this.RaiseAndSetIfChanged(ref _countries, value);
    }

    private ObservableCollection<string> _genres = new();

    public ObservableCollection<string> Genres
    {
        get => _genres;
        set => this.RaiseAndSetIfChanged(ref _genres, value);
    }

    private ObservableCollection<string> _languages = new();

    public ObservableCollection<string> Languages
    {
        get => _languages;
        set => this.RaiseAndSetIfChanged(ref _languages, value);
    }

    private int? MinYear { get; set; } = 0;
    private int? MaxYear { get; set; } = 3000;

    public ReactiveCommand<Unit, Unit> SearchCommand { get; set; }
    
    public SearchViewModel(IRegionManager regionManager, IInformationRepository informationRepository, FilmService filmService, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _informationRepository = informationRepository;
        _filmService = filmService;
        
        SearchCommand = ReactiveCommand.Create(SearchCommandExecute);
    }

    public override void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);

        Countries.Clear();
        Countries.AddRange(_informationRepository.GetAllCountries());
        
        Genres.Clear();
        Genres.AddRange(_informationRepository.GetAllGenres());
        
        Languages.Clear();
        Languages.AddRange(_informationRepository.GetAllLanguages());
    }

    private async void SearchCommandExecute()
    {
        _eventAggregator.GetEvent<ShowLoadingEvent>().Publish();
        IsSearchButtonEnabled = false;
        
        var films = (await _filmService.AdvancedSearchAsync(Title, MinYear, MaxYear, Genre, Actor, Director, Language, Country)).ToList();

        if (!films.Any())
        {
            IsSearchButtonEnabled = true;
            _eventAggregator.GetEvent<HideLoadingEvent>().Publish();

            await ShowMessageAsync("The search yielded no results.");

            return;
        }
        
        var parameters = new NavigationParameters
        {
            { FILMS_PARAMETER, new MovieSearchResult { Films = films } }
        };
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieGridView), result =>
        {
            _eventAggregator.GetEvent<HideLoadingEvent>().Publish();
            IsSearchButtonEnabled = true;
        }, parameters);
    }
    
    private async Task ShowMessageAsync(string message)
    {
        var box = MessageBoxManager.GetMessageBoxStandard("Webflix", message);
        await box.ShowWindowAsync();
    }
}