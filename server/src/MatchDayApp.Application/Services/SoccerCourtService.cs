using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services
{
    public class SoccerCourtService : ISoccerCourtService
    {
        public Task<bool> AddSoccerCourtAsync(SoccerCourtModel soccerCourt)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTeamAsync(Guid soccerCourtId)
        {
            throw new NotImplementedException();
        }

        public Task<SoccerCourtModel> GetSoccerCourtByIdAsync(Guid soccerCourtId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<SoccerCourtModel>> GetSoccerCourtsListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTeamAsync(SoccerCourtModel soccerCourt)
        {
            throw new NotImplementedException();
        }
    }
}
