using System;

namespace MatchDayApp.Domain.Configuracoes
{
    public class JwtConfiguracao
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
