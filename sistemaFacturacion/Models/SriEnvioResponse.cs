using System;
using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    /// <summary>
    /// Modelo de respuesta del endpoint /api/Sri/enviar/{idFactura}
    /// Ubicación: Models/SriEnvioResponse.cs
    /// Efecto en el Front: Define la estructura de datos que se recibe del backend al intentar autorizar una factura con el SRI
    /// </summary>
    public class SriEnvioResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("estado")]
        public string Estado { get; set; } = string.Empty;

        [JsonPropertyName("numeroAutorizacion")]
        public string? NumeroAutorizacion { get; set; }

        [JsonPropertyName("fechaAutorizacion")]
        public DateTime? FechaAutorizacion { get; set; }

        [JsonPropertyName("mensaje")]
        public string? Mensaje { get; set; }
    }
}
