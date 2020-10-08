using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchDayApp.Domain.Entities
{
    public class UserSoccerCourt
    {
        protected UserSoccerCourt()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; set; }
        public Guid SoccerCourtId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(SoccerCourtId))]
        public virtual SoccerCourt SoccerCourt { get; set; }
    }
}
