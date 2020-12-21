using System;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Respostas
{
    public class QuadraResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public decimal PrecoHora { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid UsuarioProprietarioId { get; set; }
    }
}
