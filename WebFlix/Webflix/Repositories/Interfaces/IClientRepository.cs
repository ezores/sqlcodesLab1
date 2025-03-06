using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;

namespace Webflix.Repositories.Interfaces
{
    public interface IClientRepository
    {
        // Méthodes CRUD de base
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByEmailAsync(string email);
        Task<IEnumerable<Client>> GetAllAsync();
        Task AddAsync(Client client);
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        
        // Méthodes spécifiques pour les cas d'utilisation
        Task<bool> AuthenticateAsync(string email, string password);
        Task<IEnumerable<Emprunt>> GetActiveRentalsAsync(int clientId);
        Task<bool> CanRentMoreFilmsAsync(int clientId);
        Task<int> GetMaxRentalsAllowedAsync(int clientId);
        Task<int> GetCurrentRentalsCountAsync(int clientId);
    }
}