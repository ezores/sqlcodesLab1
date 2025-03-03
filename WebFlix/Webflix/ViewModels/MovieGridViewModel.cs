using System.Collections.ObjectModel;
using Prism.Regions;
using ReactiveUI;

namespace Webflix.ViewModels;

public class MovieGridViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    private MovieTileViewModel? _selectedAlbum;

    public ObservableCollection<MovieTileViewModel> SearchResults { get; } = new();

    public MovieTileViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }

    public MovieGridViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        for (int i = 0; i < 10; i++)
        {
            SearchResults.Add(new MovieTileViewModel(_regionManager));
        }
    }
}