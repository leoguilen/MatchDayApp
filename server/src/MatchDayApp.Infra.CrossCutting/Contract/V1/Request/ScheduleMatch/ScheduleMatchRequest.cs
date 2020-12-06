using System;
using System.Text.Json.Serialization;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch
{
    public class ScheduleMatchRequest
    {
        public Guid FirstTeamId { get; set; }

        [JsonIgnore]
        public bool FirstTeamConfirmed { get; set; } = true;
        public Guid SecondTeamId { get; set; }

        [JsonIgnore]
        public bool SecondTeamConfirmed { get; set; } = false;
        public Guid SoccerCourtId { get; set; }
        public int TotalHours { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
