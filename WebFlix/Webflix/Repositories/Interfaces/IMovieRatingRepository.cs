namespace Webflix.Repositories.Interfaces;

public interface IMovieRatingRepository
{
    double? GetFilmRating(int filmId);
}