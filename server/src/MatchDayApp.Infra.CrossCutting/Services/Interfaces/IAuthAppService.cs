using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IAuthAppService
    {
        Task<AuthenticationResult> LoginAsync(LoginModel login);
        Task<AuthenticationResult> RegisterAsync(RegisterModel register);
        Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordModel resetPassword);
    }
}
