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

        public static class User
        {
            public const string GetAll = Base + "/user";
            public const string GetById = Base + "/user/{id}";
            public const string GetByEmail = Base + "/user/{email}";
            public const string Update = Base + "/user/{id}";
            public const string Delete = Base + "/user/{id}";
        }
    }
}
