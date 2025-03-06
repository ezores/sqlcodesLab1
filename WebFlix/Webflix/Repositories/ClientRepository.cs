using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyDbContext _context;
        
        public ClientRepository(MyDbContext context)
        {
            _context = context;
        }
        
        // Implémentation des méthodes CRUD de base
        
        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Adresse)
                .Include(c => c.CarteCredit)
                .Include(c => c.Abonnement)
                .Include(c => c.Emprunts)
                .FirstOrDefaultAsync(c => c.ClientId == id);
        }
        
        public async Task<Client> GetByEmailAsync(string email)
        {
            return await _context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.Courriel == email);
        }
        
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients
                .Include(c => c.Abonnement)
                .ToListAsync();
        }
        
        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
        }
        
        public Task UpdateAsync(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        
        public async Task DeleteAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var client = await _context.Clients
                .Where(c => c.Courriel == email && c.MotDePasse == password)
                .Select(c => new
                {
                    c.ClientId,
                    c.Courriel,
                    c.MotDePasse,
                    CodeAbonnementTrimmed = c.CodeAbonnement != null ? c.CodeAbonnement.Trim() : null
                })
                .FirstOrDefaultAsync();

            return client != null; // Return true if a client was found, false otherwise
        }
        
        public async Task<IEnumerable<Emprunt>> GetActiveRentalsAsync(int clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null)
                return new List<Emprunt>();
                
            return await _context.Emprunts
                .Include(e => e.Copie)
                    .ThenInclude(c => c.Film)
                .Where(e => e.NomUsager == client.Courriel)
                .ToListAsync();
        }
        
        public async Task<bool> CanRentMoreFilmsAsync(int clientId)
        {
            var client = await _context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null || client.Abonnement == null)
                return false;
                
            int currentRentals = await GetCurrentRentalsCountAsync(clientId);
            return currentRentals < client.Abonnement.EmpruntMax.GetValueOrDefault();
        }
        
        public async Task<int> GetMaxRentalsAllowedAsync(int clientId)
        {
            var client = await _context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null || client.Abonnement == null)
                return 0;
                
            return client.Abonnement.EmpruntMax.GetValueOrDefault();
        }
        
        public async Task<int> GetCurrentRentalsCountAsync(int clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null)
                return 0;
                
            return await _context.Emprunts
                .CountAsync(e => e.NomUsager == client.Courriel);
        }
    }
}