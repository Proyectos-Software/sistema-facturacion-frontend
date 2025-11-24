// Models/Facturas/FacturaCreateRequest.cs (o donde lo tengas)
using System.Collections.Generic;

namespace sistemaFacturacion.Models
{
    public class FacturaCreateRequest
    {
        public int IdCli { get; set; }      // Id del cliente
        public int IdEmp { get; set; }      // Id del empleado
        public int IdUsu { get; set; }      // Id del usuario que factura
        public int IdEmpresa { get; set; }  // Id de la empresa

        public List<FacturaDetalleCreateRequest> Detalles { get; set; }
            = new();
    }

    public class FacturaDetalleCreateRequest
    {
        public int IdPro { get; set; }      // Producto
        public int Cantidad { get; set; }   // Cantidad
    }
}
