using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models;
using Webflix.Models.Entities;

namespace Webflix.Repositories.Interfaces
{
    public interface IEmployeRepository
    {
        // Méthodes CRUD de base
        Task<Employe> GetByMatriculeAsync(string matricule);
        Task<IEnumerable<Employe>> GetAllAsync();
        Task AddAsync(Employe employe);
        Task UpdateAsync(Employe employe);
        Task DeleteAsync(string matricule);
        Task SaveChangesAsync();
        
        // Méthodes spécifiques pour les cas d'utilisation
        Task<bool> AuthenticateAsync(string matricule, string password);
        Task<Employe> GetByEmailAsync(string email);
    }
}