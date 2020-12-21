using MatchDayApp.Domain.Repositorios;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public IQuadraFutebolRepositorio QuadraFutebolRepositorio { get; }
        public ITimeRepositorio TimeRepositorio { get; }
        public IUsuarioRepositorio UsuarioRepositorio { get; }
        public IPartidaRepositorio PartidaRepositorio { get; }
        public IConfirmacaoEmailRepositorio ConfirmacaoEmailRepositorio { get; }
        Task<int> CommitAsync();
    }
}
