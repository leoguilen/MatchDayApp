using MatchDayApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.Sobrenome)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(prop => prop.Username)
                .IsUnique();
            builder.Property(prop => prop.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(prop => prop.Email)
                .IsUnique();
            builder.Property(prop => prop.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(prop => prop.Senha)
                .IsRequired();

            builder.Property(prop => prop.Salt)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(prop => prop.TipoUsuario)
                .HasConversion<int>();
        }
    }
}
