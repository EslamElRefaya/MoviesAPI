namespace MoviesAPI.Dtos.Genre
{
    public class CreateAndEditGenreDto
    {
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
