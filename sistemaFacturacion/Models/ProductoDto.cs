using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    public class ProductoDto
    {
        // ID del producto
        [JsonPropertyName("idPro")]
        public int Id { get; set; }

        // Código interno del producto
        [JsonPropertyName("codPro")]
        public string Codigo { get; set; } = string.Empty;

        // Categoría
        [JsonPropertyName("idCatPro")]
        public int? IdCategoria { get; set; }

        [JsonPropertyName("nomCatPro")]
        public string NombreCategoria { get; set; } = string.Empty;

        // Tipo tributario
        [JsonPropertyName("idTipTrib")]
        public int IdTipoTributario { get; set; }

        [JsonPropertyName("nomTipTrib")]
        public string NombreTipoTributario { get; set; } = string.Empty;

        // Datos básicos
        [JsonPropertyName("nomPro")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("desPro")]
        public string Descripcion { get; set; } = string.Empty;

        // ⭐ FALTABA ESTE CAMPO OBLIGATORIO DEL BACKEND
        [JsonPropertyName("preVen")]
        public decimal PrecioVenta { get; set; }

        // Estado
        [JsonPropertyName("estPro")]
        public bool Estado { get; set; }
    }
}
