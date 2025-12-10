namespace MoviesAPI.Dtos.Genre.GenreDTos
{
    public class genreDto
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
    }
}
