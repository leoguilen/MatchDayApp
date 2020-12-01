using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface ISoccerCourtAppService
    {
        Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsListAsync(PaginationQuery pagination = null);
        Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId);
        Task<IReadOnlyList<SoccerCourtModel>> GetSoccerCourtsByGeoLocalizationAsync(GetSoccerCourtsByGeoRequest request);
        Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt);
        Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, SoccerCourtModel soccerCourt);
        Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId);
    }
}
