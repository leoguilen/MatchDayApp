namespace MatchDayApp.Infra.CrossCutting.Contratos.V1
{
    public static class ApiRotas
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Autenticacao
        {
            public const string RegistrarUsuario = Base + "/auth/register";
            public const string Login = Base + "/auth/login";
            public const string ResetarSenha = Base + "/auth/reset";
            public const string ConfirmarEmail = Base + "/auth/confirmEmail";
        }

        public static class Usuario
        {
            public const string GetAll = Base + "/user";
            public const string Get = Base + "/user/{id}";
            public const string Update = Base + "/user/{id}";
            public const string Delete = Base + "/user/{id}";
        }

        public static class Time
        {
            public const string GetAll = Base + "/team";
            public const string Get = Base + "/team/{id}";
            public const string Create = Base + "/team";
            public const string Update = Base + "/team/{id}";
            public const string Delete = Base + "/team/{id}";
        }

        public static class Quadra
        {
            public const string GetAll = Base + "/soccercourt";
            public const string Get = Base + "/soccercourt/{id}";
            public const string Create = Base + "/soccercourt";
            public const string Update = Base + "/soccercourt/{id}";
            public const string Delete = Base + "/soccercourt/{id}";
        }

        public static class Partida
        {
            public const string GetAll = Base + "/schedulematch";
            public const string Get = Base + "/schedulematch/{id}";
            public const string MarcarPartida = Base + "/schedulematch";
            public const string ConfirmarPartida = Base + "/schedulematch/confirm";
            public const string DesmarcarPartida = Base + "/schedulematch/uncheck";
        }
    }
}
