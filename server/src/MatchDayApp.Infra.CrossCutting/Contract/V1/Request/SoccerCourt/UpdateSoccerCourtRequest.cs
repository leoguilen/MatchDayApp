using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt
{
    public class UpdateSoccerCourtRequest
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal HourPrice { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Cep { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid OwnerUserId { get; set; }
    }
}
