using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class ScheduleMatchService : IScheduleMatchService
    {
        public Task<bool> ConfirmMatchAsync(Guid teamId, Guid scheduleMatchId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<ScheduleMatchModel>> GetScheduledMatchesListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ScheduleMatchAsync(ScheduleMatchModel scheduleMatch)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UncheckMatchAsync(Guid scheduleMatchId)
        {
            throw new NotImplementedException();
        }
    }
}
