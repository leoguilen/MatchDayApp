using FluentAssertions;
using MatchDayApp.Domain.Common.Helpers;
using Xunit;

namespace MatchDayApp.UnitTest.Helpers
{
    [Trait("Helpers", "CoordenateDistance")]
    public class CalculateCoordenateDistanceTest
    {
        [Theory]
        [InlineData(-23.1089252, -46.554298, -23.1136097, -46.5649181, 1204D)]
        [InlineData(-23.6749867, -46.5411787, -23.6645041, -46.5042607, 3936D)]
        [InlineData(-23.6645041, -46.5042607, -23.1089252, -46.554298, 61987D)]
        public void CalculeToMeters_CalculateCoordenateDistance_CalculeTotalDistanceAndReturnInMeters(
            double lat1, double lon1, double lat2, double lon2, double result)
        {
            var calculeResult = CalculateCoordenateDistance
                .CalculeToMeters(lat1, lon1, lat2, lon2);

            calculeResult.Should().Be(result);
        }

        [Fact]
        public void CalculeToMeters_CalculateCoordenateDistance_ResultIsZeroWhenNotIsSetValuesInTheParameters()
        {
            var expectedResult = 0;

            var calculeResult = CalculateCoordenateDistance
                .CalculeToMeters();

            calculeResult.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(-23.1089252, -46.554298, -23.1136097, -46.5649181)]
        [InlineData(-23.6749867, -46.5411787, -23.6645041, -46.5042607)]
        [InlineData(-23.6645041, -46.5042607, -23.6640384, -46.5065571)]
        public void IsNear_CalculateCoordenateDistance_TrueIfTheDistanceIsLessThan10Km(
            double lat1, double lon1, double lat2, double lon2)
        {
            var result = CalculateCoordenateDistance
                .IsNear(lat1, lon1, lat2, lon2);

            result.Should().BeTrue();
        }

        [Theory]
        [InlineData(40.764800, -73.980800, -23.1136097, -46.5649181)]
        [InlineData(-22.6743867, -46.5411787, -33.444244, -70.648126)]
        [InlineData(-23.6645041, -46.5042607, -23.1089252, -46.554298)]
        public void IsNear_CalculateCoordenateDistance_FalseIfTheDistanceIsGreaterThan10Km(
            double lat1, double lon1, double lat2, double lon2)
        {
            var result = CalculateCoordenateDistance
                .IsNear(lat1, lon1, lat2, lon2);

            result.Should().BeFalse();
        }
    }
}
