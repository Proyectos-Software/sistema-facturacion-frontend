using System.Text.Json.Serialization;

public class ProductoCreateRequest
{
    // Categoría (opcional)
    [JsonPropertyName("idCatPro")]
    public int? IdCategoria { get; set; }

    // Tipo tributario (obligatorio)
    [JsonPropertyName("idTipTrib")]
    public int IdTipoTributario { get; set; }

    // Nombre del producto
    [JsonPropertyName("nomPro")]
    public string Nombre { get; set; } = string.Empty;

    // Descripción
    [JsonPropertyName("desPro")]
    public string? Descripcion { get; set; }

    // ⭐ ESTE ES OBLIGATORIO EN BACKEND
    [JsonPropertyName("preVen")]
    public decimal PrecioVenta { get; set; }
}
