using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Response;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class ResponseToViewModelMappingProfile : Profile
    {
        public ResponseToViewModelMappingProfile()
        {
            CreateMap<UserModel, UserResponse>().ReverseMap();
            CreateMap<TeamModel, TeamResponse>().ReverseMap();
            CreateMap<SoccerCourtModel, SoccerCourtResponse>().ReverseMap();
        }
    }
}
