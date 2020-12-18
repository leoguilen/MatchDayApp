using MatchDayApp.Domain.Repository;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ISoccerCourtRepository SoccerCourts { get; }
        public ITimeRepositorio Teams { get; }
        public IUsuarioRepositorio Users { get; }
        public IScheduleMatchRepository ScheduleMatches { get; }
        public IConfirmacaoEmailRepositorio UserConfirmEmails { get; }
        Task<int> CommitAsync();
    }
}
