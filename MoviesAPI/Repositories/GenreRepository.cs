namespace MoviesAPI.Repositories
{
    public class GenreRepository : IMoviesAPIRepository<Genre>
    {
        private readonly ApplicationDbContext _context;
        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Genre>> GetAllItems_Repo()
        {
            var genres = await _context.Genres.ToListAsync();
            return genres;
        }

        public async Task<Genre> GetItemById_Repo(int Id)
        {
            var genres = await _context.Genres.SingleOrDefaultAsync(g => g.Id == Id);
            return genres;
        }

        //this method not apply all Repository it in 'GenreRepository' only
        public async Task<bool> IsVaildGenre_Repo(int Id)
        {
            var genres = await _context.Genres.AnyAsync(g => g.Id == Id);
            return genres;
        }
        public async Task AddItem_Repo(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItem_Repo(Genre genre)
        {
             _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItem_Repo(Genre genre)
        {
             _context.Update(genre);
            await _context.SaveChangesAsync();
        }
    }
}
