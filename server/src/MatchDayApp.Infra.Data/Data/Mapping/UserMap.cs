using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchDayApp.Infra.Data.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(prop => prop.LastName)
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

            builder.Property(prop => prop.Password)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(prop => prop.Salt)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(prop => prop.UserType)
                .HasConversion<int>();
        }
    }
}
