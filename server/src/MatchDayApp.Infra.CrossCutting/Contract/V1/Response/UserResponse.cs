namespace MatchDayApp.Infra.CrossCutting.Contract.V1.Response
{
    public class UserResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public string Avatar { get; set; }
    }
}
