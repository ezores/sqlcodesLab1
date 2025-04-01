using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class MovieRatingRepository(IDbContextFactory<MyDbContext> contextFactory) : IMovieRatingRepository
{
    public double? GetFilmRating(int filmId)
    {
        using var context = contextFactory.CreateDbContext();

        return context.MovieRatings.FirstOrDefault(x => x.FilmId == filmId)?.Score;
    }
}