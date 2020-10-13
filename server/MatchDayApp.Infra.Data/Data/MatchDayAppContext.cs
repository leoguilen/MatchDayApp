using MatchDayApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatchDayApp.Infra.Data.Data
{
    public class MatchDayAppContext : DbContext
    {
        public MatchDayAppContext(DbContextOptions<MatchDayAppContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
    }
}
