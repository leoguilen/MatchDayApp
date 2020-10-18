using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    class UserSoccerCourtMap : IEntityTypeConfiguration<UserSoccerCourt>
    {
        public void Configure(EntityTypeBuilder<UserSoccerCourt> builder)
        {
            builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
            builder.HasKey(prop => new { prop.Id, prop.SoccerCourtId, prop.UserId });
        }
    }
}
