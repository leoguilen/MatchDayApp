using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Query;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface ITeamAppService
    {
        Task<IReadOnlyList<TeamModel>> GetTeamsListAsync(PaginationQuery pagination = null);
        Task<TeamModel> GetTeamByIdAsync(Guid teamId);
        Task<bool> AddTeamAsync(CreateTeamRequest team);
        Task<bool> UpdateTeamAsync(Guid teamId, UpdateTeamRequest team);
        Task<bool> DeleteTeamAsync(Guid teamId);
    }
}
