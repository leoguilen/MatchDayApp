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
            var comando = new LoginCommand
            {
                Login = new LoginModel
                {
                    Email = "test2@email.com",
                    Senha = "test321"
                }
            };

            var authResult = await _mediator.Send(comando);

            authResult.Mensagem.Should().Be(Dicionario.MS001);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(comando.Login.Email);
        }

        [Fact]
        public async Task Handle_AutenticacaoHandler_UsuarioRegistradoComSucesso()
        {
            var faker = new Faker("pt_BR");
            var novaSenha = faker.Internet.Password();
            var comando = new RegistrarUsuarioCommand
            {
                RegistrarUsuario = new RegistrarUsuarioModel
                {
                    Nome = faker.Person.FirstName,
                    Sobrenome = faker.Person.LastName,
                    Email = faker.Person.Email,
                    Username = faker.Person.UserName,
                    Senha = novaSenha,
                    ConfirmacaoSenha = novaSenha
                }
            };

            var authResult = await _mediator.Send(comando);

            authResult.Mensagem.Should().Be(Dicionario.MS003);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(comando.RegistrarUsuario.Email);
        }

        [Fact]
        public async Task Handle_AutenticacaoHandler_SenhaResetaComSucesso()
        {
            var novaSenha = new Faker().Internet.Password();
            var comando = new ResetarSenhaCommand
            {
                ResetarSenha = new ResetarSenhaModel
                {
                    Email = "test2@email.com",
                    Senha = novaSenha,
                    ConfirmacaoSenha = novaSenha
                }
            };

            var authResult = await _mediator.Send(comando);

            authResult.Mensagem.Should().Be(Dicionario.MS002);
            authResult.Sucesso.Should().BeTrue();
        }
    }
}
