using System.Collections.Generic;

namespace Webflix.Repositories.Interfaces;

public interface IInformationRepository
{
    IEnumerable<string> GetAllCountries();
    IEnumerable<string> GetAllGenres();
    IEnumerable<string> GetAllLanguages();
}