using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.Notification.Helpers;
using MatchDayApp.Infra.Notification.Interfaces;
using MatchDayApp.Infra.Notification.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MatchDayApp.Infra.Notification.Servicos
{
    public class ServicoNotificacaoSms : IServicoNotificacao
    {
        private readonly TwilioConfiguracao _twilioConfig;
        private readonly ILogger _logger;

        public ServicoNotificacaoSms(TwilioConfiguracao twilioConfig, ILogger logger)
        {
            _twilioConfig = twilioConfig
                ?? throw new ArgumentNullException(nameof(twilioConfig));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> EnviarMensagemNotificacaoAsync(NotificacaoMensagemModel mensagem)
        {
            try
            {
                TwilioClient.Init(_twilioConfig.TwilioAccountSID,
                    _twilioConfig.TwilioAuthToken);

                var msgResource = await MessageResource.CreateAsync(
                    from: new PhoneNumber(_twilioConfig.TwilioPhoneNumber),
                    to: new PhoneNumber(TwilioHelper.FormatarNumeroNoPadraoDoTwilio(mensagem.Para)),
                    addressRetention: MessageResource.AddressRetentionEnum.Retain,
                    body: mensagem.Conteudo);

                if (msgResource.Status == MessageResource.StatusEnum.Failed)
                    throw new Exception($"{msgResource.ErrorCode} - {msgResource.ErrorMessage}");

                _logger.LogInformation($"Mensagem enviada para {msgResource.To} em {DateTime.Now}");
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
