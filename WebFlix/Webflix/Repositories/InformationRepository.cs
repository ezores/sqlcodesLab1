using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Webflix.Repositories.Interfaces;

namespace Webflix.Repositories;

public class InformationRepository : IInformationRepository
{
    // private readonly MyDbContext _dbContext;
    private readonly IDbContextFactory<MyDbContext> _contextFactory;
    public InformationRepository(IDbContextFactory<MyDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public IEnumerable<string> GetAllCountries()
    {
        using var context = _contextFactory.CreateDbContext();

        var pays = context.Pays.Where(x => x.Nom != "" && x.Nom != null).Select(x => x.Nom).Distinct().ToList();
        
        return pays;
    }

    public IEnumerable<string> GetAllGenres()
    {
        using var context = _contextFactory.CreateDbContext();

        var genres = context.GenresFilms.Where(x => x.Genre != "" && x.Genre != null).Select(x => x.Genre).Distinct()
            .ToList();
        
        return genres;
    }

    public IEnumerable<string> GetAllLanguages()
    {
        using var context = _contextFactory.CreateDbContext();

        var languages = context.Films.Where(x => x.LangueOriginale != "" && x.LangueOriginale != null)
            .Select(x => x.LangueOriginale).Distinct().ToList();
        
        return languages;
    }
}