using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories
{
    public class CopieFilmRepository : ICopieFilmRepository
    {
        private IDbContextFactory<MyDbContext> _contextFactory;
        
        public CopieFilmRepository(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task<CopieFilm> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.CopiesFilm
                .Include(c => c.Film)
                .Include(c => c.Emprunt)
                .FirstOrDefaultAsync(c => c.CopieId == id);
        }
        
        public async Task<IEnumerable<CopieFilm>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.CopiesFilm
                .Include(c => c.Film)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<CopieFilm>> GetByFilmIdAsync(string filmId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.CopiesFilm
                .Include(c => c.Film)
                .Where(c => Equals(c.FilmId, filmId))
                .ToListAsync();
        }
        
        public async Task AddAsync(CopieFilm copieFilm)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            await context.CopiesFilm.AddAsync(copieFilm);
        }
        
        public async Task UpdateAsync(CopieFilm copieFilm)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            context.Entry(copieFilm).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var copieFilm = await context.CopiesFilm.FindAsync(id);
            if (copieFilm != null)
            {
                context.CopiesFilm.Remove(copieFilm);
            }

            await context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<IEnumerable<CopieFilm>> GetAvailableCopiesAsync(int filmId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.CopiesFilm
                .Include(c => c.Film)
                .Where(c => Equals(c.FilmId, filmId) && c.Statut == StatutCopie.DISPONIBLE)
                .ToListAsync();
        }
        
        public async Task<bool> IsAvailableAsync(int copieId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var copie = await context.CopiesFilm.FindAsync(copieId);
            return copie != null && copie.Statut == StatutCopie.DISPONIBLE;
        }
        
        public async Task<bool> UpdateStatusAsync(int copieId, StatutCopie statut)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var copie = await context.CopiesFilm.FindAsync(copieId);
            if (copie == null)
                return false;
                
            copie.Statut = statut;
            await context.SaveChangesAsync();
            return true;
        }

        public IEnumerable<int> GetMovieIdsFromCopyIds(IEnumerable<int> copyIds)
        {
            using var context = _contextFactory.CreateDbContext();

            return context.CopiesFilm.Where(x => copyIds.Contains(x.CopieId)).Select(x => x.FilmId).ToList();
        }
    }
}