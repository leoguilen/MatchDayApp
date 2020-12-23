using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entidades;

namespace MatchDayApp.Application.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioModel>().ReverseMap();
            CreateMap<Time, TimeModel>().ReverseMap();
            CreateMap<QuadraFutebol, QuadraModel>().ReverseMap();
            CreateMap<Partida, PartidaModel>().ReverseMap();
        }
    }
}
