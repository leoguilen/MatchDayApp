using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Resources;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "Authentication")]
    public class AuthServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuthService _authService;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Faker _faker = new Faker("pt_BR");

        public AuthServiceTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = configServices
                .GetRequiredService<IUnitOfWork>();

            _authService = new AuthService(_uow,
                configServices.GetRequiredService<IMapper>());
        }

        #region Reset Password

        [Fact]
        public async Task ResetPasswordAsync_AuthenticationResult_ErrorIfEmailNotExists()
        {
            var newPwd = _faker.Internet.Password();
            var resetPasswordModel = new ResetPasswordModel
            {
                Email = _faker.Person.Email,
                Password = newPwd,
                ConfirmPassword = newPwd
            };

            var authResult = await _authService
                .ResetPasswordAsync(resetPasswordModel);

            authResult.Message.Should().Be(Dictionary.ME001);
            authResult.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ResetPasswordAsync_AuthenticationResult_ErrorIfUserHasDeleted()
        {
            var newPwd = _faker.Internet.Password();
            var resetPasswordModel = new ResetPasswordModel
            {
                Email = "test1@email.com",
                Password = newPwd,
                ConfirmPassword = newPwd
            };

            var authResult = await _authService
                .ResetPasswordAsync(resetPasswordModel);

            authResult.Message.Should().Be(Dictionary.ME001);
            authResult.Success.Should().BeFalse();
        }

        [Fact]
        public async Task ResetPasswordAsync_AuthenticationResult_ResetedUserPassword()
        {
            var newPwd = _faker.Internet.Password();
            var resetPasswordModel = new ResetPasswordModel
            {
                Email = "test2@email.com",
                Password = newPwd,
                ConfirmPassword = newPwd
            };

            var authResult = await _authService
                .ResetPasswordAsync(resetPasswordModel);

            authResult.Message.Should().Be(Dictionary.MS002);
            authResult.Success.Should().BeTrue();
            authResult.User.Email.Should().Be(resetPasswordModel.Email);

            var userWithResetedPassword = await _uow.Users
                .GetByEmailAsync(resetPasswordModel.Email);
            var expectedPasswordHashed = SecurePasswordHasher
                .GenerateHash(resetPasswordModel.Password,
                    userWithResetedPassword.Salt);

            userWithResetedPassword.Password.Should()
                .Be(expectedPasswordHashed);
        }

        #endregion

        #region Login

        [Fact]
        public async Task LoginAsync_AuthenticationResult_ErrorIfEmailNotExists()
        {
            var loginModel = new LoginModel
            {
                Email = _faker.Person.Email,
                Password = _faker.Internet.Password()
            };

            var authResult = await _authService
                .LoginAsync(loginModel);

            authResult.Message.Should().Be(Dictionary.ME004);
            authResult.Success.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dictionary.MV001);
        }

        [Fact]
        public async Task LoginAsync_AuthenticationResult_ErrorIfPasswordIsInvalid()
        {
            var loginModel = new LoginModel
            {
                Email = "test3@email.com",
                Password = _faker.Internet.Password()
            };

            var authResult = await _authService
                .LoginAsync(loginModel);

            authResult.Message.Should().Be(Dictionary.ME004);
            authResult.Success.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dictionary.MV002);
        }

        [Fact]
        public async Task LoginAsync_AuthenticationResult_ErrorIfUserWasDeleted()
        {
            var loginModel = new LoginModel
            {
                Email = "test1@email.com",
                Password = "test123"
            };

            var authResult = await _authService
                .LoginAsync(loginModel);

            authResult.Message.Should().Be(Dictionary.ME004);
            authResult.Success.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dictionary.MV001);
        }

        [Fact]
        public async Task LoginAsync_AuthenticationResult_SuccessLogin()
        {
            var loginModel = new LoginModel
            {
                Email = "test2@email.com",
                Password = "test321"
            };

            var authResult = await _authService
                .LoginAsync(loginModel);

            authResult.Message.Should().Be(Dictionary.MS001);
            authResult.Success.Should().BeTrue();
            authResult.User.Email.Should().Be(loginModel.Email);
        }

        #endregion

        #region Register
        
        [Fact]
        public async Task RegisterAsync_AuthenticationResult_ErrorIfEmailAlreadyExists()
        {
            var newPass = _faker.Internet.Password();
            var registerModel = new RegisterModel
            {
                FirstName = _faker.Person.FirstName,
                LastName = _faker.Person.LastName,
                UserName = _faker.Person.UserName,
                Email = "test2@email.com",
                Password = newPass,
                ConfirmPassword = newPass
            };

            var authResult = await _authService
                .RegisterAsync(registerModel);

            authResult.Message.Should().Be(Dictionary.ME005);
            authResult.Success.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dictionary.MV003);
        }

        [Fact]
        public async Task RegisterAsync_AuthenticationResult_ErrorIfUsernameAlreadyExists()
        {
            var newPass = _faker.Internet.Password();
            var registerModel = new RegisterModel
            {
                FirstName = _faker.Person.FirstName,
                LastName = _faker.Person.LastName,
                UserName = "test1",
                Email = _faker.Person.Email,
                Password = newPass,
                ConfirmPassword = newPass
            };

            var authResult = await _authService
                .RegisterAsync(registerModel);

            authResult.Message.Should().Be(Dictionary.ME005);
            authResult.Success.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dictionary.MV004);
        }

        [Fact]
        public async Task RegisterAsync_AuthenticationResult_SuccessRegister()
        {
            var newPass = _faker.Internet.Password();
            var registerModel = new RegisterModel
            {
                FirstName = _faker.Person.FirstName,
                LastName = _faker.Person.LastName,
                UserName = _faker.Person.UserName,
                Email = _faker.Person.Email,
                Password = newPass,
                ConfirmPassword = newPass
            };

            var authResult = await _authService
                .RegisterAsync(registerModel);

            authResult.Message.Should().Be(Dictionary.MS003);
            authResult.Success.Should().BeTrue();
            authResult.User.Email.Should().Be(registerModel.Email);

            var insertedUser = await _uow.Users
                .GetByIdAsync(authResult.User.Id);

            insertedUser.FirstName.Should().Be(registerModel.FirstName);
            insertedUser.LastName.Should().Be(registerModel.LastName);
            insertedUser.Username.Should().Be(registerModel.UserName);
            insertedUser.Email.Should().Be(registerModel.Email);
        }

        #endregion
    }

    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<AuthenticationResult> LoginAsync(LoginModel login)
        {
            var user = await _uow.Users
                .GetByEmailAsync(login.Email);

            if(user == null || user.Deleted)
            {
                return new AuthenticationResult
                {
                    Message = Dictionary.ME004,
                    Success = false,
                    Errors = new[] { Dictionary.MV001 }
                };
            }

            if(!SecurePasswordHasher.AreEqual(
                login.Password,user.Password,user.Salt))
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
                User = _mapper.Map<UserModel>(user)
            };

            // Retornar Token
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterModel register)
        {
            var existsEmail = await _uow.Users
                .GetByEmailAsync(register.Email);

            if(existsEmail != null)
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

            if(existsUsername.Any())
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

            await _uow.Users.AddRangeAsync(new User[] { newUser });
            
            return new AuthenticationResult
            {
                Message = Dictionary.MS003,
                Success = true,
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
                Success = true,
                User = _mapper.Map<UserModel>(user)
            };
        }
    }
}
