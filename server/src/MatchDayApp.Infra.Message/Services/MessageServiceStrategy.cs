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
        private readonly TwilioSettings _twilioSettings;

        private IMessageService _messageService;

        public MessageServiceStrategy(SmtpSettings smtpSettings, TwilioSettings twilioSettings, ILogger logger)
        {
            _smtpSettings = smtpSettings
                ?? throw new System.ArgumentNullException(nameof(smtpSettings));
            _twilioSettings = twilioSettings
                ?? throw new System.ArgumentNullException(nameof(twilioSettings));
            _logger = logger
                ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public IMessageService SetStrategy(MessageType messageType)
        {
            _messageService = messageType switch
            {
                MessageType.Email => new EmailService(_smtpSettings, _logger),
                MessageType.Sms => new SmsService(_twilioSettings, _logger),
                MessageType.Whatsapp => new WhatsappService(_twilioSettings, _logger),
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
