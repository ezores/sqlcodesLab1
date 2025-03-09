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
    public class EmployeRepository : IEmployeRepository
    {
        private readonly MyDbContext _context;
        
        public EmployeRepository(MyDbContext context)
        {
            _context = context;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<Employe> GetByMatriculeAsync(string matricule)
        {
            return await _context.Employes
                .Include(e => e.Adresse)
                .FirstOrDefaultAsync(e => e.Matricule == matricule);
        }
        
        public async Task<IEnumerable<Employe>> GetAllAsync()
        {
            return await _context.Employes.ToListAsync();
        }
        
        public async Task AddAsync(Employe employe)
        {
            await _context.Employes.AddAsync(employe);
        }
        
        public Task UpdateAsync(Employe employe)
        {
            _context.Entry(employe).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(string matricule)
        {
            var employe = await _context.Employes.FindAsync(matricule);
            if (employe != null)
            {
                _context.Employes.Remove(employe);
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var employe = await _context.Employes
                .Where(e => e.Courriel == email && e.MotDePasse == password)
                .Select(e => new
                {
                    e.Matricule,
                    e.Courriel,
                    e.MotDePasse
                })
                .FirstOrDefaultAsync();

            return employe != null; // Return true if an employee was found, false otherwise
        }
        
        public async Task<Employe> GetByEmailAsync(string email)
        {
            return await _context.Employes
                .FirstOrDefaultAsync(e => e.Courriel == email);
        }
    }
}