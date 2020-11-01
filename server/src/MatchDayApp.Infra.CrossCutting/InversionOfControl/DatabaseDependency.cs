using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class DatabaseDependency
    {
        public static void AddSqlServerDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MatchDayAppContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MatchDayDB"), opt => 
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                    opt.MigrationsAssembly("MatchDayApp.Infra.Data");
                }));
        }
    }
}
