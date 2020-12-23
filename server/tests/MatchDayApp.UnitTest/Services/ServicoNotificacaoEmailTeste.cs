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
    [Trait("Servicos", "Notificacao Email")]
    public class ServicoNotificacaoEmailTeste
    {
        private readonly ServicoNotificacaoEmail _emailServico;
        private const string _emailTeste = "desenvolvimento.dev1@gmail.com";

        public ServicoNotificacaoEmailTeste()
        {
            var configServices = ServicesConfiguration.Configure();
            var logger = configServices.GetRequiredService<ILogger>();
            var smtpSettings = configServices.GetRequiredService<SmtpConfiguracao>();

            _emailServico = new ServicoNotificacaoEmail(smtpSettings, logger);
        }

        [Fact]
        public async Task EnviarMensagemNotificacaoAsync_ServicoNotificacaoEmail_EnviarEmailParaUsuario()
        {
            var faker = new Faker("pt_BR");

            var messageModel = new NotificacaoMensagemModel
            {
                De = _emailTeste,
                Para = _emailTeste,
                Assunto = faker.Lorem.Sentence(),
                Conteudo = $"Olá {faker.Person.FullName}, seja bem vindo!",
            };

            var result = await _emailServico
                .EnviarMensagemNotificacaoAsync(messageModel);

            result.Should().BeTrue();
        }
    }
}
