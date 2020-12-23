using MatchDayApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IUsuarioServico
    {
        Task<IReadOnlyList<UsuarioModel>> ObterUsuariosAsync();
        Task<UsuarioModel> ObterUsuarioPorIdAsync(Guid usuarioId);
        Task<UsuarioModel> ObterUsuarioPorEmailAsync(string usuarioEmail);
        Task<UsuarioModel> AtualizarUsuarioAsync(Guid usuarioId, UsuarioModel usuario);
        Task<bool> DeletarUsuarioAsync(Guid usuarioId);
    }
}
