using MediatR.Pipeline;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MatchDayApp.Application.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestLogger(ILogger<TRequest> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst("Id").Value);

            _logger.LogInformation("MatchDay API Request: {Name} {@UserId} {@Request}",
                name, userId, request);

            return Task.CompletedTask;
        }
    }
}
