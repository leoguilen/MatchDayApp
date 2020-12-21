using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface IPartidaAppServico
    {
        Task<IReadOnlyList<PartidaModel>> ObterPartidasAsync(PaginacaoQuery pagination = null, PartidaFilterQuery filter = null);
        Task<PartidaModel> ObterPartidaPorIdAsync(Guid partidaId);
        Task<IEnumerable<PartidaModel>> ObterPartidaPorTimeIdAsync(Guid timeId);
        Task<IEnumerable<PartidaModel>> ObterPartidaPorQuadraIdAsync(Guid quadraId);
        Task<PartidaModel> MarcarPartidaAsync(MarcarPartidaRequest request);
        Task<PartidaModel> DesmarcarPartidaAsync(Guid partidaId);
        Task<bool> ConfirmarPartidaAsync(ConfirmarPartidaRequest request);
    }
}
