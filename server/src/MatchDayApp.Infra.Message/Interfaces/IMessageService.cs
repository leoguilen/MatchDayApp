using MatchDayApp.Infra.Message.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Message.Interfaces
{
    public interface IMessageService
    {
        Task<bool> SendMessageAsync(MessageModel message);
    }
}
