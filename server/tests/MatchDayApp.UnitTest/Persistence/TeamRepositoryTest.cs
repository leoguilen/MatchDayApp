﻿using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Domain.Specification.TeamSpec;
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
    [Trait("Repositories", "TeamRepository")]
    public class TeamRepositoryTest
    {
        private readonly MatchDayAppContext _memoryDb;
        private readonly ITeamRepository _teamRepository;

        private readonly Faker<Team> _fakeUser;

        public TeamRepositoryTest()
        {
            var configServices = ServicesConfiguration.Configure();

            _memoryDb = configServices
                .GetRequiredService<MatchDayAppContext>()
                .SeedFakeTeamData();

            _teamRepository = new TeamRepository(_memoryDb);

            _fakeUser = new Faker<Team>()
                .RuleFor(u => u.Name, f => f.Company.CompanyName())
                .RuleFor(u => u.Image, f => f.Image.PicsumUrl())
                .RuleFor(u => u.TotalPlayers, f => f.Random.Int(6, 16))
                .RuleFor(u => u.OwnerUserId, f => _memoryDb.Users.First().Id);
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
            var teamFake = _memoryDb.Teams.First();
            var expectedTeam = new
            {
                Id = teamFake.Id,
                Name = "Team 3",
                Image = "team3.png",
                TotalPlayers = 11,
                OwnerUserId = teamFake.OwnerUserId
            };

            var team = await _teamRepository
                .GetByIdAsync(teamFake.Id);

            team.Should().BeEquivalentTo(expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<User>();
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
            var teamFake = _memoryDb.Teams.Last();
            var expectedTeam = new
            {
                Id = teamFake.Id,
                Name = "Team 1",
                Image = "team1.png",
                TotalPlayers = 15
            };

            var spec = new TeamWithUserSpecification(teamFake.OwnerUserId);
            var team = (await _teamRepository.GetAsync(spec)).FirstOrDefault();

            team.Should().BeEquivalentTo(expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<User>();
        }

        [Fact, Order(6)]
        public async Task GetAsync_Team_GetTeamWithMatchTheTeamNameSpecification()
        {
            var teamFake = _memoryDb.Teams.Last();
            var expectedTeam = new
            {
                Id = teamFake.Id,
                Name = "Team 1",
                Image = "team1.png",
                TotalPlayers = 15,
                OwnerUserId = teamFake.OwnerUserId
            };

            var spec = new TeamWithUserSpecification("Team 1");
            var team = (await _teamRepository.GetAsync(spec)).FirstOrDefault();

            team.Should().BeEquivalentTo(expectedTeam, options =>
                options.ExcludingMissingMembers());
            team.OwnerUser.Should()
                .NotBeNull()
                .And.BeOfType<User>();
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

            var spec = new TeamWithUserSpecification("Team 2");
            var teamsCount = await _teamRepository.CountAsync(spec);

            teamsCount.Should()
                .Be(expectedTotalCount);
        }

        [Fact, Order(9)]
        public async Task AddRangeAsync_Team_AddedListTeams()
        {
            var result = await _teamRepository
                .AddRangeAsync(_fakeUser.Generate(5));

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
                .And.BeOfType<Team>();
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
