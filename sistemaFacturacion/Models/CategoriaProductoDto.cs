using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    public sealed class CategoriaProductoDto
    {
        [JsonPropertyName("idCatPro")]
        public int Id { get; set; }

        [JsonPropertyName("nomCatPro")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("desCatPro")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("estCatPro")]
        public bool Estado { get; set; }
    }
}
