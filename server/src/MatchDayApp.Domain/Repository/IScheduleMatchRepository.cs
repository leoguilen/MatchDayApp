using MatchDayApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repository
{
    public interface IScheduleMatchRepository
    {
        Task<IReadOnlyList<ScheduleMatch>> GetMatchesAsync();
        Task<ScheduleMatch> GetMatchByIdAsync(Guid matchId);
        Task<IReadOnlyList<ScheduleMatch>> GetAsync(Expression<Func<ScheduleMatch, bool>> predicate);
        Task<bool> AddMatchAsync(ScheduleMatch match);
        Task<bool> UpdateMatchAsync(ScheduleMatch match);
    }
}
