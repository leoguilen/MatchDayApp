using MatchDayApp.Application.Commands.ScheduleMatch;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.ScheduleMatch;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class ScheduleMatchHandler :
        IRequestHandler<ConfirmMatchCommand, bool>,
        IRequestHandler<ScheduleMatchCommand, bool>,
        IRequestHandler<UncheckMatchCommand, bool>,
        IRequestHandler<GetMatchByIdQuery, ScheduleMatchModel>,
        IRequestHandler<GetMatchesBySoccerCourtIdQuery, IEnumerable<ScheduleMatchModel>>,
        IRequestHandler<GetMatchesByTeamIdQuery, IEnumerable<ScheduleMatchModel>>,
        IRequestHandler<GetMatchesQuery, IReadOnlyList<ScheduleMatchModel>>
    {
        private readonly IScheduleMatchService _scheduleMatchService;

        public ScheduleMatchHandler(IScheduleMatchService scheduleMatchService)
        {
            _scheduleMatchService = scheduleMatchService ?? throw new System.ArgumentNullException(nameof(scheduleMatchService));
        }

        public async Task<bool> Handle(ConfirmMatchCommand request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .ConfirmMatchAsync(request.TeamId, request.MatchId);
        }

        public async Task<bool> Handle(ScheduleMatchCommand request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .ScheduleMatchAsync(request.Match);
        }

        public async Task<bool> Handle(UncheckMatchCommand request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .UncheckMatchAsync(request.MatchId);
        }

        public async Task<ScheduleMatchModel> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .GetScheduledMatchByIdAsync(request.MatchId);
        }

        public async Task<IEnumerable<ScheduleMatchModel>> Handle(GetMatchesBySoccerCourtIdQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .GetScheduledMatchsBySoccerCourtIdAsync(request.SoccerCourtId);
        }

        public async Task<IEnumerable<ScheduleMatchModel>> Handle(GetMatchesByTeamIdQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .GetScheduledMatchsByTeamIdAsync(request.TeamId);
        }

        public async Task<IReadOnlyList<ScheduleMatchModel>> Handle(GetMatchesQuery request, CancellationToken cancellationToken)
        {
            return await _scheduleMatchService
                .GetScheduledMatchesListAsync();
        }
    }
}
