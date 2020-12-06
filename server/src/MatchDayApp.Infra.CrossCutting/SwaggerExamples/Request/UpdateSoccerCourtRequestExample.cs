using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class UpdateSoccerCourtRequestExample : IExamplesProvider<UpdateSoccerCourtRequest>
    {
        public UpdateSoccerCourtRequest GetExamples()
        {
            return new UpdateSoccerCourtRequest
            {
                Name = "Soccer Court Updated",
                Image = "soccer_court_updated.png",
                HourPrice = 250,
                Phone = "(11) 1020-4444",
                Address = "Rua dos tigos, 911 - Maracanã/RO",
                Cep = "09909-776",
                Latitude = -28.90044,
                Longitude = -55.903344
            };
        }
    }
}
