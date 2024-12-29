using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NumberLand.Models;
using NumberLand.Services;

namespace NumberLand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserModel user)
        {
            var result = await _authService.Register(user);
            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserModel user)
        {
            if (await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest("Login Failed");
        }
    }
}
