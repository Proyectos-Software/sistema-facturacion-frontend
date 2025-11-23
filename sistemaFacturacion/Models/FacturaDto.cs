using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sistemaFacturacion.Models
{
    public class FacturaDto
    {
        [JsonPropertyName("idFac")] public int IdFac { get; set; }
        [JsonPropertyName("numFac")] public string? NumFac { get; set; }

        [JsonPropertyName("idCli")] public int IdCli { get; set; }
        [JsonPropertyName("cliente")] public ClienteFacturaDto? Cliente { get; set; }

        [JsonPropertyName("idEmp")] public int IdEmp { get; set; }
        [JsonPropertyName("empleado")] public EmpleadoFacturaDto? Empleado { get; set; }

        [JsonPropertyName("idUsu")] public int IdUsu { get; set; }
        [JsonPropertyName("usuario")] public UsuarioFacturaDto? Usuario { get; set; }

        [JsonPropertyName("fecFac")] public DateTime FecFac { get; set; }
        [JsonPropertyName("subFac")] public decimal SubFac { get; set; }
        [JsonPropertyName("ivaFac")] public decimal IvaFac { get; set; }
        [JsonPropertyName("totFac")] public decimal TotFac { get; set; }

        [JsonPropertyName("estFac")] public string? EstFac { get; set; }
        [JsonPropertyName("claFac")] public string? ClaFac { get; set; }
        [JsonPropertyName("autFac")] public string? AutFac { get; set; }
        [JsonPropertyName("xmlFac")] public string? XmlFac { get; set; }

        [JsonPropertyName("detallesFactura")]
        public List<DetalleFacturaDto> DetallesFactura { get; set; } = new();
    }

    public class ClienteFacturaDto
    {
        [JsonPropertyName("idCli")] public int IdCli { get; set; }
        [JsonPropertyName("nomCli")] public string NomCli { get; set; } = "";
        [JsonPropertyName("dirCli")] public string DirCli { get; set; } = "";
    }

    public class EmpleadoFacturaDto
    {
        [JsonPropertyName("idEmp")] public int IdEmp { get; set; }
        [JsonPropertyName("nomEmp")] public string NomEmp { get; set; } = "";
    }

    public class UsuarioFacturaDto
    {
        [JsonPropertyName("idUsu")] public int IdUsu { get; set; }
        [JsonPropertyName("nomUsu")] public string NomUsu { get; set; } = "";
    }

    public class DetalleFacturaDto
    {
        [JsonPropertyName("idDet")] public int IdDet { get; set; }
        [JsonPropertyName("idPro")] public int IdPro { get; set; }

        [JsonPropertyName("producto")]
        public ProductoSimpleDto Producto { get; set; } = new();

        [JsonPropertyName("idLot")] public int IdLot { get; set; }

        // Usa tu LoteDto ya existente (mismo JSON que LotesDto del backend)
        [JsonPropertyName("lote")]
        public LoteDto Lote { get; set; } = new();

        [JsonPropertyName("canDet")] public int Cantidad { get; set; }
        [JsonPropertyName("preDet")] public decimal PrecioUnitario { get; set; }
        [JsonPropertyName("subDet")] public decimal Subtotal { get; set; }
        [JsonPropertyName("ivaDet")] public decimal Iva { get; set; }
        [JsonPropertyName("totDet")] public decimal Total { get; set; }
    }

    public class ProductoSimpleDto
    {
        [JsonPropertyName("idPro")] public int IdPro { get; set; }
        [JsonPropertyName("nomPro")] public string NomPro { get; set; } = "";

        [JsonPropertyName("idCatPro")] public int? IdCatPro { get; set; }
        [JsonPropertyName("nomCatPro")] public string NomCatPro { get; set; } = "";

        [JsonPropertyName("idTipTrib")] public int IdTipTrib { get; set; }
        [JsonPropertyName("nomTipTrib")] public string NomTipTrib { get; set; } = "";
    }
}
