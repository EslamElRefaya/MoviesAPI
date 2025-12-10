namespace MoviesAPI.Services
{
    public interface IGenresService
    {
        Task<IEnumerable<Genre>> GetAll();
        Task<Genre> GetById(int id);
        Task<Genre> AddGenre(Genre genre);
        Task<Genre>UpdateGenre(Genre genre);
        Task<Genre>DeleteGenre(Genre genre);
        Task<bool> IsVaildGenre(int id);
    }
}
