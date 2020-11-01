using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Services;
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
            services.AddAutoMapper(Assembly.Load("MatchDayApp.Application"));
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeamService, TeamService>();
            services.AddTransient<ISoccerCourtService, SoccerCourtService>();
            services.AddTransient<IScheduleMatchService, ScheduleMatchService>();
            services.AddMediatR(Assembly.Load("MatchDayApp.Application"));
        }
    }
}
