using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class ClientMovieRepository(IDbContextFactory<MyDbContext> contextFactory) : IClientMovieRepository
{
    public async Task AddAsync(ClientMovie clientMovie)
    {
        await using var context = await contextFactory.CreateDbContextAsync();

        if (context.ClientMovies.ToList().Any(x => x.ClientId == clientMovie.ClientId && x.MovieId == clientMovie.MovieId))
        {
            return;
        }
        
        await context.ClientMovies.AddAsync(clientMovie);
        await context.SaveChangesAsync();
    }

    public IEnumerable<int> GetRentedMoviesByClientId(int clientId)
    {
        using var context = contextFactory.CreateDbContext();
        return context.ClientMovies.Where(x => x.ClientId == clientId).Select(x => x.MovieId).ToList();
    }
}