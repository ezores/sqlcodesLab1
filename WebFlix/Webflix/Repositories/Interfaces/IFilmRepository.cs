using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;

namespace Webflix.Repositories.Interfaces
{
    public interface IFilmRepository
    {
        // Méthodes de base CRUD
        Task<Film> GetByIdAsync(int id);
        Task<IEnumerable<Film>> GetAllAsync();
        Task AddAsync(Film film);
        Task UpdateAsync(Film film);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        
        // Méthodes spécifiques pour les cas d'utilisation
        Task<IEnumerable<Film>> SearchByTitleAsync(string title);
        Task<IEnumerable<Film>> SearchByActorAsync(int actorId);
        Task<IEnumerable<Film>> SearchByDirectorAsync(int directorId);
        Task<IEnumerable<Film>> SearchByGenreAsync(string genre);
        Task<IEnumerable<Film>> SearchByYearAsync(int year);
        Task<IEnumerable<Film>> SearchByCountryAsync(int countryId);
        Task<IEnumerable<Film>> SearchByLanguageAsync(string language);
        Task<IEnumerable<Film>> SearchAdvancedAsync(string title, int? minYear, int? maxYear,string genre, 
            string actor, string director, 
            string language, int? countryId);
    }
}