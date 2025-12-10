using MoviesAPI.Models;

namespace MoviesAPI.Repositories
{
    public class MovieRepository : IMoviesAPIRepository<Movie>
    {
        private readonly ApplicationDbContext _context;
        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAllItems_Repo()
        {
            var movies = await _context.Movies
                         .Include(g => g.Genre)
                         .OrderBy(m=>m.Name)
                         .ToListAsync();
            return movies;
        }

        public async Task<Movie> GetItemById_Repo(int Id)
        {
            var movie = await _context.Movies.Include(g => g.Genre)
                .SingleOrDefaultAsync(m => m.Id == Id);
            return movie;
        }
        public async Task AddItem_Repo(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItem_Repo(Movie movie)
        {
             _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem_Repo(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }
    }
}
