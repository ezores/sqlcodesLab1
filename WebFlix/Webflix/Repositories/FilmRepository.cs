using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly MyDbContext _context;
        
        public FilmRepository(MyDbContext context)
        {
            _context = context;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<Film> GetByIdAsync(int id)
        {
            return await _context.Films
                .Include(f => f.BandesAnnonces)
                .Include(f => f.Realisateur)
                .Include(f => f.ActeursFilms)
                    .ThenInclude(af => af.Acteur)
                .Include(f => f.GenresFilms)
                .Include(f => f.PaysFilms)
                    .ThenInclude(pf => pf.Pays)
                .Include(f => f.ScenaristesFilms)
                    .ThenInclude(sf => sf.Scenariste)
                .FirstOrDefaultAsync(f => f.FilmId == id);
        }
        
        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .ToListAsync();
        }
        
        public async Task AddAsync(Film film)
        {
            await _context.Films.AddAsync(film);
        }
        
        public Task UpdateAsync(Film film)
        {
            _context.Entry(film).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film != null)
            {
                _context.Films.Remove(film);
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes de recherche spécifiques
        
        public async Task<IEnumerable<Film>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
                return await GetAllAsync();
                
            return await _context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.Titre.Contains(title))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByActorAsync(int actorId)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.ActeursFilms)
                .Where(f => f.ActeursFilms.Any(af => af.ActeurId == actorId))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByDirectorAsync(int directorId)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.RealisateurId == directorId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByGenreAsync(string genre)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.GenresFilms)
                .Where(f => f.GenresFilms.Any(gf => gf.Genre == genre))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByYearAsync(int year)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.AnneeSortie == year)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByCountryAsync(int countryId)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.PaysFilms)
                .Where(f => f.PaysFilms.Any(pf => pf.PaysId == countryId))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByLanguageAsync(string language)
        {
            return await _context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.LangueOriginale == language)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchAdvancedAsync(
            string title, int? minYear, int? maxYear,string genre, 
            string actor, string director, 
            string language, int? countryId)
        {
            var query = _context.Films
            .Include(f => f.Realisateur)
            .Include(f => f.ActeursFilms)
                .ThenInclude(f => f.Acteur)
            .Include(f => f.GenresFilms)
            .Include(f => f.PaysFilms)
                .ThenInclude(f => f.Pays)
            .AsQueryable();
                
            if (!string.IsNullOrEmpty(title))
                query = query.Where(f => f.Titre.Contains(title));
                
            if (minYear.HasValue && maxYear.HasValue)
                query = query.Where(f => f.AnneeSortie >= minYear.Value && f.AnneeSortie <= maxYear.Value);
                
            if (!string.IsNullOrEmpty(genre))
                query = query.Where(f => f.GenresFilms.Any(gf => gf.Genre == genre));
                
            if (!string.IsNullOrEmpty(actor))
                query = query.Where(f => f.ActeursFilms.Any(af => af.Acteur.Nom.Contains(actor)));
            
            if (!string.IsNullOrEmpty(director))
                query = query.Where(f => f.Realisateur.Nom.Contains(director));
                
            if (!string.IsNullOrEmpty(language))
                query = query.Where(f => f.LangueOriginale == language);
                
            if (countryId.HasValue)
                query = query.Where(f => f.PaysFilms.Any(pf => pf.PaysId == countryId.Value));
                
            return await query.ToListAsync();
        }
    }
}