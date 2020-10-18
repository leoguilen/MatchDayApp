using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MatchDayAppContext _context;
        private ISoccerCourtRepository _soccerCourt;
        private ITeamRepository _team;
        private IUserRepository _user;

        public UnitOfWork(MatchDayAppContext context)
        {
            _context = context;
        }

        public ISoccerCourtRepository SoccerCourtRepository => _soccerCourt ??= new SoccerCourtRepository(_context);
        public ITeamRepository TeamRepository => _team ??= new TeamRepository(_context);
        public IUserRepository UserRepository => _user ??= new UserRepository(_context);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            int status = 0;

            using var dbContextTransaction =
                await _context.Database
                    .BeginTransactionAsync(cancellationToken);
            try
            {
                status = await _context.SaveChangesAsync();
                await dbContextTransaction.CommitAsync();
            }
            catch
            {
                await dbContextTransaction.RollbackAsync();
            }

            return status;
        }
    }
}
