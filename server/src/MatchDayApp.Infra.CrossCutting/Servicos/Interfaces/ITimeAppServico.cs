using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface ITimeAppServico
    {
        Task<IReadOnlyList<TimeModel>> ObterTimesAsync(PaginacaoQuery pagination = null);
        Task<TimeModel> ObterTimePorIdAsync(Guid timeId);
        Task<TimeModel> AdicionarTimeAsync(CriarTimeRequest request);
        Task<TimeModel> AtualizarTimeAsync(Guid timeId, AtualizarTimeRequest request);
        Task<bool> DeletarTimeAsync(Guid timeId);
    }
}
