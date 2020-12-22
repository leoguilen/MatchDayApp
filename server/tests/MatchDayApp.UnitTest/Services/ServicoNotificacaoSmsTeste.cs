using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.Notification.Models;
using MatchDayApp.Infra.Notification.Servicos;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Servicos
{
    [Trait("Servicos", "Notificacao Sms")]
    public class ServicoNotificacaoSmsTeste
    {
        // Formato válido para números = +551155256325
        private const string _telefoneTeste = "+5511981411956";
        private readonly ServicoNotificacaoSms _smsServico;
        private readonly TwilioConfiguracao _twilioSettings;

        public ServicoNotificacaoSmsTeste()
        {
            var configServices = ServicesConfiguration.Configure();
            var logger = configServices.GetRequiredService<ILogger>();
            _twilioSettings = configServices.GetRequiredService<TwilioConfiguracao>();

            _smsServico = new ServicoNotificacaoSms(_twilioSettings, logger);
        }

        [Fact]
        public async Task EnviarMensagemNotificacaoAsync_ServicoNotificacaoSms_EnviarSmsParaUsuario()
        {
            var faker = new Faker("pt_BR");

            var messageModel = new NotificacaoMensagemModel
            {
                De = _twilioSettings.TwilioPhoneNumber,
                Para = _telefoneTeste,
                Assunto = faker.Lorem.Sentence(),
                Conteudo = faker.Lorem.Paragraph(),
            };

            var result = await _smsServico
                .EnviarMensagemNotificacaoAsync(messageModel);

            result.Should().BeTrue();
        }
    }
}
