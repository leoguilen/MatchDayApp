using AutoMapper;
using MatchDayApp.Application.Commands.Team;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Team;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class TeamAppService : ITeamAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TeamAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddTeamAsync(CreateTeamRequest team)
        {
            var addTeamCommand = new AddTeamCommand
            {
                Team = _mapper
                    .Map<TimeModel>(team)
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

        public async Task<TimeModel> GetTeamByIdAsync(Guid teamId)
        {
            var getTeamByIdQuery = new GetTeamDetailsByIdQuery
            {
                Id = teamId
            };

            var team = await _mediator.Send(getTeamByIdQuery);

            return team;
        }

        public async Task<IReadOnlyList<TimeModel>> GetTeamsListAsync(PaginationQuery pagination = null)
        {
            var getTeamsQuery = new GetTeamsQuery { };

            var teams = await _mediator.Send(getTeamsQuery);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            return teams
                .Skip(skip)
                .Take(pagination.PageSize)
                .ToList();
        }

        public async Task<bool> UpdateTeamAsync(Guid teamId, UpdateTeamRequest team)
        {
            var updateTeamCommand = new UpdateTeamCommand
            {
                Id = teamId,
                Team = _mapper
                    .Map<TimeModel>(team)
            };

            var result = await _mediator.Send(updateTeamCommand);

            return result;
        }
    }
}
