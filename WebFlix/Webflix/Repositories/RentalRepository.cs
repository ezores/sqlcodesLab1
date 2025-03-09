using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webflix.Models;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class RentalRepository : IRentalRepository
{
    private readonly MyDbContext _context;
    
    public RentalRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Emprunt emprunt)
    {
        _context.Emprunts.Add(emprunt);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Emprunt emprunt)
    {
        _context.Emprunts.Remove(emprunt);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Emprunt?> GetActiveRentalAsync(int clientId, int filmId)
    {
        return await _context.Emprunts
            .Where(e => e.ClientId == (from c in _context.Clients where c.ClientId == clientId select c.ClientId).FirstOrDefault()
                        && e.Copie.FilmId == filmId)
            .FirstOrDefaultAsync();
    }
}