using MatchDayApp.Application.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Interfaces
{
    public interface INotificationService
    {
        Task<bool> SendAsync(MessageModel message);
    }
}
