using MatchDayApp.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class QuadraFutebolMap : IEntityTypeConfiguration<QuadraFutebol>
    {
        public void Configure(EntityTypeBuilder<QuadraFutebol> builder)
        {
            builder.ToTable("QuadraFutebol");

            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.Telefone)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(prop => prop.Endereco)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(prop => prop.PrecoHora)
                .IsRequired()
                .HasColumnType("float");
        }
    }
}
