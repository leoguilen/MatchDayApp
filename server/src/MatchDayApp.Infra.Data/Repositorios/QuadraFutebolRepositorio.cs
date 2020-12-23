using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositorios
{
    public class QuadraFutebolRepositorio : Repository<QuadraFutebol>, IQuadraFutebolRepositorio
    {
        public QuadraFutebolRepositorio(MatchDayAppContext context) : base(context) { }

        public override async Task<IReadOnlyList<QuadraFutebol>> ListAllAsync()
        {
            return await Entities
                .Include(t => t.UsuarioProprietario)
                .ToListAsync();
        }

        public override async Task<QuadraFutebol> GetByIdAsync(Guid id)
        {
            return await Entities
                .Include(t => t.UsuarioProprietario)
                .SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
