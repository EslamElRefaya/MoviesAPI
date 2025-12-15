using MoviesAPI.Dtos.ApplicationUsers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MoviesAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;

        public AccountService(
            IAccountRepository accountRepository,
            IConfiguration configuration)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, IEnumerable<string> Errors)> RegisterAsync(AddNewUserDto dto)
        {
            var user = new ApplicationUsers
            {
                UserName = dto.userName,
                Email = dto.userEmail
            };

            var result = await _accountRepository.CreateUserAsync(user, dto.password);

            if (result.Succeeded)
                return (true, Enumerable.Empty<string>());

            return (false, result.Errors.Select(e => e.Description));
        }

        public async Task<object?> LoginAsync(LoginDto dto)
        {
            var user = await _accountRepository.FindByUserNameAsync(dto.UserName);
            if (user == null) return null;

            var checkPassword = await _accountRepository.CheckPasswordAsync(user, dto.Password);
            if (!checkPassword) return null;

            // ===== JWT (UNCHANGED RULES) =====
            var claims = new List<Claim>
        {
            new Claim("name", "Developer"),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var roles = await _accountRepository.GetUserRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"])
            );

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(1),
                claims: claims,
                signingCredentials: signIn
            );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }
    }

}
