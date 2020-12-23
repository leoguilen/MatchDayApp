using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Comandos.Usuario;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Usuario;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Handlers
{
    [Trait("Handler", "Usuario")]
    public class UsuarioHandlerTeste
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _usuarioId;

        public UsuarioHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _usuarioId = _memoryDb.Usuarios.Last().Id;
        }

        [Fact]
        public async Task Handle_UsuarioHandler_RetornarListaComTodosUsuarios()
        {
            var query = new ObterUsuariosQuery { };

            var usuariosResult = await _mediator.Send(query);

            usuariosResult.Should().HaveCount(2);
        }

        [Fact]
        public async Task Handle_UsuarioHandler_RetornarUsuarioPorId()
        {
            var query = new ObterUsuarioPorIdQuery
            {
                Id = _usuarioId
            };

            var usuarioResult = await _mediator.Send(query);

            usuarioResult.Email.Should().Be("test3@email.com");
        }

        [Fact]
        public async Task Handle_UsuarioHandler_RetornarUsuarioPorEmail()
        {
            var query = new ObterUsuarioPorEmailQuery
            {
                Email = "test2@email.com"
            };

            var usuarioResult = await _mediator.Send(query);

            usuarioResult.Email.Should().Be("test2@email.com");
        }

        [Fact]
        public async Task Handle_UsuarioHandler_AtualizarUsuario()
        {
            var comando = new AtualizarUsuarioCommand
            {
                UsuarioId = _memoryDb.Usuarios.Last().Id,
                Usuario = new UsuarioModel
                {
                    Nome = new Faker().Person.FirstName,
                    Sobrenome = new Faker().Person.LastName,
                    Email = "test3@email.com",
                    TipoUsuario = TipoUsuario.Jogador
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();

            var updatedUser = _memoryDb.Usuarios.Last();

            updatedUser.Nome.Should().Be(comando.Usuario.Nome);
            updatedUser.Sobrenome.Should().Be(comando.Usuario.Sobrenome);
            updatedUser.TipoUsuario.Should().Be(comando.Usuario.TipoUsuario);
        }

        [Fact]
        public async Task Handle_UsuarioHandler_DeletarUsuario()
        {
            var comando = new DeletarUsuarioCommand
            {
                Id = _usuarioId
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().BeTrue();
        }
    }
}
