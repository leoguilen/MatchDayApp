using AutoMapper;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class RequestToViewModelMappingProfile : Profile
    {
        public RequestToViewModelMappingProfile()
        {
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<RegisterRequest, RegisterModel>();
            CreateMap<ResetPasswordRequest, ResetPasswordModel>();
        }
    }
}
