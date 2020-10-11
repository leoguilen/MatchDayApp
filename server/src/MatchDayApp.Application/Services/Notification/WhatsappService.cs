using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Models;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Services.Notification
{
    public class WhatsappService : IMessageService
    {
        public Task<bool> SendAsync(MessageModel message)
        {
            throw new NotImplementedException();
        }
    }
}
