using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> LoginAsync(LoginModel login);
        Task<AuthenticationResult> RegisterAsync(RegisterModel register);
        Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordModel resetPassword);
    }
}
