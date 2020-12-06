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
            public const string Get = Base + "/user/{id}";
            public const string Update = Base + "/user/{id}";
            public const string Delete = Base + "/user/{id}";
        }

        public static class Team
        {
            public const string GetAll = Base + "/team";
            public const string Get = Base + "/team/{id}";
            public const string Create = Base + "/team";
            public const string Update = Base + "/team/{id}";
            public const string Delete = Base + "/team/{id}";
        }

        public static class SoccerCourt
        {
            public const string GetAll = Base + "/soccercourt";
            public const string Get = Base + "/soccercourt/{id}";
            public const string Create = Base + "/soccercourt";
            public const string Update = Base + "/soccercourt/{id}";
            public const string Delete = Base + "/soccercourt/{id}";
        }

        public static class ScheduleMatch
        {
            public const string GetAll = Base + "/schedulematch";
            public const string Get = Base + "/schedulematch/{id}";
            public const string ScheduledMatch = Base + "/schedulematch";
            public const string ConfirmMatch = Base + "/schedulematch/confirm";
            public const string UncheckMatch = Base + "/schedulematch/uncheck";
        }
    }
}
