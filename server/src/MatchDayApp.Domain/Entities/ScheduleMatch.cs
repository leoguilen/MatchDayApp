using MatchDayApp.Domain.Entities.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entities
{
    public class ScheduleMatch
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid FirstTeamId { get; set; }
        public bool FirstTeamConfirmed { get; set; }
        public Guid SecondTeamId { get; set; }
        public bool SecondTeamConfirmed { get; set; }
        public Guid SoccerCourtId { get; set; }
        public int TotalHours { get; set; }
        public DateTime Date { get; set; }
        public MatchStatus MatchStatus { get; set; }

        [ForeignKey(nameof(FirstTeamId))]
        public Team FirstTeam { get; set; }

        [ForeignKey(nameof(SecondTeamId))]
        public Team SecondTeam { get; set; }

        [ForeignKey(nameof(SoccerCourtId))]
        public SoccerCourt SoccerCourt { get; set; }
    }
}
