using MatchDayApp.Infra.Message.Interfaces;
using MatchDayApp.Infra.Message.Models;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Message.Services
{
    public class WhatsappService : IMessageService
    {
        public Task<bool> SendMessageAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
