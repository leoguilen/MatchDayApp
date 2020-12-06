using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class ScheduleMatchRequestExample : IExamplesProvider<ScheduleMatchRequest>
    {
        public ScheduleMatchRequest GetExamples()
        {
            return new ScheduleMatchRequest
            {
                FirstTeamId = Guid.NewGuid(),
                SecondTeamId = Guid.NewGuid(),
                SoccerCourtId = Guid.NewGuid(),
                MatchDate = DateTime.Now,
                TotalHours = 1
            };
        }
    }
}
