using System.ComponentModel.DataAnnotations.Schema;
using MatchDayApp.Domain.Entities.Base;
using System;

namespace MatchDayApp.Domain.Entities
{
    public class SoccerCourt : Entity
    {
        public SoccerCourt(string name, string image, decimal hourPrice, string phone, string address, string cep, double latitude, double longitude, Guid ownerUserId)
        {
            Name = name;
            Image = image;
            HourPrice = hourPrice;
            Phone = phone;
            Address = address;
            Cep = cep;
            Latitude = latitude;
            Longitude = longitude;
            OwnerUserId = ownerUserId;
        }

        public SoccerCourt() { }

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
