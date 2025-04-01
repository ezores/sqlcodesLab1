using System.Collections.Generic;
using System.Threading.Tasks;
using Webflix.Models.Entities;

namespace Webflix.Repositories.Interfaces;

public interface IClientMovieRepository
{
    Task AddAsync(ClientMovie clientMovie);
    IEnumerable<int> GetRentedMoviesByClientId(int clientId);
}