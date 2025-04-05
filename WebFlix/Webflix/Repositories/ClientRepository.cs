using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Models.Entities;
using Webflix.Repositories.Interfaces;
using AuthenticationResponse = Webflix.Models.AuthenticationResponse;

namespace Webflix.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private IDbContextFactory<MyDbContext> _contextFactory;
        private  int _currentClientId;
        
        public ClientRepository(IDbContextFactory<MyDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task<Client> GetByIdAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .Include(c => c.Adresse)
                .Include(c => c.CarteCredit)
                .Include(c => c.Abonnement)
                .Include(c => c.Emprunts)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null)
            {
                throw new KeyNotFoundException($"Client with ID {id} not found.");
            }

            return client;
        }
        
        public async Task<Client> GetByEmailAsync(string email)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();

            var client = await context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.Courriel == email);
            return client;
        }
        
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var clients = await context.Clients
                .Include(c => c.Abonnement)
                .ToListAsync();

            return clients;
        }
        
        public async Task AddAsync(Client client)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
        }
        
        public async Task UpdateAsync(Client client)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            context.Entry(client).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(int id)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients.FindAsync(id);
            if (client != null)
            {
                context.Clients.Remove(client);
            }

            await context.SaveChangesAsync();
        }
        
        // Implémentation des méthodes spécifiques
        
        public async Task<AuthenticationResponse> AuthenticateAsync(string email, string password)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .Where(c => c.Courriel == email && c.MotDePasse == password)
                .Select(c => new
                {
                    c.ClientId,
                    c.Courriel,
                    c.MotDePasse,
                    CodeAbonnementTrimmed = c.CodeAbonnement != null ? c.CodeAbonnement.Trim() : null
                })
                .FirstOrDefaultAsync();

            if (client != null)
            {
                _currentClientId = client.ClientId;
            }

            return new AuthenticationResponse(client is not null, client?.ClientId);
        }

        public async Task<Client> GetAuthenticatedClientAsync()
        {
            var client = await GetByIdAsync(_currentClientId);
            return client;
        }
        
        public async Task<IEnumerable<Emprunt>> GetActiveRentalsAsync(int clientId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null)
                return new List<Emprunt>();

            var emprunts = await context.Emprunts
                .Include(e => e.Copie)
                .ThenInclude(c => c.Film)
                .Where(e => e.ClientId == client.ClientId)
                .ToListAsync();
            
            return emprunts;
        }
        
        public async Task<bool> CanRentMoreFilmsAsync(int clientId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null || client.Abonnement == null)
                return false;
                
            int currentRentals = await GetCurrentRentalsCountAsync(clientId);
            return currentRentals < client.Abonnement.EmpruntMax.GetValueOrDefault();
        }
        
        public async Task<int> GetMaxRentalsAllowedAsync(int clientId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .Include(c => c.Abonnement)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null || client.Abonnement == null)
                return 0;
                
            return client.Abonnement.EmpruntMax.GetValueOrDefault();
        }
        
        public async Task<int> GetCurrentRentalsCountAsync(int clientId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            
            var client = await context.Clients
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
                
            if (client == null)
                return 0;

            var count = await context.Emprunts
                .CountAsync(e => e.ClientId == client.ClientId);
            
            return count;
        }
    }
}