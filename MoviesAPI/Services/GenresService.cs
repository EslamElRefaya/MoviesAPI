using MoviesAPI.Repositories;

namespace MoviesAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly GenreRepository _genreRepository;
        public GenresService(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            var genre = await _genreRepository.GetAllItems_Repo();
            return genre;
        }

        public async Task<Genre> GetById(int id)
        {
            var genres = await _genreRepository.GetItemById_Repo(id);
            return genres;
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            await _genreRepository.AddItem_Repo(genre);
            return genre;
        }

        public async Task<Genre> DeleteGenre(Genre genre)
        {
            await _genreRepository.DeleteItem_Repo(genre);
            return genre;
        }
        public async Task<Genre> UpdateGenre(Genre genre)
        {
            await _genreRepository.UpdateItem_Repo(genre);
            return genre;
        }

        public async Task<bool> IsVaildGenre(int id)
        {
            return await _genreRepository.IsVaildGenre_Repo(id);
        }
    }
}
