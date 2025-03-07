﻿using System.Collections.ObjectModel;
using Prism.Regions;
using ReactiveUI;
using Webflix.Repositories.Interfaces;

namespace Webflix.ViewModels;

public class MovieGridViewModel : ViewModelBase
{
    private readonly IFilmRepository _filmRepository;
    private MovieTileViewModel? _selectedAlbum;

    public ObservableCollection<MovieTileViewModel> SearchResults { get; } = new();

    public MovieTileViewModel? SelectedAlbum
    {
        get => _selectedAlbum;
        set => this.RaiseAndSetIfChanged(ref _selectedAlbum, value);
    }

    public MovieGridViewModel()
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
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
        SearchResults.Add(new MovieTileViewModel());
    }
}