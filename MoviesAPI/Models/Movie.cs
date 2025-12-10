namespace MoviesAPI.Models
{
    public class Movie: BaseEntity
    {
        public double Rate { get; set; }
        public int Year { get; set; }
        [MaxLength(500)]
        public string StoreLine { get; set; } = string.Empty;
        public byte[] Poster { get; set; } = default!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = default!;
    }
}
