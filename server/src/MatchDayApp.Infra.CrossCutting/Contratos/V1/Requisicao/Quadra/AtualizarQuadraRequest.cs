using System;
using System.Text.Json.Serialization;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Quadra
{
    public class AtualizarQuadraRequest
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public decimal PrecoHora { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Cep { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [JsonIgnore]
        public Guid UsuarioProprietarioId { get; set; }
    }
}
