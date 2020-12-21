using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface IQuadraAppServico
    {
        Task<IReadOnlyList<QuadraModel>> ObterQuadrasAsync(PaginacaoQuery pagination = null);
        Task<QuadraModel> ObterQuadraPorIdAsync(Guid quadraId);
        Task<IReadOnlyList<QuadraModel>> ObterQuadrasPorLocalizacaoAsync(ObterQuadraPorLocalizacaoRequest request);
        Task<QuadraModel> AdicionarQuadraAsync(CriarQuadraRequest request);
        Task<QuadraModel> AtualizarQuadraAsync(Guid quadraId, AtualizarQuadraRequest request);
        Task<bool> DeletarQuadraAsync(Guid quadraId);
    }
}
