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
        Task<IReadOnlyList<QuadraModel>> GetSoccerCourtsListAsync(PaginationQuery pagination = null);
        Task<QuadraModel> GetSoccerCourtByIdAsync(Guid soccerCourtId);
        Task<IReadOnlyList<QuadraModel>> GetSoccerCourtsByGeoLocalizationAsync(GetSoccerCourtsByGeoRequest request);
        Task<bool> AddSoccerCourtAsync(CreateSoccerCourtRequest request);
        Task<bool> UpdateSoccerCourtAsync(Guid soccerCourtId, UpdateSoccerCourtRequest request);
        Task<bool> DeleteSoccerCourtAsync(Guid soccerCourtId);
    }
}
