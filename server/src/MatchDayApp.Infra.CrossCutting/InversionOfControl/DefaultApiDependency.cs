using FluentValidation.AspNetCore;
using MatchDayApp.Application.Behaviours;
using MatchDayApp.Application.Commands.Auth.Validations;
using MatchDayApp.Application.Middlewares;
using MatchDayApp.Infra.CrossCutting.Services;
using MatchDayApp.Infra.CrossCutting.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Compression;

namespace MatchDayApp.Infra.CrossCutting.InversionOfControl
{
    public static class DefaultApiDependency
    {
        public static void AddDefaultApiDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                .AddFluentValidation(opt =>
                    opt.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            var loggerFactory = LoggerFactory.Create(options =>
            {
                options.AddConfiguration(configuration.GetSection("Logging"));
                options.AddConsole();
                options.AddDebug();
            }).CreateLogger("MatchDayAppLogger");

            services.AddSingleton(loggerFactory);

            // Add versionamento da API
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add(typeof(GzipCompressionProvider));
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = $"https://httpstatuses.com/400",
                        Detail = "Please refer to the errors property for additional details."
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.Items["CorrelationId"]);

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });
        }

        public static void UseDefaultDependency(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseResponseCompression();
        }
    }
}
