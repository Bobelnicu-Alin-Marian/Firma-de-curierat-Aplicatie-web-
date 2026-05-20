using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FirmaCurierat.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);
        Task<SignInResult> LoginAsync(string email, string password, bool rememberMe);
        Task LogoutAsync();
    }
}