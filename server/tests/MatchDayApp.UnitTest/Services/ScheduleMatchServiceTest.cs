using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Services;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "ScheduleMatch")]
    public class ScheduleMatchServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly IScheduleMatchService _scheduleMatchService;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Faker _faker = new Faker("pt_BR");

        public ScheduleMatchServiceTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = configServices
                .GetRequiredService<IUnitOfWork>();

            _scheduleMatchService = new ScheduleMatchService(_uow,
                configServices.GetRequiredService<IMapper>());
        }

        [Fact]
        public async Task GetScheduledMatchesListAsync_ScheduleMatchService_ListAllMatches()
        {
            var matches = await _scheduleMatchService
                .GetScheduledMatchesListAsync();

            matches.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetScheduledMatchByIdAsync_ScheduleMatchService_ReturnMatchWithId()
        {
            var matchId = _memoryDb.ScheduleMatches.First().Id;
            var match = await _scheduleMatchService
                .GetScheduledMatchByIdAsync(matchId);

            match.FirstTeam.Name.Should().Be("Team 3");
            match.SecondTeam.Name.Should().Be("Team 1");
            match.SoccerCourt.Name.Should().Be("Soccer Court 1");
            match.MatchTime.Should().Be(1);
            match.MatchDate.Should().Be(DateTime.Parse("20/10/2020 21:00:00"));
            match.MatchStatus.Should().Be(MatchStatus.Confirmed);
        }

        [Fact]
        public async Task GetScheduledMatchsBySoccerCourtIdAsync_ScheduleMatchService_ListAllMatchesWithSoccerCourtId()
        {
            var soccerCourtId = _memoryDb.SoccerCourts.First().Id;
            var matches = await _scheduleMatchService
                .GetScheduledMatchsBySoccerCourtIdAsync(soccerCourtId);

            matches.Should()
                .HaveCount(1)
                .And.SatisfyRespectively(
                    match1 =>
                    {
                        match1.FirstTeam.Name.Should().Be("Team 3");
                        match1.SecondTeam.Name.Should().Be("Team 2");
                        match1.SoccerCourt.Name.Should().Be("Soccer Court 3");
                        match1.MatchTime.Should().Be(1);
                        match1.MatchDate.Should().Be(DateTime.Parse("16/10/2020 20:00:00"));
                        match1.MatchStatus.Should().Be(MatchStatus.Finished);
                    });
        }

        [Fact]
        public async Task GetScheduledMatchsByTeamIdAsync_ScheduleMatchService_ListAllMatchesWithTeamId()
        {
            var teamId = _memoryDb.Teams.First().Id;
            var matches = await _scheduleMatchService
                .GetScheduledMatchsByTeamIdAsync(teamId);

            matches.Should()
                .HaveCount(3)
                .And.SatisfyRespectively(
                    match1 =>
                    {
                        match1.FirstTeam.Name.Should().Be("Team 3");
                        match1.SecondTeam.Name.Should().Be("Team 1");
                        match1.SoccerCourt.Name.Should().Be("Soccer Court 1");
                        match1.MatchTime.Should().Be(1);
                        match1.MatchDate.Should().Be(DateTime.Parse("20/10/2020 21:00:00"));
                        match1.MatchStatus.Should().Be(MatchStatus.Confirmed);
                    },
                    match2 =>
                    {
                        match2.FirstTeam.Name.Should().Be("Team 2");
                        match2.SecondTeam.Name.Should().Be("Team 3");
                        match2.SoccerCourt.Name.Should().Be("Soccer Court 2");
                        match2.MatchTime.Should().Be(1);
                        match2.MatchDate.Should().Be(DateTime.Parse("19/10/2020 18:00:00"));
                        match2.MatchStatus.Should().Be(MatchStatus.WaitingForConfirmation);
                    },
                    match3 =>
                    {
                        match3.FirstTeam.Name.Should().Be("Team 3");
                        match3.SecondTeam.Name.Should().Be("Team 2");
                        match3.SoccerCourt.Name.Should().Be("Soccer Court 3");
                        match3.MatchTime.Should().Be(1);
                        match3.MatchDate.Should().Be(DateTime.Parse("16/10/2020 20:00:00"));
                        match3.MatchStatus.Should().Be(MatchStatus.Finished);
                    });
        }

        [Fact]
        public async Task ConfirmMatchAsync_ScheduleMatchService_ConfirmedMatch()
        {
            var teamId = _memoryDb.Teams.First().Id;
            var matchId = _memoryDb.ScheduleMatches.ToList()[1].Id;

            var confirmMatchResult = await _scheduleMatchService
                .ConfirmMatchAsync(teamId, matchId);

            confirmMatchResult.Should().BeTrue();

            var confirmedMatch = await _scheduleMatchService
                .GetScheduledMatchByIdAsync(matchId);

            confirmedMatch.SecondTeam.Id.Should().Be(teamId);
            confirmedMatch.SecondTeamConfirmed.Should().BeTrue();
            confirmedMatch.MatchStatus.Should().Be(MatchStatus.Confirmed);
        }

        [Fact]
        public async Task ScheduleMatchAsync_ScheduleMatchService_ScheduledNewMatch()
        {
            var faker = new Faker("pt_BR");
            var newMatch = new ScheduleMatchModel
            {
                FirstTeam = new TeamModel
                {
                    Name = faker.Company.CompanyName(),
                    TotalPlayers = faker.Random.Int(12, 20),
                    Image = faker.Image.PicsumUrl(),
                    OwnerUserId = _memoryDb.Users.Last().Id
                },
                SecondTeam = new TeamModel
                {
                    Name = faker.Company.CompanyName(),
                    TotalPlayers = faker.Random.Int(12, 20),
                    Image = faker.Image.PicsumUrl(),
                    OwnerUserId = _memoryDb.Users.First().Id
                },
                SoccerCourt = new SoccerCourtModel
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
            };

            var confirmNewMatch = await _scheduleMatchService
                .ScheduleMatchAsync(newMatch);

            confirmNewMatch.Should().BeTrue();

            var createdMatch = await _scheduleMatchService
                .GetScheduledMatchesListAsync();

            createdMatch.Should().HaveCount(6);
            createdMatch[createdMatch.Count - 1].FirstTeam.Name.Should().Be(newMatch.FirstTeam.Name);
            createdMatch[createdMatch.Count - 1].FirstTeamConfirmed.Should().BeTrue();
            createdMatch[createdMatch.Count - 1].SecondTeam.Name.Should().Be(newMatch.SecondTeam.Name);
            createdMatch[createdMatch.Count - 1].SecondTeamConfirmed.Should().BeFalse();
            createdMatch[createdMatch.Count - 1].SoccerCourt.Name.Should().Be(newMatch.SoccerCourt.Name);
            createdMatch[createdMatch.Count - 1].MatchDate.Should().Be(newMatch.MatchDate);
            createdMatch[createdMatch.Count - 1].MatchStatus.Should().Be(MatchStatus.WaitingForConfirmation);
        }

        [Fact]
        public async Task ScheduleMatchAsync_ScheduleMatchService_NotScheduledMatchIfAlreadyMatchInDate()
        {
            var faker = new Faker("pt_BR");
            var newMatch = new ScheduleMatchModel
            {
                FirstTeam = new TeamModel
                {
                    Name = faker.Company.CompanyName(),
                    TotalPlayers = faker.Random.Int(12, 20),
                    Image = faker.Image.PicsumUrl(),
                    OwnerUserId = _memoryDb.Users.Last().Id
                },
                SecondTeam = new TeamModel
                {
                    Name = faker.Company.CompanyName(),
                    TotalPlayers = faker.Random.Int(12, 20),
                    Image = faker.Image.PicsumUrl(),
                    OwnerUserId = _memoryDb.Users.First().Id
                },
                SoccerCourt = new SoccerCourtModel
                {
                    Id = _memoryDb.SoccerCourts.ToList()[1].Id,
                    Name = faker.Company.CompanyName(),
                    Image = faker.Image.PicsumUrl(),
                    HourPrice = faker.Random.Decimal(90, 130),
                    Phone = faker.Phone.PhoneNumber("(##) ####-####"),
                    Address = faker.Address.FullAddress(),
                    Cep = faker.Address.ZipCode("#####-###"),
                    Latitude = faker.Address.Latitude(),
                    Longitude = faker.Address.Longitude(),
                    OwnerUserId = _memoryDb.Users.Last().Id
                },
                FirstTeamConfirmed = true,
                SecondTeamConfirmed = false,
                MatchTime = 1,
                MatchDate = new DateTime(2020, 10, 21, 19, 0, 0, DateTimeKind.Local)
            };

            var confirmNewMatch = await _scheduleMatchService
                .ScheduleMatchAsync(newMatch);

            confirmNewMatch.Should().BeFalse();
        }

        [Fact]
        public async Task UncheckMatchAsync_ScheduleMatchService_UncheckExistingMatch()
        {
            var matchId = _memoryDb.ScheduleMatches.First().Id;

            var uncheckedMatchResult = await _scheduleMatchService
                .UncheckMatchAsync(matchId);

            uncheckedMatchResult.Should().BeTrue();

            var uncheckedMatch = await _scheduleMatchService
                .GetScheduledMatchByIdAsync(matchId);

            uncheckedMatch.MatchStatus.Should().Be(MatchStatus.Canceled);
        }
    }
}
