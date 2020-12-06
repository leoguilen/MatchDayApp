using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class UpdateTeamRequestExample : IExamplesProvider<UpdateTeamRequest>
    {
        public UpdateTeamRequest GetExamples()
        {
            return new UpdateTeamRequest
            {
                Name = "New team name",
                Image = "new_team_image.jpg"
            };
        }
    }
}
