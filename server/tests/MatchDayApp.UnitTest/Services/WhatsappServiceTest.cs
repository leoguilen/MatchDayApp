using Bogus;
using FluentAssertions;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "SendMessage")]
    public class WhatsappServiceTest
    {
        // Formato válido para números = +551155256325
        private const string _phoneNumberTest = "+5511981411956";
        private readonly ServicoNotificacaoWhatsapp _whatsService;
        private readonly ConfiguracaoTwilio _twilioSettings;

        public WhatsappServiceTest()
        {
            var configServices = ServicesConfiguration.Configure();
            var logger = configServices.GetRequiredService<ILogger>();
            _twilioSettings = configServices.GetRequiredService<ConfiguracaoTwilio>();

            _whatsService = new ServicoNotificacaoWhatsapp(_twilioSettings, logger);
        }

        [Fact]
        public async Task SendMessageAsync_WhatsappService_SendMessageInWhatsappToUser()
        {
            var faker = new Faker("pt_BR");

            var messageModel = new MessageModel
            {
                From = _twilioSettings.TwilioPhoneNumber,
                To = _phoneNumberTest,
                Subject = faker.Lorem.Sentence(),
                Body = faker.Lorem.Paragraph(),
            };

            var result = await _whatsService
                .SendMessageAsync(messageModel);

            result.Should().BeTrue();
        }
    }
}
