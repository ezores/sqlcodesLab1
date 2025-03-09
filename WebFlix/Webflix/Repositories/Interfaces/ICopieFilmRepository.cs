using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Models.Entities;

namespace Webflix.Repositories.Interfaces
{
    public interface ICopieFilmRepository
    {
        // Méthodes CRUD de base
        Task<CopieFilm> GetByIdAsync(int id);
        Task<IEnumerable<CopieFilm>> GetAllAsync();
        Task<IEnumerable<CopieFilm>> GetByFilmIdAsync(string filmId);
        Task AddAsync(CopieFilm copieFilm);
        Task UpdateAsync(CopieFilm copieFilm);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        
        // Méthodes spécifiques pour les cas d'utilisation
        Task<IEnumerable<CopieFilm>> GetAvailableCopiesAsync(int filmId);
        Task<bool> IsAvailableAsync(int copieId);
        Task<bool> UpdateStatusAsync(int copieId, StatutCopie statut);
    }
}