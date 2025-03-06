using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories
{
    public class PersonneRepository : IPersonneRepository
    {
        private readonly MyDbContext _context;
        
        public PersonneRepository(MyDbContext context)
        {
            _context = context;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<Personne> GetByIdAsync(int id)
        {
            return await _context.Personnes
                .Include(p => p.FilmsCommeActeur)
                    .ThenInclude(af => af.Film)
                .Include(p => p.FilmsRealises)
                .FirstOrDefaultAsync(p => p.PersonneId == id);
        }
        
        public async Task<IEnumerable<Personne>> GetAllAsync()
        {
            return await _context.Personnes.ToListAsync();
        }
        
        public async Task AddAsync(Personne personne)
        {
            await _context.Personnes.AddAsync(personne);
        }
        
        public Task UpdateAsync(Personne personne)
        {
            _context.Entry(personne).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne != null)
            {
                _context.Personnes.Remove(personne);
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<IEnumerable<Personne>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return await GetAllAsync();
                
            return await _context.Personnes
                .Where(p => p.Nom.Contains(name))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Personne>> GetActorsByFilmIdAsync(int filmId)
        {
            return await _context.ActeursFilms
                .Where(af => af.FilmId == filmId)
                .Select(af => af.Acteur)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> GetFilmsByActorIdAsync(int actorId)
        {
            return await _context.ActeursFilms
                .Where(af => af.ActeurId == actorId)
                .Select(af => af.Film)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> GetFilmsByDirectorIdAsync(int directorId)
        {
            return await _context.Films
                .Where(f => f.RealisateurId == directorId)
                .ToListAsync();
        }
    }
}