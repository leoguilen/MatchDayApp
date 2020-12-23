using FluentAssertions;
using MatchDayApp.Domain.Helpers;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "Calculador Distancia Coordenadas Helper")]
    public class CalculadorDistanciaCoordenadasHelperTeste
    {
        [Theory]
        [InlineData(-23.1089252, -46.554298, -23.1136097, -46.5649181, 1204D)]
        [InlineData(-23.6749867, -46.5411787, -23.6645041, -46.5042607, 3936D)]
        [InlineData(-23.6645041, -46.5042607, -23.1089252, -46.554298, 61987D)]
        public void CalcularParaMetros_CalculadorDistanciaCoordenadasHelper_CalcularDistanciaTotalERetornaValorEmMetros(
            double lat1, double lon1, double lat2, double lon2, double resultadoEsperado)
        {
            var calculoResult = CalculadorDistanciaCoordenadasHelper
                .CalcularParaMetros(lat1, lon1, lat2, lon2);

            calculoResult.Should().Be(resultadoEsperado);
        }

        [Fact]
        public void CalcularParaMetros_CalculadorDistanciaCoordenadasHelper_RetornoDeveSerZeroQuandoNenhumParametroEhPassado()
        {
            var resultadoEsperado = 0;

            var calculoResult = CalculadorDistanciaCoordenadasHelper
                .CalcularParaMetros();

            calculoResult.Should().Be(resultadoEsperado);
        }

        [Theory]
        [InlineData(-23.1089252, -46.554298, -23.1136097, -46.5649181)]
        [InlineData(-23.6749867, -46.5411787, -23.6645041, -46.5042607)]
        [InlineData(-23.6645041, -46.5042607, -23.6640384, -46.5065571)]
        public void EhProximo_CalculadorDistanciaCoordenadasHelper_RetornaVerdadeiroSeADistanciaForMenorQue10Km(
            double lat1, double lon1, double lat2, double lon2)
        {
            var result = CalculadorDistanciaCoordenadasHelper
                .EhProximo(lat1, lon1, lat2, lon2);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(40.764800, -73.980800, -23.1136097, -46.5649181)]
        [InlineData(-22.6743867, -46.5411787, -33.444244, -70.648126)]
        [InlineData(-23.6645041, -46.5042607, -23.1089252, -46.554298)]
        public void EhProximo_CalculadorDistanciaCoordenadasHelper_RetornaFalsoSeADistanciaForMaiorQue10Km(
            double lat1, double lon1, double lat2, double lon2)
        {
            var result = CalculadorDistanciaCoordenadasHelper
                .EhProximo(lat1, lon1, lat2, lon2);

            result.Should().BeFalse();
        }
    }
}
