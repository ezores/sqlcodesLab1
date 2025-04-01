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
    public class PersonneRepository : IPersonneRepository
    {
        private readonly IDbContextFactory<MyDbContext> _contextFactory;
        
        public PersonneRepository(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task<Personne> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Personnes
                .Include(p => p.FilmsCommeActeur)
                    .ThenInclude(af => af.Film)
                .Include(p => p.FilmsRealises)
                .FirstOrDefaultAsync(p => p.PersonneId == id);
        }
        
        public async Task<IEnumerable<Personne>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Personnes.ToListAsync();
        }
        
        public async Task AddAsync(Personne personne)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            await context.Personnes.AddAsync(personne);

            await context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Personne personne)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            context.Entry(personne).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var personne = await context.Personnes.FindAsync(id);
            if (personne != null)
            {
                context.Personnes.Remove(personne);
            }

            await context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<IEnumerable<Personne>> SearchByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return await GetAllAsync();
            }
                
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Personnes
                .Where(p => p.Nom.Contains(name))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Personne>> GetActorsByFilmIdAsync(int filmId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.ActeursFilms
                .Where(af => af.FilmId == filmId)
                .Select(af => af.Acteur)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> GetFilmsByActorIdAsync(int actorId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.ActeursFilms
                .Where(af => af.ActeurId == actorId)
                .Select(af => af.Film)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Film>> GetFilmsByDirectorIdAsync(int directorId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            return await context.Films
                .Where(f => f.RealisateurId == directorId)
                .ToListAsync();
        }
    }
}