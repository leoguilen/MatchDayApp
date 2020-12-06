using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch
{
    public class ConfirmMatchRequest
    {
        public Guid TeamId { get; set; }
        public Guid MatchId { get; set; }
    }
}
