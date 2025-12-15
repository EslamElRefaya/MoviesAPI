using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MoviesAPI.Dtos.ApplicationUsers;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUsersAsync(AddNewUserDto addNewUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.RegisterAsync(addNewUserDto);

            if (result.IsSuccess)
                return Ok("Succeeded!");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _accountService.LoginAsync(loginDto);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }
    }

}
