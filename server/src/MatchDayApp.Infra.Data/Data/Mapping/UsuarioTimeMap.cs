using MatchDayApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class UsuarioTimeMap : IEntityTypeConfiguration<UsuarioTime>
    {
        public void Configure(EntityTypeBuilder<UsuarioTime> builder)
        {
            builder.Property(prop => prop.Id).ValueGeneratedOnAdd();
            builder.Ignore(prop => prop.Usuario)
                .HasKey(prop => new { prop.Id, prop.UsuarioId, prop.TimeId });
        }
    }
}
