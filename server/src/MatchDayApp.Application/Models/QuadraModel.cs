﻿using MatchDayApp.Application.Models.Base;
using System;

namespace MatchDayApp.Application.Models
{
    public class QuadraModel : BaseModel
    {
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
