
namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesAPIRepository<Movie> _movieRepository;
    
        public MoviesService(IMoviesAPIRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAllItems_Repo();
            return movies;
        }

        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetItemById_Repo(id);
            return movie;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
           await _movieRepository.AddItem_Repo(movie);
            return movie;
        }

        public async Task<Movie> DeleteMovie(Movie movie)
        {
            await _movieRepository.DeleteItem_Repo(movie);
            return movie;
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
           await _movieRepository.UpdateItem_Repo(movie);
            return movie;
        }
    }
}
