using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MoviesAPI.Dtos.ApplicationUsers;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsController(UserManager<ApplicationUsers> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // create Registration to users in system
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUsers(AddNewUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var applicationUsers = new ApplicationUsers
                {
                    UserName = userDto.userName,
                    Email = userDto.userEmail,
                };
                //this method to create user
                IdentityResult result = await _userManager.CreateAsync(applicationUsers, userDto.password);
                if (result.Succeeded)
                {
                    return Ok("Succeeded!");
                }
                else
                {
                    //add error in model State
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

            }
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (ModelState.IsValid)
            {
                // get on user by using method in Class UserManger
                var userName = await _userManager.FindByNameAsync(loginDto.UserName);

                if (userName != null)
                {
                    //this method check on userName and Password
                    bool checkPassword = await _userManager.CheckPasswordAsync(userName, loginDto.Password);

                    if (checkPassword)
                    {
                        //return Ok("Token-->> this genrate aoutmatically by JWT");
                        #region this part Genrate JWT Token aoutmatically
                        // add Payload == any Data 
                        var _claims = new List<Claim>();
                       _claims.Add(new Claim("name","Developer") );
                        _claims.Add(new Claim(ClaimTypes.Name,userName.UserName));
                        _claims.Add(new Claim(ClaimTypes.NameIdentifier,userName.Id));
                        _claims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString() ) );
                        var roles = await _userManager.GetRolesAsync(userName);
                        foreach (var role in roles)
                        {
                            _claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }

                        //signing Credentails
                        var key = new SymmetricSecurityKey(Encoding.UTF8
                                                       .GetBytes(_configuration["JWT:SecretKey"]));
                         
                        var _signCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                        // this part JWt
                        var tokens = new JwtSecurityToken(
                            claims: _claims,
                            issuer: _configuration["JWT:Issuer"],
                            audience: _configuration["JWT:Audience"],
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials:_signCredentials
                            );
                        //add annmous object
                        var _token = new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(tokens),
                            expiration=tokens.ValidTo,
                        };
                        return Ok(_token);
                        #endregion
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                    ModelState.AddModelError("", "Username or Password Is invaild");
            }

            return BadRequest(ModelState);
        }

    }
}
