﻿using FluentValidation;
using MatchDayApp.Domain.Contract.V1.Request.Auth;

namespace MatchDayApp.Domain.Contract.V1.Validations.Auth
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {

        }
    }
}
