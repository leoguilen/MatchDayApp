using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Auth;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Partida;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Usuario;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class RequestToViewModelMappingProfile : Profile
    {
        public RequestToViewModelMappingProfile()
        {
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<RegistrarUsuarioRequest, RegistrarUsuarioModel>();
            CreateMap<ResetarSenhaRequest, ResetarSenhaModel>();
            CreateMap<AtualizarUsuarioRequest, UsuarioModel>();
            CreateMap<CriarTimeRequest, TimeModel>();
            CreateMap<AtualizarTimeRequest, TimeModel>();
            CreateMap<CriarQuadraRequest, QuadraModel>();
            CreateMap<AtualizarQuadraRequest, QuadraModel>();
            CreateMap<MarcarPartidaRequest, PartidaModel>();
        }
    }
}
