using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;

namespace Webflix.Repositories.Interfaces
{
    public interface IPersonneRepository
    {
        // Méthodes CRUD de base
        Task<Personne> GetByIdAsync(int id);
        Task<IEnumerable<Personne>> GetAllAsync();
        Task AddAsync(Personne personne);
        Task UpdateAsync(Personne personne);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        
        // Méthodes spécifiques pour les cas d'utilisation
        Task<IEnumerable<Personne>> SearchByNameAsync(string name);
        Task<IEnumerable<Personne>> GetActorsByFilmIdAsync(int filmId);
        Task<IEnumerable<Film>> GetFilmsByActorIdAsync(int actorId);
        Task<IEnumerable<Film>> GetFilmsByDirectorIdAsync(int directorId);
    }
}