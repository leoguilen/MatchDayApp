using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface ITimeServico
    {
        Task<IReadOnlyList<TimeModel>> ObterTimesAsync();
        Task<TimeModel> ObterTimePorIdAsync(Guid timeId);
        Task<TimeModel> AdicionarTimeAsync(TimeModel time);
        Task<TimeModel> AtualizarTimeAsync(Guid timeId, TimeModel timeModel);
        Task<bool> DeletarTimeAsync(Guid timeId);
    }
}
