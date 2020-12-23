using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting.Servicos.Interfaces
{
    public interface IAutenticacaoAppServico
    {
        Task<AutenticacaoResult> LoginAsync(LoginRequest request);
        Task<AutenticacaoResult> RegistrarUsuarioAsync(RegistrarUsuarioRequest request);
        Task<AutenticacaoResult> ResetarSenhaAsync(ResetarSenhaRequest request);
        Task<AutenticacaoResult> ConfirmarEmailAsync(Guid chave);
    }
}
