using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MatchDayApp.UnitTest.Configuration
{
    public static class ServicesConfiguration
    {
        public static IServiceProvider Configure()
        {
            var serviceProvider = new ServiceCollection();

            serviceProvider.AddDbContext<MatchDayAppContext>(options => options
                .UseInMemoryDatabase($"TestDB-{Guid.NewGuid()}")
                .UseInternalServiceProvider(new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .EnableServiceProviderCaching(false));

            serviceProvider.AddSingleton<IUnitOfWork, UnitOfWork>();
            serviceProvider.AddAutoMapper(Assembly.Load("MatchDayApp.Application"));

            return serviceProvider
                .BuildServiceProvider();
        }
    }
}
