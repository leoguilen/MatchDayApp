using System;

namespace MatchDayApp.Domain.Configuration
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
