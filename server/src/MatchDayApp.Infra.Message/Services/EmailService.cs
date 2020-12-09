using MailKit.Net.Smtp;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Message.Interfaces;
using MatchDayApp.Infra.Message.Models;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Message.Services
{
    public class EmailService : IMessageService
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly ILogger _logger;

        public EmailService(SmtpSettings smtpSettings, ILogger logger)
        {
            _smtpSettings = smtpSettings
                ?? throw new ArgumentNullException(nameof(smtpSettings));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendMessageAsync(MessageModel message)
        {
            try
            {
                var msg = new MimeMessage();

                msg.From.Add(MailboxAddress.Parse(message.From));
                msg.To.Add(MailboxAddress.Parse(message.To));
                msg.Subject = message.Subject;
                msg.Body = new TextPart(TextFormat.Html)
                {
                    Text = message.Body
                };

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                client.Connect(_smtpSettings.SmtpAddress,
                    _smtpSettings.SmtpPort, _smtpSettings.UseSsl);

                client.Authenticate(_smtpSettings.SmtpUsername,
                    _smtpSettings.SmtpPassword);

                await client.SendAsync(msg);

                client.MessageSent += (sender, e) =>
                {
                    _logger.LogInformation($"{e.Message}\n\n{e.Response}");
                };

                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
