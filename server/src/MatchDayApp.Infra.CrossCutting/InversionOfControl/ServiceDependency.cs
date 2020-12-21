using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
using MatchDayApp.Domain.Configuration;
using MatchDayApp.Infra.CrossCutting.Services;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpSetting = new ConfiguracaoSmtp();
            configuration.Bind(nameof(ConfiguracaoSmtp), smtpSetting);
            services.AddSingleton(smtpSetting);

            var twilioSettings = new ConfiguracaoTwilio();
            configuration.Bind(nameof(ConfiguracaoTwilio), twilioSettings);
            services.AddSingleton(twilioSettings);

            // Application Services
            services.AddAutoMapper(new[] { Assembly.Load("MatchDayApp.Application"), Assembly.Load("MatchDayApp.Infra.CrossCutting") });
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsuarioServico, UsuarioServico>();
            services.AddTransient<ITimeServico, TimeServico>();
            services.AddTransient<IQuadraFutebolServico, QuadraFutebolServico>();
            services.AddTransient<IScheduleMatchService, ScheduleMatchService>();
            services.AddMediatR(Assembly.Load("MatchDayApp.Application"));

            // App Services
            services.AddTransient<IAuthAppService, AuthAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<ITeamAppService, TeamAppService>();
            services.AddTransient<ISoccerCourtAppService, SoccerCourtAppService>();
            services.AddTransient<IScheduleMatchAppService, ScheduleMatchAppService>();
        }
    }
}
