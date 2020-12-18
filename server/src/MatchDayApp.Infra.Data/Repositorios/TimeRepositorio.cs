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
    public class TimeRepositorio : Repository<Time>, ITimeRepositorio
    {
        public TimeRepositorio(MatchDayAppContext context) : base(context) { }

        public override async Task<IReadOnlyList<Time>> ListAllAsync()
        {
            return await Entities
                .Include(t => t.OwnerUser)
                .ToListAsync();
        }

        public override async Task<Time> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(t => t.OwnerUser)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
