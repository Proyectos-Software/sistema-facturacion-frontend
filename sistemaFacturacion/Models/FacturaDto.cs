using System;
using System.Collections.Generic;

namespace sistemaFacturacion.Models
{
    public class FacturaDto
    {
        public int IdFac { get; set; }
        public string NumFac { get; set; } = string.Empty;

        public int IdCli { get; set; }
        public ClienteDto? Cliente { get; set; }

        public int IdEmp { get; set; }
        public EmpleadoDto? Empleado { get; set; }

        public int IdEmpresa { get; set; }
        // Si tienes un dto de empresa, lo pones aquí. Si no:
        public string? NomEmpresa { get; set; }
        public string? RucEmpresa { get; set; }
        public string? Ambiente { get; set; }

        public int IdUsu { get; set; }
        public UsuarioDto? Usuario { get; set; }

        public DateTime FecFac { get; set; }

        public decimal SubFac { get; set; }
        public decimal IvaFac { get; set; }
        public decimal TotFac { get; set; }

        public string EstFac { get; set; } = string.Empty;
        public string? ClaFac { get; set; }
        public string? AutFac { get; set; }
        public string? XmlFac { get; set; }

        public List<DetalleFacturaDto> DetallesFactura { get; set; } = new();
    }

    public class DetalleFacturaDto
    {
        public int IdDet { get; set; }
        public int IdPro { get; set; }
        public string NomProducto { get; set; } = string.Empty;
        public string CodProducto { get; set; } = string.Empty;

        public int IdLot { get; set; }
        public string CodLote { get; set; } = string.Empty;

        public int CanDet { get; set; }
        public decimal PreDet { get; set; }
        public decimal SubDet { get; set; }
        public decimal IvaDet { get; set; }
        public decimal TotDet { get; set; }

        public decimal Tarifa { get; set; } // IVA %
    }
}
