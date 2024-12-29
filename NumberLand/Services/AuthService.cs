using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NumberLand.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NumberLand.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> Register(UserModel user)
        {
            var identityUser = new IdentityUser
            {
                PhoneNumber = user.phoneNumber,
                UserName = user.phoneNumber
            };
            var result = await _userManager.CreateAsync(identityUser, user.password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return error.Description;
                }
            }
            return "Register Successful:)";
        }
        public async Task<bool> Login(UserModel user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.phoneNumber);
            if (identityUser == null)
            {
                return false;
            }
            var result = await _userManager.CheckPasswordAsync(identityUser, user.password);
            if (!result)
            {
                return false;
            }
            return true;
        }

        public string GenerateTokenString(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var signinCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            IEnumerable<System.Security.Claims.Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.phoneNumber),
                new Claim(ClaimTypes.Role, "Admin")
            };
            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: _config.GetSection("JWT:Issuer").Value,
                audience: _config.GetSection("JWT:Audience").Value,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: signinCred
            );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
        }
    }
}
