using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IAutenticacaoServico
    {
        Task<AutenticacaoResult> LoginAsync(LoginModel model);
        Task<AutenticacaoResult> RegistrarUsuarioAsync(RegistrarUsuarioModel model);
        Task<AutenticacaoResult> ResetarSenhaAsync(ResetarSenhaModel model);
        Task<AutenticacaoResult> ConfirmarEmailAsync(ConfirmacaoEmailModel model);
        Task<Guid> AdicionarSolicitacaoConfirmacaoEmail(Guid usuarioId);
    }
}
