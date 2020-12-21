using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class ResponseToViewModelMappingProfile : Profile
    {
        public ResponseToViewModelMappingProfile()
        {
            CreateMap<UserModel, UserResponse>();
            CreateMap<TimeModel, TeamResponse>();
            CreateMap<QuadraModel, SoccerCourtResponse>();
            CreateMap<ScheduleMatchModel, ScheduleMatchResponse>()
                .ForMember(res => res.FirstTeamId, prop => prop.MapFrom(sm => sm.FirstTeam.Id))
                .ForMember(res => res.FirstTeamName, prop => prop.MapFrom(sm => sm.FirstTeam.Name))
                .ForMember(res => res.SecondTeamId, prop => prop.MapFrom(sm => sm.SecondTeam.Id))
                .ForMember(res => res.SecondTeamName, prop => prop.MapFrom(sm => sm.SecondTeam.Name))
                .ForMember(res => res.SoccerCourtId, prop => prop.MapFrom(sm => sm.SoccerCourt.Id))
                .ForMember(res => res.SoccerCourtName, prop => prop.MapFrom(sm => sm.SoccerCourt.Name));
        }
    }
}
