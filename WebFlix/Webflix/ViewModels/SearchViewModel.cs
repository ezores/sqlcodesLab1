using System.Reactive;
using Prism.Regions;
using ReactiveUI;
using Webflix.Resources;
using Webflix.Views;

namespace Webflix.ViewModels;

public class SearchViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    
    public string SearchString => "Search for a movie";
    public string TitleWatermark => "Title";
    public string ActorWatermark => "Actor";
    public string DirectorWatermark => "Director";
    public string GenrePlaceholder => "Genre";
    
    public string CountryPlaceholder => "Country";
    public string LanguagePlaceholder => "Language";
    public string ClearButtonString => "Clear";
    public string SearchButtonString => "Search";

    private string _title;

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    private string _actor;

    public string Actor
    {
        get => _actor;
        set => this.RaiseAndSetIfChanged(ref _actor, value);
    }

    private string _director;

    public string Director
    {
        get => _director;
        set => this.RaiseAndSetIfChanged(ref _director, value);
    }

    public ReactiveCommand<Unit, Unit> SearchCommand { get; set; }
    
    public SearchViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        
        SearchCommand = ReactiveCommand.Create(SearchCommandExecute);
    }

    private void SearchCommandExecute()
    {
        // ajouter loading
        //effectuer la recherche
        
        _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieGridView), result =>
        {
            if (result.Result is true)
            {
                //remove loading
            }
        });
    }
}