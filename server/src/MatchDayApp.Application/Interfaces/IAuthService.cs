using MatchDayApp.Contract.Contract.V1.Request.Auth;
using MatchDayApp.Contract.Contract.V1.Response;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Response<object>> LoginAsync(AuthRequest request);
        Task<Response<object>> ForgotPasswordAsync(ForgotPasswordRequest request);
        Task<Response<object>> RegisterAsync(RegisterRequest request);
        Task<Response<object>> ResetPasswordAsync(ResetPasswordRequest request);
    }
}
