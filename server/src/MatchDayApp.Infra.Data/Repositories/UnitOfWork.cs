using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MatchDayAppContext _context;
        private ISoccerCourtRepository _soccerCourt;
        private ITeamRepository _team;
        private IUserRepository _user;
        private IScheduleMatchRepository _match;
        private IUserConfirmEmailRepository _userConfirmEmail;

        public UnitOfWork(MatchDayAppContext context) => _context = context;

        public ITeamRepository Teams => _team ??= new TeamRepository(_context);
        public IUserRepository Users => _user ??= new UserRepository(_context);
        public ISoccerCourtRepository SoccerCourts => _soccerCourt ??= new SoccerCourtRepository(_context);
        public IScheduleMatchRepository ScheduleMatches => _match ??= new ScheduleMatchRepository(_context);
        public IUserConfirmEmailRepository UserConfirmEmails => _userConfirmEmail ??= new UserConfirmEmailRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
