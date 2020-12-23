using MatchDayApp.Domain.Entidades;
using MatchDayApp.Domain.Especificacoes.Base;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Data.Repositorios
{
    public class PartidaRepositorio : IPartidaRepositorio
    {
        private readonly MatchDayAppContext _context;

        public PartidaRepositorio(MatchDayAppContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Partida>> ObterPartidasAsync()
        {
            return await _context.Partidas
                .Include(prop => prop.PrimeiroTime)
                .Include(prop => prop.SegundoTime)
                .Include(prop => prop.QuadraFutebol)
                .ToListAsync();
        }

        public async Task<Partida> ObterPartidaPorIdAsync(Guid partidaId)
        {
            return await _context.Partidas
                .Where(prop => prop.Id == partidaId)
                .Include(prop => prop.PrimeiroTime)
                .Include(prop => prop.SegundoTime)
                .Include(prop => prop.QuadraFutebol)
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Partida>> ObterAsync(Expression<Func<Partida, bool>> predicate)
        {
            return await _context.Partidas
                .Where(predicate)
                .Include(prop => prop.PrimeiroTime)
                .Include(prop => prop.SegundoTime)
                .Include(prop => prop.QuadraFutebol)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Partida>> ObterAsync(ISpecification<Partida> spec)
        {
            return await _context.Partidas
                .Where(spec.Criteria)
                .Include(prop => prop.PrimeiroTime)
                .Include(prop => prop.SegundoTime)
                .Include(prop => prop.QuadraFutebol)
                .ToListAsync();
        }

        public async Task<bool> AdicionarPartidaAsync(Partida partida)
        {
            var cmdResult = await _context.Partidas
                .AddAsync(partida);

            return cmdResult.State == EntityState.Added;
        }

        public async Task<bool> AtualizarPartidaAsync(Partida partida)
        {
            var cmdResult = await Task.FromResult(
                _context.Partidas.Update(partida));

            return cmdResult.State == EntityState.Modified;
        }
    }
}
