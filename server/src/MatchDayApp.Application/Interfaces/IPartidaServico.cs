using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IPartidaServico
    {
        Task<IReadOnlyList<PartidaModel>> ObterPartidasAsync();
        Task<PartidaModel> ObterPartidaPorIdAsync(Guid partidaId);
        Task<IEnumerable<PartidaModel>> ObterPartidaPorTimeIdAsync(Guid timeId);
        Task<IEnumerable<PartidaModel>> ObterPartidaPorQuadraIdAsync(Guid quadraId);
        Task<PartidaModel> MarcarPartidaAsync(PartidaModel partidaModel);
        Task<PartidaModel> DesmarcarPartidaAsync(Guid partidaId);
        Task<bool> ConfirmarPartidaAsync(Guid timeId, Guid partidaId);
    }
}
