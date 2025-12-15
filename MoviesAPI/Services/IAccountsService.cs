using MoviesAPI.Dtos.ApplicationUsers;

namespace MoviesAPI.Services
{
    public interface IAccountService
    {
        Task<(bool IsSuccess, IEnumerable<string> Errors)> RegisterAsync(AddNewUserDto dto);
        Task<object?> LoginAsync(LoginDto dto);
    }
}
