using System.Collections.Generic;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string message = null)
        {
            Sucesso = true;
            Mensagem = message;
            Dados = data;
        }

        public Response(string message)
        {
            Sucesso = false;
            Mensagem = message;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<string> Errors { get; set; }
        public T Dados { get; set; }
    }
}
