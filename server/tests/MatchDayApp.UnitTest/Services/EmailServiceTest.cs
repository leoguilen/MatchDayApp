using Bogus;
using FluentAssertions;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Message.Common.Helpers;
using MatchDayApp.Infra.Message.Models;
using MatchDayApp.Infra.Message.Services;
using MatchDayApp.UnitTest.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit;

namespace MatchDayApp.UnitTest.Services
{
    [Trait("Services", "SendEmail")]
    public class EmailServiceTest
    {
        private readonly EmailService _emailService;

        public EmailServiceTest()
        {
            var configServices = ServicesConfiguration.Configure();
            var logger = configServices.GetRequiredService<ILogger>();
            var smtpSettings = configServices.GetRequiredService<SmtpSettings>();

            _emailService = new EmailService(smtpSettings, logger);
        }

        [Fact]
        public async Task SendAsync_EmailService_SendEmailToUser()
        {
            var faker = new Faker("pt_BR");

            var messageModel = new MessageModel
            {
                From = "desenvolvimento.dev1@gmail.com",
                To = "desenvolvimento.dev1@gmail.com",
                Subject = faker.Lorem.Sentence(),
                Body = TemplateHelper.GetWelcomeTemplateToString(),
            };

            var result = await _emailService
                .SendMessageAsync(messageModel);

            result.Should().BeTrue();
        }
    }
}
