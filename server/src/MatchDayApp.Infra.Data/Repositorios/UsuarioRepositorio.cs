using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositorios.Base;
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
                .Include(u => u.UsuarioTime)
                .ToListAsync();
        }

        public override async Task<Usuario> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(u => u.UsuarioTime.Time)
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObterUsuarioPorEmailAsync(string email)
        {
            return await Entities
                .Include(u => u.UsuarioTime.Time)
                .SingleOrDefaultAsync(u => u.Email.Contains(email));
        }
    }
}
