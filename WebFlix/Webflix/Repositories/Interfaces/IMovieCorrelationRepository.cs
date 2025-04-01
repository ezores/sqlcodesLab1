using System.Collections.Generic;

namespace Webflix.Repositories.Interfaces;

public interface IMovieCorrelationRepository
{
    IEnumerable<int> GetRecommendations(int filmId, IEnumerable<int> alreadyRentedMovieIds);
}