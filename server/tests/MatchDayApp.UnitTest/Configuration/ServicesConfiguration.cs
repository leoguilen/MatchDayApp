using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

            var builder = new ConfigurationBuilder()
                .AddUserSecrets("c809d130-6225-4c9f-91c0-1e2a516a72ae")
                .Build();

            var smtpSetting = new SmtpSettings();
            builder.Bind(nameof(SmtpSettings), smtpSetting);
            serviceProvider.AddSingleton(smtpSetting);

            var twilioSettings = new TwilioSettings();
            builder.Bind(nameof(TwilioSettings), twilioSettings);
            serviceProvider.AddSingleton(twilioSettings);

            var jwtOptions = new JwtOptions();
            builder.Bind(nameof(JwtOptions), jwtOptions);
            jwtOptions.TokenLifetime = TimeSpan.FromHours(2);
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

            var loggerFactory = new LoggerFactory();

            serviceProvider.AddSingleton(typeof(ILogger),
                loggerFactory.CreateLogger("Testing"));

            serviceProvider.AddMediatR(Assembly.Load("MatchDayApp.Application"));

            return serviceProvider
                .BuildServiceProvider();
        }
    }
}
