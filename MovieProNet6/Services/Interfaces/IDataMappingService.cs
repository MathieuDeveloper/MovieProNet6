using MovieProNet6.Models.Database;
using MovieProNet6.Models.TMDB;

namespace MovieProNet6.Services.Interfaces
{
    public interface IDataMappingService
    {
        Task<Movie> MapMovieDetailAsync(MovieDetail movie);
        ActorDetail MapActorDetail(ActorDetail actor);
    }
}
