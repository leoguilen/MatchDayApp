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

        public async Task<bool> ConfirmMatchAsync(ConfirmarPartidaRequest request)
        {
            var confirmMatchCommand = new ConfirmarPartidaCommand
            {
                TeamId = request.TeamId,
                MatchId = request.MatchId
            };

            var result = await _mediator.Send(confirmMatchCommand);

            return result;
        }

        public async Task<PartidaModel> GetScheduledMatchByIdAsync(Guid matchId)
        {
            var getMatchByIdQuery = new ObterPartidaPorIdQuery { MatchId = matchId };

            var match = await _mediator.Send(getMatchByIdQuery);

            return match;
        }

        public async Task<IReadOnlyList<PartidaModel>> GetScheduledMatchesListAsync(PaginationQuery pagination = null, PartidaFilterQuery filter = null)
        {
            var getMatchesQuery = new ObterPartidasQuery { };

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

        public async Task<IEnumerable<PartidaModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId)
        {
            var getMatchesBySoccerCourtIdQuery = new ObterPartidasPorQuadraIdQuery
            {
                SoccerCourtId = soccerCourtId
            };

            var matches = await _mediator.Send(getMatchesBySoccerCourtIdQuery);

            return matches;
        }

        public async Task<IEnumerable<PartidaModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId)
        {
            var getMatchesByTeamIdQuery = new ObterPartidasPorTimeIdQuery
            {
                TeamId = teamId
            };

            var matches = await _mediator.Send(getMatchesByTeamIdQuery);

            return matches;
        }

        public async Task<bool> ScheduleMatchAsync(MarcarPartidaRequest request)
        {
            var matchModel = _mapper
                    .Map<PartidaModel>(request);

            var scheduleMatchCommand = new MarcarPartidaCommand
            {
                Match = matchModel
            };

            var result = await _mediator.Send(scheduleMatchCommand);

            if (!result)
                return result;

            await _mediator.Publish(new TimeMarcadoPartidaEvent { Match = matchModel });
            await _mediator.Publish(new QuadraPartidaMarcadaEvent { Match = matchModel });

            return result;
        }

        public async Task<bool> UncheckMatchAsync(Guid scheduleMatchId)
        {
            var uncheckMatchCommand = new DesmarcarPartidaCommand { MatchId = scheduleMatchId };

            var result = await _mediator.Send(uncheckMatchCommand);

            if (!result)
                return result;

            await _mediator.Publish(new TimeCanceladaPartidaEvent { MatchId = scheduleMatchId });
            await _mediator.Publish(new QuadraPartidaCanceladaEvent { MatchId = scheduleMatchId });

            return result;
        }
    }
}
