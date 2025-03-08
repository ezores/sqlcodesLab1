using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using Prism.Regions;
using ReactiveUI;
using Webflix.Models;
using Webflix.Repositories.Interfaces;
using Webflix.Resources;
using Webflix.Views;
using Webflix.Services;

namespace Webflix.ViewModels;

public class SearchViewModel : ViewModelBase
{
    private readonly IRegionManager _regionManager;
    private readonly FilmService _filmService;
    private readonly CopieFilmService _copieService;
    
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
    
    public SearchViewModel(IRegionManager regionManager, CopieFilmService copieService)
    {
        _regionManager = regionManager;
        // _filmService = filmService;
        _copieService = copieService;
        SearchCommand = ReactiveCommand.Create(SearchCommandExecute);
    }

    private async void SearchCommandExecute()
    {
        // Ajouter loading (ex: IsLoading = true;)
        Console.WriteLine("Loading...");
        
        try
        {
            // var films = await _filmService.AdvancedSearchAsync("Witness for the Prosecution", 1950, 1960, "Crime", "",
            //     "",
            //     "English", 1);
            await _copieService.RentMovie(93105,171684);
           //await _copieService.ReturnMovie(120794,171684)
            
            
            _regionManager.RequestNavigate(Regions.MainRegion, nameof(MovieGridView), result =>
            {
                if (result.Result is true)
                {
                    // //remove loading
                    // Console.WriteLine("Films found:");
                    // foreach (var film in films)
                    // {
                    //     Console.WriteLine($"Title: {film.Titre}");
                    // }
                    
                }
            });
        }
        catch (Exception ex)
        {
            // Gérer les erreurs (ex: afficher un message d'erreur)
            Console.WriteLine("An error occured");
            Console.WriteLine(ex.Message);
            
        }
        finally
        {
            // Remove loading (ex: IsLoading = false;)
            Console.WriteLine("Loading removed");
        }
    }
}