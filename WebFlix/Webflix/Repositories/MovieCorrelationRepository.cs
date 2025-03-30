using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class MovieCorrelationRepository(IDbContextFactory<MyDbContext> contextFactory) : IMovieCorrelationRepository
{
    public List<int> GetRecommendations(int filmId)
    {
        using var context = contextFactory.CreateDbContext();

        return context.MovieCorrelations
            .Where(x => x.FirstMovie == filmId)
            .OrderByDescending(x => x.Correlation)
            .Take(3)
            .Select(x => x.SecondMovie)
            .ToList();
    }
}