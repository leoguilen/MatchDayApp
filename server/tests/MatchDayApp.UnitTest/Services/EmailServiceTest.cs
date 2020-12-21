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
    public class EmailServiceTest
    {
        private readonly ServicoEmail _emailService;
        private const string _emailTest = "desenvolvimento.dev1@gmail.com";

        public EmailServiceTest()
        {
            var configServices = ServicesConfiguration.Configure();
            var logger = configServices.GetRequiredService<ILogger>();
            var smtpSettings = configServices.GetRequiredService<ConfiguracaoSmtp>();

            _emailService = new ServicoEmail(smtpSettings, logger);
        }

        [Fact]
        public async Task SendMessageAsync_EmailService_SendEmailToUser()
        {
            var faker = new Faker("pt_BR");

            var messageModel = new MessageModel
            {
                From = _emailTest,
                To = _emailTest,
                Subject = faker.Lorem.Sentence(),
                Body = $"Olá {faker.Person.FullName}, seja bem vindo!",
            };

            var result = await _emailService
                .SendMessageAsync(messageModel);

            result.Should().BeTrue();
        }
    }
}
