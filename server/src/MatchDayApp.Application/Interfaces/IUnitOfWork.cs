using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
