using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.Data.Data;
using MatchDayApp.Infra.Data.Repositorios;
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
    public class ServicesConfiguration
    {
        public static IServiceProvider Configure()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.Development.json")
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .AddEnvironmentVariables()
                .Build();

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
            serviceProvider.AddTransient<IAutenticacaoServico, AutenticacaoServico>();
            serviceProvider.AddTransient<IUsuarioServico, UsuarioServico>();
            serviceProvider.AddTransient<ITimeServico, TimeServico>();
            serviceProvider.AddTransient<IQuadraFutebolServico, QuadraFutebolServico>();
            serviceProvider.AddTransient<IPartidaServico, PartidaServico>();

            var smtpSetting = new SmtpConfiguracao();
            configuration.Bind(nameof(SmtpConfiguracao), smtpSetting);

            var twilioSettings = new TwilioConfiguracao();
            configuration.Bind(nameof(TwilioConfiguracao), twilioSettings);

            var jwtOptions = new JwtConfiguracao();
            configuration.Bind(nameof(JwtConfiguracao), jwtOptions);

            serviceProvider.AddSingleton(twilioSettings);
            serviceProvider.AddSingleton(smtpSetting);
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
