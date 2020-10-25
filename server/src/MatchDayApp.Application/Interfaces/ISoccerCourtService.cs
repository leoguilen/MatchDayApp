using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface ISoccerCourtService
    {
        Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsListAsync();
        Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId);
        Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt);
        Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, SoccerCourtModel soccerCourt);
        Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId);
    }
}
