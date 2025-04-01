using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class MovieCorrelationRepository(IDbContextFactory<MyDbContext> contextFactory, IRentalRepository rentalRepository) : IMovieCorrelationRepository
{
    public IEnumerable<int> GetRecommendations(int filmId, IEnumerable<int> alreadyRentedMovieIds)
    {
        using var context = contextFactory.CreateDbContext();

        return context.MovieCorrelations
            .Where(x => x.FirstMovie == filmId)
            .OrderByDescending(x => x.Correlation)
            .Select(x => x.SecondMovie)
            .ToList()
            .Except(alreadyRentedMovieIds)
            .Take(3);
    }
}