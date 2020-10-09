using MatchDayApp.Application.Models.Base;
using System;

namespace MatchDayApp.Application.Models
{
    public class ScheduleMatchModel : BaseModel
    {
        public Guid FirstTeamId { get; set; }
        public Guid SecondTeamId { get; set; }
        public Guid SoccerCourtId { get; set; }
        public int MatchTime { get; set; } = 1;
        public DateTime MatchDate { get; set; }
    }
}
