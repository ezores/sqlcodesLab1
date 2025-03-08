using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;

namespace Webflix.Repositories.Interfaces;

public interface IRentalRepository
{
    Task AddAsync(Emprunt emprunt);
    Task DeleteAsync(Emprunt emprunt);
    Task<Emprunt?> GetActiveRentalAsync(int clientId, int filmId);
}