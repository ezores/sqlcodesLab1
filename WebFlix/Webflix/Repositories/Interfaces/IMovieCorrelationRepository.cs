using System.Collections.Generic;

namespace Webflix.Repositories.Interfaces;

public interface IMovieCorrelationRepository
{
    List<int> GetRecommendations(int filmId);
}