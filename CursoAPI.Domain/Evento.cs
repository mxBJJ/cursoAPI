using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CursoAPI.Domain
{
    public class Evento
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string ImageURL { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public List<Lote> Lotes { get; set; }
        public List<RedeSocial> RedeSociais { get; set; }

        [JsonIgnore]
        public List<PalestranteEvento> PalestranteEventos { get; set; }
    }
}
