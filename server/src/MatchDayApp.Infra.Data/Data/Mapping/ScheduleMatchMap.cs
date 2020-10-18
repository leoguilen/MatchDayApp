using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class ScheduleMatchMap : IEntityTypeConfiguration<ScheduleMatch>
    {
        public void Configure(EntityTypeBuilder<ScheduleMatch> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.MatchStatus)
                .HasConversion<int>();
        }
    }
}
