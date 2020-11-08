using MatchDayApp.Infra.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class DatabaseDependency
    {
        public static void AddSqlServerDependency(this IServiceCollection services, IConfiguration configuration, bool isTest = false)
        {
            if (!isTest)
            {
                services.AddDbContext<MatchDayAppContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("MatchDayDB"), opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                    opt.MigrationsAssembly("MatchDayApp.Infra.Data");
                }));

                return;
            }

            services.AddDbContext<MatchDayAppContext>(options =>
            {
                options.UseInMemoryDatabase($"db-test-{Guid.NewGuid()}");
                options.ConfigureWarnings(x => x
                    .Ignore(InMemoryEventId.TransactionIgnoredWarning))
                    .UseInternalServiceProvider(new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider());
            });
        }
    }
}
