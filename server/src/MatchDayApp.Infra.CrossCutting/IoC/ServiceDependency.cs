using AutoMapper;
using MatchDayApp.Application.Interfaces;
using MatchDayApp.Application.Servicos;
using MatchDayApp.Domain.Configuracoes;
using MatchDayApp.Infra.CrossCutting.Servicos;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MatchDayApp.Infra.CrossCutting.Ioc
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var smtpSetting = new SmtpConfiguracao();
            configuration.Bind(nameof(SmtpConfiguracao), smtpSetting);
            services.AddSingleton(smtpSetting);

            var twilioSettings = new TwilioConfiguracao();
            configuration.Bind(nameof(TwilioConfiguracao), twilioSettings);
            services.AddSingleton(twilioSettings);
            services.AddSingleton<ICacheServico, CacheServico>();

            // Application Services
            services.AddAutoMapper(new[] { Assembly.Load("MatchDayApp.Application"), Assembly.Load("MatchDayApp.Infra.CrossCutting") });
            services.AddTransient<IAutenticacaoServico, AutenticacaoServico>();
            services.AddTransient<IUsuarioServico, UsuarioServico>();
            services.AddTransient<ITimeServico, TimeServico>();
            services.AddTransient<IQuadraFutebolServico, QuadraFutebolServico>();
            services.AddTransient<IPartidaServico, PartidaServico>();
            services.AddMediatR(Assembly.Load("MatchDayApp.Application"));

            // App Services
            services.AddTransient<IAutenticacaoAppServico, AutenticacaoAppServico>();
            services.AddTransient<IUsuarioAppServico, UsuarioAppServico>();
            services.AddTransient<ITimeAppServico, TimeAppServico>();
            services.AddTransient<IQuadraAppServico, QuadraAppServico>();
            services.AddTransient<IPartidaAppServico, PartidaAppServico>();
        }
    }
}
