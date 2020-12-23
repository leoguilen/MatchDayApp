using MatchDayApp.Application.Interfaces;
using MatchDayApp.Domain.Repositorios;
using MatchDayApp.Infra.Data.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace MatchDayApp.Infra.CrossCutting.Ioc
{
    public static class RepositoryDependency
    {
        public static void AddSqlServerRepositoryDependency(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<ITimeRepositorio, TimeRepositorio>();
            services.AddTransient<IQuadraFutebolRepositorio, QuadraFutebolRepositorio>();
            services.AddTransient<IPartidaRepositorio, PartidaRepositorio>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
