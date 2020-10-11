using MatchDayApp.Application.Interfaces;
using MatchDayApp.Contract.Contract.V1.Request.Auth;
using MatchDayApp.Contract.Contract.V1.Response;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class AuthService : IAuthService
    {
        public Task<Response<object>> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<object>> LoginAsync(AuthRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<object>> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<Response<object>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
