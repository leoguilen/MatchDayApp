using MatchDayApp.Application.Events.Usuario;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Domain.Resources;
using MatchDayApp.Infra.Notification.Enum;
using MatchDayApp.Infra.Notification.Models;
using MatchDayApp.Infra.Notification.Servicos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class UsuarioEventHandler :
        INotificationHandler<UsuarioRegistradoEvent>,
        INotificationHandler<UsuarioSenhaResetadaEvent>,
        INotificationHandler<UsuarioDeletadoEvent>
    {
        private readonly SmtpConfiguracao _smtpSettings;
        private readonly TwilioConfiguracao _twilioSettings;
        private readonly EstrategiaServicoNotificacao _messageStrategy;
        private readonly IAutenticacaoServico _autenticacaoServico;
        private readonly IHttpContextAccessor _accessor;

        public UsuarioEventHandler(IAutenticacaoServico autenticacaoServico, SmtpConfiguracao smtpSettings,
            TwilioConfiguracao twilioSettings, ILogger logger, IHttpContextAccessor accessor)
        {
            _autenticacaoServico = autenticacaoServico
                ?? throw new ArgumentNullException(nameof(autenticacaoServico));
            _smtpSettings = smtpSettings
                ?? throw new ArgumentNullException(nameof(smtpSettings));
            _twilioSettings = twilioSettings
                ?? throw new ArgumentNullException(nameof(twilioSettings));
            _accessor = accessor
                ?? throw new ArgumentNullException(nameof(accessor));

            _messageStrategy = new EstrategiaServicoNotificacao(
                smtpSettings, twilioSettings, logger);
        }

        public Task Handle(UsuarioDeletadoEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public Task Handle(UsuarioSenhaResetadaEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public async Task Handle(UsuarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            var requisicaoId = await _autenticacaoServico
                .AdicionarSolicitacaoConfirmacaoEmail(notification.UsuarioId);

            var request = _accessor.HttpContext.Request;
            var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
            var confirmarEmailUri = $"{absoluteUri}/auth/confirmarEmail?chave={requisicaoId}";

            // Enviando email
            await _messageStrategy.DefineEstrategia(TipoNotificacao.Email)
                .EnviarMensagemNotificacaoAsync(new NotificacaoMensagemModel
                {
                    De = _smtpSettings.SmtpUsername,
                    Para = notification.Email,
                    Assunto = "[MatchDayApp] | SEJA BEM VINDO",
                    Conteudo = Dicionario_EmailTemplate.TPT01
                        .Replace("{UsuarioNome}", notification.Nome)
                        .Replace("{UrlConfirmacao}", confirmarEmailUri)
                });

            // Enviando Sms
            await _messageStrategy.DefineEstrategia(TipoNotificacao.Sms)
                .EnviarMensagemNotificacaoAsync(new NotificacaoMensagemModel
                {
                    De = _twilioSettings.TwilioPhoneNumber,
                    Para = notification.Telefone,
                    Assunto = "[MatchDayApp] | SEJA BEM VINDO",
                    Conteudo = $"Olá {notification.Nome}, seja bem vindo ao MatchDayApp!!\n" +
                           "Essa mensagem é para confirmar o seu registro no nosso sistema" +
                           " e desejar boas vindas!"
                });

            // Enviando Whatsapp
            await _messageStrategy.DefineEstrategia(TipoNotificacao.Whatsapp)
                .EnviarMensagemNotificacaoAsync(new NotificacaoMensagemModel
                {
                    De = _twilioSettings.TwilioWhatsappNumber,
                    Para = notification.Telefone,
                    Assunto = "[MatchDayApp] | SEJA BEM VINDO",
                    Conteudo = $"Olá {notification.Nome}, seja bem vindo ao MatchDayApp!!\n" +
                           "Essa mensagem é para confirmar o seu registro no nosso sistema" +
                           " e desejar boas vindas!"
                });
        }
    }
}
