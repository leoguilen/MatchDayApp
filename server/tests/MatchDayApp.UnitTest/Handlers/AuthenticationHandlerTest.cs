using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Commands.Auth;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Resources;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Handlers
{
    [Trait("Handler", "Authentication")]
    public class AuthenticationHandlerTest
    {
        private readonly IMediator _mediator;

        public AuthenticationHandlerTest()
        {
            var cfg = ServicesConfiguration.Configure();

            cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_AuthenticationHandler_SuccessLogin()
        {
            var loginCommand = new LoginCommand
            {
                Login = new LoginModel
                {
                    Email = "test2@email.com",
                    Password = "test321"
                }
            };

            var authResult = await _mediator.Send(loginCommand);

            authResult.Message.Should().Be(Dictionary.MS001);
            authResult.Success.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.User.Email.Should().Be(loginCommand.Login.Email);
        }

        [Fact]
        public async Task Handle_AuthenticationHandler_SuccessRegister()
        {
            var faker = new Faker("pt_BR");
            var newPass = faker.Internet.Password();
            var registerCommand = new RegisterCommand
            {
                Register = new RegistrarUsuarioModel
                {
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Email = faker.Person.Email,
                    UserName = faker.Person.UserName,
                    Password = newPass,
                    ConfirmPassword = newPass
                }
            };

            var authResult = await _mediator.Send(registerCommand);

            authResult.Message.Should().Be(Dictionary.MS003);
            authResult.Success.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.User.Email.Should().Be(registerCommand.Register.Email);
        }

        [Fact]
        public async Task Handle_AuthenticationHandler_SuccessResetPassword()
        {
            var newPass = new Faker().Internet.Password();
            var resetPasswordCommand = new ResetPasswordCommand
            {
                ResetPassword = new ResetarSenhaModel
                {
                    Email = "test2@email.com",
                    Password = newPass,
                    ConfirmPassword = newPass
                }
            };

            var authResult = await _mediator.Send(resetPasswordCommand);

            authResult.Message.Should().Be(Dictionary.MS002);
            authResult.Success.Should().BeTrue();
        }
    }
}
