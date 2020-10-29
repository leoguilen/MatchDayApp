using AutoMapper;
using MatchDayApp.Application.Commands.Auth;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Reflection;
using System.Text;

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
            serviceProvider.AddTransient<IAuthService, AuthService>();
            serviceProvider.AddTransient<IUserService, UserService>();
            serviceProvider.AddTransient<ITeamService, TeamService>();
            serviceProvider.AddTransient<ISoccerCourtService, SoccerCourtService>();
            serviceProvider.AddTransient<IScheduleMatchService, ScheduleMatchService>();

            var jwtOptions = new JwtOptions
            {
                Secret = "9ce891b219b6fb5b0088e3e05e05baf5",
                TokenLifetime = TimeSpan.FromMinutes(5)
            };

            serviceProvider.AddSingleton(jwtOptions);
            serviceProvider.AddSingleton(new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            });

            serviceProvider.AddMediatR(Assembly.Load("MatchDayApp.Application"));

            return serviceProvider
                .BuildServiceProvider();
        }
    }
}
