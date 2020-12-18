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
    public class UsuarioRepositorio : Repository<Usuario>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MatchDayAppContext context)
            : base(context) { }

        public override async Task<IReadOnlyList<Usuario>> ListAllAsync()
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .ToListAsync();
        }

        public override async Task<Usuario> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await Entities
                .Include(u => u.UserTeam.Team)
                .SingleOrDefaultAsync(u => u.Email.Contains(email));
        }
    }
}
