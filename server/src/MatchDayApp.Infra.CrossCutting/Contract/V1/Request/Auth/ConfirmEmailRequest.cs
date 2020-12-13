using System;

namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Request.Auth
{
    public class ConfirmEmailRequest
    {
        public Guid ConfirmKey { get; set; }
    }
}
