using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class ScheduleMatchRepository : IScheduleMatchRepository
    {
        private readonly MatchDayAppContext _context;

        public ScheduleMatchRepository(MatchDayAppContext context)
        {
            _context = context;
        }

        public async Task<bool> AddMatchAsync(ScheduleMatch match)
        {
            var cmdResult = await _context.ScheduleMatches.AddAsync(match);
            return cmdResult.State == EntityState.Added;
        }

        public async Task<bool> UpdateMatchAsync(ScheduleMatch match)
        {
            var cmdResult = _context.ScheduleMatches.Update(match);
            return cmdResult.State == EntityState.Modified;
        }

        public async Task<IReadOnlyList<ScheduleMatch>> GetAsync(Expression<Func<ScheduleMatch, bool>> predicate)
        {
            return await _context.ScheduleMatches
                .Where(predicate)
                .Include(prop => prop.FirstTeam)
                .Include(prop => prop.SecondTeam)
                .Include(prop => prop.SoccerCourt)
                .ToListAsync();
        }

        public async Task<ScheduleMatch> GetMatchByIdAsync(Guid matchId)
        {
            return await _context.ScheduleMatches
                .Where(prop => prop.Id == matchId)
                .Include(prop => prop.FirstTeam)
                .Include(prop => prop.SecondTeam)
                .Include(prop => prop.SoccerCourt)
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<ScheduleMatch>> GetMatchesAsync()
        {
            return await _context.ScheduleMatches
                .Include(prop => prop.FirstTeam)
                .Include(prop => prop.SecondTeam)
                .Include(prop => prop.SoccerCourt)
                .ToListAsync();
        }
    }
}
