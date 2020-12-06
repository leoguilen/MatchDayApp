using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface IScheduleMatchAppService
    {
        Task<IReadOnlyList<ScheduleMatchModel>> GetScheduledMatchesListAsync(PaginationQuery pagination = null, MatchFilterQuery filter = null);
        Task<ScheduleMatchModel> GetScheduledMatchByIdAsync(Guid matchId);
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsByTeamIdAsync(Guid teamId);
        Task<IEnumerable<ScheduleMatchModel>> GetScheduledMatchsBySoccerCourtIdAsync(Guid soccerCourtId);
        Task<bool> ScheduleMatchAsync(ScheduleMatchRequest request);
        Task<bool> UncheckMatchAsync(Guid scheduleMatchId);
        Task<bool> ConfirmMatchAsync(ConfirmMatchRequest request);
    }
}
