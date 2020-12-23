using MatchDayApp.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MatchDayApp.Domain.Repositorios
{
    public interface IPartidaRepositorio
    {
        Task<IReadOnlyList<Partida>> ObterPartidasAsync();
        Task<Partida> ObterPartidaPorIdAsync(Guid partidaId);
        Task<IReadOnlyList<Partida>> ObterAsync(Expression<Func<Partida, bool>> predicate);
        Task<bool> AdicionarPartidaAsync(Partida partida);
        Task<bool> AtualizarPartidaAsync(Partida partida);
    }
}
