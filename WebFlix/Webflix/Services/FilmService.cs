using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;

namespace Webflix.Services;

public class FilmService
{
    private readonly IFilmRepository _filmRepository;
    
    public FilmService(IFilmRepository filmRepository)
    {
        _filmRepository = filmRepository;
    }
    
    public async Task<Film> GetFilmDetailsAsync(int id)
    {
        return await _filmRepository.GetByIdAsync(id);
    }
    
    public async Task<IEnumerable<Film>> SearchFilmsAsync(string searchTerm)
    {
        return await _filmRepository.SearchByTitleAsync(searchTerm);
    }
    
    public async Task<IEnumerable<Film>> AdvancedSearchAsync(
        string title, int? year, string genre, 
        int? actorId, int? directorId, 
        string language, int? countryId)
    {
        return await _filmRepository.SearchAdvancedAsync(
            title, year, genre, actorId, directorId, language, countryId);
    }
}