using MatchDayApp.Application.Interfaces;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDayApp.Infra.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MatchDayAppContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MatchDayDB")));

            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            return services;
        } 
    }
}
