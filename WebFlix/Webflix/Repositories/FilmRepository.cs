using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NP.Utilities;
using Webflix.Models;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        // private readonly MyDbContext context;
        private IDbContextFactory<MyDbContext> _contextFactory;
        public FilmRepository(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<Film> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
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
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .ToListAsync();
        }
        
        public async Task AddAsync(Film film)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            await context.Films.AddAsync(film);
            await context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Film film)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            context.Entry(film).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var film = await context.Films.FindAsync(id);
            if (film != null)
            {
                context.Films.Remove(film);
            }

            await context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes de recherche spécifiques
        
        public async Task<IEnumerable<Film>> SearchByTitleAsync(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return await GetAllAsync();
            }
                
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.Titre.Contains(title))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByActorAsync(int actorId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.ActeursFilms)
                .Where(f => f.ActeursFilms.Any(af => af.ActeurId == actorId))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByDirectorAsync(int directorId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.RealisateurId == directorId)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByGenreAsync(string genre)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.GenresFilms)
                .Where(f => f.GenresFilms.Any(gf => gf.Genre == genre))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByYearAsync(int year)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.AnneeSortie == year)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByCountryAsync(int countryId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.PaysFilms)
                .Where(f => f.PaysFilms.Any(pf => pf.PaysId == countryId))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchByLanguageAsync(string language)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Include(f => f.Realisateur)
                .Where(f => f.LangueOriginale == language)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> SearchAdvancedAsync(
            string? title, int? minYear, int? maxYear,string? genre, 
            string? actor, string? director, 
            string? language, string? country)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var query = context.Films
                .Include(f => f.Realisateur)
                .Include(f => f.ActeursFilms).ThenInclude(af => af.Acteur)
                .Include(f => f.GenresFilms)
                .Include(f => f.PaysFilms).ThenInclude(pf => pf.Pays)
                .Include(f => f.ScenaristesFilms).ThenInclude(sf => sf.Scenariste)
                .Include(f => f.BandesAnnonces)
                .AsQueryable()
                .AsSplitQuery();

            // Apply filters only if values are provided to avoid unnecessary query modifications
            if (!string.IsNullOrEmpty(title))
                query = query.Where(f => f.Titre != null && f.Titre.ToLower().Contains(title.ToLower()));

            if (minYear.HasValue)
                query = query.Where(f => f.AnneeSortie >= minYear.Value);

            if (maxYear.HasValue)
                query = query.Where(f => f.AnneeSortie <= maxYear.Value);

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(f => f.GenresFilms.Any(gf => gf.Genre != null && gf.Genre == genre));

            if (!string.IsNullOrEmpty(actor))
                query = query.Where(f => f.ActeursFilms.Any(af => af.Acteur != null && af.Acteur.Nom != null && af.Acteur.Nom.ToLower().Contains(actor.ToLower())));

            if (!string.IsNullOrEmpty(director))
                query = query.Where(f => f.Realisateur != null && f.Realisateur.Nom != null && f.Realisateur.Nom.ToLower().Contains(director.ToLower()));

            if (!string.IsNullOrEmpty(language))
                query = query.Where(f => f.LangueOriginale != null && f.LangueOriginale == language);

            if (!string.IsNullOrEmpty(country))
                query = query.Where(f => f.PaysFilms.Any(pf => pf.Pays != null && pf.Pays.Nom != null && pf.Pays.Nom == country));

            return await query.ToListAsync();

        }
    }
}