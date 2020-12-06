using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Response
{
    public class ScheduleMatchResponse
    {
        public Guid FirstTeamId { get; set; }
        public string FirstTeamName { get; set; }
        public bool FirstTeamConfirmed { get; set; }
        public Guid SecondTeamId { get; set; }
        public string SecondTeamName { get; set; }
        public bool SecondTeamConfirmed { get; set; }
        public Guid SoccerCourtId { get; set; }
        public string SoccerCourtName { get; set; }
        public int MatchTime { get; set; } = 1;
        public DateTime MatchDate { get; set; }
        public int MatchStatus { get; set; }
    }
}
