using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Entities;

namespace MatchDayApp.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterModel, Usuario>().ReverseMap();
            CreateMap<TeamModel, Time>().ReverseMap();
            CreateMap<SoccerCourtModel, QuadraFutebol>().ReverseMap();
            CreateMap<ScheduleMatchModel, Partida>().ReverseMap()
                .ForMember(scm => scm.MatchDate, prop => prop.MapFrom(sc => sc.Date));
        }
    }
}
