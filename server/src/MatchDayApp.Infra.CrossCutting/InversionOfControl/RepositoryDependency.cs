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
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<ITimeRepositorio, TimeRepositorio>();
            services.AddTransient<ISoccerCourtRepository, QuadraFutebolRepositorio>();
            services.AddTransient<IScheduleMatchRepository, PartidaRepositorio>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
