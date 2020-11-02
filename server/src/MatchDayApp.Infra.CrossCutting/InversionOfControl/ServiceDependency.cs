using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
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
            // Application Services
            services.AddAutoMapper(new[] { Assembly.Load("MatchDayApp.Application"), Assembly.Load("MatchDayApp.Infra.CrossCutting") });
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ISoccerCourtService, SoccerCourtService>();
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
