using System.Collections.Generic;

namespace MatchDayApp.Application.Models
{
    public class AuthenticationResult
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
