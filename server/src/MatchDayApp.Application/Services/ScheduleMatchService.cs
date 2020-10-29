using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class ScheduleMatchService : IScheduleMatchService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ScheduleMatchService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ConfirmMatchAsync(Guid teamId, Guid scheduleMatchId)
        {
            var match = await _uow.ScheduleMatches
                .GetMatchByIdAsync(scheduleMatchId);

            if (match.FirstTeamId == teamId)
                if (!match.FirstTeamConfirmed)
                {
                    match.FirstTeamConfirmed = true;
                    match.MatchStatus = MatchStatus.WaitingForConfirmation;
                }

            if (match.SecondTeamId == teamId)
                if (!match.SecondTeamConfirmed)
                {
                    match.SecondTeamConfirmed = true;
                    match.MatchStatus = MatchStatus.WaitingForConfirmation;
                }

            if (match.FirstTeamConfirmed && match.SecondTeamConfirmed)
                match.MatchStatus = MatchStatus.Confirmed;

            await _uow.ScheduleMatches.UpdateMatchAsync(match);
            var commited = await _uow.CommitAsync();

            return commited > 0;
        }

        public async Task<ScheduleMatchModel> GetScheduledMatchByIdAsync(Guid matchId)
        {
            var match = await _uow.ScheduleMatches
                .GetMatchByIdAsync(matchId);

            return _mapper.Map<ScheduleMatchModel>(match);
        }

        public async Task<IReadOnlyList<ScheduleMatchModel>> GetScheduledMatchesListAsync()
        {
            var matches = await _uow.ScheduleMatches
                .GetMatchesAsync();

            return _mapper.Map<IReadOnlyList<ScheduleMatchModel>>(matches);
        }

        public async Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId)
        {
            var matches = await _uow.ScheduleMatches
                .GetAsync(m => m.SoccerCourtId == soccerCourtId);

            return _mapper.Map<IEnumerable<ScheduleMatchModel>>(matches);
        }

        public async Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId)
        {
            var matches = await _uow.ScheduleMatches
                .GetAsync(m => m.FirstTeamId == teamId || m.SecondTeamId == teamId);

            return _mapper.Map<IEnumerable<ScheduleMatchModel>>(matches);
        }

        public async Task<bool> ScheduleMatchAsync(ScheduleMatchModel scheduleMatch)
        {
            var hasScheduleMatch = await _uow.ScheduleMatches
                .GetAsync(sc => sc.SoccerCourtId == scheduleMatch.SoccerCourt.Id
                    && sc.Date == scheduleMatch.MatchDate);

            if (hasScheduleMatch.Any())
                return false;

            var scModel = _mapper.Map<ScheduleMatch>(scheduleMatch);
            scModel.MatchStatus = MatchStatus.WaitingForConfirmation;
            scModel.Date = scheduleMatch.MatchDate;

            await _uow.ScheduleMatches.AddMatchAsync(scModel);
            await _uow.CommitAsync();

            return true;
        }

        public async Task<bool> UncheckMatchAsync(Guid scheduleMatchId)
        {
            var match = await _uow.ScheduleMatches
                .GetMatchByIdAsync(scheduleMatchId);

            if (match == null)
                return false;

            match.MatchStatus = MatchStatus.Canceled;

            await _uow.ScheduleMatches.UpdateMatchAsync(match);
            await _uow.CommitAsync();

            return true;
        }
    }
}
