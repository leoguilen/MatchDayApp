using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface ISoccerCourtService
    {
        Task<IReadOnlyCollection<SoccerCourtModel>> GetSoccerCourtsListAsync();
        Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId);
        Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt);
        Task<bool> UpdateTeamAsync(SoccerCourtModel soccerCourt);
        Task<bool> DeleteTeamAsync(Guid soccerCourtId);
    }
}
