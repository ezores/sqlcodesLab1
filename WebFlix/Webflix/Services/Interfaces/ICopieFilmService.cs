using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models.Entities;

namespace Webflix.Services.Interfaces;

public interface ICopieFilmService
{
    Task<bool> CheckIfCanRentMore(int clientId);

    Task<IEnumerable<CopieFilm>> GetAvailableCopiesAsync(int filmId);

    Task RentMovieAsync(int filmId, IEnumerable<CopieFilm> availableCopies, int clientId);

    Task ReturnMovie(int filmId, int clientId);
}