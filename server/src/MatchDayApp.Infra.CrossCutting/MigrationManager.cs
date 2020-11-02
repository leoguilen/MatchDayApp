using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace MatchDayApp.Infra.CrossCutting
{
    public static class MigrationManager
    {
        public async static Task<IHost> MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var appContext = scope.ServiceProvider
                .GetRequiredService<MatchDayAppContext>();

            await appContext.Database.MigrateAsync();

            return host;
        }
    }
}
