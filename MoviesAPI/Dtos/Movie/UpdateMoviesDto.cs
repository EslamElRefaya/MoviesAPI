namespace MoviesAPI.Dtos.Movie
{
    public class UpdateMoviesDto:BaseEntityMovieDto
    {
        public IFormFile? Poster { get; set; } = default!;

    }
}
