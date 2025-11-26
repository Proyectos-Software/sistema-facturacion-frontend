// Models/Facturas/FacturaCreateRequest.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{

    public class FacturaCreateRequest
    {
        [Required]
        [JsonPropertyName("idCli")]
        public int IdCli { get; set; }

        [Required]
        [JsonPropertyName("idEmp")]
        public int IdEmp { get; set; }      

        [Required]
        [JsonPropertyName("idUsu")]
        public int IdUsu { get; set; }      

        [Required]
        [JsonPropertyName("idEmpresa")]
        public int IdEmpresa { get; set; } 

        [MinLength(1, ErrorMessage = "Debe incluir al menos un detalle.")]
        [JsonPropertyName("detalles")]
        public List<FacturaDetalleCreateRequest> Detalles { get; set; } = new();
    }

    public class FacturaDetalleCreateRequest
    {
        [Required]
        [JsonPropertyName("idPro")]
        public int IdPro { get; set; }      

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que cero.")]
        [JsonPropertyName("cantidad")]
        public int Cantidad { get; set; }  
    }
}
