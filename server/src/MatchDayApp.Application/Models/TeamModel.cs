using MatchDayApp.Application.Models.Base;
using System;

namespace MatchDayApp.Application.Models
{
    public class TeamModel : BaseModel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int TotalPlayers { get; set; } = 0;
        public Guid OwnerUserId { get; set; }
    }
}
