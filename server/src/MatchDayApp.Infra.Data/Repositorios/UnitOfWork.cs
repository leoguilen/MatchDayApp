using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MatchDayAppContext _context;
        private IQuadraFutebolRepositorio _quadraFutebol;
        private ITimeRepositorio _time;
        private IUsuarioRepositorio _usuario;
        private IPartidaRepositorio _partida;
        private IConfirmacaoEmailRepositorio _confirmacaoEmail;

        public UnitOfWork(MatchDayAppContext context) => _context = context;

        public IQuadraFutebolRepositorio QuadraFutebolRepositorio => _quadraFutebol ??= new QuadraFutebolRepositorio(_context);
        public ITimeRepositorio TimeRepositorio => _time ??= new TimeRepositorio(_context);
        public IUsuarioRepositorio UsuarioRepositorio => _usuario ??= new UsuarioRepositorio(_context);
        public IPartidaRepositorio PartidaRepositorio => _partida ??= new PartidaRepositorio(_context);
        public IConfirmacaoEmailRepositorio ConfirmacaoEmailRepositorio => _confirmacaoEmail ??= new ConfirmacaoEmailRepositorio(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
