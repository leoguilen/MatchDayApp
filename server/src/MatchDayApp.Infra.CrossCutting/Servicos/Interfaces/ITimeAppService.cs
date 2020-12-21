using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface ITimeAppService
    {
        Task<IReadOnlyList<TimeModel>> ObterTimesAsync();
        Task<TimeModel> ObterTimePorIdAsync(Guid timeId);
        Task<TimeModel> AdicionarTimeAsync(CriarTimeRequest time);
        Task<TimeModel> AtualizarTimeAsync(Guid timeId, AtualizarTimeRequest request);
        Task<bool> DeletarTimeAsync(Guid timeId);
    }
}
