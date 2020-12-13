using MatchDayApp.Application.Events.UserEvents;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Message.Models;
using MatchDayApp.Infra.Message.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Handlers
{
    public class UserEventHandler :
        INotificationHandler<UserRegisteredEvent>,
        INotificationHandler<UserResetPasswordEvent>,
        INotificationHandler<UserDeletedEvent>
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly TwilioSettings _twilioSettings;
        private readonly MessageServiceStrategy _messageStrategy;
        private readonly IUserService _userService;

        public UserEventHandler(IUserService userService, SmtpSettings smtpSettings, TwilioSettings twilioSettings, ILogger logger)
        {
            _userService = userService 
                ?? throw new ArgumentNullException(nameof(userService));
            _smtpSettings = smtpSettings
                ?? throw new ArgumentNullException(nameof(smtpSettings));
            _twilioSettings = twilioSettings
                ?? throw new ArgumentNullException(nameof(twilioSettings));

            _messageStrategy = new MessageServiceStrategy(
                smtpSettings, twilioSettings, logger);
        }

        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public Task Handle(UserResetPasswordEvent notification, CancellationToken cancellationToken)
        {
            //Enviar notificação para usuário
            return Task.CompletedTask;
        }

        public async Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var request = await _userService
                .AddRequestConfirmEmailAsync(notification.UserId);

            if(request)
            {
                // Enviando email
                await _messageStrategy.SetStrategy(MessageType.Email)
                    .SendMessageAsync(new MessageModel
                    {
                        From = _smtpSettings.SmtpUsername,
                        To = notification.Email,
                        Subject = "[MatchDayApp] | SEJA BEM VINDO",
                        Body = $"Olá {notification.Name}, seja bem vindo!"
                    });

                // Enviando Sms
                await _messageStrategy.SetStrategy(MessageType.Sms)
                    .SendMessageAsync(new MessageModel
                    {
                        From = _twilioSettings.TwilioPhoneNumber,
                        To = notification.PhoneNumber,
                        Subject = "[MatchDayApp] | SEJA BEM VINDO",
                        Body = $"Olá {notification.Name}, seja bem vindo ao MatchDayApp!!\n" +
                               "Essa mensagem é para confirmar o seu registro no nosso sistema" +
                               " e desejar boas vindas!"
                    });

                // Enviando Whatsapp
                await _messageStrategy.SetStrategy(MessageType.Whatsapp)
                    .SendMessageAsync(new MessageModel
                    {
                        From = _twilioSettings.TwilioWhatsappNumber,
                        To = notification.PhoneNumber,
                        Subject = "[MatchDayApp] | SEJA BEM VINDO",
                        Body = $"Olá {notification.Name}, seja bem vindo ao MatchDayApp!!\n" +
                               "Essa mensagem é para confirmar o seu registro no nosso sistema" +
                               " e desejar boas vindas!"
                    });
            }
        }
    }
}
