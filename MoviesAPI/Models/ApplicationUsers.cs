namespace MoviesAPI.Models
{
    public class ApplicationUsers:IdentityUser
    {
        [MaxLength(200)]
        public string FristName { get; set; }=string.Empty;
        [MaxLength(200)]
        public string LastName { get; set; }=string.Empty;
    }
}
