using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Regions;
using ReactiveUI;
using Webflix.Mappers;
using Webflix.Models;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class MovieGridViewModel : ViewModelBase
{
    public static readonly string MOVIE_PARAMETER = "movie-parameter";
    public static readonly string POSTER_PARAMETER = "poster-parameter";
    
    private readonly IRegionManager _regionManager;
    private readonly IHttpClientFactory _httpClientFactory;
    private MovieSearchResult? _movieSearchResult;
    private bool _isComingBack;
    
    private ObservableCollection<MovieTileViewModel> _movies = new();

    public ObservableCollection<MovieTileViewModel> Movies
    {
        get => _movies;
        set => this.RaiseAndSetIfChanged(ref _movies, value);
    }
    
    private MovieTileViewModel? _selectedMovie;

    public MovieTileViewModel? SelectedMovie
    {
        get => _selectedMovie;
        set => this.RaiseAndSetIfChanged(ref _selectedMovie, value);
    }

    public ReactiveCommand<Unit, Unit> TriggerItem { get; set; }
    
    public MovieGridViewModel(IRegionManager regionManager, IHttpClientFactory httpClientFactory)
    {
        _regionManager = regionManager;
        _httpClientFactory = httpClientFactory;
        
        TriggerItem = ReactiveCommand.Create(TriggerItemExecute);
    }

    public override async void OnNavigatedTo(NavigationContext navigationContext)
    {
        base.OnNavigatedTo(navigationContext);

        if (navigationContext.Parameters[SearchViewModel.FILMS_PARAMETER] is MovieSearchResult movieSearchResult)
        {
            _movieSearchResult = movieSearchResult;
        }

        if (!_isComingBack)
        {
            await SetItems();
        }
        else
        {
            _isComingBack = false;
        }
    }

    private void TriggerItemExecute()
    {
        _isComingBack = true;

        var parameters = new NavigationParameters
        {
            { MOVIE_PARAMETER, SelectedMovie?.Movie},
            { POSTER_PARAMETER, SelectedMovie?.MoviePoster}
        };
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieView), parameters);
    }

    private async Task SetItems()
    {
        if (_movieSearchResult is null)
        {
            return;
        }
        
        var items = ModelMapper.ToMovieTileViewModel(_movieSearchResult.Films, _httpClientFactory).ToList();
        
        _movies.Clear();
        _movies.AddRange(items);
        this.RaisePropertyChanged(nameof(Movies));
        
        var loadImagesTask = items.Select(x => x.LoadMoviePosterAsync()).ToArray();
        await Task.WhenAll(loadImagesTask);
    }
}