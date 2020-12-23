using FluentAssertions;
using MatchDayApp.Infra.Notification.Helpers;
using System.Text.RegularExpressions;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "Twilio")]
    public class TwilioHelperTeste
    {
        private const string _regexPattern = @"^\+[1-9]\d{1,14}$";

        [Theory]
        [InlineData("+55 (11)5525-6325", "+551155256325")]
        [InlineData("+55 (13)91234-0000", "+5513912340000")]
        [InlineData("+55 (15)91020-3040", "+5515910203040")]
        public void Format_TwilioHelper_FormatarNumeroParaPadraoDaApiTwilio(string numero, string resultadoEsperado)
        {
            var result = TwilioHelper
                .FormatarNumeroNoPadraoDoTwilio(numero);

            result.Should().Be(resultadoEsperado);
            Regex.IsMatch(result, _regexPattern).Should().BeTrue();
        }
    }
}
