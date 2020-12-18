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
        private ITimeRepositorio _team;
        private IUsuarioRepositorio _user;
        private IScheduleMatchRepository _match;
        private IUserConfirmEmailRepository _userConfirmEmail;

        public UnitOfWork(MatchDayAppContext context) => _context = context;

        public ITimeRepositorio Teams => _team ??= new TimeRepositorio(_context);
        public IUsuarioRepositorio Users => _user ??= new UsuarioRepositorio(_context);
        public IScheduleMatchRepository ScheduleMatches => _match ??= new PartidaRepositorio(_context);
        public IUserConfirmEmailRepository UserConfirmEmails => _userConfirmEmail ??= new ConfirmacaoEmailRepositorio(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
