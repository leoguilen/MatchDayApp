using MatchDayApp.Domain.Entidades;
using MatchDayApp.Infra.Data.Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace MatchDayApp.Infra.Data.Data
{
    public class MatchDayAppContext : DbContext
    {
        public MatchDayAppContext(DbContextOptions<MatchDayAppContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public MatchDayAppContext() { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioTime> UsuarioTimes { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<QuadraFutebol> Quadras { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<ConfirmacaoEmail> ConfirmacaoEmails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=MatchDayDB;Trusted_Connection=yes;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioTimeMap());
            modelBuilder.ApplyConfiguration(new TimeMap());
            modelBuilder.ApplyConfiguration(new QuadraFutebolMap());
            modelBuilder.ApplyConfiguration(new PartidaMap());
        }
    }
}
