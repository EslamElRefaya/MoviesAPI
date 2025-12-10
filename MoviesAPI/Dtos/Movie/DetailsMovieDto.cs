namespace MoviesAPI.Dtos.Movie
{
    public class DetailsMovieDto: BaseEntityMovieDto
    {
        //this movieId 
        public int Id { get; set; }
        public string GenreName { get; set; }=string.Empty;
        public byte[] Poster { get; set; } = default!;
    }
}
