using System.Collections.Generic;
using System.Linq;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class InformationRepository : IInformationRepository
{
    private readonly MyDbContext _dbContext;
    
    public InformationRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<string> GetAllCountries() => _dbContext.Pays.Where(x => string.IsNullOrEmpty(x.Nom)).Select(x => x.Nom).Distinct();
    public IEnumerable<string> GetAllGenres() => _dbContext.GenresFilms.Where(x => string.IsNullOrEmpty(x.Genre)).Select(x => x.Genre).Distinct();
    public IEnumerable<string> GetAllLanguages() => _dbContext.Films.Where(x => string.IsNullOrEmpty(x.LangueOriginale)).Select(x => x.LangueOriginale).Distinct();
}