namespace MoviesAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUsers user, string password);
        Task<ApplicationUsers?> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(ApplicationUsers user, string password);
        Task<IList<string>> GetUserRolesAsync(ApplicationUsers user);
    }
}
