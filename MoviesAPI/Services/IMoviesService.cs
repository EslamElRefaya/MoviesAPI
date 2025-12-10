using MoviesAPI.Models;

namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovieById(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(Movie movie);
    }
}
