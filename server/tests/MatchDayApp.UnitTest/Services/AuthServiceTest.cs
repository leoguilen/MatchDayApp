using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Application.Services;
using MatchDayApp.Domain.Common.Helpers;
using MatchDayApp.Domain.Configuration;
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
                configServices.GetRequiredService<IMapper>(),
                configServices.GetService<JwtConfiguracao>());
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

            var userWithResetedPassword = await _uow.Users
                .GetByEmailAsync(resetPasswordModel.Email);
            var expectedPasswordHashed = SecurePasswordHasherHelper
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
            authResult.Token.Should().NotBeNullOrEmpty();
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
            authResult.Token.Should().NotBeNullOrEmpty();
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
}
