using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Message.Interfaces;
using MatchDayApp.Infra.Message.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Message.Services
{
    public class MessageServiceStrategy
    {
        private readonly ILogger _logger;
        private readonly SmtpSettings _smtpSettings;

        private IMessageService _messageService;

        public MessageServiceStrategy(SmtpSettings smtpSettings, ILogger logger)
        {
            _smtpSettings = smtpSettings
                ?? throw new System.ArgumentNullException(nameof(smtpSettings));
            _logger = logger
                ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public IMessageService SetStrategy(string messageType)
        {
            _messageService = messageType switch
            {
                "email" => new EmailService(_smtpSettings, _logger),
                "sms" => new SmsService(),
                "whatsapp" => new WhatsappService(),
                _ => throw new System.NotImplementedException()
            };

            return _messageService;
        }

        public async Task<bool> SendMessageAsync(MessageModel message)
        {
            return await _messageService
                .SendMessageAsync(message);
        }
    }
}
