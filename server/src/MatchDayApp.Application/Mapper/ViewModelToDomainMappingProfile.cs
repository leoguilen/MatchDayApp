using AutoMapper;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Domain.Entities;

namespace MatchDayApp.Application.Mapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}
