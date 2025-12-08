using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    public sealed class TipoTributarioDto
    {
        [JsonPropertyName("idTipTrib")]
        public int Id { get; set; }

        [JsonPropertyName("nomTributario")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;

        [JsonPropertyName("tarifa")]
        public decimal Tarifa { get; set; }

        [JsonPropertyName("codigoSri")]
        public string CodigoSri { get; set; } = string.Empty;

        [JsonPropertyName("estado")]
        public bool Estado { get; set; }
    }
}
