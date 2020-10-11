using MatchDayApp.Application.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface IMessageService
    {
        Task<bool> SendAsync(MessageModel message);
    }
}
