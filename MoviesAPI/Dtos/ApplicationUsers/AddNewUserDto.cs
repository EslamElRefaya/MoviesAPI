namespace MoviesAPI.Dtos.ApplicationUsers
{
    public class AddNewUserDto
    {
       
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string userEmail { get; set; } = string.Empty;
        public string? userPhone { get; set; } 

    }
}
