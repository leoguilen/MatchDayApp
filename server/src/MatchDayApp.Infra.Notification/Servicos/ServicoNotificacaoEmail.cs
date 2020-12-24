using MailKit.Net.Smtp;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.Notification.Interfaces;
using MatchDayApp.Infra.Notification.Models;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Notification.Servicos
{
    public class ServicoNotificacaoEmail : IServicoNotificacao
    {
        private readonly SmtpConfiguracao _smtpConfig;
        private readonly ILogger _logger;

        public ServicoNotificacaoEmail(SmtpConfiguracao smtpConfig, ILogger logger)
        {
            _smtpConfig = smtpConfig
                ?? throw new ArgumentNullException(nameof(smtpConfig));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> EnviarMensagemNotificacaoAsync(NotificacaoMensagemModel mensagem)
        {
            try
            {
                var msg = new MimeMessage();

                msg.From.Add(MailboxAddress.Parse(mensagem.De));
                msg.To.Add(MailboxAddress.Parse(mensagem.Para));
                msg.Subject = mensagem.Assunto;
                msg.Body = new TextPart(TextFormat.Html)
                {
                    Text = mensagem.Conteudo
                };

                using var client = new SmtpClient
                {
                    ServerCertificateValidationCallback = (s, c, h, e) => true
                };

                client.Connect(_smtpConfig.SmtpAddress,
                    _smtpConfig.SmtpPort, _smtpConfig.UseSsl);

                client.Authenticate(_smtpConfig.SmtpUsername,
                    _smtpConfig.SmtpPassword);

                await client.SendAsync(msg);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Mensagem enviada para {msg.To} em {DateTime.Now}");
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
