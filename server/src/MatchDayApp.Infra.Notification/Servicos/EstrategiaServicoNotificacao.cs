using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.Notification.Enum;
using MatchDayApp.Infra.Notification.Interfaces;
using MatchDayApp.Infra.Notification.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.Notification.Servicos
{
    public class EstrategiaServicoNotificacao
    {
        private readonly ILogger _logger;
        private readonly SmtpConfiguracao _smtpConfig;
        private readonly TwilioConfiguracao _twilioConfig;

        private IServicoNotificacao _servicoNotificacao;

        public EstrategiaServicoNotificacao(SmtpConfiguracao smtpConfig, TwilioConfiguracao twilioConfig, ILogger logger)
        {
            _smtpConfig = smtpConfig
                ?? throw new ArgumentNullException(nameof(smtpConfig));
            _twilioConfig = twilioConfig
                ?? throw new ArgumentNullException(nameof(twilioConfig));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public IServicoNotificacao DefineEstrategia(TipoNotificacao tipoNotificacao)
        {
            _servicoNotificacao = tipoNotificacao switch
            {
                TipoNotificacao.Email => new ServicoNotificacaoEmail(_smtpConfig, _logger),
                TipoNotificacao.Sms => new ServicoNotificacaoSms(_twilioConfig, _logger),
                TipoNotificacao.Whatsapp => new ServicoNotificacaoWhatsapp(_twilioConfig, _logger),
                _ => throw new NotImplementedException()
            };

            return _servicoNotificacao;
        }

        public async Task<bool> EnviarMensagemNotificacaoAsync(NotificacaoMensagemModel mensagem)
        {
            return await _servicoNotificacao
                .EnviarMensagemNotificacaoAsync(mensagem);
        }
    }
}
