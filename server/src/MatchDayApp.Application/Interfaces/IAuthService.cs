using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IAutenticacaoServico
    {
        Task<AuthenticationResult> LoginAsync(LoginModel model);
        Task<AuthenticationResult> RegistrarUsuarioAsync(RegisterModel model);
        Task<AuthenticationResult> ResetarSenhaAsync(ResetPasswordModel model);
        Task<AuthenticationResult> ConfirmarEmailAsync(ConfirmEmailModel model);
        Task<bool> AdicionarSolicitacaoConfirmacaoEmail(Guid usuarioId);
    }
}
