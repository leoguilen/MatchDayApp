using System;
using System.Text.Json.Serialization;

namespace MatchDayApp.Infra.CrossCutting.Contratos.V1.Requisicao.Time
{
    public class CriarTimeRequest
    {
        public string Nome { get; set; }
        public string Imagem { get; set; }

        [JsonIgnore]
        public Guid UsuarioProprietarioId { get; set; }
    }
}
