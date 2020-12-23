using MatchDayApp.Infra.Notification.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Notification.Interfaces
{
    public interface IServicoNotificacao
    {
        Task<bool> EnviarMensagemNotificacaoAsync(NotificacaoMensagemModel mensagem);
    }
}
