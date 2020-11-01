using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                var name = typeof(TRequest).Name;
                var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("Id").Value);

                _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
                    name, _timer.ElapsedMilliseconds, userId, request);
            }

            return response;
        }
    }
}
