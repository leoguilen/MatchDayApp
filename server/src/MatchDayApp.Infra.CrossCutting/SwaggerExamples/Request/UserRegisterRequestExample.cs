﻿using MatchDayApp.Domain.Entities.Enum;
using MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace MatchDayApp.Infra.CrossCutting.SwaggerExamples.Request
{
    public class UserRegisterRequestExample : IExamplesProvider<RegisterRequest>
    {
        public RegisterRequest GetExamples()
        {
            return new RegisterRequest
            {
                FirstName = "Mateus",
                LastName = "Silva",
                UserName = "mateus.silva",
                Email = "mateussilva@email.com",
                Password = "Mateus@123",
                ConfirmPassword = "Mateus@123",
                PhoneNumber = "+55 (11)1234-5678",
                TipoUsuario = TipoUsuario.Player,
                Avatar = "avatar.png"
            };
        }
    }
}
