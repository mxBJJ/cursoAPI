using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CursoAPI.Domain
{
    public class Palestrante
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public List<RedeSocial> RedesSociais { get; set; }

        [JsonIgnore]
        public List<PalestranteEvento> PalestranteEventos { get; set; }
    }
}
