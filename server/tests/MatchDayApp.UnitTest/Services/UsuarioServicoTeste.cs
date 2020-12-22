using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Servicos
{
    [Trait("Servicos", "Usuario")]
    public class UsuarioServicoTeste
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsuarioServico _usuarioServico;
        private readonly MatchDayAppContext _memoryDb;

        public UsuarioServicoTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _usuarioServico = new UsuarioServico(_uow,
                cfg.GetRequiredService<IMapper>());
        }

        [Fact]
        public async Task DeletarUsuarioAsync_UsuarioServico_DeletarUsuario()
        {
            var usuarioId = _memoryDb.Usuarios.Last().Id;

            var cmdResult = await _usuarioServico
                .DeletarUsuarioAsync(usuarioId);

            cmdResult.Should().BeTrue();

            var usuarioDeletado = await _uow.UsuarioRepositorio
                .GetByIdAsync(usuarioId);

            usuarioDeletado.Deletado.Should().BeTrue();
        }

        [Fact]
        public async Task ObterUsuarioPorEmailAsync_UsuarioServico_RetornarUsuarioPorEmail()
        {
            var usuarioEmail = "test2@email.com";

            var usuario = await _usuarioServico
                .ObterUsuarioPorEmailAsync(usuarioEmail);

            usuario.Email.Should().Be(usuarioEmail);
        }

        [Fact]
        public async Task ObterUsuarioPorIdAsync_UsuarioServico_RetornarUsuarioPorId()
        {
            var usuarioId = _memoryDb.Usuarios.Last().Id;

            var usuario = await _usuarioServico
                .ObterUsuarioPorIdAsync(usuarioId);

            usuario.Email.Should().Be("test3@email.com");
        }

        [Fact]
        public async Task ObterUsuariosAsync_UsuarioServico_RetornarListaComUsuarios()
        {
            var usuarios = await _usuarioServico
                .ObterUsuariosAsync();

            usuarios.Should().HaveCount(2);
        }

        [Fact]
        public async Task UpdateUserAsync_UsuarioServico_UpdatedUserInSystem()
        {
            var novoNomeUsuario = new Faker().Person.FirstName;
            var novoSobrenomeUsuario = new Faker().Person.LastName;
            var usuarioId = _memoryDb.Usuarios.ToList()[2].Id;

            var usuario = await _usuarioServico
                .ObterUsuarioPorIdAsync(usuarioId);
            usuario.Nome = novoNomeUsuario;
            usuario.Sobrenome = novoSobrenomeUsuario;

            var cmdResult = await _usuarioServico
                .AtualizarUsuarioAsync(usuarioId, usuario);

            cmdResult.Should().NotBeNull();

            var usuarioAtualizado = await _usuarioServico
                .ObterUsuarioPorIdAsync(usuarioId);

            usuarioAtualizado.Nome.Should().Be(novoNomeUsuario);
            usuarioAtualizado.Sobrenome.Should().Be(novoSobrenomeUsuario);
        }
    }
}
