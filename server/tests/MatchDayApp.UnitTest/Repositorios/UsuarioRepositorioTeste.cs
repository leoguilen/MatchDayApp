using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Domain.Especificacoes.Usuario;
using MatchDayApp.Domain.Helpers;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositorios;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace MatchDayApp.UnitTest.Repositorios
{
    [Trait("Repositorios", "Usuario")]
    public class UsuarioRepositorioTeste
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly Faker<Usuario> _usuarioFake;
        private readonly Usuario _usuarioTest;
        private readonly object _usuarioEsperado;

        public UsuarioRepositorioTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _usuarioRepositorio = new UsuarioRepositorio(_memoryDb);
            _usuarioTest = _memoryDb.Usuarios.First();

            string salt = SenhaHasherHelper.CriarSalt(8);
            _usuarioFake = new Faker<Usuario>()
                .RuleFor(u => u.Nome, f => f.Person.FirstName)
                .RuleFor(u => u.Sobrenome, f => f.Person.LastName)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.Telefone, f => f.Person.Phone)
                .RuleFor(u => u.Username, f => f.UniqueIndex + f.Person.UserName)
                .RuleFor(u => u.Senha, f => SenhaHasherHelper.GerarHash(f.Internet.Password(), salt))
                .RuleFor(u => u.Salt, salt)
                .RuleFor(u => u.TipoUsuario, TipoUsuario.Jogador);

            _usuarioEsperado = new
            {
                Nome = "Test",
                Sobrenome = "One",
                Username = "test1",
                Email = "test1@email.com",
                Telefone = "+551155256325",
                TipoUsuario = TipoUsuario.ProprietarioQuadra,
            };
        }

        [Fact, Order(1)]
        public void DeveSerPossivelConectarNoBancoDeDadosEmMemoria()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_UsuarioRepositorio_RetornarListaComTodosUsuariosRegistrados()
        {
            var usuarios = await _usuarioRepositorio
                .ListAllAsync();

            usuarios.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    u1 =>
                    {
                        u1.Username.Should().Be("test1");
                        u1.Email.Should().Be("test1@email.com");
                        u1.TipoUsuario.Should().Be(TipoUsuario.ProprietarioQuadra);
                        u1.UsuarioTime.Time.Nome.Should().Be("Team 1");
                    },
                    u2 =>
                    {
                        u2.Username.Should().Be("test2");
                        u2.Email.Should().Be("test2@email.com");
                        u2.TipoUsuario.Should().Be(TipoUsuario.ProprietarioTime);
                        u2.UsuarioTime.Time.Nome.Should().Be("Team 2");
                    },
                    u3 =>
                    {
                        u3.Username.Should().Be("test3");
                        u3.Email.Should().Be("test3@email.com");
                        u3.TipoUsuario.Should().Be(TipoUsuario.ProprietarioQuadra);
                        u3.UsuarioTime.Time.Nome.Should().Be("Team 3");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_UsuarioRepositorio_RetornarTimePorId()
        {
            var usuario = await _usuarioRepositorio
                .GetByIdAsync(_usuarioTest.Id);

            usuario.Should().BeEquivalentTo(_usuarioEsperado, options =>
                options.ExcludingMissingMembers());
            usuario.UsuarioTime.Time.Nome.Should().Be("Team 1");
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_UsuarioRepositorio_RetornarNuloQuandoUsuarioNaoExistir()
        {
            var invalidoUsuarioId = Guid.NewGuid();

            var usuario = await _usuarioRepositorio
                .GetByIdAsync(invalidoUsuarioId);

            usuario.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_UsuarioRepositorio_RetornarUsuarioPorEmailOuUsernameUsandoEspecificacao()
        {
            var spec = new UsuarioContendoEmailOuUsernameEspecificacao("test1@email.com");
            var usuario = (await _usuarioRepositorio.GetAsync(spec)).FirstOrDefault();

            usuario.Should().BeEquivalentTo(_usuarioEsperado, options =>
                options.ExcludingMissingMembers());
        }

        [Fact, Order(6)]
        public async Task GetAsync_UsuarioRepositorio_RetornarUsuarioQueCorrespondaACondicao()
        {
            var usuario = (await _usuarioRepositorio
                .GetAsync(u => u.Username == "test1"))
                .FirstOrDefault();

            usuario.Should().BeEquivalentTo(_usuarioEsperado, options =>
                options.ExcludingMissingMembers());
        }

        [Fact, Order(7)]
        public async Task GetAsync_UsuarioRepositorio_RetornarUsuariosQueCorrespondamACondicaoEOrdenandoDeFormaDecrescentePorUsername()
        {
            var usuarios = await _usuarioRepositorio
                .GetAsync(null, x => x.OrderByDescending(u => u.Username), "", true);

            usuarios.Should()
                .HaveCount(3)
                .And.SatisfyRespectively(
                u1 =>
                {
                    u1.Username.Should().Be("test3");
                    u1.Email.Should().Be("test3@email.com");
                },
                u2 =>
                {
                    u2.Username.Should().Be("test2");
                    u2.Email.Should().Be("test2@email.com");
                },
                u3 =>
                {
                    u3.Username.Should().Be("test1");
                    u3.Email.Should().Be("test1@email.com");
                });
        }

        [Fact, Order(8)]
        public async Task CountAsync_UsuarioRepositorio_RetornarQuantidadeDeUsuariosQueCorrespondemAEspecificacao()
        {
            const int totalEsperado = 2;

            var spec = new UsuarioContendoTipoUsuarioEspecificacao(TipoUsuario.ProprietarioQuadra);
            var usuarioCount = await _usuarioRepositorio.CountAsync(spec);

            usuarioCount.Should().Be(totalEsperado);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_UsuarioRepositorio_AdicionarListaComNovosUsuarios()
        {
            var usuariosFake = _usuarioFake.Generate(5);

            usuariosFake.ForEach(u => u.UsuarioTime = new UsuarioTime
            { UsuarioId = u.Id, TimeId = Guid.NewGuid(), Aceito = true });

            var result = await _usuarioRepositorio
                .AddRangeAsync(usuariosFake);

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.Usuarios.Should().HaveCount(8);
            _memoryDb.UsuarioTimes.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_UsuarioRepositorio_AtualizarUsuario()
        {
            var usuarioAtualizar = _memoryDb.Usuarios.Last();

            usuarioAtualizar.Email = "test.updated@email.com";
            usuarioAtualizar.Sobrenome = "Updated";

            var result = await _usuarioRepositorio
                .SaveAsync(usuarioAtualizar);

            result.Should().NotBeNull()
                .And.BeOfType<Usuario>();
            result.Sobrenome.Should().Be("Updated");
            result.Email.Should().Be("test.updated@email.com");
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_UsuarioRepositorio_DeletarUsuario()
        {
            var usuarioDeletar = _memoryDb.Usuarios.Last();

            await _usuarioRepositorio
                .DeleteAsync(usuarioDeletar);

            var usuarioDeletado = await _usuarioRepositorio
                .GetByIdAsync(usuarioDeletar.Id);

            usuarioDeletado.Should().BeNull();
            _memoryDb.Usuarios.Should().HaveCount(2);
        }

        [Fact, Order(12)]
        public async Task GetByEmailAsync_UsuarioRepositorio_RetornarUsuarioPorEmail()
        {
            var usuario = await _usuarioRepositorio
                .ObterUsuarioPorEmailAsync("test1@email.com");

            usuario.Should().BeEquivalentTo(_usuarioEsperado, options =>
                options.ExcludingMissingMembers());
            usuario.UsuarioTime.Time.Nome.Should().Be("Team 1");
        }

        [Fact, Order(13)]
        public async Task GetByEmailAsync_UsuarioRepositorio_RetornarNuloQuandoNaoExistirUsuarioComEmail()
        {
            var usuario = await _usuarioRepositorio
                .ObterUsuarioPorEmailAsync(new Faker().Person.Email);

            usuario.Should().BeNull();
        }
    }
}
