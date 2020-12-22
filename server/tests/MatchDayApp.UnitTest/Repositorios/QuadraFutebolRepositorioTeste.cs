using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Quadra;
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
    [Trait("Repositorios", "Quadra de Futebol")]
    public class QuadraFutebolRepositorioTeste
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IQuadraFutebolRepositorio _quadraFutebolRepositorio;

        private readonly Faker<QuadraFutebol> _quadrasFake;
        private readonly QuadraFutebol _quadraTeste;
        private readonly object _quadraEsperada;

        public QuadraFutebolRepositorioTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _quadraFutebolRepositorio = new QuadraFutebolRepositorio(_memoryDb);
            _quadraTeste = _memoryDb.Quadras.First();

            _quadrasFake = new Faker<QuadraFutebol>()
                .RuleFor(sc => sc.Nome, f => f.Company.CompanyName())
                .RuleFor(sc => sc.Imagem, f => f.Image.PicsumUrl())
                .RuleFor(sc => sc.PrecoHora, f => f.Random.Decimal(80M, 200M))
                .RuleFor(sc => sc.Telefone, f => f.Phone.PhoneNumber("(##) ####-####"))
                .RuleFor(sc => sc.Endereco, f => f.Address.FullAddress())
                .RuleFor(sc => sc.Cep, f => f.Address.ZipCode("#####-###"))
                .RuleFor(sc => sc.Latitude, f => f.Address.Latitude())
                .RuleFor(sc => sc.Longitude, f => f.Address.Longitude())
                .RuleFor(sc => sc.UsuarioProprietarioId, f => _memoryDb.Usuarios.First().Id);

            _quadraEsperada = new
            {
                Nome = "Soccer Court 3",
                Imagem = "soccerCourt3.png",
                PrecoHora = 90M,
                Telefone = "(11) 3692-1472",
                Endereco = "Av. teste 321, teste",
                Cep = "01012-345",
                Latitude = -23.1096504,
                Longitude = -46.533172,
            };
        }

        [Fact, Order(1)]
        public void DeveSerPossivelConectarNoBancoDeDadosEmMemoria()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_QuadraFutebolRepositorio_RetornarListaComTodasQuadrasRegistradas()
        {
            var quadras = await _quadraFutebolRepositorio
                .ListAllAsync();

            quadras.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    q1 =>
                    {
                        q1.Nome.Should().Be("Soccer Court 3");
                        q1.PrecoHora.Should().Be(90M);
                        q1.UsuarioProprietario.Username.Should().Be("test3");
                        q1.UsuarioProprietario.Email.Should().Be("test3@email.com");
                    },
                    q2 =>
                    {
                        q2.Nome.Should().Be("Soccer Court 2");
                        q2.PrecoHora.Should().Be(110M);
                        q2.UsuarioProprietario.Username.Should().Be("test2");
                        q2.UsuarioProprietario.Email.Should().Be("test2@email.com");
                    },
                    q3 =>
                    {
                        q3.Nome.Should().Be("Soccer Court 1");
                        q3.PrecoHora.Should().Be(100M);
                        q3.UsuarioProprietario.Username.Should().Be("test1");
                        q3.UsuarioProprietario.Email.Should().Be("test1@email.com");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_QuadraFutebolRepositorio_RetornarQuadraPorId()
        {
            var quadra = await _quadraFutebolRepositorio
                .GetByIdAsync(_quadraTeste.Id);

            quadra.Should().BeEquivalentTo(_quadraEsperada, options =>
                options.ExcludingMissingMembers());
            quadra.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_QuadraFutebolRepositorio_RetornarNuloQuandoQuadraNaoExistir()
        {
            var invalidoQuadraId = Guid.NewGuid();

            var quadra = await _quadraFutebolRepositorio
                .GetByIdAsync(invalidoQuadraId);

            quadra.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_QuadraFutebolRepositorio_RetornarQuadraComUsuarioProprietarioUsandoEspecificacao()
        {
            var spec = new QuadraComUsuarioEspecificacao(_quadraTeste.UsuarioProprietarioId);
            var quadra = (await _quadraFutebolRepositorio.GetAsync(spec)).FirstOrDefault();

            quadra.Should().BeEquivalentTo(_quadraEsperada, options =>
                options.ExcludingMissingMembers());
            quadra.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(6)]
        public async Task GetAsync_QuadraFutebolRepositorio_RetornarQuadraComUsuarioProprietarioPorNomeUsandoEspecificacao()
        {
            var spec = new QuadraComUsuarioEspecificacao("Soccer Court 3");
            var quadra = (await _quadraFutebolRepositorio.GetAsync(spec)).FirstOrDefault();

            quadra.Should().BeEquivalentTo(_quadraEsperada, options =>
                options.ExcludingMissingMembers());
            quadra.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(7)]
        public async Task GetAsync_QuadraFutebolRepositorio_RetornarQuadrasOrdenasPeloNome()
        {
            var quadras = await _quadraFutebolRepositorio
                .GetAsync(null, x => x.OrderBy(u => u.Nome), "UsuarioProprietario", true);

            quadras.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    q1 =>
                    {
                        q1.Nome.Should().Be("Soccer Court 1");
                        q1.PrecoHora.Should().Be(100M);
                        q1.UsuarioProprietario.Username.Should().Be("test1");
                        q1.UsuarioProprietario.Email.Should().Be("test1@email.com");
                    },
                    q2 =>
                    {
                        q2.Nome.Should().Be("Soccer Court 2");
                        q2.PrecoHora.Should().Be(110M);
                        q2.UsuarioProprietario.Username.Should().Be("test2");
                        q2.UsuarioProprietario.Email.Should().Be("test2@email.com");
                    },
                    q3 =>
                    {
                        q3.Nome.Should().Be("Soccer Court 3");
                        q3.PrecoHora.Should().Be(90M);
                        q3.UsuarioProprietario.Username.Should().Be("test3");
                        q3.UsuarioProprietario.Email.Should().Be("test3@email.com");
                    });
        }

        [Fact, Order(8)]
        public async Task CountAsync_QuadraFutebolRepositorio_RetornarQuantidadeDeQuadrasQueCorrespondemAEspecificacao()
        {
            const int totalEsperado = 1;

            var spec = new QuadraComUsuarioEspecificacao("Soccer Court 1");
            var quadrasCount = await _quadraFutebolRepositorio.CountAsync(spec);

            quadrasCount.Should().Be(totalEsperado);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_QuadraFutebolRepositorio_AdicionarListaComNovasQuadras()
        {
            var result = await _quadraFutebolRepositorio
                .AddRangeAsync(_quadrasFake.Generate(5));

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.Quadras.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_QuadraFutebolRepositorio_AtualizarQuadra()
        {
            var quadraAtualizar = _memoryDb.Quadras.Last();

            quadraAtualizar.Nome = "Soccer court updated";
            quadraAtualizar.PrecoHora = 150M;

            var result = await _quadraFutebolRepositorio
                .SaveAsync(quadraAtualizar);

            result.Should().NotBeNull()
                .And.BeOfType<QuadraFutebol>();
            result.Nome.Should().Be("Soccer court updated");
            result.PrecoHora.Should().Be(150M);
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_QuadraFutebolRepositorio_DeletarQuadra()
        {
            var quadraDeletar = _memoryDb.Quadras.Last();

            await _quadraFutebolRepositorio
                .DeleteAsync(quadraDeletar);

            var quadraDeletada = await _quadraFutebolRepositorio
                .GetByIdAsync(quadraDeletar.Id);

            quadraDeletada.Should().BeNull();
            _memoryDb.Quadras.Should().HaveCount(2);
        }

        [Fact, Order(12)]
        public async Task GetAsync_QuadraFutebolRepositorio_RetornarQuadrasProximasALocalizacaoUsandoEspecificacao()
        {
            var spec = new QuadraProximaAoUsuarioEspecificacao(-23.109136, -46.5582639);
            var quadras = await _quadraFutebolRepositorio.GetAsync(spec);

            quadras.Should()
                .HaveCount(2)
                .And.SatisfyRespectively(
                q1 =>
                {
                    q1.Nome.Should().Be("Soccer Court 1");
                    q1.Endereco.Should().Be("Av. teste 10, teste");
                    q1.UsuarioProprietario.Should().NotBeNull();
                    q1.UsuarioProprietario.Username.Should().Be("test1");
                },
                q2 =>
                {
                    q2.Nome.Should().Be("Soccer Court 3");
                    q2.Endereco.Should().Be("Av. teste 321, teste");
                    q2.UsuarioProprietario.Should().NotBeNull();
                    q2.UsuarioProprietario.Username.Should().Be("test3");
                });
        }
    }
}
