using MatchDayApp.Application.Commands.ScheduleMatch;
using MatchDayApp.Application.Events.SoccerCourtEvents;
using MatchDayApp.Application.Events.TeamEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.ScheduleMatch;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class ScheduleMatchAppService : IScheduleMatchAppService
    {
        private readonly IMediator _mediator;

        public ScheduleMatchAppService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<bool> ConfirmMatchAsync(Guid teamId, Guid scheduleMatchId)
        {
            var confirmMatchCommand = new ConfirmMatchCommand
            {
                TeamId = teamId,
                MatchId = scheduleMatchId
            };

            var result = await _mediator.Send(confirmMatchCommand);

            return result;
        }

        public async Task<ScheduleMatchModel> GetScheduledMatchByIdAsync(Guid matchId)
        {
            var getMatchByIdQuery = new GetMatchByIdQuery { MatchId = matchId };

            var match = await _mediator.Send(getMatchByIdQuery);

            return match;
        }

        public async Task<IReadOnlyList<ScheduleMatchModel>> GetScheduledMatchesListAsync()
        {
            var getMatchesQuery = new GetMatchesQuery { };

            var matches = await _mediator.Send(getMatchesQuery);

            return matches;
        }

        public async Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId)
        {
            var getMatchesBySoccerCourtIdQuery = new GetMatchesBySoccerCourtIdQuery
            {
                SoccerCourtId = soccerCourtId
            };

            var matches = await _mediator.Send(getMatchesBySoccerCourtIdQuery);

            return matches;
        }

        public async Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId)
        {
            var getMatchesByTeamIdQuery = new GetMatchesByTeamIdQuery
            {
                TeamId = teamId
            };

            var matches = await _mediator.Send(getMatchesByTeamIdQuery);

            return matches;
        }

        public async Task<bool> ScheduleMatchAsync(ScheduleMatchModel scheduleMatch)
        {
            var scheduleMatchCommand = new ScheduleMatchCommand
            {
                Match = scheduleMatch
            };

            var result = await _mediator.Send(scheduleMatchCommand);

            if (!result)
                return result;

            await _mediator.Publish(new TeamScheduledMatchEvent { Match = scheduleMatch });
            await _mediator.Publish(new SoccerCourtScheduledMatchEvent { Match = scheduleMatch });

            return result;
        }

        public async Task<bool> UncheckMatchAsync(Guid scheduleMatchId)
        {
            var uncheckMatchCommand = new UncheckMatchCommand { MatchId = scheduleMatchId };

            var result = await _mediator.Send(uncheckMatchCommand);

            if (!result)
                return result;

            await _mediator.Publish(new TeamCanceledMatchEvent { MatchId = scheduleMatchId });
            await _mediator.Publish(new SoccerCourtCanceledMatchEvent { MatchId = scheduleMatchId });

            return result;
        }
    }
}
