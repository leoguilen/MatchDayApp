using MatchDayApp.Application.Models.Base;
using MatchDayApp.Domain.Entities.Enum;
using System;

namespace MatchDayApp.Application.Models
{
    public class ScheduleMatchModel : BaseModel
    {
        public TeamModel FirstTeam { get; set; }
        public bool FirstTeamConfirmed { get; set; }
        public TeamModel SecondTeam { get; set; }
        public bool SecondTeamConfirmed { get; set; }
        public SoccerCourtModel SoccerCourt { get; set; }
        public int MatchTime { get; set; } = 1;
        public DateTime MatchDate { get; set; }
        public StatusPartida MatchStatus { get; set; }
    }
}
