using AutoMapper;
using MatchDayApp.Application.Models;
using MatchDayApp.Application.Models.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.ScheduleMatch;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.SoccerCourt;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Team;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.User;

namespace MatchDayApp.Infra.CrossCutting.Mapper
{
    public class RequestToViewModelMappingProfile : Profile
    {
        public RequestToViewModelMappingProfile()
        {
            CreateMap<LoginRequest, LoginModel>();
            CreateMap<RegisterRequest, RegistrarUsuarioModel>();
            CreateMap<ResetPasswordRequest, ResetarSenhaModel>();
            CreateMap<UpdateUserRequest, UserModel>();
            CreateMap<CreateTeamRequest, TimeModel>();
            CreateMap<UpdateTeamRequest, TimeModel>();
            CreateMap<CreateSoccerCourtRequest, QuadraModel>();
            CreateMap<UpdateSoccerCourtRequest, QuadraModel>();
            CreateMap<ScheduleMatchRequest, ScheduleMatchModel>();
        }
    }
}
