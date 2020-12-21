using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Query;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Usuario;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface IUsuarioAppService
    {
        Task<IReadOnlyList<UsuarioModel>> ObterUsuariosAsync(PaginacaoQuery pagination = null);
        Task<UsuarioModel> ObterUsuarioPorIdAsync(Guid usuarioId);
        Task<UsuarioModel> ObterUsuarioPorEmailAsync(string usuarioEmail);
        Task<UsuarioModel> AtualizarUsuarioAsync(Guid usuarioId, AtualizarUsuarioRequest request);
        Task<bool> DeletarUsuarioAsync(Guid usuarioId);
    }
}
