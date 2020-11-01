using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IScheduleMatchAppService
    {
        Task<IReadOnlyList<ScheduleMatchModel>> GetScheduledMatchesListAsync();
        Task<ScheduleMatchModel> GetScheduledMatchByIdAsync(Guid matchId);
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId);
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId);
        Task<bool> ScheduleMatchAsync(ScheduleMatchModel scheduleMatch);
        Task<bool> UncheckMatchAsync(Guid scheduleMatchId);
        Task<bool> ConfirmMatchAsync(Guid teamId, Guid scheduleMatchId);
    }
}
