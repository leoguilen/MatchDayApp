using MatchDayApp.Infra.Message.Interfaces;
using MatchDayApp.Infra.Message.Models;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Message.Services
{
    public class SmsService : IMessageService
    {
        public Task<bool> SendMessageAsync(MessageModel message)
        {
            throw new System.NotImplementedException();
        }
    }
}
