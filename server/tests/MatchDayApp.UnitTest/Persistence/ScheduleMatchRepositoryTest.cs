using FluentAssertions;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace MatchDayApp.UnitTest.Persistence
{
    [Trait("Repository", "ScheduleMatch")]
    public class ScheduleMatchRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly IScheduleMatchRepository _matchRepository;

        private readonly ScheduleMatch _matchTest;

        public ScheduleMatchRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _matchRepository = new ScheduleMatchRepository(_memoryDb);
            _matchTest = _memoryDb.ScheduleMatches.First();
        }

        [Fact, Order(1)]
        public void ShouldCanConnectInMemoryDatabase()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task GetMatchesAsync_ScheduleMatch_AllMatchesRegistered()
        {
            var matches = await _matchRepository
                .GetMatchesAsync();

            matches.Should()
                .NotBeNull()
                .And.HaveCount(5)
                .And.SatisfyRespectively(
                    match1 =>
                    {
                        match1.FirstTeam.Name.Should().Be("Team 3");
                        match1.FirstTeamConfirmed.Should().BeTrue();
                        match1.SecondTeam.Name.Should().Be("Team 1");
                        match1.SecondTeamConfirmed.Should().BeTrue();
                        match1.SoccerCourt.Name.Should().Be("Soccer Court 1");
                        match1.Date.Should().Be(DateTime.Parse("20/10/2020 21:00:00"));
                        match1.MatchStatus.Should().Be(1);
                    },
                    match2 =>
                    {
                        match2.FirstTeam.Name.Should().Be("Team 2");
                        match2.FirstTeamConfirmed.Should().BeTrue();
                        match2.SecondTeam.Name.Should().Be("Team 3");
                        match2.SecondTeamConfirmed.Should().BeFalse();
                        match2.SoccerCourt.Name.Should().Be("Soccer Court 2");
                        match2.Date.Should().Be(DateTime.Parse("19/10/2020 18:00:00"));
                        match2.MatchStatus.Should().Be(3);
                    },
                    match3 =>
                    {
                        match3.FirstTeam.Name.Should().Be("Team 2");
                        match3.FirstTeamConfirmed.Should().BeTrue();
                        match3.SecondTeam.Name.Should().Be("Team 1");
                        match3.SecondTeamConfirmed.Should().BeTrue();
                        match3.SoccerCourt.Name.Should().Be("Soccer Court 2");
                        match3.Date.Should().Be(DateTime.Parse("21/10/2020 19:00:00"));
                        match3.MatchStatus.Should().Be(1);
                    },
                    match4 =>
                    {
                        match4.FirstTeam.Name.Should().Be("Team 3");
                        match4.FirstTeamConfirmed.Should().BeTrue();
                        match4.SecondTeam.Name.Should().Be("Team 2");
                        match4.SecondTeamConfirmed.Should().BeTrue();
                        match4.SoccerCourt.Name.Should().Be("Soccer Court 3");
                        match4.Date.Should().Be(DateTime.Parse("16/10/2020 20:00:00"));
                        match4.MatchStatus.Should().Be(4);
                    },
                    match5 =>
                    {
                        match5.FirstTeam.Name.Should().Be("Team 1");
                        match5.FirstTeamConfirmed.Should().BeFalse();
                        match5.SecondTeam.Name.Should().Be("Team 2");
                        match5.SecondTeamConfirmed.Should().BeTrue();
                        match5.SoccerCourt.Name.Should().Be("Soccer Court 1");
                        match5.Date.Should().Be(DateTime.Parse("18/10/2020 17:00:00"));
                        match5.MatchStatus.Should().Be(2);
                    });
        }

        [Fact, Order(3)]
        public async Task GetMatchByIdAsync_ScheduleMatch_GetOneMatchById()
        {
            var match = await _matchRepository
                .GetMatchByIdAsync(_matchTest.Id);

            match.Should().NotBeNull();
            match.FirstTeam.Name.Should().Be("Team 3");
            match.FirstTeamConfirmed.Should().BeTrue();
            match.SecondTeam.Name.Should().Be("Team 1");
            match.SecondTeamConfirmed.Should().BeTrue();
            match.SoccerCourt.Name.Should().Be("Soccer Court 1");
            match.TotalHours.Should().Be(1);
            match.Date.Should().Be(DateTime.Parse("20/10/2020 21:00:00"));
            match.MatchStatus.Should().Be(MatchStatus.Confirmed);
        }

        [Fact, Order(4)]
        public async Task GetAsync_ScheduleMatch_GetMatchesInSoccerCourt()
        {
            var soccerCourtId = _memoryDb.SoccerCourts.Last().Id;

            var matches = await _matchRepository
                .GetAsync(m => m.SoccerCourtId == soccerCourtId);

            matches.Should()
                .HaveCount(2)
                .And.SatisfyRespectively(
                match1 =>
                {
                    match1.FirstTeam.Name.Should().Be("Team 3");
                    match1.FirstTeamConfirmed.Should().BeTrue();
                    match1.SecondTeam.Name.Should().Be("Team 1");
                    match1.SecondTeamConfirmed.Should().BeTrue();
                    match1.SoccerCourt.Name.Should().Be("Soccer Court 1");
                    match1.Date.Should().Be(DateTime.Parse("20/10/2020 21:00:00"));
                    match1.MatchStatus.Should().Be(1);
                },
                match2 =>
                {
                    match2.FirstTeam.Name.Should().Be("Team 1");
                    match2.FirstTeamConfirmed.Should().BeFalse();
                    match2.SecondTeam.Name.Should().Be("Team 2");
                    match2.SecondTeamConfirmed.Should().BeTrue();
                    match2.SoccerCourt.Name.Should().Be("Soccer Court 1");
                    match2.Date.Should().Be(DateTime.Parse("18/10/2020 17:00:00"));
                    match2.MatchStatus.Should().Be(2);
                });
        }

        [Fact, Order(5)]
        public async Task AddMatchAsync_ScheduleMatch_NewScheduledMatch()
        {
            var newMatch = new ScheduleMatch
            {
                FirstTeamId = _memoryDb.Teams.First().Id,
                FirstTeamConfirmed = true,
                SecondTeamId = _memoryDb.Teams.Last().Id,
                SecondTeamConfirmed = false,
                SoccerCourtId = _memoryDb.SoccerCourts.First().Id,
                TotalHours = 1,
                Date = DateTime.Parse("25/10/2020 19:30:00"),
                MatchStatus = MatchStatus.WaitingForConfirmation
            };

            var result = await _matchRepository
                .AddMatchAsync(newMatch);

            result.Should().BeTrue();
        }

        [Fact, Order(6)]
        public async Task UpdateMatchAsync_ScheduleMatch_UpdateExitingMatch()
        {
            _matchTest.Date = DateTime.Parse("26/10/2020 18:30:00");
            _matchTest.MatchStatus = MatchStatus.Canceled;

            var result = await _matchRepository
                .UpdateMatchAsync(_matchTest);

            result.Should().BeTrue();
            _matchTest.Date.Should().Be(DateTime.Parse("26/10/2020 18:30:00"));
            _matchTest.MatchStatus.Should().Be(MatchStatus.Canceled);
        }
    }
}
