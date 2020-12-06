using System;
using System.Text.Json.Serialization;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team
{
    public class CreateTeamRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }

        [JsonIgnore]
        public Guid OwnerUserId { get; set; }
    }
}
