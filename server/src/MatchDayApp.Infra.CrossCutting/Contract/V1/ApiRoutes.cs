namespace MatchDayApp.Infra.CrossCutting.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Authentication
        {
            public const string Register = Base + "/auth/register";
            public const string Login = Base + "/auth/login";
            public const string ResetPassword = Base + "/auth/reset";
        }
    }
}
