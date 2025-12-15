namespace MoviesAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUsers user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<ApplicationUsers?> FindByUserNameAsync(string userName)
            => await _userManager.FindByNameAsync(userName);

        public async Task<bool> CheckPasswordAsync(ApplicationUsers user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUsers user)
            => await _userManager.GetRolesAsync(user);
    }

}
