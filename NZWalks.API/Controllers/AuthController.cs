using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public AuthController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        //POST : /api/Auth/Register

        #region Register

        [HttpPost]
        [Route("Register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };
            var IdentityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (IdentityResult.Succeeded)
            {

                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User was registered !Please Login.");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }
        #endregion

        //POST : /api/Auth/Login
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
                    return Ok();
                }
            }
            return BadRequest("Username or password incorrect");
        }

    }
}
