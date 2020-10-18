using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class UserTeamMap : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
            builder.Ignore(prop => prop.User).HasKey(prop => new { prop.Id, prop.UserId, prop.TeamId });
        }
    }
}
