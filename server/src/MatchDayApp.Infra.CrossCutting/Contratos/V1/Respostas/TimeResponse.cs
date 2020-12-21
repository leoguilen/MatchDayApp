using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class TimeResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public int QtdIntegrantes { get; set; } = 0;
        public Guid UsuarioProprietarioId { get; set; }
    }
}
