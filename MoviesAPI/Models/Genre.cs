namespace MoviesAPI.Models
{
    public class Genre: BaseEntity
    {
        public ICollection<Movie> Movie { get; set; } = new List<Movie>();
    }
}
