using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IAutenticacaoServico
    {
        Task<AuthenticationResult> LoginAsync(LoginModel model);
        Task<AuthenticationResult> RegistrarUsuarioAsync(RegistrarUsuarioModel model);
        Task<AuthenticationResult> ResetarSenhaAsync(ResetarSenhaModel model);
        Task<AuthenticationResult> ConfirmarEmailAsync(ConfirmacaoEmailModel model);
        Task<bool> AdicionarSolicitacaoConfirmacaoEmail(Guid usuarioId);
    }
}
