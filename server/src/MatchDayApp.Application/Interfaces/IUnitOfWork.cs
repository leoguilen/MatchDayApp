using MatchDayApp.Domain.Repository;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ISoccerCourtRepository SoccerCourts { get; }
        public ITeamRepository Teams { get; }
        public IUserRepository Users { get; }
        public IScheduleMatchRepository ScheduleMatches { get; }
        public IUserConfirmEmailRepository UserConfirmEmails { get; }
        Task<int> CommitAsync();
    }
}
