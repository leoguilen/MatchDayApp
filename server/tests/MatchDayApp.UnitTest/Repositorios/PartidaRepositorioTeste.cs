using FluentAssertions;
using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Entidades.Enum;
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
    [Trait("Repositorios", "Partida")]
    public class PartidaRepositorioTeste
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IPartidaRepositorio _partidaRepositorio;

        private readonly Partida _partidaTeste;

        public PartidaRepositorioTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _partidaRepositorio = new PartidaRepositorio(_memoryDb);
            _partidaTeste = _memoryDb.Partidas.First();
        }

        [Fact, Order(1)]
        public void DeveSerPossivelConectarNoBancoDeDadosEmMemoria()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ObterPartidasAsync_PartidaRepositorio_RetornarListaComTodasPartidasRegistradas()
        {
            var partidas = await _partidaRepositorio
                .ObterPartidasAsync();

            partidas.Should()
                .NotBeNull()
                .And.HaveCount(5)
                .And.SatisfyRespectively(
                    p1 =>
                    {
                        p1.PrimeiroTime.Nome.Should().Be("Team 3");
                        p1.PrimeiroTimeConfirmado.Should().BeTrue();
                        p1.SegundoTime.Nome.Should().Be("Team 1");
                        p1.SegundoTimeConfirmado.Should().BeTrue();
                        p1.QuadraFutebol.Nome.Should().Be("Soccer Court 1");
                        p1.DataPartida.Should().Be(new DateTime(2020, 10, 20, 21, 0, 0));
                        p1.StatusPartida.Should().Be(1);
                    },
                    p2 =>
                    {
                        p2.PrimeiroTime.Nome.Should().Be("Team 2");
                        p2.PrimeiroTimeConfirmado.Should().BeTrue();
                        p2.SegundoTime.Nome.Should().Be("Team 3");
                        p2.SegundoTimeConfirmado.Should().BeFalse();
                        p2.QuadraFutebol.Nome.Should().Be("Soccer Court 2");
                        p2.DataPartida.Should().Be(new DateTime(2020, 10, 19, 18, 0, 0));
                        p2.StatusPartida.Should().Be(3);
                    },
                    p3 =>
                    {
                        p3.PrimeiroTime.Nome.Should().Be("Team 2");
                        p3.PrimeiroTimeConfirmado.Should().BeTrue();
                        p3.SegundoTime.Nome.Should().Be("Team 1");
                        p3.SegundoTimeConfirmado.Should().BeTrue();
                        p3.QuadraFutebol.Nome.Should().Be("Soccer Court 2");
                        p3.DataPartida.Should().Be(new DateTime(2020, 10, 21, 19, 0, 0));
                        p3.StatusPartida.Should().Be(1);
                    },
                    p4 =>
                    {
                        p4.PrimeiroTime.Nome.Should().Be("Team 3");
                        p4.PrimeiroTimeConfirmado.Should().BeTrue();
                        p4.SegundoTime.Nome.Should().Be("Team 2");
                        p4.SegundoTimeConfirmado.Should().BeTrue();
                        p4.QuadraFutebol.Nome.Should().Be("Soccer Court 3");
                        p4.DataPartida.Should().Be(new DateTime(2020, 10, 16, 20, 0, 0));
                        p4.StatusPartida.Should().Be(4);
                    },
                    p5 =>
                    {
                        p5.PrimeiroTime.Nome.Should().Be("Team 1");
                        p5.PrimeiroTimeConfirmado.Should().BeFalse();
                        p5.SegundoTime.Nome.Should().Be("Team 2");
                        p5.SegundoTimeConfirmado.Should().BeTrue();
                        p5.QuadraFutebol.Nome.Should().Be("Soccer Court 1");
                        p5.DataPartida.Should().Be(new DateTime(2020, 10, 18, 17, 0, 0));
                        p5.StatusPartida.Should().Be(2);
                    });
        }

        [Fact, Order(3)]
        public async Task ObterPartidaPorIdAsync_PartidaRepositorio_RetornarPartidaPorId()
        {
            var partida = await _partidaRepositorio
                .ObterPartidaPorIdAsync(_partidaTeste.Id);

            partida.Should().NotBeNull();
            partida.PrimeiroTime.Nome.Should().Be("Team 3");
            partida.PrimeiroTimeConfirmado.Should().BeTrue();
            partida.SegundoTime.Nome.Should().Be("Team 1");
            partida.SegundoTimeConfirmado.Should().BeTrue();
            partida.QuadraFutebol.Nome.Should().Be("Soccer Court 1");
            partida.HorasPartida.Should().Be(1);
            partida.DataPartida.Should().Be(new DateTime(2020, 10, 20, 21, 0, 0));
            partida.StatusPartida.Should().Be(StatusPartida.Confirmada);
        }

        [Fact, Order(4)]
        public async Task ObterAsync_PartidaRepositorio_RetonarPartidasPelaQuadra()
        {
            var quadraId = _memoryDb.Quadras.Last().Id;

            var partidas = await _partidaRepositorio
                .ObterAsync(p => p.QuadraFutebolId == quadraId);

            partidas.Should()
                .HaveCount(2)
                .And.SatisfyRespectively(
                p1 =>
                {
                    p1.PrimeiroTime.Nome.Should().Be("Team 3");
                    p1.PrimeiroTimeConfirmado.Should().BeTrue();
                    p1.SegundoTime.Nome.Should().Be("Team 1");
                    p1.SegundoTimeConfirmado.Should().BeTrue();
                    p1.QuadraFutebol.Nome.Should().Be("Soccer Court 1");
                    p1.DataPartida.Should().Be(new DateTime(2020, 10, 20, 21, 0, 0));
                    p1.StatusPartida.Should().Be(1);
                },
                p2 =>
                {
                    p2.PrimeiroTime.Nome.Should().Be("Team 1");
                    p2.PrimeiroTimeConfirmado.Should().BeFalse();
                    p2.SegundoTime.Nome.Should().Be("Team 2");
                    p2.SegundoTimeConfirmado.Should().BeTrue();
                    p2.QuadraFutebol.Nome.Should().Be("Soccer Court 1");
                    p2.DataPartida.Should().Be(new DateTime(2020, 10, 18, 17, 0, 0));
                    p2.StatusPartida.Should().Be(2);
                });
        }

        [Fact, Order(5)]
        public async Task AdicionarPartidaAsync_PartidaRepositorio_CriarSolicitacaoDePartida()
        {
            var novaPartida = new Partida
            {
                PrimeiroTimeId = _memoryDb.Times.First().Id,
                PrimeiroTimeConfirmado = true,
                SegundoTimeId = _memoryDb.Times.Last().Id,
                SegundoTimeConfirmado = false,
                QuadraFutebolId = _memoryDb.Quadras.First().Id,
                HorasPartida = 1,
                DataPartida = new DateTime(2020, 10, 15, 19, 30, 0),
                StatusPartida = StatusPartida.AguardandoConfirmacao
            };

            var result = await _partidaRepositorio
                .AdicionarPartidaAsync(novaPartida);

            result.Should().BeTrue();
        }

        [Fact, Order(6)]
        public async Task AtualizarPartidaAsync_PartidaRepositorio_AtualizarPartidaExistente()
        {
            _partidaTeste.DataPartida = new DateTime(2020, 10, 26, 18, 30, 0);
            _partidaTeste.StatusPartida = StatusPartida.Cancelada;

            var result = await _partidaRepositorio
                .AtualizarPartidaAsync(_partidaTeste);

            result.Should().BeTrue();
            _partidaTeste.DataPartida.Should().Be(new DateTime(2020, 10, 26, 18, 30, 0));
            _partidaTeste.StatusPartida.Should().Be(StatusPartida.Cancelada);
        }
    }
}
