using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Domain.Helpers;
using MatchDayApp.Domain.Resources;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Servicos
{
    [Trait("Servicos", "Autenticacao")]
    public class AutenticacaoServicoTeste
    {
        private readonly IUnitOfWork _uow;
        private readonly IAutenticacaoServico _autenticacaoServico;
        private readonly Faker _faker = new Faker("pt_BR");

        public AutenticacaoServicoTeste()
        {
            var configServices = ServicesConfiguration.Configure();
            configServices.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _uow = configServices
                .GetRequiredService<IUnitOfWork>();

            _autenticacaoServico = new AutenticacaoServico(_uow,
                configServices.GetRequiredService<IMapper>(),
                configServices.GetService<JwtConfiguracao>());
        }

        #region Resetar Senha

        [Fact]
        public async Task ResetarSenhaAsync_AutenticacaoServico_RespostaDeErrorSeEmailNaoExistir()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new ResetarSenhaModel
            {
                Email = _faker.Person.Email,
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .ResetarSenhaAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME001);
            authResult.Sucesso.Should().BeFalse();
        }

        [Fact]
        public async Task ResetarSenhaAsync_AutenticacaoServico_RespostaDeErrorSeUsuarioEstiverDeletado()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new ResetarSenhaModel
            {
                Email = "test1@email.com",
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .ResetarSenhaAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME001);
            authResult.Sucesso.Should().BeFalse();
        }

        [Fact]
        public async Task ResetarSenhaAsync_AutenticacaoServico_SucessoNoResetDaSenhaDoUsuario()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new ResetarSenhaModel
            {
                Email = "test2@email.com",
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .ResetarSenhaAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.MS002);
            authResult.Sucesso.Should().BeTrue();

            var usuarioComSenhaResetada = await _uow.UsuarioRepositorio
                .ObterUsuarioPorEmailAsync(model.Email);
            var senhaHashEsperado = SenhaHasherHelper
                .GerarHash(model.Senha, usuarioComSenhaResetada.Salt);

            usuarioComSenhaResetada.Senha.Should()
                .Be(senhaHashEsperado);
        }

        #endregion

        #region Login

        [Fact]
        public async Task LoginAsync_AutenticacaoServico_RespostaDeErrorSeEmailNaoExistir()
        {
            var model = new LoginModel
            {
                Email = _faker.Person.Email,
                Senha = _faker.Internet.Password()
            };

            var authResult = await _autenticacaoServico
                .LoginAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME004);
            authResult.Sucesso.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dicionario.MV001);
        }

        [Fact]
        public async Task LoginAsync_AutenticacaoServico_RespostaDeErroSeSenhaForInvalida()
        {
            var model = new LoginModel
            {
                Email = "test2@email.com",
                Senha = _faker.Internet.Password()
            };

            var authResult = await _autenticacaoServico
                .LoginAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME004);
            authResult.Sucesso.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dicionario.MV002);
        }

        [Fact]
        public async Task LoginAsync_AutenticacaoServico_RespostaDeErroSeUsuarioFoiDeletado()
        {
            var model = new LoginModel
            {
                Email = "test1@email.com",
                Senha = "test123"
            };

            var authResult = await _autenticacaoServico
                .LoginAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME004);
            authResult.Sucesso.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dicionario.MV001);
        }

        [Fact]
        public async Task LoginAsync_AutenticacaoServico_SucessoNoLoginDoUsuario()
        {
            var model = new LoginModel
            {
                Email = "test2@email.com",
                Senha = "test321"
            };

            var authResult = await _autenticacaoServico
                .LoginAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.MS001);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(model.Email);
        }

        #endregion

        #region Registrar Usuário

        [Fact]
        public async Task RegistrarUsuarioAsync_AutenticacaoServico_RespostaDeErroSeEmailJaExistir()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new RegistrarUsuarioModel
            {
                Nome = _faker.Person.FirstName,
                Sobrenome = _faker.Person.LastName,
                Username = _faker.Person.UserName,
                Email = "test2@email.com",
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .RegistrarUsuarioAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME005);
            authResult.Sucesso.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dicionario.MV003);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_AutenticacaoServico_RespostaDeErrorSeUsernameJaExistir()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new RegistrarUsuarioModel
            {
                Nome = _faker.Person.FirstName,
                Sobrenome = _faker.Person.LastName,
                Username = "test1",
                Email = _faker.Person.Email,
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .RegistrarUsuarioAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.ME005);
            authResult.Sucesso.Should().BeFalse();
            authResult.Errors.First().Should().Be(Dicionario.MV004);
        }

        [Fact]
        public async Task RegistrarUsuarioAsync_AutenticacaoServico_SucessoNoRegistroDoUsuario()
        {
            var novaSenha = _faker.Internet.Password();
            var model = new RegistrarUsuarioModel
            {
                Nome = _faker.Person.FirstName,
                Sobrenome = _faker.Person.LastName,
                Username = _faker.Person.UserName,
                Email = _faker.Person.Email,
                Senha = novaSenha,
                ConfirmacaoSenha = novaSenha
            };

            var authResult = await _autenticacaoServico
                .RegistrarUsuarioAsync(model);

            authResult.Mensagem.Should().Be(Dicionario.MS003);
            authResult.Sucesso.Should().BeTrue();
            authResult.Token.Should().NotBeNullOrEmpty();
            authResult.Usuario.Email.Should().Be(model.Email);

            var usuarioInserido = await _uow.UsuarioRepositorio
                .GetByIdAsync(authResult.Usuario.Id);

            usuarioInserido.Nome.Should().Be(model.Nome);
            usuarioInserido.Sobrenome.Should().Be(model.Sobrenome);
            usuarioInserido.Username.Should().Be(model.Username);
            usuarioInserido.Email.Should().Be(model.Email);
        }

        #endregion
    }
}
