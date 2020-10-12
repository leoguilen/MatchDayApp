using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class AuthService : IAuthService
    {
        public Task<AuthenticationResult> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> LoginAsync(LoginModel login)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> RegisterAsync(RegisterModel register)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordModel resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
