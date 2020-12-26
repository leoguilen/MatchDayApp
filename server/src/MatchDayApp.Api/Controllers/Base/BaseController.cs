using AutoMapper;
using MatchDayApp.Infra.CrossCutting.Servicos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchDayApp.Api.Controllers.Base
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        protected readonly ICacheServico CacheServico;
        protected readonly IUriServico UriServico;
        protected readonly IMapper Mapper;

        public BaseController(ICacheServico cacheServico, IUriServico uriServico, IMapper mapper)
        {
            CacheServico = cacheServico ?? throw new System.ArgumentNullException(nameof(cacheServico));
            UriServico = uriServico ?? throw new System.ArgumentNullException(nameof(uriServico));
            Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));
        }
    }
}
