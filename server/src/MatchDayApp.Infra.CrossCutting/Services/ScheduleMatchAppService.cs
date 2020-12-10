using AutoMapper;
using MatchDayApp.Application.Commands.ScheduleMatch;
using MatchDayApp.Application.Events.SoccerCourtEvents;
using MatchDayApp.Application.Events.TeamEvents;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Queries.ScheduleMatch;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services
{
    public class ScheduleMatchAppService : IScheduleMatchAppService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ScheduleMatchAppService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator
                ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ConfirmMatchAsync(ConfirmMatchRequest request)
        {
            var confirmMatchCommand = new ConfirmMatchCommand
            {
                TeamId = request.TeamId,
                MatchId = request.MatchId
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

        public async Task<IReadOnlyList<ScheduleMatchModel>> GetScheduledMatchesListAsync(PaginationQuery pagination = null, MatchFilterQuery filter = null)
        {
            var getMatchesQuery = new GetMatchesQuery { };

            var matches = await _mediator.Send(getMatchesQuery);

            var skip = (pagination.PageNumber - 1) * pagination.PageSize;

            if (!(filter is null))
            {
                return matches
                    .Where(m => m.SoccerCourt.Id == filter.SoccerCourtId)
                    .Where(m => m.FirstTeam.Id == filter.TeamId || m.SecondTeam.Id == filter.TeamId)
                    .Skip(skip)
                    .Take(pagination.PageSize)
                    .ToList();
            }

            return matches
                    .Skip(skip)
                    .Take(pagination.PageSize)
                    .ToList();
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

        public async Task<bool> ScheduleMatchAsync(ScheduleMatchRequest request)
        {
            var matchModel = _mapper
                    .Map<ScheduleMatchModel>(request);

            var scheduleMatchCommand = new ScheduleMatchCommand
            {
                Match = matchModel
            };

            var result = await _mediator.Send(scheduleMatchCommand);

            if (!result)
                return result;

            await _mediator.Publish(new TeamScheduledMatchEvent { Match = matchModel });
            await _mediator.Publish(new SoccerCourtScheduledMatchEvent { Match = matchModel });

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
