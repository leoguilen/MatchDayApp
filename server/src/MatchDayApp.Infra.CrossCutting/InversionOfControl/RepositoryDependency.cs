using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class RepositoryDependency
    {
        public static void AddSqlServerRepositoryDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<ISoccerCourtRepository, SoccerCourtRepository>();
            services.AddSingleton<IScheduleMatchRepository, ScheduleMatchRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
        }
    }
}
