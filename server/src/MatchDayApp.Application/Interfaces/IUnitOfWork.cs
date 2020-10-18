using MatchDayApp.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ISoccerCourtRepository SoccerCourtRepository { get; }
        public ITeamRepository TeamRepository { get; }
        public IUserRepository UserRepository { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
