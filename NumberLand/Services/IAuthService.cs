using NumberLand.Models;

namespace NumberLand.Services
{
    public interface IAuthService
    {
        Task<string> Register(UserModel user);
        Task<bool> Login(UserModel user);
        string GenerateTokenString(UserModel user);
    }
}