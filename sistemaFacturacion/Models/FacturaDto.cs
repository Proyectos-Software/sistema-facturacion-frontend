using System;
using System.Collections.Generic;

namespace sistemaFacturacion.Models
{
    public class FacturaDto
    {
        [JsonPropertyName("idFac")]
        public int IdFac { get; set; }

        [JsonPropertyName("numFac")]
        public string NumFac { get; set; } = string.Empty;

        [JsonPropertyName("idCli")]
        public int IdCli { get; set; }

        [JsonPropertyName("cliente")]
        public ClienteFacturaDto? Cliente { get; set; }

        [JsonPropertyName("idEmp")]
        public int IdEmp { get; set; }

        [JsonPropertyName("empleado")]
        public EmpleadoFacturaDto? Empleado { get; set; }

        [JsonPropertyName("idEmpresa")]
        public int IdEmpresa { get; set; }

        [JsonPropertyName("empresa")]
        public EmpresaFacturaDto? Empresa { get; set; }

        [JsonPropertyName("idUsu")]
        public int IdUsu { get; set; }

        [JsonPropertyName("usuario")]
        public UsuarioFacturaDto? Usuario { get; set; }

        [JsonPropertyName("fecFac")]
        public DateTime FecFac { get; set; }

        [JsonPropertyName("subFac")]
        public decimal SubFac { get; set; }

        [JsonPropertyName("ivaFac")]
        public decimal IvaFac { get; set; }

        [JsonPropertyName("totFac")]
        public decimal TotFac { get; set; }

        [JsonPropertyName("estFac")]
        public string? EstFac { get; set; }

        [JsonPropertyName("claFac")]
        public string? ClaFac { get; set; }

        [JsonPropertyName("autFac")]
        public string? AutFac { get; set; }

        [JsonPropertyName("xmlFac")]
        public string? XmlFac { get; set; }

        [JsonPropertyName("detallesFactura")]
        public List<FacturaDetalleDto> DetallesFactura { get; set; } = new();
    }

    public class ClienteFacturaDto
    {
        [JsonPropertyName("idCli")]
        public int IdCli { get; set; }

        [JsonPropertyName("nomCli")]
        public string NomCli { get; set; } = string.Empty;

        [JsonPropertyName("apeCli")]
        public string? ApeCli { get; set; }

        [JsonPropertyName("dirCli")]
        public string? DirCli { get; set; }

        [JsonPropertyName("ideCli")]
        public string? IdeCli { get; set; }
    }

    public class EmpleadoFacturaDto
    {
        [JsonPropertyName("idEmp")]
        public int IdEmp { get; set; }

        [JsonPropertyName("nomEmp")]
        public string NomEmp { get; set; } = string.Empty;
    }

    public class EmpresaFacturaDto
    {
        [JsonPropertyName("idEmp")]
        public int IdEmp { get; set; }

        [JsonPropertyName("nomEmp")]
        public string NomEmp { get; set; } = string.Empty;

        [JsonPropertyName("rucEmp")]
        public string RucEmp { get; set; } = string.Empty;

        [JsonPropertyName("ambEmp")]
        public string AmbEmp { get; set; } = string.Empty;
    }

    public class UsuarioFacturaDto
    {
        [JsonPropertyName("idUsu")]
        public int IdUsu { get; set; }

        [JsonPropertyName("nomUsu")]
        public string NomUsu { get; set; } = string.Empty;

        [JsonPropertyName("apeUsu")]
        public string? ApeUsu { get; set; }
    }

    public class FacturaDetalleDto
    {
        [JsonPropertyName("idDet")]
        public int IdDet { get; set; }

        [JsonPropertyName("idPro")]
        public int IdPro { get; set; }

        [JsonPropertyName("nomProducto")]
        public string NomProducto { get; set; } = string.Empty;

        [JsonPropertyName("codProducto")]
        public string CodProducto { get; set; } = string.Empty;

        [JsonPropertyName("idLot")]
        public int IdLot { get; set; }

        [JsonPropertyName("codLote")]
        public string CodLote { get; set; } = string.Empty;

        [JsonPropertyName("canDet")]
        public decimal CanDet { get; set; }

        [JsonPropertyName("preDet")]
        public decimal PreDet { get; set; }

        [JsonPropertyName("subDet")]
        public decimal SubDet { get; set; }

        [JsonPropertyName("ivaDet")]
        public decimal IvaDet { get; set; }

        [JsonPropertyName("totDet")]
        public decimal TotDet { get; set; }

        [JsonPropertyName("tarifa")]
        public decimal Tarifa { get; set; }
    }
}
