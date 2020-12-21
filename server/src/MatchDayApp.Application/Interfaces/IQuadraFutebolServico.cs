using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IQuadraFutebolServico
    {
        Task<IReadOnlyList<QuadraModel>> ObterQuadrasAsync();
        Task<QuadraModel> ObterQuadraPorIdAsync(Guid quadraId);
        Task<IReadOnlyList<QuadraModel>> ObterQuadrasPorLocalizacaoAsync(double lat, double lon);
        Task<QuadraModel> AdicionarQuadraAsync(QuadraModel quadra);
        Task<QuadraModel> AtualizarQuadraAsync(Guid quadraId, QuadraModel quadra);
        Task<bool> DeletarQuadraAsync(Guid quadraId);
    }
}
