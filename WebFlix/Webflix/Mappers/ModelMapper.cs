using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Webflix.Models.Entities;
using Webflix.ViewModels;

namespace Webflix.Mappers;

public static class ModelMapper
{
    public static IEnumerable<MovieTileViewModel> ToMovieTileViewModel(IEnumerable<Film> films, IHttpClientFactory clientFactory) 
        => films.Select(x => new MovieTileViewModel(clientFactory) { Movie = x, MovieTitle = x.Titre });
}