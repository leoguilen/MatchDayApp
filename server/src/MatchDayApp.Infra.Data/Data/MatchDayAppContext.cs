using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDayApp.Infra.Data.Data
{
    public class MatchDayAppContext : DbContext
    {
        public MatchDayAppContext(DbContextOptions<MatchDayAppContext> options) : base(options) 
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public MatchDayAppContext() {}

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
