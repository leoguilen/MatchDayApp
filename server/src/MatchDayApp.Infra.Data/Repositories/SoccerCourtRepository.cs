using MatchDayApp.Domain.Entities;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositories
{
    public class SoccerCourtRepository : Repository<SoccerCourt>, ISoccerCourtRepository
    {
        public SoccerCourtRepository(MatchDayAppContext context) : base(context) { }

        public override async Task<IReadOnlyList<SoccerCourt>> ListAllAsync()
        {
            return await Entities
                .Include(t => t.OwnerUser)
                .ToListAsync();
        }

        public override async Task<SoccerCourt> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(t => t.OwnerUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
