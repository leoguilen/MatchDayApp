using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Comandos.Autenticacao;
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
    [Trait("Handler", "Autenticacao")]
    public class AutenticacaoHandlerTeste
    {
        private readonly IMediator _mediator;

        public AutenticacaoHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task Handle_AutenticacaoHandler_LogadoComSucesso()
        {
            var loginCommand = new LoginCommand
            {
                Login = new LoginModel
                {
                    Email = "test2@email.com",
                    Senha = "test321"
                }
            };

            var authResult = await _mediator.Send(loginCommand);

            authResult.Mensagem.Should().Be(Dicionario.MS001);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(loginCommand.Login.Email);
        }

        [Fact]
        public async Task Handle_AutenticacaoHandler_UsuarioRegistradoComSucesso()
        {
            var faker = new Faker("pt_BR");
            var newPass = faker.Internet.Password();
            var registerCommand = new RegistrarUsuarioCommand
            {
                RegistrarUsuario = new RegistrarUsuarioModel
                {
                    Nome = faker.Person.FirstName,
                    Sobrenome = faker.Person.LastName,
                    Email = faker.Person.Email,
                    Username = faker.Person.UserName,
                    Senha = newPass,
                    ConfirmacaoSenha = newPass
                }
            };

            var authResult = await _mediator.Send(registerCommand);

            authResult.Mensagem.Should().Be(Dicionario.MS003);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(registerCommand.RegistrarUsuario.Email);
        }

        [Fact]
        public async Task Handle_AutenticacaoHandler_SenhaResetaComSucesso()
        {
            var newPass = new Faker().Internet.Password();
            var resetPasswordCommand = new ResetarSenhaCommand
            {
                ResetarSenha = new ResetarSenhaModel
                {
                    Email = "test2@email.com",
                    Senha = newPass,
                    ConfirmacaoSenha = newPass
                }
            };

            var authResult = await _mediator.Send(resetPasswordCommand);

            authResult.Mensagem.Should().Be(Dicionario.MS002);
            authResult.Sucesso.Should().BeTrue();
        }
    }
}
