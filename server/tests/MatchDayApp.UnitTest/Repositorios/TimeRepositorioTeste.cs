using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Time;
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
    [Trait("Repositorios", "Time")]
    public class TimeRepositorioTeste
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly ITimeRepositorio _timeRepositorio;

        private readonly Faker<Time> _timeFake;
        private readonly Time _timeTeste;
        private readonly object _timeEsperado;

        public TimeRepositorioTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _timeRepositorio = new TimeRepositorio(_memoryDb);
            _timeTeste = _memoryDb.Times.First();

            _timeFake = new Faker<Time>()
                .RuleFor(u => u.Nome, f => f.Company.CompanyName())
                .RuleFor(u => u.Imagem, f => f.Image.PicsumUrl())
                .RuleFor(u => u.QtdIntegrantes, f => f.Random.Int(6, 16))
                .RuleFor(u => u.UsuarioProprietarioId, f => _memoryDb.Usuarios.First().Id);

            _timeEsperado = new
            {
                Nome = "Team 3",
                Imagem = "team3.png",
                QtdIntegrantes = 11
            };
        }

        [Fact, Order(1)]
        public void DeveSerPossivelConectarNoBancoDeDadosEmMemoria()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_TimeRepositorio_RetornarListaComTodosTimesRegistrados()
        {
            var times = await _timeRepositorio
                .ListAllAsync();

            times.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    t1 =>
                    {
                        t1.Nome.Should().Be("Team 3");
                        t1.QtdIntegrantes.Should().Be(11);
                        t1.UsuarioProprietario.Username.Should().Be("test3");
                        t1.UsuarioProprietario.Email.Should().Be("test3@email.com");
                    },
                    t2 =>
                    {
                        t2.Nome.Should().Be("Team 2");
                        t2.QtdIntegrantes.Should().Be(13);
                        t2.UsuarioProprietario.Username.Should().Be("test2");
                        t2.UsuarioProprietario.Email.Should().Be("test2@email.com");
                    },
                    t3 =>
                    {
                        t3.Nome.Should().Be("Team 1");
                        t3.QtdIntegrantes.Should().Be(15);
                        t3.UsuarioProprietario.Username.Should().Be("test1");
                        t3.UsuarioProprietario.Email.Should().Be("test1@email.com");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_TimeRepositorio_RetornarTimePorId()
        {
            var time = await _timeRepositorio
                .GetByIdAsync(_timeTeste.Id);

            time.Should().BeEquivalentTo(_timeEsperado, options =>
                options.ExcludingMissingMembers());
            time.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_TimeRepositorio_RetornarNuloQuandoTimeNaoExistir()
        {
            var invalidoTimeId = Guid.NewGuid();

            var time = await _timeRepositorio
                .GetByIdAsync(invalidoTimeId);

            time.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_TimeRepositorio_RetornarTimeComUsuarioProprietarioUsandoEspecificacao()
        {
            var spec = new TimeComUsuarioEspecificacao(_timeTeste.UsuarioProprietarioId);
            var time = (await _timeRepositorio.GetAsync(spec)).FirstOrDefault();

            time.Should().BeEquivalentTo(_timeEsperado, options =>
                options.ExcludingMissingMembers());
            time.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(6)]
        public async Task GetAsync_TimeRepositorio_RetornarTimeComUsuarioProprietarioPorNomeUsandoEspecificacao()
        {
            var spec = new TimeComUsuarioEspecificacao("Team 3");
            var time = (await _timeRepositorio.GetAsync(spec)).FirstOrDefault();

            time.Should().BeEquivalentTo(_timeEsperado, options =>
                options.ExcludingMissingMembers());
            time.UsuarioProprietario.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(7)]
        public async Task GetAsync_TimeRepositorio_RetornarTimesOrdenadosPeloNome()
        {
            var times = await _timeRepositorio
                .GetAsync(null, x => x.OrderBy(u => u.Nome), "UsuarioProprietario", true);

            times.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    t1 =>
                    {
                        t1.Nome.Should().Be("Team 1");
                        t1.QtdIntegrantes.Should().Be(15);
                        t1.UsuarioProprietario.Username.Should().Be("test1");
                        t1.UsuarioProprietario.Email.Should().Be("test1@email.com");
                    },
                    t2 =>
                    {
                        t2.Nome.Should().Be("Team 2");
                        t2.QtdIntegrantes.Should().Be(13);
                        t2.UsuarioProprietario.Username.Should().Be("test2");
                        t2.UsuarioProprietario.Email.Should().Be("test2@email.com");
                    },
                    t3 =>
                    {
                        t3.Nome.Should().Be("Team 3");
                        t3.QtdIntegrantes.Should().Be(11);
                        t3.UsuarioProprietario.Username.Should().Be("test3");
                        t3.UsuarioProprietario.Email.Should().Be("test3@email.com");
                    });
        }

        [Fact, Order(8)]
        public async Task CountAsync_TimeRepositorio_RetornarQuantidadeDeTimesQueCorrespondemAEspecificacao()
        {
            const int totalEsperado = 1;

            var spec = new TimeComUsuarioEspecificacao("Team 2");
            var timesCount = await _timeRepositorio.CountAsync(spec);

            timesCount.Should()
                .Be(totalEsperado);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_TimeRepositorio_AdicionarListaComNovosTimes()
        {
            var result = await _timeRepositorio
                .AddRangeAsync(_timeFake.Generate(5));

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.Times.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_TimeRepositorio_AtualizarTime()
        {
            var timeAtualizar = _memoryDb.Times.Last();

            timeAtualizar.Nome = "Team updated";
            timeAtualizar.Imagem = "teamupdated.jpg";

            var result = await _timeRepositorio
                .SaveAsync(timeAtualizar);

            result.Should().NotBeNull()
                .And.BeOfType<Time>();
            result.Nome.Should().Be("Team updated");
            result.Imagem.Should().Be("teamupdated.jpg");
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_TimeRepositorio_DeletarTime()
        {
            var timeDeletar = _memoryDb.Times.Last();

            await _timeRepositorio
                .DeleteAsync(timeDeletar);

            var timeDeletado = await _timeRepositorio
                .GetByIdAsync(timeDeletar.Id);

            timeDeletado.Should().BeNull();
            _memoryDb.Times.Should().HaveCount(2);
        }
    }
}
