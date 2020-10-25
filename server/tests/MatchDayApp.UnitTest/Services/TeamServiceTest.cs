using AutoMapper;
using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Services;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "Team")]
    public class TeamServiceTest
    {
        private readonly IUnitOfWork _uow;
        private readonly ITeamService _teamService;
        private readonly MatchDayAppContext _memoryDb;

        private readonly Guid _teamId;

        public TeamServiceTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();
            _uow = cfg.GetRequiredService<IUnitOfWork>();

            _teamService = new TeamService(_uow,
                cfg.GetRequiredService<IMapper>());

            _teamId = _memoryDb.Teams.Last().Id;
        }

        [Fact]
        public async Task DeleteTeamAsync_TeamService_DeletedTeamInSystem()
        {
            var cmdResult = await _teamService
                .DeleteTeamAsync(_teamId);

            cmdResult.Should().BeTrue();

            var deletedTeam = await _uow.Users
                .GetByIdAsync(_teamId);

            deletedTeam.Should().BeNull();
        }

        [Fact]
        public async Task GetTeamByIdAsync_TeamService_GetTeamIfSameId()
        {
            var team = await _teamService
                .GetTeamByIdAsync(_teamId);

            team.Name.Should().Be("Team 1");
        }

        [Fact]
        public async Task GetTeamsListAsync_TeamService_ListAllTeams()
        {
            var teams = await _teamService
                .GetTeamsListAsync();

            teams.Should().HaveCount(3);
        }

        [Fact]
        public async Task UpdateTeamAsync_TeamService_UpdatedTeamInSystem()
        {
            var newTeamName = new Faker().Company.CompanyName();
            var newImage = new Faker().Image.PicsumUrl();

            var team = await _teamService
                .GetTeamByIdAsync(_teamId);
            team.Name = newTeamName;
            team.Image = newImage;

            var cmdResult = await _teamService
                .UpdateTeamAsync(_teamId, team);

            cmdResult.Should().BeTrue();

            var updatedTeam = await _teamService
                .GetTeamByIdAsync(_teamId);

            updatedTeam.Name.Should().Be(newTeamName);
            updatedTeam.Image.Should().Be(newImage);
        }

        [Fact]
        public async Task AddTeamAsync_TeamService_AddedTeamInSystem()
        {
            var faker = new Faker("pt_BR");
            var newTeam = new TeamModel
            {
                Name = faker.Company.CompanyName(),
                Image = faker.Image.PicsumUrl(),
                TotalPlayers = faker.Random.Int(10, 20),
                OwnerUserId = _memoryDb.Users.First().Id
            };

            var cmdResult = await _teamService.AddTeamAsync(newTeam);

            cmdResult.Should().BeTrue();

            var insertedTeam = _memoryDb.Teams.Last();

            insertedTeam.Name.Should().Be(newTeam.Name);
            insertedTeam.Image.Should().Be(newTeam.Image);
            insertedTeam.TotalPlayers.Should().Be(newTeam.TotalPlayers);
            insertedTeam.OwnerUserId.Should().Be(newTeam.OwnerUserId);
        }
    }
}
