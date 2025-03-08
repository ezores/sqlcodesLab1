using System.Collections.Generic;
using Webflix.Models.Entities;

namespace Webflix.Models;

public class MovieSearchResult
{
    public IEnumerable<Film> Films { get; set; }
}