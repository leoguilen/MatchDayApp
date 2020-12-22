using AutoMapper;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Domain.Entidades.Enum;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Servicos
{
    [Trait("Servicos", "Partida")]
    public class PartidaServicoTeste
    {
        private readonly IUnitOfWork _uow;
        private readonly IPartidaServico _partidaServico;
        private readonly MatchDayAppContext _memoryDb;

        public PartidaServicoTeste()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = configServices
                .GetRequiredService<IUnitOfWork>();

            _partidaServico = new PartidaServico(_uow,
                configServices.GetRequiredService<IMapper>());
        }

        [Fact]
        public async Task ObterPartidasAsync_PartidaServico_RetornarListaComTodasPartidas()
        {
            var partidas = await _partidaServico
                .ObterPartidasAsync();

            partidas.Should().HaveCount(5);
        }

        [Fact]
        public async Task ObterPartidaPorIdAsync_PartidaServico_RetornarPartidaPorId()
        {
            var partidaId = _memoryDb.Partidas.First().Id;
            var partida = await _partidaServico
                .ObterPartidaPorIdAsync(partidaId);

            partida.PrimeiroTimeId.Should().Be(_memoryDb.Times.First().Id);
            partida.SegundoTimeId.Should().Be(_memoryDb.Times.Last().Id);
            partida.QuadraFutebolId.Should().Be(_memoryDb.Quadras.Last().Id);
            partida.HorasPartida.Should().Be(1);
            partida.DataPartida.Should().Be(new DateTime(2020, 10, 20, 21, 0, 0));
            partida.StatusPartida.Should().Be(StatusPartida.Confirmada);
        }

        [Fact]
        public async Task ObterPartidaPorQuadraIdAsync_PartidaServico_RetornarListaComPartidasDaQuadraEspecificada()
        {
            var quadraId = _memoryDb.Quadras.First().Id;
            var partidas = await _partidaServico
                .ObterPartidaPorQuadraIdAsync(quadraId);

            partidas.Should()
                .HaveCount(1)
                .And.SatisfyRespectively(
                    p1 =>
                    {
                        p1.PrimeiroTimeId.Should().Be(_memoryDb.Times.First().Id);
                        p1.SegundoTimeId.Should().Be(_memoryDb.Times.ToList()[1].Id);
                        p1.QuadraFutebolId.Should().Be(_memoryDb.Quadras.First().Id);
                        p1.HorasPartida.Should().Be(1);
                        p1.DataPartida.Should().Be(new DateTime(2020, 10, 16, 20, 0, 0));
                        p1.StatusPartida.Should().Be(StatusPartida.Finalizada);
                    });
        }

        [Fact]
        public async Task ObterPartidaPorTimeIdAsync_PartidaServico_RetornarListaComPartidasDeTimeEspecificado()
        {
            var timeId = _memoryDb.Times.First().Id;
            var partidas = await _partidaServico
                .ObterPartidaPorTimeIdAsync(timeId);

            partidas.Should()
                .HaveCount(3)
                .And.SatisfyRespectively(
                    p1 =>
                    {
                        p1.PrimeiroTimeId.Should().Be(_memoryDb.Times.First().Id);
                        p1.SegundoTimeId.Should().Be(_memoryDb.Times.Last().Id);
                        p1.QuadraFutebolId.Should().Be(_memoryDb.Quadras.Last().Id);
                        p1.HorasPartida.Should().Be(1);
                        p1.DataPartida.Should().Be(new DateTime(2020, 10, 20, 21, 0, 0));
                        p1.StatusPartida.Should().Be(StatusPartida.Confirmada);
                    },
                    p2 =>
                    {
                        p2.PrimeiroTimeId.Should().Be(_memoryDb.Times.ToList()[1].Id);
                        p2.SegundoTimeId.Should().Be(_memoryDb.Times.First().Id);
                        p2.QuadraFutebolId.Should().Be(_memoryDb.Quadras.ToList()[1].Id);
                        p2.HorasPartida.Should().Be(1);
                        p2.DataPartida.Should().Be(new DateTime(2020, 10, 19, 18, 0, 0));
                        p2.StatusPartida.Should().Be(StatusPartida.AguardandoConfirmacao);
                    },
                    p3 =>
                    {
                        p3.PrimeiroTimeId.Should().Be(_memoryDb.Times.First().Id);
                        p3.SegundoTimeId.Should().Be(_memoryDb.Times.ToList()[1].Id);
                        p3.QuadraFutebolId.Should().Be(_memoryDb.Quadras.First().Id);
                        p3.HorasPartida.Should().Be(1);
                        p3.DataPartida.Should().Be(new DateTime(2020, 10, 16, 20, 0, 0));
                        p3.StatusPartida.Should().Be(StatusPartida.Finalizada);
                    });
        }

        [Fact]
        public async Task ConfirmarPartidaAsync_PartidaServico_ConfirmarPartidaPendente()
        {
            var timeId = _memoryDb.Times.First().Id;
            var partidaId = _memoryDb.Partidas.ToList()[1].Id;

            var confirmacaoResult = await _partidaServico
                .ConfirmarPartidaAsync(timeId, partidaId);

            confirmacaoResult.Should().BeTrue();

            var partidaConfirmada = await _partidaServico
                .ObterPartidaPorIdAsync(partidaId);

            partidaConfirmada.SegundoTimeId.Should().Be(timeId);
            partidaConfirmada.SegundoTimeConfirmado.Should().BeTrue();
            partidaConfirmada.StatusPartida.Should().Be(StatusPartida.Confirmada);
        }

        [Fact]
        public async Task MarcarPartidaAsync_PartidaServico_CriarSolicitacaoDeNovaPartida()
        {
            var model = new PartidaModel
            {
                PrimeiroTimeId = _memoryDb.Times.First().Id,
                PrimeiroTimeConfirmado = true,
                SegundoTimeId = _memoryDb.Times.Last().Id,
                SegundoTimeConfirmado = false,
                QuadraFutebolId = _memoryDb.Quadras.First().Id,
                HorasPartida = 1,
                DataPartida = new DateTime(2020, 12, 31, 23, 59, 59)
            };

            var novaPartida = await _partidaServico
                .MarcarPartidaAsync(model);

            novaPartida.Should().NotBeNull();

            var partidaCriada = await _partidaServico
                .ObterPartidasAsync();

            partidaCriada.Should().HaveCount(6);
            partidaCriada[partidaCriada.Count - 1].PrimeiroTimeId.Should().Be(novaPartida.PrimeiroTimeId);
            partidaCriada[partidaCriada.Count - 1].PrimeiroTimeConfirmado.Should().BeTrue();
            partidaCriada[partidaCriada.Count - 1].SegundoTimeId.Should().Be(novaPartida.SegundoTimeId);
            partidaCriada[partidaCriada.Count - 1].SegundoTimeConfirmado.Should().BeFalse();
            partidaCriada[partidaCriada.Count - 1].QuadraFutebolId.Should().Be(novaPartida.QuadraFutebolId);
            partidaCriada[partidaCriada.Count - 1].DataPartida.Should().Be(novaPartida.DataPartida);
            partidaCriada[partidaCriada.Count - 1].StatusPartida.Should().Be(StatusPartida.AguardandoConfirmacao);
        }

        [Fact]
        public async Task MarcarPartidaAsync_PartidaServico_NaoMarcarPartidaSeExistirPartidaNaMesmaData()
        {
            var model = new PartidaModel
            {
                PrimeiroTimeId = _memoryDb.Times.First().Id,
                PrimeiroTimeConfirmado = true,
                SegundoTimeId = _memoryDb.Times.Last().Id,
                SegundoTimeConfirmado = false,
                QuadraFutebolId = _memoryDb.Quadras.ToList()[1].Id,
                HorasPartida = 1,
                DataPartida = new DateTime(2020, 10, 21, 19, 0, 0, DateTimeKind.Local)
            };

            var partidaCriada = await _partidaServico
                .MarcarPartidaAsync(model);

            partidaCriada.Should().BeNull();
        }

        [Fact]
        public async Task DesmarcarPartidaAsync_PartidaServico_DesmarcarPartidaExistente()
        {
            var partidaId = _memoryDb.Partidas.First().Id;

            var partidaDesmarcadaResult = await _partidaServico
                .DesmarcarPartidaAsync(partidaId);

            partidaDesmarcadaResult.Should().NotBeNull();

            var partidaDesmarcada = await _partidaServico
                .ObterPartidaPorIdAsync(partidaId);

            partidaDesmarcada.StatusPartida.Should().Be(StatusPartida.Cancelada);
        }
    }
}
