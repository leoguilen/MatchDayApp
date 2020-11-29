using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class CreateTeamRequestExample : IExamplesProvider<CreateTeamRequest>
    {
        public CreateTeamRequest GetExamples()
        {
            return new CreateTeamRequest
            {
                Name = "Team example",
                Image = "example_team_img.jpg"
            };
        }
    }
}
