using System.ComponentModel.DataAnnotations.Schema;
using MatchDayApp.Domain.Entities.Base;
using System;

namespace MatchDayApp.Domain.Entities
{
    public class SoccerCourt : Entity
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

        [ForeignKey(nameof(OwnerUserId))]
        public virtual User OwnerUser { get; set; }
    }
}
