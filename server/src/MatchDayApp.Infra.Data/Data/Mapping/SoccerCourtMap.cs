using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class SoccerCourtMap : IEntityTypeConfiguration<SoccerCourt>
    {
        public void Configure(EntityTypeBuilder<SoccerCourt> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.Phone)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(prop => prop.Address)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(prop => prop.HourPrice)
                .IsRequired();
        }
    }
}
