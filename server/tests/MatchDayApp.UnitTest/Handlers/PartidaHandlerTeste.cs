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
        private readonly Guid _matchId;

        public PartidaHandlerTeste()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _matchId = _memoryDb.Partidas.Last().Id;
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarListaComTodasPartidas()
        {
            var getMatchesQuery = new ObterPartidasQuery { };

            var matchesResult = await _mediator.Send(getMatchesQuery);

            matchesResult.Should().HaveCount(5);
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidaPorId()
        {
            var getMatchByIdQuery = new ObterPartidaPorIdQuery
            {
                PartidaId = _matchId
            };

            var matchResult = await _mediator.Send(getMatchByIdQuery);

            matchResult.DataPartida.Should()
                .Be(new DateTime(2020, 10, 18, 17, 0, 0));
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidasPelaQuadra()
        {
            var getMatchesBySoccerCourtIdQuery = new ObterPartidasPorQuadraIdQuery
            {
                QuadraId = _memoryDb.Quadras.First().Id
            };

            var matchesResult = await _mediator.Send(getMatchesBySoccerCourtIdQuery);

            matchesResult.Should().HaveCount(1);
            matchesResult.First().DataPartida.Should()
                .Be(new DateTime(2020, 10, 16, 20, 0, 0));
        }

        [Fact]
        public async Task Handle_PartidaHandler_RetornarPartidasPeloTime()
        {
            var getMatchesByTeamIdQuery = new ObterPartidasPorTimeIdQuery
            {
                TimeId = _memoryDb.Times.ToList()[1].Id
            };

            var matchesResult = await _mediator.Send(getMatchesByTeamIdQuery);

            matchesResult.Should().HaveCount(4);
        }

        [Fact]
        public async Task Handle_PartidaHandler_MarcarNovaPartida()
        {
            var faker = new Faker("pt_BR");
            var scheduleMatchCommand = new MarcarPartidaCommand
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

            var cmdResult = await _mediator.Send(scheduleMatchCommand);

            cmdResult.Should().NotBeNull();
            _memoryDb.Partidas.Should().HaveCount(6);
        }

        [Fact]
        public async Task Handle_PartidaHandler_ConfirmarPartidaPendente()
        {
            var confirmMatchCommand = new ConfirmarPartidaCommand
            {
                TimeId = _memoryDb.Times.Last().Id,
                PartidaId = _matchId
            };

            var cmdResult = await _mediator.Send(confirmMatchCommand);

            cmdResult.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_PartidaHandler_DesmarcarPartida()
        {
            var uncheckMatchCommand = new DesmarcarPartidaCommand
            {
                PartidaId = _matchId
            };

            var cmdResult = await _mediator.Send(uncheckMatchCommand);

            cmdResult.Should().NotBeNull();
        }
    }
}
