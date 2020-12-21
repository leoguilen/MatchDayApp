using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entities;

namespace MatchDayApp.Application.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UserModel>().ReverseMap();
            CreateMap<Time, TimeModel>().ReverseMap();
            CreateMap<QuadraFutebol, QuadraModel>().ReverseMap();
            CreateMap<Partida, ScheduleMatchModel>().ReverseMap()
                .ForMember(sc => sc.Date, prop => prop.MapFrom(scm => scm.MatchDate));
        }
    }
}
