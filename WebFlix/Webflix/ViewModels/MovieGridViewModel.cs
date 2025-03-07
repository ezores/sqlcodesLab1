using System.Collections.ObjectModel;
using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class MovieGridViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    private ObservableCollection<MovieTileViewModel> _searchResults = new();

    public ObservableCollection<MovieTileViewModel> SearchResults
    {
        get => _searchResults;
        set => this.RaiseAndSetIfChanged(ref _searchResults, value);
    }
    
    private MovieTileViewModel? _selectedAlbum;

    public MovieTileViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }

    public ReactiveCommand<Unit, Unit> TriggerItem { get; set; }
    
    public MovieGridViewModel(IRegionManager regionManager)
    {
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());
        //SearchResults.Add(new MovieTileViewModel());

        _regionManager = regionManager;
        
        TriggerItem = ReactiveCommand.Create(TriggerItemExecute);
    }

    private void TriggerItemExecute()
    {
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieView));
    }
}