using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Entidades;

namespace MatchDayApp.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegistrarUsuarioModel, Usuario>().ReverseMap();
            CreateMap<TimeModel, Time>().ReverseMap();
            CreateMap<QuadraModel, QuadraFutebol>().ReverseMap();
            CreateMap<PartidaModel, Partida>().ReverseMap();
        }
    }
}
