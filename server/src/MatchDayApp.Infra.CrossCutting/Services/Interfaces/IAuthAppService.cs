using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IAuthAppService
    {
        Task<AuthenticationResult> LoginAsync(LoginRequest login);
        Task<AuthenticationResult> RegisterAsync(RegisterRequest register);
        Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordRequest resetPassword);
    }
}
