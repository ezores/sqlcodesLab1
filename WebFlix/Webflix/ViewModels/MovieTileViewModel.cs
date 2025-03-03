using System;
using System.Linq;
using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class MovieTileViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    public ReactiveCommand<Unit, Unit> OpenMovieDetailsCommand { get; set; }
    
    public MovieTileViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        OpenMovieDetailsCommand = ReactiveCommand.Create(OpenMovieDetailsCommandExecute);
    }

    private void OpenMovieDetailsCommandExecute()
    {
        // ajouter loading
        //effectuer la recherche
        Console.WriteLine(_regionManager.Regions.First());
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieDetailsView));
    }
}