namespace MoviesAPI.Dtos.Movie
{
    public class AddMoviesDto: BaseEntityMovieDto
    {
       public IFormFile Poster { get; set; } = default!;
      
    }
}
