using MatchDayApp.Application.Commands.Team;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.Team;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class TeamHandler :
        IRequestHandler<AddTeamCommand, bool>,
        IRequestHandler<DeleteTeamCommand, bool>,
        IRequestHandler<UpdateTeamCommand, bool>,
        IRequestHandler<GetTeamDetailsByIdQuery, TimeModel>,
        IRequestHandler<GetTeamsQuery, IReadOnlyList<TimeModel>>
    {
        private readonly ITimeServico _teamService;

        public TeamHandler(ITimeServico teamService)
        {
            _teamService = teamService;
        }

        public async Task<bool> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            return await _teamService.AddTeamAsync(request.Team);
        }

        public async Task<bool> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            return await _teamService.DeleteTeamAsync(request.Id);
        }

        public async Task<bool> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            return await _teamService.UpdateTeamAsync(request.Id, request.Team);
        }

        public async Task<TimeModel> Handle(GetTeamDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            return await _teamService.GetTeamByIdAsync(request.Id);
        }

        public async Task<IReadOnlyList<TimeModel>> Handle(GetTeamsQuery request, CancellationToken cancellationToken)
        {
            return await _teamService.GetTeamsListAsync();
        }
    }
}
