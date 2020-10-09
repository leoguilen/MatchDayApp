using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class TeamService : ITeamService
    {
        public Task<bool> AddTeamAsync(TeamModel team)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTeamAsync(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamModel> GetTeamByIdAsync(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<TeamModel>> GetTeamsListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTeamAsync(TeamModel team)
        {
            throw new NotImplementedException();
        }
    }
}
