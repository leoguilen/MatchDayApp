using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Services.Interfaces
{
    public interface ITeamAppService
    {
        Task<IReadOnlyList<TeamModel>> GetTeamsListAsync();
        Task<TeamModel> GetTeamByIdAsync(Guid teamId);
        Task<bool> AddTeamAsync(TeamModel team);
        Task<bool> UpdateTeamAsync(Guid teamId, TeamModel teamModel);
        Task<bool> DeleteTeamAsync(Guid teamId);
    }
}
