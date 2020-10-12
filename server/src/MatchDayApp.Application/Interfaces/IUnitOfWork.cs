using MatchDayApp.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ISoccerCourtRepository SoccerCourtRepository { get; set; }
        public ITeamRepository TeamRepository { get; set; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
