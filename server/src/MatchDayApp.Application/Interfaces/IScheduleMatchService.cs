using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IScheduleMatchService
    {
        Task<IReadOnlyCollection<ScheduleMatchModel>> GetScheduledMatchesListAsync();
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId);
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId);
        Task<bool> ScheduleMatchAsync(ScheduleMatchModel scheduleMatch);
        Task<bool> UncheckMatchAsync(Guid scheduleMatchId);
        Task<bool> ConfirmMatchAsync(Guid teamId, Guid scheduleMatchId);
    }
}
