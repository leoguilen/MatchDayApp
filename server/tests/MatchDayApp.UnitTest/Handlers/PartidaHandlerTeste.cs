using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Comandos.Partida;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Partida;
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
    [Trait("Handler", "Partida")]
    public class PartidaHandlerTeste
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _partidaId;

        public PartidaHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _partidaId = _memoryDb.Partidas.Last().Id;
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarListaComTodasPartidas()
        {
            var query = new ObterPartidasQuery { };

            var partidasResult = await _mediator.Send(query);

            partidasResult.Should().HaveCount(5);
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidaPorId()
        {
            var query = new ObterPartidaPorIdQuery
            {
                PartidaId = _partidaId
            };

            var partidaResult = await _mediator.Send(query);

            partidaResult.DataPartida.Should()
                .Be(new DateTime(2020, 10, 18, 17, 0, 0));
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidasPelaQuadra()
        {
            var query = new ObterPartidasPorQuadraIdQuery
            {
                QuadraId = _memoryDb.Quadras.First().Id
            };

            var partidasResult = await _mediator.Send(query);

            partidasResult.Should().HaveCount(1);
            partidasResult.First().DataPartida.Should()
                .Be(new DateTime(2020, 10, 16, 20, 0, 0));
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidasPeloTime()
        {
            var query = new ObterPartidasPorTimeIdQuery
            {
                TimeId = _memoryDb.Times.ToList()[1].Id
            };

            var partidasResult = await _mediator.Send(query);

            partidasResult.Should().HaveCount(4);
        }

        [Fact]
        public async Task Handle_PartidaHandler_MarcarNovaPartida()
        {
            var faker = new Faker("pt_BR");
            var comando = new MarcarPartidaCommand
            {
                Partida = new PartidaModel
                {
                    PrimeiroTimeId = _memoryDb.Times.First().Id,
                    PrimeiroTimeConfirmado = true,
                    SegundoTimeId = _memoryDb.Times.Last().Id,
                    SegundoTimeConfirmado = false,
                    QuadraFutebolId = _memoryDb.Quadras.First().Id,
                    HorasPartida = 1,
                    DataPartida = faker.Date.Recent()
                }
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();
            _memoryDb.Partidas.Should().HaveCount(6);
        }

        [Fact]
        public async Task Handle_PartidaHandler_ConfirmarPartidaPendente()
        {
            var comando = new ConfirmarPartidaCommand
            {
                TimeId = _memoryDb.Times.Last().Id,
                PartidaId = _partidaId
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_PartidaHandler_DesmarcarPartida()
        {
            var comando = new DesmarcarPartidaCommand
            {
                PartidaId = _partidaId
            };

            var cmdResult = await _mediator.Send(comando);

            cmdResult.Should().NotBeNull();
        }
    }
}
