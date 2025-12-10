namespace MoviesAPI.Dtos.Movie
{
    public class BaseEntityMovieDto
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        public double Rate { get; set; }
        public int Year { get; set; }
        [MaxLength(500)]
        public string StoreLine { get; set; } = string.Empty;
        public int GenreId { get; set; }
       
    }
}
