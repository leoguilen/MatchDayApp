using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class CreateSoccerCourtRequestExample : IExamplesProvider<CreateSoccerCourtRequest>
    {
        public CreateSoccerCourtRequest GetExamples()
        {
            return new CreateSoccerCourtRequest
            {
                Name = "Soccer Court Example",
                Image = "soccer_court_example.png",
                HourPrice = 110,
                Phone = "(11) 4412-2012",
                Address = "Al. Rio branco, 402 - Centro/SP",
                Cep = "12345-600",
                Latitude = -23.90044,
                Longitude = -40.903344
            };
        }
    }
}
