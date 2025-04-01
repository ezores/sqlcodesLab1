using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webflix.Models;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly IDbContextFactory<MyDbContext> _contextFactory;
    public RentalRepository(IDbContextFactory<MyDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }
    
    public async Task AddAsync(Emprunt emprunt)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        context.Emprunts.Add(emprunt);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Emprunt emprunt)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        context.Emprunts.Remove(emprunt);
        await context.SaveChangesAsync();
    }
    
    public async Task<Emprunt?> GetActiveRentalAsync(int clientId, int filmId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        
        return await context.Emprunts
            .Where(e => e.ClientId == (from c in context.Clients where c.ClientId == clientId select c.ClientId).FirstOrDefault()
                        && e.Copie.FilmId == filmId)
            .FirstOrDefaultAsync();
    }

    public IEnumerable<int> GetRentedCopyIdsByClient(int clientId)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Emprunts.Where(x => x.ClientId == clientId).Select(x => x.CopieId).ToList();
    }
}