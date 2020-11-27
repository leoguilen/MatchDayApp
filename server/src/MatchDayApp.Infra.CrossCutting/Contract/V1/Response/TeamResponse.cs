using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Response
{
    public class TeamResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int TotalPlayers { get; set; } = 0;
        public Guid OwnerUserId { get; set; }
    }
}
