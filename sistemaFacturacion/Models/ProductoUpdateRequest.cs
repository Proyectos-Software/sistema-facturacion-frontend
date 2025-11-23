using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    public class ProductoUpdateRequest
    {
        [JsonPropertyName("idCatPro")]
        public int? IdCategoria { get; set; }

        [JsonPropertyName("idTipTrib")]
        public int IdTipoTributario { get; set; }

        [JsonPropertyName("nomPro")]
        public string? Nombre { get; set; }

        [JsonPropertyName("desPro")]
        public string? Descripcion { get; set; }
         
        [JsonPropertyName("preVen")]
        public decimal? PrecioVenta { get; set; }
    }
}
