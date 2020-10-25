using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Domain.Entities;

namespace MatchDayApp.Application.Mapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Team, TeamModel>().ReverseMap();
        }
    }
}
