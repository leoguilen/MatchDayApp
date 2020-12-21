﻿using Bogus;
using FluentAssertions;
using MatchDayApp.Application.Models;
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
    [Trait("Handler", "Team")]
    public class TeamHandlerTest
    {
        private readonly IMediator _mediator;
        private readonly MatchDayAppContext _memoryDb;
        private readonly Guid _teamId;
        private readonly Faker _faker;

        public TeamHandlerTest()
        {
            var cfg = ServicesConfiguration.Configure();

            _memoryDb = cfg.GetRequiredService<MatchDayAppContext>()
                .SeedFakeData();

            _mediator = cfg.GetRequiredService<IMediator>();
            _teamId = _memoryDb.Teams.Last().Id;

            _faker = new Faker("pt_BR");
        }

        [Fact]
        public async Task Handle_TeamHandler_GetAllTeams()
        {
            var getTeamsQuery = new ObterTimesQuery { };

            var teamsResult = await _mediator.Send(getTeamsQuery);

            teamsResult.Should().HaveCount(3);
        }

        [Fact]
        public async Task Handle_TeamHandler_GetTeamById()
        {
            var getTeamByIdQuery = new ObterTimePorIdQuery
            {
                Id = _teamId
            };

            var teamResult = await _mediator.Send(getTeamByIdQuery);

            teamResult.Name.Should().Be("Team 1");
        }

        [Fact]
        public async Task Handle_TeamHandler_AddedTeam()
        {
            var addTeamCommand = new AdicionarTimeCommand
            {
                Team = new TimeModel
                {
                    Name = _faker.Company.CompanyName(),
                    Image = _faker.Image.PicsumUrl(),
                    TotalPlayers = _faker.Random.Int(10, 20),
                    OwnerUserId = _memoryDb.Users.ToList()[1].Id
                }
            };

            var cmdResult = await _mediator.Send(addTeamCommand);

            cmdResult.Should().BeTrue();

            var insertedTeam = _memoryDb.Teams.Last();

            insertedTeam.Name.Should().Be(addTeamCommand.Team.Name);
            insertedTeam.Image.Should().Be(addTeamCommand.Team.Image);
            insertedTeam.TotalPlayers.Should().Be(addTeamCommand.Team.TotalPlayers);
            insertedTeam.OwnerUserId.Should().Be(addTeamCommand.Team.OwnerUserId);
        }

        [Fact]
        public async Task Handle_TeamHandler_UpdatedTeam()
        {
            var updateTeamCommand = new AtualizarTimeCommand
            {
                Id = _teamId,
                Team = new TimeModel
                {
                    Name = _faker.Company.CompanyName(),
                    Image = _faker.Image.PicsumUrl()
                }
            };

            var cmdResult = await _mediator.Send(updateTeamCommand);

            cmdResult.Should().BeTrue();

            var updatedTeam = _memoryDb.Teams.Last();

            updatedTeam.Name.Should().Be(updateTeamCommand.Team.Name);
            updatedTeam.Image.Should().Be(updateTeamCommand.Team.Image);
        }

        [Fact]
        public async Task Handle_TeamHandler_DeletedTeam()
        {
            var deleteTeamCommand = new DeletarTimeCommand
            {
                Id = _teamId
            };

            var cmdResult = await _mediator.Send(deleteTeamCommand);

            cmdResult.Should().BeTrue();
        }
    }
}
