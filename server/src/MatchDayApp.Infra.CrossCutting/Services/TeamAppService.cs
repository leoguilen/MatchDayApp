using MatchDayApp.Application.Commands.Team;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Team;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class TeamAppService : ITeamAppService
    {
        private readonly IMediator _mediator;

        public TeamAppService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> AddTeamAsync(TeamModel team)
        {
            var addTeamCommand = new AddTeamCommand
            {
                Team = team
            };

            var result = await _mediator.Send(addTeamCommand);

            return result;
        }

        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            var deleteTeamCommand = new DeleteTeamCommand
            {
                Id = teamId
            };

            var result = await _mediator.Send(deleteTeamCommand);

            return result;
        }

        public async Task<TeamModel> GetTeamByIdAsync(Guid teamId)
        {
            var getTeamByIdQuery = new GetTeamDetailsByIdQuery
            {
                Id = teamId
            };

            var team = await _mediator.Send(getTeamByIdQuery);

            return team;
        }

        public async Task<IReadOnlyList<TeamModel>> GetTeamsListAsync()
        {
            var getTeamsQuery = new GetTeamsQuery { };

            var teams = await _mediator.Send(getTeamsQuery);

            return teams;
        }

        public async Task<bool> UpdateTeamAsync(Guid teamId, TeamModel teamModel)
        {
            var updateTeamCommand = new UpdateTeamCommand
            {
                Id = teamId,
                Team = teamModel
            };

            var result = await _mediator.Send(updateTeamCommand);

            return result;
        }
    }
}
