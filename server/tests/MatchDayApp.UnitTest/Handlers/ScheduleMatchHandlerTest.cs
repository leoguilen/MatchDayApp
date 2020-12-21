using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Commands.ScheduleMatch;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.ScheduleMatch;
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
    [Trait("Handler", "ScheduleMatch")]
    public class ScheduleMatchHandlerTest
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _matchId;

        public ScheduleMatchHandlerTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _matchId = _memoryDb.ScheduleMatches.Last().Id;
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_GetAllMatches()
        {
            var getMatchesQuery = new ObterPartidasQuery { };

            var matchesResult = await _mediator.Send(getMatchesQuery);

            matchesResult.Should().HaveCount(5);
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_GetMatchById()
        {
            var getMatchByIdQuery = new ObterPartidaPorIdQuery
            {
                MatchId = _matchId
            };

            var matchResult = await _mediator.Send(getMatchByIdQuery);

            matchResult.MatchDate.Should()
                .Be(new DateTime(2020, 10, 18, 17, 0, 0));
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_GetMatchesBySoccerCourtId()
        {
            var getMatchesBySoccerCourtIdQuery = new ObterPartidasPorQuadraIdQuery
            {
                SoccerCourtId = _memoryDb.SoccerCourts.First().Id
            };

            var matchesResult = await _mediator.Send(getMatchesBySoccerCourtIdQuery);

            matchesResult.Should().HaveCount(1);
            matchesResult.First().MatchDate.Should()
                .Be(new DateTime(2020, 10, 16, 20, 0, 0));
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_GetMatchesByTeamId()
        {
            var getMatchesByTeamIdQuery = new ObterPartidasPorTimeIdQuery
            {
                TeamId = _memoryDb.Teams.ToList()[1].Id
            };

            var matchesResult = await _mediator.Send(getMatchesByTeamIdQuery);

            matchesResult.Should().HaveCount(4);
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_ScheduledNewMatch()
        {
            var faker = new Faker("pt_BR");
            var scheduleMatchCommand = new MarcarPartidaCommand
            {
                Match = new PartidaModel
                {
                    FirstTeam = new TimeModel
                    {
                        Name = faker.Company.CompanyName(),
                        TotalPlayers = faker.Random.Int(12, 20),
                        Image = faker.Image.PicsumUrl(),
                        OwnerUserId = _memoryDb.Users.Last().Id
                    },
                    SecondTeam = new TimeModel
                    {
                        Name = faker.Company.CompanyName(),
                        TotalPlayers = faker.Random.Int(12, 20),
                        Image = faker.Image.PicsumUrl(),
                        OwnerUserId = _memoryDb.Users.First().Id
                    },
                    SoccerCourt = new QuadraModel
                    {
                        Name = faker.Company.CompanyName(),
                        Image = faker.Image.PicsumUrl(),
                        HourPrice = faker.Random.Decimal(90, 130),
                        Phone = faker.Phone.PhoneNumber("(##) ####-####"),
                        Address = faker.Address.FullAddress(),
                        Cep = faker.Address.ZipCode("#####-###"),
                        Latitude = faker.Address.Latitude(),
                        Longitude = faker.Address.Longitude(),
                        OwnerUserId = _memoryDb.Users.ToList()[1].Id
                    },
                    FirstTeamConfirmed = true,
                    SecondTeamConfirmed = false,
                    MatchTime = 1,
                    MatchDate = faker.Date.Recent()
                }
            };

            var cmdResult = await _mediator.Send(scheduleMatchCommand);

            cmdResult.Should().BeTrue();
            _memoryDb.ScheduleMatches.Should().HaveCount(6);
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_ConfirmedMatch()
        {
            var confirmMatchCommand = new ConfirmarPartidaCommand
            {
                TeamId = _memoryDb.Teams.Last().Id,
                MatchId = _matchId
            };

            var cmdResult = await _mediator.Send(confirmMatchCommand);

            cmdResult.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_ScheduleMatchHandler_UncheckedMatch()
        {
            var uncheckMatchCommand = new DesmarcarPartidaCommand
            {
                MatchId = _matchId
            };

            var cmdResult = await _mediator.Send(uncheckMatchCommand);

            cmdResult.Should().BeTrue();
        }
    }
}
