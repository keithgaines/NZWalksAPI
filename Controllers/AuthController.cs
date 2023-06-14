using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public ITokenRepository TokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            TokenRepository = tokenRepository;
        }

        // POST: /api/Auth/Register
        // POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    foreach (var role in registerRequestDto.Roles)
                    {
                        if (role == "reader")
                        {
                            await userManager.AddToRoleAsync(identityUser, "Reader");
                        }
                        else if (role == "writer")
                        {
                            await userManager.AddToRoleAsync(identityUser, "Writer");
                        }
                        // Add additional role conditions if needed
                    }

                    return Ok("User was registered");
                }
            }

            return BadRequest("Something went wrong.");
        }


        // POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    // Get Roles for user
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        // Create token
                        var jwtToken = TokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        }; // Create an anonymous object with a JwtToken property

                        return Ok(jwtToken); // Return the response object
                    }
                }
            }
            return BadRequest("Username or password is incorrect.");
        }


    }
}
