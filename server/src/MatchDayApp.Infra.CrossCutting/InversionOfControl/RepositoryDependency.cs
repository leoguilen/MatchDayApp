using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repository;
using MatchDayApp.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class RepositoryDependency
    {
        public static void AddSqlServerRepositoryDependency(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITeamRepository, TeamRepository>();
            services.AddTransient<ISoccerCourtRepository, SoccerCourtRepository>();
            services.AddTransient<IScheduleMatchRepository, ScheduleMatchRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
