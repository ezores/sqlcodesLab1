using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly MyDbContext _context;
        
        public CopieFilmRepository(MyDbContext context)
        {
            _context = context;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<CopieFilm> GetByIdAsync(int id)
        {
            return await _context.CopiesFilm
                .Include(c => c.Film)
                .Include(c => c.Emprunt)
                .FirstOrDefaultAsync(c => c.CopieId == id);
        }
        
        public async Task<IEnumerable<CopieFilm>> GetAllAsync()
        {
            return await _context.CopiesFilm
                .Include(c => c.Film)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<CopieFilm>> GetByFilmIdAsync(string filmId)
        {
            return await _context.CopiesFilm
                .Include(c => c.Film)
                .Where(c => Equals(c.FilmId, filmId))
                .ToListAsync();
        }
        
        public async Task AddAsync(CopieFilm copieFilm)
        {
            await _context.CopiesFilm.AddAsync(copieFilm);
        }
        
        public Task UpdateAsync(CopieFilm copieFilm)
        {
            _context.Entry(copieFilm).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(int id)
        {
            var copieFilm = await _context.CopiesFilm.FindAsync(id);
            if (copieFilm != null)
            {
                _context.CopiesFilm.Remove(copieFilm);
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<IEnumerable<CopieFilm>> GetAvailableCopiesAsync(int filmId)
        {
            return await _context.CopiesFilm
                .Include(c => c.Film)
                .Where(c => Equals(c.FilmId, filmId) && c.Statut == StatutCopie.DISPONIBLE)
                .ToListAsync();
        }
        
        public async Task<bool> IsAvailableAsync(int copieId)
        {
            var copie = await _context.CopiesFilm.FindAsync(copieId);
            return copie != null && copie.Statut == StatutCopie.DISPONIBLE;
        }
        
        public async Task<bool> UpdateStatusAsync(int copieId, StatutCopie statut)
        {
            var copie = await _context.CopiesFilm.FindAsync(copieId);
            if (copie == null)
                return false;
                
            copie.Statut = statut;
            await SaveChangesAsync();
            return true;
        }
    }
}