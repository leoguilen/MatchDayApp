using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Message.Common.Helpers;
using MatchDayApp.Infra.Message.Interfaces;
using MatchDayApp.Infra.Message.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace MatchDayApp.Infra.Message.Services
{
    public class SmsService : IMessageService
    {
        private readonly TwilioSettings _twilioSettings;
        private readonly ILogger _logger;

        public SmsService(TwilioSettings twilioSettings, ILogger logger)
        {
            _twilioSettings = twilioSettings
                ?? throw new ArgumentNullException(nameof(twilioSettings));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendMessageAsync(MessageModel message)
        {
            try
            {
                TwilioClient.Init(_twilioSettings.TwilioAccountSID,
                    _twilioSettings.TwilioAuthToken);

                var msgResource = await MessageResource.CreateAsync(
                    from: new PhoneNumber(_twilioSettings.TwilioPhoneNumber),
                    to: new PhoneNumber(TwilioHelper.FormatPhoneNumber(message.To)),
                    addressRetention: MessageResource.AddressRetentionEnum.Retain,
                    body: message.Body);

                if (msgResource.Status == MessageResource.StatusEnum.Failed)
                    throw new Exception($"{msgResource.ErrorCode} - {msgResource.ErrorMessage}");

                _logger.LogInformation($"Mensagem sms enviada com sucesso");
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
