using Bogus;
using FluentAssertions;
using MatchDayApp.Infra.Data.Data;
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
    [Trait("Repositories", "TeamRepository")]
    public class TeamRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly ITimeRepositorio _teamRepository;

        private readonly Faker<Time> _fakeTeam;
        private readonly Time _teamTest;
        private readonly object _expectedTeam;

        public TeamRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _teamRepository = new TimeRepositorio(_memoryDb);
            _teamTest = _memoryDb.Teams.First();

            _fakeTeam = new Faker<Time>()
                .RuleFor(u => u.Name, f => f.Company.CompanyName())
                .RuleFor(u => u.Image, f => f.Image.PicsumUrl())
                .RuleFor(u => u.TotalPlayers, f => f.Random.Int(6, 16))
                .RuleFor(u => u.OwnerUserId, f => _memoryDb.Users.First().Id);

            _expectedTeam = new
            {
                Name = "Team 3",
                Image = "team3.png",
                TotalPlayers = 11
            };
        }

        [Fact, Order(1)]
        public void ShouldCanConnectInMemoryDatabase()
        {
            _memoryDb.Database.IsInMemory().Should().BeTrue();
            _memoryDb.Database.CanConnect().Should().BeTrue();
        }

        [Fact, Order(2)]
        public async Task ListAllAsync_Team_AllTeamsRegistered()
        {
            var teams = await _teamRepository
                .ListAllAsync();

            teams.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    team1 =>
                    {
                        team1.Name.Should().Be("Team 3");
                        team1.TotalPlayers.Should().Be(11);
                        team1.OwnerUser.Username.Should().Be("test3");
                        team1.OwnerUser.Email.Should().Be("test3@email.com");
                    },
                    team2 =>
                    {
                        team2.Name.Should().Be("Team 2");
                        team2.TotalPlayers.Should().Be(13);
                        team2.OwnerUser.Username.Should().Be("test2");
                        team2.OwnerUser.Email.Should().Be("test2@email.com");
                    },
                    team3 =>
                    {
                        team3.Name.Should().Be("Team 1");
                        team3.TotalPlayers.Should().Be(15);
                        team3.OwnerUser.Username.Should().Be("test1");
                        team3.OwnerUser.Email.Should().Be("test1@email.com");
                    });
        }

        [Fact, Order(3)]
        public async Task GetByIdAsync_Team_OneTeamWithSameId()
        {
            var team = await _teamRepository
                .GetByIdAsync(_teamTest.Id);

            team.Should().BeEquivalentTo(_expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(4)]
        public async Task GetByIdAsync_Team_NullWithTeamIdNotRegistered()
        {
            var invalidId = Guid.NewGuid();

            var team = await _teamRepository
                .GetByIdAsync(invalidId);

            team.Should().BeNull();
        }

        [Fact, Order(5)]
        public async Task GetAsync_Team_GetTeamWithMatchTheOwnerUserSpecification()
        {
            var spec = new TimeComUsuarioEspecificacao(_teamTest.OwnerUserId);
            var team = (await _teamRepository.GetAsync(spec)).FirstOrDefault();

            team.Should().BeEquivalentTo(_expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(6)]
        public async Task GetAsync_Team_GetTeamWithMatchTheTeamNameSpecification()
        {
            var spec = new TimeComUsuarioEspecificacao("Team 3");
            var team = (await _teamRepository.GetAsync(spec)).FirstOrDefault();

            team.Should().BeEquivalentTo(_expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<Usuario>();
        }

        [Fact, Order(7)]
        public async Task GetAsync_Team_GetTeamsOrdernedByName()
        {
            var teams = await _teamRepository
                .GetAsync(null, x => x.OrderBy(u => u.Name), "OwnerUser", true);

            teams.Should()
                .NotBeNull()
                .And.HaveCount(3)
                .And.SatisfyRespectively(
                    team1 =>
                    {
                        team1.Name.Should().Be("Team 1");
                        team1.TotalPlayers.Should().Be(15);
                        team1.OwnerUser.Username.Should().Be("test1");
                        team1.OwnerUser.Email.Should().Be("test1@email.com");
                    },
                    team2 =>
                    {
                        team2.Name.Should().Be("Team 2");
                        team2.TotalPlayers.Should().Be(13);
                        team2.OwnerUser.Username.Should().Be("test2");
                        team2.OwnerUser.Email.Should().Be("test2@email.com");
                    },
                    team3 =>
                    {
                        team3.Name.Should().Be("Team 3");
                        team3.TotalPlayers.Should().Be(11);
                        team3.OwnerUser.Username.Should().Be("test3");
                        team3.OwnerUser.Email.Should().Be("test3@email.com");
                    });
        }

        [Fact, Order(8)]
        public async Task CountAsync_Team_CountTotalTeamsMatchedTheSpecification()
        {
            const int expectedTotalCount = 1;

            var spec = new TimeComUsuarioEspecificacao("Team 2");
            var teamsCount = await _teamRepository.CountAsync(spec);

            teamsCount.Should()
                .Be(expectedTotalCount);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_Team_AddedListTeams()
        {
            var result = await _teamRepository
                .AddRangeAsync(_fakeTeam.Generate(5));

            result.Should().NotBeNull()
                .And.HaveCount(5);
            _memoryDb.Teams.Should().HaveCount(8);
        }

        [Fact, Order(10)]
        public async Task SaveAsync_Team_UpdatedTeam()
        {
            var teamToUpdate = _memoryDb.Teams.Last();

            teamToUpdate.Name = "Team updated";
            teamToUpdate.Image = "teamupdated.jpg";

            var result = await _teamRepository
                .SaveAsync(teamToUpdate);

            result.Should().NotBeNull()
                .And.BeOfType<Time>();
            result.Name.Should().Be("Team updated");
            result.Image.Should().Be("teamupdated.jpg");
        }

        [Fact, Order(11)]
        public async Task DeleteAsync_Team_UpdatedTeam()
        {
            var teamToDelete = _memoryDb.Teams.Last();

            await _teamRepository
                .DeleteAsync(teamToDelete);

            var deletedTeam = await _teamRepository
                .GetByIdAsync(teamToDelete.Id);

            deletedTeam.Should().BeNull();
            _memoryDb.Teams.Should().HaveCount(2);
        }
    }
}
