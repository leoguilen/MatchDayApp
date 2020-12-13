using MatchDayApp.Domain.Entities;
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

        public DbSet<User> Users { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<SoccerCourt> SoccerCourts { get; set; }
        public DbSet<ScheduleMatch> ScheduleMatches { get; set; }
        public DbSet<UserConfirmEmail> UserConfirmEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new UserTeamMap());
            modelBuilder.ApplyConfiguration(new TeamMap());
            modelBuilder.ApplyConfiguration(new SoccerCourtMap());
            modelBuilder.ApplyConfiguration(new ScheduleMatchMap());
        }
    }
}
