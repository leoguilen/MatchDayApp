using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public AuthService(IUnitOfWork uow, IMapper mapper, JwtOptions jwtOptions)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginModel login)
        {
            var user = await _uow.Users
                .GetByEmailAsync(login.Email);

            if (user == null || user.Deleted)
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME004,
                    Success = false,
                    Errors = new[] { Dictionary.MV001 }
                };
            }

            if (!SecurePasswordHasher.AreEqual(
                login.Password, user.Password, user.Salt))
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME004,
                    Success = false,
                    Errors = new[] { Dictionary.MV002 }
                };
            }

            return new AuthenticationResult
            {
                Message = Dictionary.MS001,
                Success = true,
                Token = await TokenHelper.GenerateTokenForUserAsync(user, _jwtOptions),
                User = _mapper.Map<UserModel>(user)
            };
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterModel register)
        {
            var existsEmail = await _uow.Users
                .GetByEmailAsync(register.Email);

            if (existsEmail != null)
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME005,
                    Success = false,
                    Errors = new[] { Dictionary.MV003 }
                };
            }

            var existsUsername = await _uow.Users
                .GetAsync(u => u.Username.Contains(register.UserName));

            if (existsUsername.Any())
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME005,
                    Success = false,
                    Errors = new[] { Dictionary.MV004 }
                };
            }

            string salt = SecurePasswordHasher.CreateSalt(8);
            string hashedPassword = SecurePasswordHasher.GenerateHash(register.Password, salt);

            var newUser = _mapper.Map<User>(register);

            newUser.Salt = salt;
            newUser.Password = hashedPassword;

            await _uow.Users.AddRangeAsync(new[] { newUser });

            return new AuthenticationResult
            {
                Message = Dictionary.MS003,
                Success = true,
                Token = await TokenHelper.GenerateTokenForUserAsync(newUser, _jwtOptions),
                User = _mapper.Map<UserModel>(newUser)
            };
        }

        public async Task<AuthenticationResult> ResetPasswordAsync(ResetPasswordModel resetPassword)
        {
            var user = await _uow.Users
                .GetByEmailAsync(resetPassword.Email);

            if (user == null || user.Deleted)
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME001,
                    Success = false,
                };
            }

            user.Salt = SecurePasswordHasher.CreateSalt(8);
            user.Password = SecurePasswordHasher
                .GenerateHash(resetPassword.Password, user.Salt);

            await _uow.Users.SaveAsync(user);

            return new AuthenticationResult
            {
                Message = Dictionary.MS002,
                Success = true
            };
        }
    }
}
