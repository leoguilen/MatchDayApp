using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services.Notification
{
    public class EmailService : IMessageService
    {
        public Task<bool> SendAsync(MessageModel message)
        {
            throw new System.NotImplementedException();
        }
    }
}
