namespace MatchDayApp.Infra.CrossCutting.Contratos.V1
{
    public static class ApiRotas
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Autenticacao
        {
            public const string RegistrarUsuario = Base + "/auth/registrar";
            public const string Login = Base + "/auth/login";
            public const string ResetarSenha = Base + "/auth/resetarSenha";
            public const string ConfirmarEmail = Base + "/auth/confirmarEmail";
        }

        public static class Usuario
        {
            public const string GetAll = Base + "/usuario";
            public const string Get = Base + "/usuario/{id}";
            public const string Update = Base + "/usuario/{id}";
            public const string Delete = Base + "/usuario/{id}";
        }

        public static class Time
        {
            public const string GetAll = Base + "/time";
            public const string Get = Base + "/time/{id}";
            public const string Create = Base + "/time";
            public const string Update = Base + "/time/{id}";
            public const string Delete = Base + "/time/{id}";
        }

        public static class Quadra
        {
            public const string GetAll = Base + "/quadra";
            public const string Get = Base + "/quadra/{id}";
            public const string Create = Base + "/quadra";
            public const string Update = Base + "/quadra/{id}";
            public const string Delete = Base + "/quadra/{id}";
        }

        public static class Partida
        {
            public const string GetAll = Base + "/partida";
            public const string Get = Base + "/partida/{id}";
            public const string MarcarPartida = Base + "/partida";
            public const string ConfirmarPartida = Base + "/partida/confirmar";
            public const string DesmarcarPartida = Base + "/partida/desmarcar";
        }
    }
}
