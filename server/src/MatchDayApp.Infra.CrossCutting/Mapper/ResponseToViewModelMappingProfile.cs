using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class ResponseToViewModelMappingProfile : Profile
    {
        public ResponseToViewModelMappingProfile()
        {
            CreateMap<UsuarioModel, UsuarioResponse>();
            CreateMap<TimeModel, TimeResponse>();
            CreateMap<QuadraModel, QuadraResponse>();
            CreateMap<PartidaModel, PartidaResponse>()
                .ForMember(res => res.PrimeiroTimeId, prop => prop.MapFrom(sm => sm.PrimeiroTimeId))
                .ForMember(res => res.SegundoTimeId, prop => prop.MapFrom(sm => sm.SegundoTimeId))
                .ForMember(res => res.QuadraFutebolId, prop => prop.MapFrom(sm => sm.QuadraFutebolId));
        }
    }
}
